using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RetroGamesGo.Core.Controls
{
    public class AnimatedLabel : Label
    {
        public static new readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(AnimatedLabel), null);

        public new string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        public static new readonly BindableProperty IsVisibleProperty = BindableProperty.Create(nameof(IsVisible), typeof(bool), typeof(AnimatedLabel), true);

        public new bool IsVisible
        {
            get => (bool)this.GetValue(IsVisibleProperty);
            set => this.SetValue(IsVisibleProperty, value);
        }

        protected override async void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName.Equals(IsVisibleProperty.PropertyName))
            {
                if (this.IsVisible)
                {
                    if (this.Text != null)
                    {
                        int index = 0;
                        Device.StartTimer(TimeSpan.FromMilliseconds(30), () =>
                        {
                            if (index < this.Text.Length)
                            {
                                base.Text = this.Text.Substring(0, index);
                                ++index;
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        });
                    }
                }
                else
                {
                    base.Text = null;
                }
            }
        }
    }
}
