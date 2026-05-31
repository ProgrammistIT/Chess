using Chess.Model.Core.Enums;
using Chess.Model.Models;

namespace Chess.Model.Core.Models.Services;

public partial class GameService
{
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