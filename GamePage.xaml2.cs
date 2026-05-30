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
    // Получение информации про очередность хода
    private bool CheckTurn(Piece piece)
    {
        return (_gameService.Board.IsWhiteTurn && piece.Color == PieceColor.White) ||
               (!_gameService.Board.IsWhiteTurn && piece.Color == PieceColor.Black);
    }
    // Работа с клетками подсказок
    private void DeleteTurn(int row, int col)
    {
        DeleteImage(_turnImages, row, col);
    }
    private void ClearTurns()
    {
        Clear(_turnImages);
    }
    // Работа с клетками фигур
    private void DeletePieces(int row, int col)
    {
        DeleteImage(_pieceImages, row, col);
    }
    private void ClearPieces()
    {
        Clear(_pieceImages);
    }
    // Работа со всеми видами картинок на клетках
    private void DeleteImage(Image[,] images, int row, int col)
    {
        if (images[row, col] != null)
        {
            ChessBoardGrid.Children.Remove(images[row, col]);
            images[row, col] = null;
        }
    }
    private void Clear(Image[,] images)
    {
        for (int i = 0; i < images.GetLength(0); i++)
        {
            for (int j = 0; j < images.GetLength(1); j++)
            {
                DeleteImage(images, i, j);
            }
        }
    }
    // Получение цвета клетки для заливки
    private Color GetColor(Square square)
    {
        var black = new Color(0, 0, 25);
        var white = new Color(255, 255, 230);
        if (square.ColorOfSquare == SquareColor.Dark) return black;
        else return white;
    }
    // Работа с ускоренным созданием изображений
    private Image GetImage(ImageSource source)
    {
        var image = new Image
        {
            Aspect = Aspect.AspectFit,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Margin = 0,
            InputTransparent = true
        };
        image.Source = source;
        return image;
    }
    private Image GetFigurePicture(int row, int col)
    {
        var piece =  _gameService[row, col];
        if (piece.Piece == null) return null;
        var img = GetImage(GetImageName(piece.Piece));
        return img;
    }
    private ImageSource GetImageName(Piece piece)
    {
        
        if (piece.Color == PieceColor.Black)
        {
            if (piece.Type == PieceType.King)
            {
                // Проверка на шах/мат
                return ImageSource.FromFile("king_black.png");
            }
            if(piece.Type == PieceType.Queen) return ImageSource.FromFile("queen_black.png");
            if(piece.Type == PieceType.Pawn) return ImageSource.FromFile("pawn_black.png");
            if(piece.Type == PieceType.Knight) return ImageSource.FromFile("knight_black.png");
            if(piece.Type == PieceType.Bishop) return ImageSource.FromFile("bishop_black.png");
            if(piece.Type == PieceType.Rook) return ImageSource.FromFile("rook_black.png");
        }
        else if (piece.Color == PieceColor.White)
        {
            if (piece.Type == PieceType.King)
            {
                return ImageSource.FromFile("king_white.png");
            }
            if(piece.Type == PieceType.Queen)  return ImageSource.FromFile("queen_white.png");
            if(piece.Type == PieceType.Pawn) return ImageSource.FromFile("pawn_white.png");
            if(piece.Type == PieceType.Knight) return ImageSource.FromFile("knight_white.png");
            if(piece.Type == PieceType.Bishop) return ImageSource.FromFile("bishop_white.png");
            if(piece.Type == PieceType.Rook)  return ImageSource.FromFile("rook_white.png");
                    
        }

        return null;
    }
    // Действия при нажатии на пустую клетку
    private void OnEmptyClick(int row, int col)
    {
        if (!_isChosen) return;

        bool moved = _gameService.TryMove(_rowChosen, _colChosen, row, col);

        if (moved)
        {
            if (_pieceImages[row, col] != null)
            {
                ChessBoardGrid.Children.Remove(_pieceImages[row, col]);
                _pieceImages[row, col] = null;
            }
            var movedImage = _pieceImages[_rowChosen, _colChosen];
            if (movedImage != null)
            {
                ChessBoardGrid.Children.Remove(movedImage);
                Grid.SetRow(movedImage, row);
                Grid.SetColumn(movedImage, col);
                ChessBoardGrid.Children.Add(movedImage);
                _pieceImages[row, col] = movedImage;
            }
            _pieceImages[_rowChosen, _colChosen] = null;
        }

        _isChosen = false;
        ClearTurns();
    }
    // Действие при нажатии на клетку с фигурой
    private void OnPieceClick(int row, int col)
    {
        _isChosen = true;
        _colChosen = col;
        _rowChosen = row;
        var piece = _gameService[row, col];
        ClearTurns();
        var imgChose = GetImage(ImageSource.FromFile("chose.png"));
        Grid.SetColumn(imgChose, col);
        Grid.SetRow(imgChose, row);
        ChessBoardGrid.Children.Add(imgChose);
        _turnImages[row, col] = imgChose;
        foreach (var (r, c) in piece.Piece.GetValidMoves(_gameService.Board.Squares, row, col))
        {
            Image img = _gameService[r, c].Piece == null
                ? GetImage(ImageSource.FromFile("move.png"))
                : GetImage(ImageSource.FromFile("atack.png"));
            Grid.SetRow(img, r);
            Grid.SetColumn(img, c);
            ChessBoardGrid.Children.Add(img);
            _turnImages[r, c] = img;
        }
    }
}