﻿using Android.Content;
using Android.Opengl;
using RetroGamesGo.Core.Controls;
using RetroGamesGo.Droid.ArCore.Renderers;
using RetroGamesGo.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ArView), typeof(ArViewRenderer))]
namespace RetroGamesGo.Droid.Renderers
{
    /// <summary>
    /// View renderer for creating an OpenGL Surface on Android
    /// for the AR Render
    /// </summary>
    public class ArViewRenderer : ViewRenderer<ArView, GLSurfaceView>
    {
        private bool disposed;
        private readonly Context context;


        public ArViewRenderer(Context context) : base(context)
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


        protected override async void OnElementChanged(ElementChangedEventArgs<ArView> e)
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

                surfaceView.SetRenderer(new ArRenderer(this.context, surfaceView));
                surfaceView.RenderMode = Rendermode.Continuously;
            }
        }


    }
}
