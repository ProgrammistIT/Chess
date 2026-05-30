using Chess.Backend.Enums;

namespace Chess.Backend.Models;

public interface IPiece
{
    PieceColor Color { get; }
    PieceType Type { get; }
    IEnumerable<(int Row, int Col)> GetValidMoves(Square[,] squares, int row, int col);
}