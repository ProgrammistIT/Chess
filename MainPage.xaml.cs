using System.Diagnostics;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Controls;

namespace Chess;

public partial class MainPage : ContentPage
{
    private bool _isTimerActive;
    private bool _shouldStop;
    private const double IntervalTimer = 1.0;

    public MainPage()
    {
        InitializeComponent();
        _isTimerActive = false;
        _shouldStop = false;

        FolderPath = FileSystem.Current.AppDataDirectory;
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
        string XMLFileName = "game.xml";
        string JSONFileName = "game.json";

        string XMLFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, XMLFileName);
        string JSONFilePath = Path.Combine(FileSystem.Current.AppDataDirectory, JSONFileName);

        File.Create(XMLFilePath).Close();
        File.Create(JSONFilePath).Close();

        await DisplayAlertAsync("Ready", $"{FileSystem.Current.AppDataDirectory}", "OH NOOOOOO");
    }

    // ДЕБАГ
    // УДАЛИТЬ НА РЕЛИЗЕ
    private string FolderPath;

    private async void OpenFolder_Clicked(object sender, EventArgs e)
    {
        if (DeviceInfo.Platform == DevicePlatform.WinUI)               // 12. Проверяем, запущено ли на Windows
        {
            await DisplayAlertAsync("Внимание",                               // 17. Показываем модальное окно
                FolderPath,
                "Понятно");// 13. Запускаем Проводник Windows с указанием пути
            Process.Start(FolderPath);
        }
        else if (DeviceInfo.Platform == DevicePlatform.macOS)            // 14. Проверяем, запущено ли на macOS
        {
            Process.Start("open", FolderPath);                           // 15. Запускаем системную команду Finder для открытия пути
        }
        else                                                             // 16. Если платформа не Windows и не macOS (Android, iOS, Tizen, Web)
        {
            await DisplayAlertAsync("Внимание",                               // 17. Показываем модальное окно
                "На мобильных платформах прямое открытие папок запрещено системой безопасности. Используйте FolderPicker для выбора файлов.",
                "Понятно");                                              // 18. Текст кнопки закрытия алерта
        }
    }
}