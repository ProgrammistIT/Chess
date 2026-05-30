using Chess.Backend.Enums;
namespace Chess.Backend.Models;
public class Square
{
    public int Row { get; }
    public int Column { get; }
    public SquareColor ColorOfSquare { get; }
    public Piece? Piece { get; set; }

    public Square(int row, int column)
    {
        Row = row;
        Column = column;
        Piece = null;
        ColorOfSquare = (row + column) % 2 == 0 ? SquareColor.Light : SquareColor.Dark;
    }
}