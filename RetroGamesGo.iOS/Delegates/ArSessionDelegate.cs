using System;
using ARKit;

namespace RetroGamesGo.iOS.Delegates
{
    /// <summary>
    /// Handles the camera changes
    /// </summary>
    public class ArSessionDelegate : ARSessionDelegate
    {
        public override void CameraDidChangeTrackingState(ARSession session, ARCamera camera)
        {
            var state = string.Empty;
            var reason = string.Empty;
            switch (camera.TrackingState)
            {
                case ARTrackingState.NotAvailable:
                    state = "Tracking Not Available";
                    break;
                case ARTrackingState.Normal:
                    state = "Tracking Normal";
                    break;
                case ARTrackingState.Limited:
                    state = "Tracking Limited";
                    switch (camera.TrackingStateReason)
                    {
                        case ARTrackingStateReason.ExcessiveMotion:
                            reason = "because of excessive motion";
                            break;
                        case ARTrackingStateReason.Initializing:
                            reason = "because tracking is initializing";
                            break;
                        case ARTrackingStateReason.InsufficientFeatures:
                            reason = "because of insufficient features in the environment";
                            break;
                        case ARTrackingStateReason.None:
                            reason = "because of an unknown reason";
                            break;
                    }
                    break;
            }         
            Console.WriteLine("{0} {1}", state, reason);
        }

        
    }
}