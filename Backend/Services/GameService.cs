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
    
    public delegate void OnGameOver(PieceColor winner);
    public event OnGameOver? GameOver;

    public GameService()
    {
        Board = new Board();
    }

    public bool TryMove(int fromRow, int fromColumn, int toRow, int toColumn)
    {
        var from =  this[fromRow, fromColumn];
        
        if (from.Piece is null) return false;
        if (from.Piece.Color != (Board.IsWhiteTurn ? PieceColor.White : PieceColor.Black)) return false;

        if (!GetLegalMoves(from.Row, from.Column).Contains((toRow, toColumn))) return false;
        
        Board.Squares[toRow, toColumn].Piece = from.Piece;
        from.Piece = null;
        
        Board.ChangeTurn();
        var nextColor = Board.IsWhiteTurn ? PieceColor.White : PieceColor.Black;

        if (IsCheckmate(nextColor))
            GameOver?.Invoke(nextColor == PieceColor.White ? PieceColor.Black : PieceColor.White);

        MoveCompleted?.Invoke(nextColor);
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

    public IEnumerable<(int Row, int Col)> GetLegalMoves(int row, int column)
    {
        var square = this[row, column];
        if (square.Piece == null) yield break;

        foreach (var (r, c) in square.Piece.GetValidMoves(Board.Squares, row, column))
        {
            var captured = Board.Squares[r, c].Piece;
            Board.Squares[r, c].Piece = square.Piece;
            square.Piece = null;

            bool inCheck = (IsInCheck(Board.IsWhiteTurn ? PieceColor.White : PieceColor.Black));
            
            square.Piece = Board.Squares[r, c].Piece;
            Board.Squares[r, c].Piece = captured;

            if (!inCheck)
                yield return (r, c);
        }
    }

    public bool IsCheckmate(PieceColor color)
    {
        if (!IsInCheck(color)) return false;

        foreach (var square in Board.Squares)
        {
            if (square.Piece == null) continue;

            if (square.Piece.Color == color)
            {
                if (GetLegalMoves(square.Row, square.Column).Any()) return false;
            }
        }
        return true;
    }
}