using System.Text.Json;
using Chess.Backend.Core.Enums;
using Chess.Backend.Core.Models.Pieces;
using Chess.Backend.Models;

namespace Chess.Backend.Data;

public abstract class GameSerializer : ISerialize
{
    protected readonly string FilePath;
    
    protected GameSerializer(string filePath)
    {
        FilePath = filePath;
    }

    public abstract void Serialize(Board board);
    public abstract GameState? Deserialize();

    protected T DeserializeFromJson<T>(string json) where T : class
    {
        return JsonSerializer.Deserialize<T>(json) ?? throw new ArgumentException("Invalid JSON");
    }
    public void Restore(Board board, GameState state)
    {
        foreach (var square in board.Squares)
            square.Piece = null;

        foreach (var pieceState in state.Pieces)
        {
            IPiece piece = CreatePiece(pieceState); // приведение к интерфейсу
            board.Squares[pieceState.Row, pieceState.Col].Piece = piece as Piece;
        }

        if (!state.IsWhiteTurn)
            board.ChangeTurn();
    }

    protected Piece CreatePiece(PieceState state)
    {
        return state.Type switch
        {
            PieceType.Pawn   => new Pawn(state.Color),
            PieceType.Rook   => new Rook(state.Color),
            PieceType.Knight => new Knight(state.Color),
            PieceType.Bishop => new Bishop(state.Color),
            PieceType.Queen  => new Queen(state.Color),
            PieceType.King   => new King(state.Color),
            _ => throw new ArgumentException($"Unknown piece type: {state.Type}")
        };
    }
}