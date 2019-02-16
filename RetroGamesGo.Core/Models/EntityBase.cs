using System;
using SQLite;

namespace RetroGamesGo.Core.Models
{
    public class EntityBase
    {
        [PrimaryKey, AutoIncrement]
        public Guid Id { get; set; }
    }
}
