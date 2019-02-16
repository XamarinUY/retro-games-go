using System;

namespace RetroGamesGo.Core.Models
{
    public class Character: EntityBase
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string FunFact { get; set; }
        public string Picture { get; set; }
        public string Silhouette { get; set; }
        public bool Captured { get; set; }

        public Character()
        {

        }
    }
}
