using System;
namespace RetroGamesGo.Core.Models
{
    public class Character
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string FunFact { get; set; }
        public string Picture { get; set; }
        public string Silhouette { get; set; }
        public bool Captured { get; set; }
    }
}
