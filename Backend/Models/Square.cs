namespace Chess.Backend.Models;

public class Square
{
    public int Row { get; }
    public int Column { get; }
    public Piece? Piece { get; set; }

    public Square(int row, int column)
    {
        Row = row;
        Column = column;
        Piece = null;
    }
}