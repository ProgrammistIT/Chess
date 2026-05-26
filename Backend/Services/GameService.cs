using Chess.Backend.Models;
using Chess.Backend.Enums;

namespace Chess.Backend.Services;

public class GameService
{
    public Board Board { get; }
    public PieceColor CurrentTurn { get; private  set; } = PieceColor.White;

    public delegate void OnMoveComplete(PieceColor nextTurn);

    public event OnMoveComplete? MoveCompleted;

    public GameService()
    {
        Board = new Board();
    }

    public bool TryMove(int fromRow, int fromColumn, int toRow, int toColumn)
    {
        var from = Board.Squares[fromRow, fromColumn];
        var to = Board.Squares[toRow, toColumn];
        
        if (from.Piece == null) return false; // проверка на наличие фигуры на клетке
        if (from.Piece.Color != CurrentTurn) return false; // не твой черед ходить
        if (to.Piece?.Color == CurrentTurn) return false; // бить своих нельзя
        
        to.Piece = from.Piece;
        from.Piece = null;
        
        CurrentTurn = (CurrentTurn == PieceColor.White) ? PieceColor.Black : PieceColor.White;
        
        MoveCompleted?.Invoke(CurrentTurn);
        return true;
    }
}