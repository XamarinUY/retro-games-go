using Android.Content;
using RetroGamesGo.Core.Controls;
using RetroGamesGo.Droid.Activities;
using RetroGamesGo.Droid.Game;
using RetroGamesGo.Droid.Renderers;
using Urho.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ArView), typeof(ArViewRenderer))]
namespace RetroGamesGo.Droid.Renderers
{
    public class ArViewRenderer : ViewRenderer<ArView, Android.Widget.RelativeLayout>
    {
        private DroidArGame game;
        private UrhoSurfacePlaceholder surface;


        public ArViewRenderer(Context context) : base(context)
        {
            
        }

      
        protected override async void OnElementChanged(ElementChangedEventArgs<ArView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {                
                SetNativeControl(new Android.Widget.RelativeLayout(this.Context));
            }


            if (surface == null)
            {
                this.surface = UrhoSurface.CreateSurface(MainActivity.Instance);

                this.Control.AddView(surface);

                game = await surface.Show<DroidArGame>(
                    new Urho.ApplicationOptions
                    {
                       // ResourcePaths = new[] { "MutantData" }
                    });
            }
        }

        
    }
}
