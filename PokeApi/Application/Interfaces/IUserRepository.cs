using PokeApi.Domain.Entities;

namespace PokeApi.Application.Interfaces;

public interface IUserRepository
{
    User? GetByUsername(string username);
    User? GetById(int id);
    void Add(User user);
    bool Exists(string username);
}