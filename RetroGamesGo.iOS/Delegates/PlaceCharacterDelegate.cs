using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ARKit;
using CoreGraphics;
using Foundation;
using SceneKit;
using UIKit;

namespace RetroGamesGo.iOS.Delegates
{
    /// <summary>
    /// Delegate for AR Rendering
    /// </summary>
    public class PlaceCharacterDelegate : ARSCNViewDelegate
    {
        public override void DidAddNode(ISCNSceneRenderer renderer, SCNNode node, ARAnchor anchor)
        {
        }

        public override void DidFail(ARSession session, NSError error)
        {
            if (error.Code == 102)
            {
                session.Pause();

                // Restart session with a different worldAlignment - prevents from crashing app
                session.Run(new ARWorldTrackingConfiguration
                    {
                        AutoFocusEnabled = true,
                        PlaneDetection = ARPlaneDetection.Horizontal,
                        LightEstimationEnabled = true,
                        WorldAlignment = ARWorldAlignment.Gravity
                    }, ARSessionRunOptions.ResetTracking | ARSessionRunOptions.RemoveExistingAnchors);

            }
        }

    }
}