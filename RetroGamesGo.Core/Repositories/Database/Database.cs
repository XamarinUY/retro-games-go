﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using MvvmCross;
using RetroGamesGo.Core.Models;

namespace RetroGamesGo.Core.Repositories
{
    public class Database<T> : IDatabase<T> where T : EntityBase, new()
    {
      

        private IDatabaseConnection databaseConnection;
        protected SQLiteAsyncConnection database;

        public Database(IDatabaseConnection databaseConn = null)
        {
            databaseConnection = databaseConn ?? Mvx.IoCProvider.Resolve<IDatabaseConnection>();
            database = databaseConnection.GetConnection("retro_game_go_db");
            InitDatabase();
        }

        private void InitDatabase()
        {
            if (database != null)
            {
                Task.Run(() => { database.CreateTableAsync<T>(); });
            }
        }

        public async Task<int> Delete(T item)
        {
            return await database.DeleteAsync(item);
        }

        public async Task<T> GetFirst()
        {
            return await database.Table<T>().FirstOrDefaultAsync();
        }

        public async Task<int> Insert(T item)
        {
            return await database.InsertAsync(item);
        }

        public async Task<List<T>> Select()
        {
            AsyncTableQuery<T> tb = database.Table<T>();
            return await tb.ToListAsync();
        }

        public async Task<T> Select(Guid identifier)
        {
            AsyncTableQuery<T> tb = database.Table<T>();
            var item = tb.Where(x => x.Id == identifier);
            return await item.FirstAsync();
        }

        public async Task<int> Update(T item)
        {
            var list = new List<T>() { item };
            return await database.UpdateAllAsync(list);
        }

        public async Task<List<T>> Select(string query, params object[] parameters)
        {
            return await database.QueryAsync<T>(query, parameters);
        }

        public async Task<bool> DeleteAll()
        {
            var result = await database.DeleteAllAsync<T>();
            return result == 0;
        }
    }
}
