using PokeApi.Domain.Entities;

namespace PokeApi.Application.Interfaces;

public interface IMoveRepository
{
    List<Move> GetAll();
    Move? GetById(int id);
    void Add(Move move);
    void AddRange(List<Move> moves);
}