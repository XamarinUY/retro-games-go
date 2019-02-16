using System;
using System.IO;
using RetroGamesGo.Core.Repositories;
using SQLite;

namespace RetroGamesGo.iOS
{
    public class SQLiteClient : IDatabaseConnection
    {
        public SQLiteAsyncConnection GetConnection(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }
            string fileFolder = Path.Combine(libFolder, filename);
            return new SQLiteAsyncConnection(fileFolder);
        }
    }
}
