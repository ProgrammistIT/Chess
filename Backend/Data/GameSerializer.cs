using Chess.Backend.Models;
using Chess.Backend.Models.Pieces;
using Chess.Backend.Enums;

namespace Chess.Backend.Services;

public abstract class GameSerializer
{
    protected readonly string FilePath;
    
    protected GameSerializer(string filePath)
    {
        FilePath = filePath;
    }

    public abstract void Serialize(Board board);
    public abstract GameState? Deserialize();

    public void Restore(Board board, GameState state)
    {
        foreach (var square in board.Squares)
            square.Piece = null;

        foreach (var pieceState in state.Pieces)
            board.Squares[pieceState.Row, pieceState.Col].Piece = CreatePiece(pieceState);

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