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
    private Board _board;
    private Image[,] _pieceImages;
    private Image[,] _turnImages;
    private int _colChosen,  _rowChosen;
    private bool _isChosen;
    // Инициализация
    public GamePage()
    {
        _board = new Board();
        _pieceImages = new Image[8, 8];
        _turnImages = new Image[8, 8];
        _colChosen = -1;
        _rowChosen = -1;
        _isChosen = false;
        InitializeComponent();
        InitializeChessBoard();
    }
    // Инициализация шахматной доски
    private void InitializeChessBoard()
    {
        for(int i = 0; i < 8; i++) ChessBoardGrid.ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)});
        for(int i = 0; i < 8; i++) ChessBoardGrid.RowDefinitions.Add(new  RowDefinition {Height = new GridLength(1, GridUnitType.Star)});
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                // Впихуиваем клетку
                var square = _board[i, j];
                var cell = new BoxView();
                cell.Color = GetColor(square);
                cell.Margin = new Thickness(0, 0, 0, 0);
                Grid.SetRow(cell, i);
                Grid.SetColumn(cell, j);
                
                // Впихуиваем обработчик нажатия К САМОЙ КЛЕТКЕ
                var tapTrack = new TapGestureRecognizer();
                tapTrack.Tapped += OnSquareTapped;
                cell.GestureRecognizers.Add(tapTrack);
                
                // Добавляем отрисованную и готовую к нажатиям клетку
                ChessBoardGrid.Children.Add(cell);
                
                // Впихуиваем картинку в клетку
                if(_board[i, j].Piece == null) continue; // Если надо, конечно
                var pieceImage = GetFigurePicture(i, j);
                _pieceImages[i, j] = pieceImage;
                Grid.SetRow(pieceImage, i);
                Grid.SetColumn(pieceImage, j);
                ChessBoardGrid.Children.Add(pieceImage);
            }
        }
        ClearTurns();
    }
    // При нажатии на клетку
    private void OnSquareTapped(object sender, EventArgs e)
    {
        
        if (sender is not BoxView cell)
        {
            Console.WriteLine("Empty");
            return;
        }
        int row = Grid.GetRow(cell);
        int col = Grid.GetColumn(cell);
        
        var piece = _board[row, col];
        if (piece.Piece != null)
        {
            if (!CheckTurn(piece.Piece))
            {
                OnEmptyClick(row, col);
                return;
            }
            OnPieceClick(row, col);
        }
        else{
            OnEmptyClick(row, col);
        }
        
    }
    // Кнопка возврата
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