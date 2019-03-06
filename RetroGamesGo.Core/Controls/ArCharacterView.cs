using RetroGamesGo.Core.Models;
using Xamarin.Forms;

namespace RetroGamesGo.Core.Controls
{
    /// <summary>
    /// 3D Character View 
    /// </summary>
    public class ArCharacterView : View
    {
        public static readonly BindableProperty CharacterProperty = BindableProperty.Create(nameof(Character), typeof(Character), typeof(ArCharacterView));

        /// <summary>
        /// 3D Character to show
        /// </summary>
        public Character Character
        {
            get => (Character)GetValue(CharacterProperty);
            set => SetValue(CharacterProperty, value);
        }


    }
}
