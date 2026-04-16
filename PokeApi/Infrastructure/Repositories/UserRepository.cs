using PokeApi.Application.Interfaces;
using PokeApi.Domain.Entities;
using System.Security.Cryptography;
using System.Text;

namespace PokeApi.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly List<User> _users = new();

    public UserRepository()
    {
        SeedUsers();
    }

    public User? GetByUsername(string username)
    {
        return _users.FirstOrDefault(u => u.Username == username);
    }

    public User? GetById(int id)
    {
        return _users.FirstOrDefault(u => u.Id == id);
    }

    public void Add(User user)
    {
        user.Id = _users.Any() ? _users.Max(u => u.Id) + 1 : 1;
        user.CreatedAt = DateTime.UtcNow;
        _users.Add(user);
    }

    public bool Exists(string username)
    {
        return _users.Any(u => u.Username == username);
    }

    public static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    public static bool VerifyPassword(string password, string hash)
    {
        return HashPassword(password) == hash;
    }

    private void SeedUsers()
    {
        _users.Add(new User
        {
            Id = 1,
            Username = "admin",
            PasswordHash = HashPassword("admin123"),
            Email = "admin@pokeapi.com",
            Role = "Admin"
        });

        _users.Add(new User
        {
            Id = 2,
            Username = "user",
            PasswordHash = HashPassword("user123"),
            Email = "user@pokeapi.com",
            Role = "User"
        });
    }
}