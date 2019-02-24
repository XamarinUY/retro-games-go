namespace RetroGamesGo.Droid.ArCore.Helpers
{
    using Android.Content;
    using Android.Hardware.Display;
    using Android.Views;
    using Java.Interop;

    /// <summary>
    /// Helper to class to handle the device rotation
    /// Taken from the Hello AR demo in GitHub
    /// </summary>
    public class DisplayRotationHelper : Java.Lang.Object, DisplayManager.IDisplayListener
    {
        private bool viewportChanged;
        private int viewportWidth;
        private int viewportHeight;
        private Context context;
        private Display display;


        /// <summary>
        ///  Constructs the DisplayRotationHelper but does not register the listener yet.        
        /// </summary>
        public DisplayRotationHelper(Context context)
        {
            this.context = context;
            this.display = context.GetSystemService(
                    Java.Lang.Class.FromType(typeof(IWindowManager)))
                    .JavaCast<IWindowManager>().DefaultDisplay;
        }


        /// <summary>
        /// Registers the display listener. Should be called from {@link Activity#onResume()}.
        /// </summary>
        public void OnResume()
        {
            this.context.GetSystemService(Java.Lang.Class.FromType(typeof(DisplayManager)))
                    .JavaCast<DisplayManager>().RegisterDisplayListener(this, null);
        }


        /// <summary>
        ///  Un registers the display listener. Should be called from {@link Activity#onPause()}.
        /// </summary>
        public void OnPause()
        {
            this.context.GetSystemService(Java.Lang.Class.FromType(typeof(DisplayManager)))
                .JavaCast<DisplayManager>().UnregisterDisplayListener(this);
        }


        /// <summary>
        /// Records a change in surface dimensions. This will be later used by
        /// {@link #updateSessionIfNeeded(Session)}. Should be called from
        /// {@link android.opengl.GLSurfaceView.Renderer
        ///  #onSurfaceChanged(javax.microedition.khronos.opengles.GL10, int, int)}.
        /// </summary>
        public void OnSurfaceChanged(int width, int height)
        {
            this.viewportWidth = width;
            this.viewportHeight = height;
            this.viewportChanged = true;
        }


        /// <summary>
        /// Updates the session display geometry if a change was posted either by
        /// {@link #onSurfaceChanged(int, int)} call or by {@link #onDisplayChanged(int)} system
        /// callback. This function should be called explicitly before each call to
        /// {@link Session#update()}. This function will also clear the 'pending update'
        /// (viewportChanged) flag.
        /// </summary>
        public void UpdateSessionIfNeeded(Google.AR.Core.Session session)
        {
            if (!this.viewportChanged) return;
            var displayRotation = (int)this.display.Rotation;
            session.SetDisplayGeometry(displayRotation, this.viewportWidth, this.viewportHeight);
            this.viewportChanged = false;
        }


        /// <summary>
        ///  Returns the current rotation state of android display.
        /// Same as {@link Display#getRotation()}.
        /// </summary>
        public int GetRotation()
        {
            return (int)this.display.Rotation;
        }


        public void OnDisplayAdded(int displayId)
        {
        }


        public void OnDisplayRemoved(int displayId)
        {
        }


        public void OnDisplayChanged(int displayId)
        {
            this.viewportChanged = true;
        }
    }
}