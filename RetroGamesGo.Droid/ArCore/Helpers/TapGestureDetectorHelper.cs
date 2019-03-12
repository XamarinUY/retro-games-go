namespace RetroGamesGo.Droid.ArCore.Helpers
{
    using System;
    using Android.Views;

    /// <summary>
    /// Helper class to detect the gesture over the GL Surface
    /// </summary>
    internal class TapGestureDetectorHelper : GestureDetector.SimpleOnGestureListener
    {
        public Func<MotionEvent, bool> SingleTapUpHandler { get; set; }

        public Func<MotionEvent, bool> DownHandler { get; set; }


        public override bool OnSingleTapUp(MotionEvent e)
        {
            return SingleTapUpHandler?.Invoke(e) ?? false;
        }


        public override bool OnDown(MotionEvent e)
        {
            return DownHandler?.Invoke(e) ?? false;
        }
    }
}