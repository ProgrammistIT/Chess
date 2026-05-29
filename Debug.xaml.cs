using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Chess;

public partial class Debug : ContentPage
{
    private bool _isTimerActive;
    private bool _shouldStop;
    private const double IntervalTimer = 1.0;
    private static readonly string FolderPath = FileSystem.Current.AppDataDirectory;
    
    public Debug()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _shouldStop = false;
        _isTimerActive = true;
        CheckFileAndUpdateButton();

        if (!_isTimerActive)
        {
            _isTimerActive = true;
            Dispatcher.StartTimer(TimeSpan.FromSeconds(IntervalTimer), CheckFileAndUpdateButton);
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _shouldStop = true;
    }
    private bool CheckFileAndUpdateButton()
    {
        if (_shouldStop)
        {
            _isTimerActive = false;
            return false;
        }

        DelFiles.IsEnabled = MainPageButtons.HasSavedFile();
        
        return true;
    }

    private async void OpenFolder_Clicked(object sender, EventArgs e)
    {
        if (DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            Process.Start("explorer.exe", FolderPath);
        }
        else if (DeviceInfo.Platform == DevicePlatform.macOS)
        {
            Process.Start("open", FolderPath);
        }
        else
        {
            await DisplayAlertAsync("Внимание",                               
                "На мобильных платформах прямое открытие папок запрещено системой безопасности. " +
                "Используйте FolderPicker для выбора файлов.",
                "Понятно");                                            
        }
    }

    private void OpenNewWindow_OnClicked(object? sender, EventArgs e)
    {
        var newGame = new GamePage();
        Navigation.PushModalAsync(newGame);
    }

    private void CloseDebug_OnClicked(object? sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private void DelFiles_OnClicked(object? sender, EventArgs e)
    {
        File.Delete(Path.Combine(FolderPath, "game.xml"));
        File.Delete(Path.Combine(FolderPath, "game.json"));
        CheckFileAndUpdateButton();
    }
}