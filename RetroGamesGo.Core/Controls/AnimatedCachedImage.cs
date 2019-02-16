using System;
using System.Runtime.CompilerServices;
using FFImageLoading.Forms;
using Xamarin.Forms;

namespace RetroGamesGo.Core.Controls
{
    public class AnimatedCachedImage : CachedImage
    {
        public AnimatedCachedImage()
        {
            this.Opacity = 0;
        }

        public static readonly BindableProperty CapturedProperty = BindableProperty.Create(nameof(Captured), typeof(bool), typeof(AnimatedCachedImage), false);

        public bool Captured
        {
            get => (bool)this.GetValue(CapturedProperty);
            set => this.SetValue(CapturedProperty, value);
        }

        protected override async void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName.Equals(CapturedProperty.PropertyName))
            {
                if (this.Captured)
                {
                    await this.FadeTo(1, 3000);
                }
                else
                {
                    this.Opacity = 0;
                }
            }
        }
    }
}
