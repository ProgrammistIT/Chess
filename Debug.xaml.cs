using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Chess;

public partial class Debug : ContentPage
{
    public Debug()
    {
        InitializeComponent();
    }
    private static readonly string FolderPath = FileSystem.Current.AppDataDirectory;

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
                "На мобильных платформах прямое открытие папок запрещено системой безопасности. Используйте FolderPicker для выбора файлов.",
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
}