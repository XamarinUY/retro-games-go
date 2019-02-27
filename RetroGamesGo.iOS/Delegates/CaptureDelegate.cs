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
                var imageSize = imageAnchor.ReferenceImage.PhysicalSize;

                var plane = new SCNPlane { Width = imageSize.Width, Height = imageSize.Height };
                plane.FirstMaterial.Diffuse.Contents = UIColor.Clear;


                //var planeNode = new SCNNode { Geometry = plane };
                //planeNode.Transform = SCNMatrix4.CreateRotationX((float)(Math.PI / -2.0));
                ////planeNode.Opacity = 0.25f;
                //node.AddChildNode(planeNode);

                var imageName = imageAnchor.ReferenceImage.Name;

                //if (imageName.Equals("ARImage-2"))
                //{
                //    //this.AddInfo(imageSize, planeNode, "$ 1244", "$ 995", "cart.png", "20dto.png");
                //    this.AddInfo(imageSize, planeNode, "$ 60", "$ 48", "cart.png", "20dto.png");
                //}
                //else
                //{
                //    //this.AddInfo(imageSize, planeNode, "$ 1244", "$ 1058", "cart.png", "15dto.png");
                //    this.AddInfo(imageSize, planeNode, "$ 60", "$ 51", "cart.png", "15dto.png");
                //}
            }
        }
       
    }
}