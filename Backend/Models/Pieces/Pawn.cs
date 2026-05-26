using Chess.Backend.Enums;
namespace Chess.Backend.Models.Pieces;

public class Pawn : Piece
{
    public Pawn(PieceColor color) : base(color, PieceType.Pawn) {}

    public override IEnumerable<(int Row, int Col)> GetValidMoves(Square[,] square, int row, int column)
    {
        int direction = Color == PieceColor.White ? -1 : 1;
        int startRow = Color == PieceColor.White ? 6 : 1;
        
        // обработка хода
        int nextRow = row + direction;
        if (nextRow >= 0 && nextRow < 8 && square[nextRow, column].Piece == null)
        {
            yield return (nextRow, column);
            
            int doubleRow = row + 2 * direction;
            if (row == startRow && square[doubleRow, column].Piece == null)
                yield return (doubleRow, column);
        }
        
        // обработка съедания
        foreach (int dc in new[] {-1, 1})
        {
            int diagonalColumn = column + dc;
            if (nextRow >= 0 && nextRow < 8 && diagonalColumn >= 0 && diagonalColumn < 8)
            {
                var target = square[nextRow, diagonalColumn].Piece;
                if (target != null && target.Color != this.Color)
                    yield return (nextRow, diagonalColumn);
            }
        }
    }
}