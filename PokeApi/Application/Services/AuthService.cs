using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PokeApi.Application.DTOs;
using PokeApi.Application.Interfaces;

namespace PokeApi.Application.Services;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly string _jwtSecret;
    private readonly string _jwtIssuer;
    private readonly int _jwtExpiryMinutes;

    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _jwtSecret = configuration["Jwt:Secret"] ?? "PokeApiSuperSecretKey12345678901234567890";
        _jwtIssuer = configuration["Jwt:Issuer"] ?? "PokeApi";
        _jwtExpiryMinutes = configuration.GetValue<int>("Jwt:ExpiryMinutes", 60);
    }

    public AuthResponseDto Login(LoginDto loginDto)
    {
        var user = _userRepository.GetByUsername(loginDto.Username);
        if (user == null)
            throw new UnauthorizedAccessException("Usuario o contraseña incorrectos");

        if (!Infrastructure.Repositories.UserRepository.VerifyPassword(loginDto.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Usuario o contraseña incorrectos");

        var token = GenerateToken(user);
        
        return new AuthResponseDto
        {
            Token = token,
            Username = user.Username,
            ExpiresIn = _jwtExpiryMinutes * 60
        };
    }

    public AuthResponseDto Register(RegisterDto registerDto)
    {
        if (_userRepository.Exists(registerDto.Username))
            throw new InvalidOperationException("El usuario ya existe");

        var passwordHash = Infrastructure.Repositories.UserRepository.HashPassword(registerDto.Password);

        var user = new Domain.Entities.User
        {
            Username = registerDto.Username,
            PasswordHash = passwordHash,
            Email = registerDto.Email,
            Role = "User"
        };

        _userRepository.Add(user);

        var token = GenerateToken(user);

        return new AuthResponseDto
        {
            Token = token,
            Username = user.Username,
            ExpiresIn = _jwtExpiryMinutes * 60
        };
    }

    private string GenerateToken(Domain.Entities.User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: _jwtIssuer,
            audience: _jwtIssuer,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtExpiryMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}