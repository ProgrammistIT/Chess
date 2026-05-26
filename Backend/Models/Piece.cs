using Chess.Backend.Enums;

namespace Chess.Backend.Models;

public abstract class Piece
{
    public PieceColor Color { get; }
    public PieceType Type { get; }

    protected Piece(PieceColor color, PieceType type)
    {
        Color = color;
        Type = type;
    }

    public abstract IEnumerable<(int Row, int Col)> GetValidMoves(Square[,] square, int row, int column);

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
}