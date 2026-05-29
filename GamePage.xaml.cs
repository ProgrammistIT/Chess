using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Backend.Enums;
using Chess.Backend.Models;
using Microsoft.Maui.Controls;

namespace Chess;

public partial class GamePage : ContentPage
{
    public GamePage()
    {
        InitializeComponent();
        InitializeChessBoard();
    }

    private Color GetColor(Square square)
    {
        var black = new Color(0, 0, 25);
        var white = new Color(255, 255, 230);
        if (square.ColorOfSquare == SquareColor.Dark) return black;
        else return white;
    }
    private void InitializeChessBoard()
    {
        for(int i = 0; i < 8; i++) ChessBoardGrid.ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)});
        for(int i = 0; i < 8; i++) ChessBoardGrid.RowDefinitions.Add(new  RowDefinition {Height = new GridLength(1, GridUnitType.Star)});
        Board board = new Board();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                var square = board[i, j];
                var cell = new BoxView();
                cell.Color = GetColor(square);
                cell.Margin = new Thickness(0, 0, 0, 0);
                Grid.SetRow(cell, i);
                Grid.SetColumn(cell, j);
                ChessBoardGrid.Children.Add(cell);
                if(board[i, j].Piece == null) continue;
                var pieceImage = new Image();
                pieceImage.Aspect = Aspect.AspectFit;
                pieceImage.HorizontalOptions = LayoutOptions.Center;
                pieceImage.VerticalOptions = LayoutOptions.Center;
                pieceImage.Margin = 1;
                var piece =  board[i, j].Piece;
                if (piece.Color == PieceColor.Black)
                {
                    if (piece.Type == PieceType.King)
                    {
                        // Проверка на шах/мат
                        pieceImage.Source = ImageSource.FromFile("king_black.png");
                    }
                    else if(piece.Type == PieceType.Queen) pieceImage.Source = ImageSource.FromFile("queen_black.png");
                    else if(piece.Type == PieceType.Pawn) pieceImage.Source = ImageSource.FromFile("pawn_black.png");
                    else if(piece.Type == PieceType.Knight) pieceImage.Source = ImageSource.FromFile("knight_black.png");
                    else if(piece.Type == PieceType.Bishop) pieceImage.Source = ImageSource.FromFile("bishop_black.png");
                    else if(piece.Type == PieceType.Rook) pieceImage.Source = ImageSource.FromFile("rook_black.png");
                }
                else if (piece.Color == PieceColor.White)
                {
                    if (piece.Type == PieceType.King)
                    {
                        pieceImage.Source = ImageSource.FromFile("king_white.png");
                    }
                    else if(piece.Type == PieceType.Queen)  pieceImage.Source = ImageSource.FromFile("queen_white.png");
                    else if(piece.Type == PieceType.Pawn) pieceImage.Source = ImageSource.FromFile("pawn_white.png");
                    else if(piece.Type == PieceType.Knight) pieceImage.Source = ImageSource.FromFile("knight_white.png");
                    else if(piece.Type == PieceType.Bishop) pieceImage.Source = ImageSource.FromFile("bishop_white.png");
                    else if(piece.Type == PieceType.Rook)  pieceImage.Source = ImageSource.FromFile("rook_white.png");
                    
                }
                Grid.SetColumn(pieceImage, j);
                Grid.SetRow(pieceImage, i);
                ChessBoardGrid.Children.Add(pieceImage);
            }
        }
        
    }
    private async void Back_OnClicked(object? sender, EventArgs e)
    {
        try
        {
            await Navigation.PopModalAsync();   
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}