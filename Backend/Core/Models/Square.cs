using Chess.Backend.Core.Enums;

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
    
    // перегрузка оператора
    public static bool operator ==(Square? a, Square? b)
    {
        if (a is null && b is null) return true;
        if (a is null || b is null) return false;
        return a.Row == b.Row && a.Column == b.Column;
    }

    public static bool operator !=(Square? a, Square? b)
    {
        return !(a == b);
    }

    public override bool Equals(object? obj)
    {
        if (obj is Square other)
            return this == other;
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Row, Column);
    }
}