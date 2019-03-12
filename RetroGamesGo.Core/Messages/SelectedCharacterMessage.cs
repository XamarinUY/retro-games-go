namespace RetroGamesGo.Core.Messages
{
    using MvvmCross.Plugin.Messenger;
    using Models;

    /// <summary>
    /// Message sent when the selected character changes
    /// </summary>
    public class SelectedCharacterMessage : MvxMessage
    {
        public Character Character { get; private set; }

        public SelectedCharacterMessage(Character character, object sender) : base(sender)
        {
            this.Character = character;
        }
    }
}
