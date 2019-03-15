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
        /// <summary>
        /// Called when a node is added / detected
        /// </summary>        
        public override void DidAddNode(ISCNSceneRenderer renderer, SCNNode node, ARAnchor anchor)
        {
            if (anchor is ARPlaneAnchor planeAnchor)
            {
                PlaceAnchorNode(node, planeAnchor);
            }
        }


        /// <summary>
        /// Called when a node is updated
        /// </summary>        
        public override void DidUpdateNode(ISCNSceneRenderer renderer, SCNNode node, ARAnchor anchor)
        {
            if (anchor is ARPlaneAnchor planeAnchor)
            {
                var currentPlaneNode = node.ChildNodes.FirstOrDefault();
                if (currentPlaneNode?.Geometry is SCNPlane currentPlane)
                {                    
                    currentPlaneNode.Position = new SCNVector3(planeAnchor.Center.X, 0.0f, planeAnchor.Center.Z);
                    currentPlane.Width = planeAnchor.Extent.X;
                    currentPlane.Height = planeAnchor.Extent.Z;
                }

                //planeAnchor.plan .planeGeometry.width = CGFloat(anchor.extent.x)
                //self.planeGeometry.height = CGFloat(anchor.extent.z)
                //self.position = SCNVector3Make(anchor.center.x, -0.002, anchor.center.z)

                //foreach (var chilNode in node.ChildNodes)
                //{
                //    //if (chilNode.i
                //}
                // self.planeGeometry.width = anchor.extent.x;
                //self.planeGeometry.height = anchor.extent.z;

                // When the plane is first created it's center is 0,0,0 and 
                // the nodes transform contains the translation parameters. 
                // As the plane is updated the planes translation remains the 
                // same but it's center is updated so we need to update the 3D
                // geometry position
                //self.position = SCNVector3Make(anchor.center.x, 0,
                //planeAnchor.Identifier
                //System.Console.WriteLine($"The (updated) extent of the anchor is [{planeAnchor.Extent.X} , {planeAnchor.Extent.Y} , {planeAnchor.Extent.Z} ]");
            }
        }


        /// <summary>
        /// Something wrong happened, tries again to create the session
        /// </summary>        
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


        /// <summary>
        /// Creates a plane an place in on the center of the plane anchor
        /// and rotates so it lies flat
        /// </summary>        
        private void PlaceAnchorNode(SCNNode node, ARPlaneAnchor anchor)
        {
            var plane = SCNPlane.Create(anchor.Extent.X, anchor.Extent.Z);

            var material = new SCNMaterial();
            material.Diffuse.Contents = UIImage.FromFile("art.scnassets/PlaneGrid/grid.png");
            plane.Materials = new[] {material};
            plane.FirstMaterial.Transparency = 0.1f;
            
            var planeNode = SCNNode.FromGeometry(plane);
            planeNode.Position = new SCNVector3(anchor.Center.X, 0.0f, anchor.Center.Z);
            planeNode.Transform = SCNMatrix4.CreateRotationX((float)(-Math.PI / 2.0));
            node.AddChildNode(planeNode);         
        }


    }
}