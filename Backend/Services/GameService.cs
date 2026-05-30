using Chess.Backend.Models;
using Chess.Backend.Enums;
using Chess.Backend.Models.Pieces;

namespace Chess.Backend.Services;

public class GameService
{
    public Board Board { get; }
    public Square this[int row, int col] => Board.Squares[row, col];
    public delegate void OnMoveComplete(PieceColor nextTurn);

    public event OnMoveComplete? MoveCompleted;

    public GameService()
    {
        Board = new Board();
    }

    public bool TryMove(int fromRow, int fromColumn, int toRow, int toColumn)
    {
        var from =  this[fromRow, fromColumn];
        
        if (from.Piece is null) return false;
        if (from.Piece.Color != (Board.IsWhiteTurn ? PieceColor.White : PieceColor.Black)) return false;

        var validMoves = from.Piece.GetValidMoves(Board.Squares, fromRow, fromColumn);
        if (!validMoves.Contains((toRow, toColumn))) return false;
        
        var captured = Board.Squares[toRow, toColumn].Piece;
        Board.Squares[toRow, toColumn].Piece = from.Piece;
        from.Piece = null;

        if (IsInCheck(Board.IsWhiteTurn ? PieceColor.White : PieceColor.Black))
        {
            from.Piece = Board.Squares[toRow, toColumn].Piece;
            Board.Squares[toRow, toColumn].Piece = captured;
            return false;
        }
        
        Board.ChangeTurn();
        MoveCompleted?.Invoke(Board.IsWhiteTurn ? PieceColor.White : PieceColor.Black);
        return true;
    }

    public bool IsInCheck(PieceColor color)
    {
        int rowKing = 0, colKing = 0;
        foreach (var piece in Board.Squares)
        {
            if (piece.Piece is King k && k.Color == color)
            {
                rowKing = piece.Row;
                colKing = piece.Column;
                break;
            }
        }

        foreach (var piece in Board.Squares)
        {
            if (piece.Piece != null && piece.Piece.Color != color)
            {
                foreach (var (r, c) in piece.Piece.GetValidMoves(Board.Squares, piece.Row, piece.Column))
                {
                    if (r == rowKing && c == colKing)
                        return true; // король под ударом
                }
            }
        }
        return false;
    }
}