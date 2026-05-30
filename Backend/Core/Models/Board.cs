using Chess.Backend.Enums;
using Chess.Backend.Models.Pieces;

namespace Chess.Backend.Models;

public class Board
{
    public Square[,] Squares { get; } = new Square[8, 8];
    public Square this[int row, int col] => Squares[row, col];
    public bool IsWhiteTurn { get; private set; }

    public void ChangeTurn()
    {
        IsWhiteTurn = !IsWhiteTurn;
    }
    public Board()
    {
        IsWhiteTurn = true;
        InitSquares();
        PlacePieces();
    }

    private void InitSquares()
    {
        for (int i = 0; i < Squares.GetLength(0); i++)
        for (int j = 0; j < Squares.GetLength(1); j++)
            Squares[i, j] = new Square(i, j);
    }

    private void PlacePieces()
    {
        PlaceBackRow(0, PieceColor.Black);
        PlaceBackRow(7, PieceColor.White);

        for (int col = 0; col < 8; col++)
        {
            Squares[1, col].Piece = new Pawn(PieceColor.Black);
            Squares[6, col].Piece = new Pawn(PieceColor.White);
        }
    }

    private void PlaceBackRow(int row, PieceColor color)
    {
        Squares[row, 0].Piece = new Rook(color);
        Squares[row, 1].Piece = new Knight(color);
        Squares[row, 2].Piece = new Bishop(color);
        Squares[row, 3].Piece = new Queen(color);
        Squares[row, 4].Piece = new King(color);
        Squares[row, 5].Piece = new Bishop(color);
        Squares[row, 6].Piece = new Knight(color);
        Squares[row, 7].Piece = new Rook(color);
    }
}