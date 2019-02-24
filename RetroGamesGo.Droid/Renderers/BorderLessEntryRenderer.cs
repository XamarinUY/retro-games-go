using System;
using Android.Content;
using RetroGamesGo.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(BorderLessEntryRenderer))]
namespace RetroGamesGo.Droid.Renderers
{
    public class BorderLessEntryRenderer : EntryRenderer
    {
        public BorderLessEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Background = null;
                Control.SetPaddingRelative(0, 0, 0, 0);
            }
        }
    }
}
