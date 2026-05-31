using System.Text.Json;
using Chess.Backend.Models;

namespace Chess.Backend.Data;

public class JsonGameSerializer : GameSerializer
{
    public JsonGameSerializer(string filePath) : base(filePath) {}

    public override void Serialize(Board board)
    {
        var state = new GameState { IsWhiteTurn = board.IsWhiteTurn };
        foreach (var square in board.Squares)
        {
            if (square.Piece == null) continue;
            state.Pieces.Add(new PieceState
            {
                Row = square.Row,
                Col = square.Column,
                Type = square.Piece.Type,
                Color = square.Piece.Color
            });
        }
        var json = JsonSerializer.Serialize(state, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, json);
    }

    public override GameState? Deserialize()
    {
        if (!File.Exists(FilePath)) return null;
        var json = File.ReadAllText(FilePath);
        return DeserializeFromJson<GameState>(json); // используем обобщённый метод
    }
}