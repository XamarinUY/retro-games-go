using System;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using ARKit;
using CoreGraphics;
using Foundation;
using MvvmCross;
using MvvmCross.Plugin.Messenger;
using OpenTK;
using RetroGamesGo.Core.Messages;
using RetroGamesGo.Core.Models;
using RetroGamesGo.Core.Repositories;
using RetroGamesGo.iOS.Delegates;
using RetroGamesGo.iOS.Pages;
using RetroGamesGo.iOS.Renderers;
using SceneKit;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PlaceCharacterPage), typeof(PlaceCharacterPageRenderer))]
namespace RetroGamesGo.iOS.Renderers
{  
    /// <summary>
    /// Custom renderer for supporting ARKit on iOS
    /// </summary>
    public class PlaceCharacterPageRenderer : PageRenderer, IARSCNViewDelegate
    {
        private ARSCNView sceneView;
        private Character selectedCharacter;
        private MvxSubscriptionToken selectedCharacterMvxSubscriptionToken;
        private IMvxMessenger messengerService;


        public override bool ShouldAutorotate() => true;
   

        /// <summary>
        /// Initializes the ArKit scene
        /// </summary>
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.sceneView = new ARSCNView
            {
                Frame = this.View.Frame,
                UserInteractionEnabled = true,                                
                Delegate = new PlaceCharacterDelegate(),
                Session =
                {
                    Delegate = new ArSessionDelegate()
                },
            };                        
            this.View.AddSubview(this.sceneView);
            this.messengerService = Mvx.IoCProvider.GetSingleton<IMvxMessenger>();
            selectedCharacterMvxSubscriptionToken = this.messengerService.Subscribe<SelectedCharacterMessage>((e) =>
            {
                this.selectedCharacter = e.Character;
            });


            // Selects by default the first character
            Task.Run(async () =>
            {
                try
                {
                    var characters = await Mvx.IoCProvider.GetSingleton<ICharacterRepository>()?.GetAll();
                    if (characters.Any())
                    {
                        this.selectedCharacter = characters.FirstOrDefault(x => x.Captured);
                    }
                }
                catch
                {
                    // Ignored
                }
            });
        }


        /// <summary>
        /// Load the resources for the scene
        /// </summary>        
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);            
            this.sceneView.Session.Run(new ARWorldTrackingConfiguration
                {
                    AutoFocusEnabled = true,
                    PlaneDetection = ARPlaneDetection.Horizontal,
                    LightEstimationEnabled = true,
                    WorldAlignment = ARWorldAlignment.GravityAndHeading
                }, ARSessionRunOptions.ResetTracking | ARSessionRunOptions.RemoveExistingAnchors);

        }


        /// <summary>
        /// Pause the AR scene
        /// </summary>        
        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            this.sceneView.Session.Pause();
        }


        /// <summary>
        /// Handles the screen touches
        /// </summary>        
        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            var touch = touches.AnyObject as UITouch;
            if (touch != null)
            {
                var loc = touch.LocationInView(this.sceneView);
                var worldPos = WorldPositionFromHitTest(loc);

                //if corresponds to some world position
                if (worldPos.Item1.HasValue)
                {
                    PlaceModel(worldPos.Item1.Value);
                }
            }
        }


        /// <summary>
        ///  Getting world position from touch hit
        /// </summary>
        private Tuple<SCNVector3?, ARAnchor> WorldPositionFromHitTest(CGPoint pt)
        {
            var hits = this.sceneView.HitTest(pt, ARHitTestResultType.ExistingPlaneUsingExtent);
            if (hits != null && hits.Length > 0)
            {
                var anchors = hits.Where(r => r.Anchor is ARPlaneAnchor);
                if (anchors.Any())
                {
                    var first = anchors.First();
                    var pos = PositionFromTransform(first.WorldTransform);
                    return new Tuple<SCNVector3?, ARAnchor>(pos, (ARPlaneAnchor)first.Anchor);
                }
            }
            return new Tuple<SCNVector3?, ARAnchor>(null, null);
        }


        private SCNVector3 PositionFromTransform(NMatrix4 xform)
        {
            return new SCNVector3(xform.M14, xform.M24, xform.M34);
        }


        /// <summary>
        /// Places the model
        /// </summary>        
        private void PlaceModel(SCNVector3 pos)
        {
            if (this.selectedCharacter == null) return;
            var asset = $"art.scnassets/{this.selectedCharacter.AssetModel}";
            var texture = $"art.scnassets/{this.selectedCharacter.AssetTexture}";
            var model = CreateModelFromFile(asset, texture, this.selectedCharacter.Name, pos);
            if (model == null) return;            
            this.sceneView.Scene.RootNode.AddChildNode(model);            
        }


        /// <summary>
        /// Loads the model from file
        /// </summary>     
        private SCNNode CreateModelFromFile(string modelName, string textureName, string nodeName, SCNVector3 vector)
        {
            try
            {
                var mat = new SCNMaterial();
                mat.Diffuse.Contents = UIImage.FromFile(textureName);
                mat.LocksAmbientWithDiffuse = true;
               
                var scene = SCNScene.FromFile(modelName);
                var geometry = scene.RootNode.ChildNodes[0].Geometry;
                var modelNode = new SCNNode
                {
                    Position = vector,
                    Geometry = geometry,
                    Scale = new SCNVector3(0.1f, 0.1f, 0.1f)
                };
                modelNode.Geometry.Materials = new[] {mat};                               
                return modelNode;
            }
            catch(Exception ex)
            {
                var e = ex.Message;
            }

            return null;

        }
    }
}