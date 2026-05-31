using Chess.Backend.Core.Enums;

namespace Chess.Backend.Models;

public abstract class Piece : IPiece
{
    public PieceColor Color { get; }
    public PieceType Type { get; }

    protected Piece(PieceColor color, PieceType type)
    {
        Color = color;
        Type = type;
    }

    public abstract IEnumerable<(int Row, int Col)> GetValidMoves(Square[,] square, int row, int column);

    // for King and Knight
    protected IEnumerable<(int Row, int Col)> Jump(Square[,] square, int row, int column, (int, int)[] directions)
    {
        foreach (var (dr, dc) in directions)
        {
            int nextRow = row + dr;
            int nextColumn = column + dc;

            if (nextRow is >= 0 and < 8 && nextColumn is >= 0 and < 8)
            {
                if (square[nextRow, nextColumn].Piece == null || square[nextRow, nextColumn].Piece!.Color != Color)
                    yield return (nextRow, nextColumn);
            }
        }
    }
    // for Rook, Bishop and Queen
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
