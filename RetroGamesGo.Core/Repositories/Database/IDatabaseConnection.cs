using System;
using SQLite;

namespace RetroGamesGo.Core.Repositories
{
    public interface IDatabaseConnection
    {
        SQLiteAsyncConnection GetConnection(string path);
    }
}
