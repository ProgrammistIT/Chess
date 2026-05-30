using System.Diagnostics;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Controls;

namespace Chess;

public partial class MainPage : ContentPage
{
    private bool _isTimerActive;
    private bool _shouldStop;
    private const double IntervalTimer = 1.0;
    private static readonly string FolderPath = FileSystem.Current.AppDataDirectory;

    public MainPage()
    {
        InitializeComponent();
        _isTimerActive = false;
        _shouldStop = false;
    }
    // При появлении страницы
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _shouldStop = false;
        CheckFileAndUpdateButton();
        if (!_isTimerActive)
        {
            _isTimerActive = true;
            Dispatcher.StartTimer(TimeSpan.FromSeconds(IntervalTimer), CheckFileAndUpdateButton);
        }
    }
    // При "уходе" страницы
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _shouldStop = true;
    }
    // Проверка на наличие файла
    private bool CheckFileAndUpdateButton()
    {
        if (_shouldStop)
        {
            _isTimerActive = false;
            return false;
        }

        ContinueButton.IsEnabled = MainPageButtons.HasSavedFile();
        return true;
    }
    // На нажатие новой кнопки
    private async void NewGame_Clicked(object sender, EventArgs e)
    {
        const string xmlFileName = "game.xml";
        const string jsonFileName = "game.json";

        var xmlFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, xmlFileName);
        var jsonFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, jsonFileName);

        File.Create(xmlFilePath).Close();
        File.Create(jsonFilePath).Close();

        var gamePage = new GamePage();
        await Navigation.PushModalAsync(gamePage);  
    }

    // ДЕБАГ
    // УДАЛИТЬ НА РЕЛИЗЕ

    private void OpenDebugWindow_OnClicked(object? sender, EventArgs e)
    {
        var debug = new Debug();
        Navigation.PushModalAsync(debug);
    }
    private async void ContinueGame_Clicked(object sender, EventArgs e)
{
    var filePath = Path.Combine(FileSystem.Current.AppDataDirectory, "game.json");
    var gamePage = new GamePage(filePath);
    await Navigation.PushModalAsync(gamePage);
}
}