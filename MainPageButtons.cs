using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class MainPageButtons
    {
        public static bool HasSavedFile()
        {
            string appDataPath = FileSystem.Current.AppDataDirectory;
            string jsonPath = Path.Combine(appDataPath, "game.json");
            string xmlPath = Path.Combine(appDataPath, "game.xml");
            return File.Exists(jsonPath) || File.Exists(xmlPath);
        }
    }
}
