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
                Grid.SetRow(cell, j);
                Grid.SetColumn(cell, i);
                ChessBoardGrid.Children.Add(cell);
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