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
    /// Delete for AR Rendering
    /// </summary>
    public class CaptureDelegate : ARSCNViewDelegate
    {
        public override void DidAddNode(ISCNSceneRenderer renderer, SCNNode node, ARAnchor anchor)
        {
            if (anchor != null && anchor is ARImageAnchor)
            {
                var imageAnchor = (ARImageAnchor)anchor;
                //var imageSize = imageAnchor.ReferenceImage.PhysicalSize;
                //var plane = new SCNPlane { Width = imageSize.Width, Height = imageSize.Height };
                //plane.FirstMaterial.Diffuse.Contents = UIColor.Clear;

                // Todo: show a message for the captured image
                var imageName = imageAnchor.ReferenceImage.Name;
            
            }
        }

        public override void DidFail(ARSession session, NSError error)
        {
           // Handle the error
        }

    }
}