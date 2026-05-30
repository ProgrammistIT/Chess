using Chess.Backend.Enums;
using Chess.Backend.Models;
using Chess.Backend.Services;
using Microsoft.Maui.Controls;

namespace Chess;

public partial class GamePage : ContentPage
{
    private GameService _gameService;
    private Image[,] _pieceImages;
    private Image[,] _turnImages;
    private int _colChosen, _rowChosen;
    private bool _isChosen;

    public GamePage()
    {
        _gameService = new GameService();
        _pieceImages = new Image[8, 8];
        _turnImages = new Image[8, 8];
        _colChosen = -1;
        _rowChosen = -1;
        _isChosen = false;

        _gameService.MoveCompleted += HandleMoveCompleted; // правильная подписка
        
        InitializeComponent();
        InitializeChessBoard();
        Loaded += OnPageLoaded;
    }

    private void HandleMoveCompleted(PieceColor nextTurn)
    {
        TitleGame.Text = nextTurn == PieceColor.White ? "White turn" : "Black turn";
    }

    private void InitializeChessBoard()
    {
        for (int i = 0; i < 8; i++) ChessBoardGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        for (int i = 0; i < 8; i++) ChessBoardGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                var square = _gameService[i, j];
                var cell = new BoxView();
                cell.Color = GetColor(square);
                cell.Margin = new Thickness(0, 0, 0, 0);
                Grid.SetRow(cell, i);
                Grid.SetColumn(cell, j);

                var tapTrack = new TapGestureRecognizer();
                tapTrack.Tapped += OnSquareTapped;
                cell.GestureRecognizers.Add(tapTrack);

                ChessBoardGrid.Children.Add(cell);

                if (_gameService[i, j].Piece == null) continue;
                var pieceImage = GetFigurePicture(i, j);
                _pieceImages[i, j] = pieceImage;
                Grid.SetRow(pieceImage, i);
                Grid.SetColumn(pieceImage, j);
                ChessBoardGrid.Children.Add(pieceImage);
            }
        }
        ClearTurns();
    }

    private void OnSquareTapped(object sender, EventArgs e)
    {
        if (sender is not BoxView cell)
        {
            Console.WriteLine("Empty");
            return;
        }
        int row = Grid.GetRow(cell);
        int col = Grid.GetColumn(cell);

        var piece = _gameService[row, col];
        if (piece.Piece != null)
        {
            if (!CheckTurn(piece.Piece))
            {
                OnEmptyClick(row, col);
                return;
            }
            OnPieceClick(row, col);
        }
        else
        {
            OnEmptyClick(row, col);
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

    private void OnPageLoaded(object sender, EventArgs e)
    {
        Loaded -= OnPageLoaded;
        AdjustChessBoardSize();
        this.SizeChanged += (s, args) => AdjustChessBoardSize();
    }

    private void AdjustChessBoardSize()
    {
        double horizontalMargin = 30 * 2;
        double strokeBuffer = Table.StrokeThickness * 2;
        double maxSize = 600;

        double availableWidth = this.Width
                                - Padding.HorizontalThickness
                                - horizontalMargin;

        double targetSize = Math.Min(availableWidth, maxSize);

        if (targetSize > 0)
        {
            int size = (int)Math.Round(targetSize, MidpointRounding.AwayFromZero);
            Table.WidthRequest = size + strokeBuffer;
            Table.HeightRequest = size + strokeBuffer;
            ChessBoardGrid.WidthRequest = size;
            ChessBoardGrid.HeightRequest = size;
        }
    }
}