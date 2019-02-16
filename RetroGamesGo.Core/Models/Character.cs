using System;
namespace RetroGamesGo.Core.Models
{
    public class Character
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FunFact { get; set; }
        public string Image { get; set; }
        public bool Unlocked { get; set; }
    }
}
