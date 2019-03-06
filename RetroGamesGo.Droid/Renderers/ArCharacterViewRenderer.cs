using Android.Content;
using Android.Opengl;
using RetroGamesGo.Core.Controls;
using RetroGamesGo.Droid.ArCore.Renderers;
using RetroGamesGo.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ArCharacterView), typeof(ArCharacterViewRenderer))]
namespace RetroGamesGo.Droid.Renderers
{
    /// <summary>
    /// View renderer for creating an OpenGL Surface on Android
    /// for the AR Render
    /// </summary>
    public class ArCharacterViewRenderer : ViewRenderer<ArCharacterView, GLSurfaceView>
    {
        private bool disposed;
        private readonly Context context;

     
        public ArCharacterViewRenderer(Context context) : base(context)
        {
            AutoPackage = false;
            this.context = context;            
        }


        protected override void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                disposed = true;
            }
            base.Dispose(disposing);
        }


        /// <summary>
        /// Crates the native control
        /// For AR in Android is a GLSurfaceView
        /// </summary>
        /// <returns></returns>
        protected override GLSurfaceView CreateNativeControl()
        {
            return new GLSurfaceView(Context);
        }


        /// <summary>
        /// Setups the el GL Surface for AR
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<ArCharacterView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null) return;
            var surfaceView = Control;

            if (surfaceView == null)
            {
                surfaceView = CreateNativeControl();
                surfaceView.SetEGLContextClientVersion(2);
                SetNativeControl(surfaceView);
            }

            var character = e.NewElement.Character;
            surfaceView.SetRenderer(new PlaceCharacterRenderer(this.context, surfaceView, character));
            surfaceView.RenderMode = Rendermode.Continuously;
        }


    }
}
