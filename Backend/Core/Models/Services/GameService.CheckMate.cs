using Chess.Backend.Enums;

namespace Chess.Backend.Services;

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