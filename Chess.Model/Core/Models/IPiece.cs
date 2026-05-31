using Chess.Model.Core.Enums;

namespace Chess.Model.Models;

public interface IPiece
{
    PieceColor Color { get; }
    PieceType Type { get; }
    IEnumerable<(int Row, int Col)> GetValidMoves(Square[,] squares, int row, int col);
}