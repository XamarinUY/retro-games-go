using Android.Content;
using Android.Opengl;
using RetroGamesGo.Core.Controls;
using RetroGamesGo.Droid.ArCore.Renderers;
using RetroGamesGo.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ArCaptureView), typeof(ArCaptureViewRenderer))]
namespace RetroGamesGo.Droid.Renderers
{
    /// <summary>
    /// View renderer for creating an OpenGL Surface on Android
    /// for the AR Render
    /// </summary>
    public class ArCaptureViewRenderer : ViewRenderer<ArCaptureView, GLSurfaceView>
    {
        private bool disposed;
        private readonly Context context;


        public ArCaptureViewRenderer(Context context) : base(context)
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

        protected override GLSurfaceView CreateNativeControl()
        {
            return new GLSurfaceView(Context);
        }


        protected override async void OnElementChanged(ElementChangedEventArgs<ArCaptureView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var surfaceView = Control;
                if (surfaceView == null)
                {
                    surfaceView = CreateNativeControl();
                    surfaceView.SetEGLContextClientVersion(2);
                    SetNativeControl(surfaceView);
                }

                surfaceView.SetRenderer(new ImageRecognitionRenderer(this.context, surfaceView, 
                    (s) => (Element as ArCaptureView).ImageCapturedCommand?.Execute(s)));
                surfaceView.RenderMode = Rendermode.Continuously;
            }
        }


    }
}
