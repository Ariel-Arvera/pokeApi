using PokeApi.Application.DTOs;
using PokeApi.Application.Interfaces;
using PokeApi.Domain.Entities;

namespace PokeApi.Application.Services;

public class MoveService
{
    private readonly IMoveRepository _moveRepository;

    public MoveService(IMoveRepository moveRepository)
    {
        _moveRepository = moveRepository;
    }

    public List<MoveDto> GetAll()
    {
        return _moveRepository.GetAll().Select(MapToDto).ToList();
    }

    public MoveDto? GetById(int id)
    {
        var move = _moveRepository.GetById(id);
        return move != null ? MapToDto(move) : null;
    }

    public MoveDto Create(MoveDto moveDto)
    {
        var move = new Move
        {
            Id = GetNextId(),
            Name = moveDto.Name,
            Power = moveDto.Power,
            Type = moveDto.Type
        };

        _moveRepository.Add(move);
        return MapToDto(move);
    }

    private int GetNextId()
    {
        var moves = _moveRepository.GetAll();
        return moves.Any() ? moves.Max(m => m.Id) + 1 : 1;
    }

    private static MoveDto MapToDto(Move move)
    {
        return new MoveDto
        {
            Id = move.Id,
            Name = move.Name,
            Power = move.Power,
            Type = move.Type
        };
    }
}