using Chess.Backend.Enums;

namespace Chess.Backend.Models.Pieces;

public class Knight : Piece
{
    public Knight(PieceColor color) : base(color, PieceType.Knight) {}

    public override IEnumerable<(int Row, int Col)> GetValidMoves(Square[,] square, int row, int column)
    {
        (int, int)[] directions = [(1, 2), (1, -2), (-1, 2), (-1, -2), (2, 1), (-2, 1), (2, -1), (-2, -1)];
        return Jump(square, row, column, directions);
    }
}