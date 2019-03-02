namespace RetroGamesGo.Core.Models
{
    /// <summary>
    /// Game character to capture
    /// </summary>
    public class Character : EntityBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Number { get; set; }

        public int Year { get; set; }

        public string Url { get; set; }

        public string FunFact { get; set; }

        public string Animation { get; set; }

        public string Silhouette { get; set; }

        public string AssetSticker { get; set; }

        public string AssetModel { get; set; }

        public string AssetTexture { get; set; }

        private bool captured;

        public bool Captured
        {
            get => this.captured;
            set
            {
                this.captured = value;
                this.RaisePropertyChanged(() => this.Captured);
            }
        }

        public Character()
        {

        }
    }
}
