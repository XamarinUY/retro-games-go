using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RetroGamesGo.Core.Controls
{
    public class ArView : View
    {
        public static readonly BindableProperty ImageCapturedCommandProperty =
            BindableProperty.Create("ImageCapturedCommand", typeof(ICommand), typeof(ArView), null);

        public ICommand ImageCapturedCommand
        {
            get { return (ICommand)GetValue(ImageCapturedCommandProperty); }
            set { SetValue(ImageCapturedCommandProperty, value); }
        }
    }
}
