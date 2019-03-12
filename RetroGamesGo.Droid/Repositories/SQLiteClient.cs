using System;
using System.IO;
using RetroGamesGo.Core.Repositories;
using SQLite;

namespace RetroGamesGo.Droid
{
    public class SQLiteClient : IDatabaseConnection
    {
        public SQLiteAsyncConnection GetConnection(string filename)
        {
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var fullPath = Path.Combine(documentsPath, filename);
            var conn = new SQLiteAsyncConnection(fullPath);
            return conn;
        }
    }
}
