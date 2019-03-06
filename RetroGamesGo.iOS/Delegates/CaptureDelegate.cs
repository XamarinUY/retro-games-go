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
    public class CaptureDelegate : ARSCNViewDelegate
    {
        public override void DidAddNode(ISCNSceneRenderer renderer, SCNNode node, ARAnchor anchor)
        {
            if (anchor != null && anchor is ARImageAnchor)
            {
                var imageAnchor = (ARImageAnchor)anchor;
                var imageName = imageAnchor.ReferenceImage.Name;
            
            }
        }

        public override void DidFail(ARSession session, NSError error)
        {
           // Handle the error
        }

    }
}