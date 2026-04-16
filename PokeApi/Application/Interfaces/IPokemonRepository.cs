using PokeApi.Domain.Entities;

namespace PokeApi.Application.Interfaces;

public interface IPokemonRepository
{
    List<Pokemon> GetAll();
    Pokemon? GetById(int id);
    void Add(Pokemon pokemon);
    void Update(Pokemon pokemon);
    void Delete(int id);
    void AddRange(List<Pokemon> pokemons);
}