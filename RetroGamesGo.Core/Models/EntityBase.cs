using System;
using MvvmCross.ViewModels;
using SQLite;

namespace RetroGamesGo.Core.Models
{
    public class EntityBase : MvxNotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public Guid Id { get; set; }
    }
}
