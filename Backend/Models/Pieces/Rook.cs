using Chess.Backend.Enums;

namespace Chess.Backend.Models.Pieces;

public class Rook : Piece
{
    public Rook(PieceColor color) : base(color, PieceType.Rook) {}
    protected Rook(PieceColor color, PieceType type) : base(color, type) {}
    public override IEnumerable<(int Row, int Col)> GetValidMoves(Square[,] square, int row, int column)
    {
        foreach (var (dr, dc) in new[] {(-1, 0), (1, 0), (0, -1), (0, 1) })
        {
            foreach (var move in Slide(square, row, column, dr, dc))
                yield return move;
        }
    }

    protected IEnumerable<(int, int)> Slide(Square[,] square, int row, int column, int dr, int dc)
    {
        int r = row + dr;
        int c = column + dc;

        while (r is >= 0 and < 8 && c is >= 0 and < 8)
        {
            if (square[r, c].Piece == null)
                yield return (r, c);
            else
            {
                if (square[r, c].Piece!.Color != Color)
                    yield return (r, c);
                break;
            }
            r += dr;
            c += dc;
        }
    }
}