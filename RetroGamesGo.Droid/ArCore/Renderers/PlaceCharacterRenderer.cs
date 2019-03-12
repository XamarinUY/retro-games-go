using System.Collections.Concurrent;
using System.Collections.Generic;
using Android.Content;
using Android.Opengl;
using Android.Views;
using Google.AR.Core;
using MvvmCross;
using MvvmCross.Plugin.Messenger;
using RetroGamesGo.Core.Messages;
using RetroGamesGo.Core.Models;
using RetroGamesGo.Droid.ArCore.Helpers;
using EGLConfig = Javax.Microedition.Khronos.Egl.EGLConfig;

namespace RetroGamesGo.Droid.ArCore.Renderers
{
    /// <summary>
    /// AR Renderer for 3D model placement
    /// </summary>
    public class PlaceCharacterRenderer : BaseArRenderer, Android.Views.View.IOnTouchListener
    {
        private readonly GestureDetector gestureDetector;        
        private readonly PointCloudRenderer pointCloudRenderer = new PointCloudRenderer();
        private readonly Dictionary<Anchor, ObjModelRenderer> anchors = new Dictionary<Anchor, ObjModelRenderer>();
        private readonly ConcurrentQueue<MotionEvent> queuedSingleTaps = new ConcurrentQueue<MotionEvent>();
        private readonly List<PlaneAttachmentHelper> touches = new List<PlaneAttachmentHelper>();
        private static readonly  float[] anchorMatrix = new float[16];
        private readonly IMvxMessenger messengerService;
        private Character selectedCharacter;
        private MvxSubscriptionToken selectedCharacterMvxSubscriptionToken;


        /// <summary>
        /// Creates the AR Renderer
        /// </summary>        
        public PlaceCharacterRenderer(Context context, GLSurfaceView surfaceView, Character character) : base(context, surfaceView)
        {
            this.selectedCharacter = character;
            this.gestureDetector = new GestureDetector(this.context, new TapGestureDetectorHelper
            {
                SingleTapUpHandler = (arg) =>
                {
                    OnSingleTap(arg);
                    return true;
                },
                DownHandler = (arg) => true
            });
            this.surfaceView.SetOnTouchListener(this);


            this.messengerService = Mvx.IoCProvider.GetSingleton<IMvxMessenger>();
            selectedCharacterMvxSubscriptionToken = this.messengerService.Subscribe<SelectedCharacterMessage>((e) =>
            {
                this.selectedCharacter = e.Character;
            });
        }


        /// <summary>
        /// Handles tha tab event.
        /// Add it to a queue to user as anchor point later
        /// </summary>        
        private void OnSingleTap(MotionEvent e)
        {
            if (queuedSingleTaps.Count < 5)
            {
                queuedSingleTaps.Enqueue(e);
            }
        }


        /// <summary>
        /// Handles the touch event
        /// </summary>        
        public bool OnTouch(Android.Views.View v, MotionEvent e)
        {
            return this.gestureDetector.OnTouchEvent(e);
        }


        /// <summary>
        /// Handle taps. Handling only one tap per frame, as taps are usually low frequency
        /// Adds and anchor when the tap is over a plane
        /// </summary>
        protected override void HandleTaps(Camera camera, Google.AR.Core.Frame frame)
        {
            queuedSingleTaps.TryDequeue(out var tap);

            if (tap == null || camera.TrackingState != TrackingState.Tracking) return;
            foreach (var hit in frame.HitTest(tap))
            {
                var trackable = hit.Trackable;                    
                if (trackable is Plane plane && plane.IsPoseInPolygon(hit.HitPose))
                {                        
                    if (touches.Count >= 10)
                    {                
                        break;
                    }

                    var model = new ObjModelRenderer();
                    model.CreateOnGlThread(this.context, selectedCharacter.AssetModel, selectedCharacter.AssetTexture);
                    model.SetMaterialProperties(0.0f, 3.5f, 1.0f, 6.0f);
                    this.anchors.Add(hit.CreateAnchor(), model);                        
                    break;
                }
            }
        }


        /// <summary>
        /// Renders the point cloud and the detected planes
        /// </summary>        
        protected override void RenderPlanes(Camera camera, Google.AR.Core.Frame frame)
        {
            //var projectionMatrix = new float[16];
            //camera.GetProjectionMatrix(projectionMatrix, 0, 0.1f, 100.0f);
            //var viewMatrix = new float[16];
            //camera.GetViewMatrix(viewMatrix, 0);
      
            //// Renders the point cloud
            //var pointCloud = frame.AcquirePointCloud();
            //pointCloudRenderer.Update(pointCloud);
            //pointCloudRenderer.Draw(camera.DisplayOrientedPose, viewMatrix, projectionMatrix);
            //pointCloud.Release();

            //// Check if we detected at least one plane. If so, hide the loading message.              
            //var planes = new List<Plane>();
            //foreach (var p in session.GetAllTrackables(Java.Lang.Class.FromType(typeof(Plane))))
            //{
            //    var plane = (Plane)p;
            //    planes.Add(plane);
            //}

            //foreach (var plane in planes)
            //{
            //    if (plane.GetType() == Plane.Type.HorizontalUpwardFacing
            //        && plane.TrackingState == TrackingState.Tracking)
            //    {
            //        // Todo: show a message while its tracking for a plane to put the model into and hide when a plane is found                        
            //        break;
            //    }
            //}

            // mPlaneRenderer.DrawPlanes(planes, camera.DisplayOrientedPose, projectionMatrix);
        }


        /// <summary>
        /// Renders the model for each anchor
        /// </summary>        
        protected override void RenderAnchors(Camera camera, Google.AR.Core.Frame frame)
        {
            var scaleFactor = 0.1f;
            var lightIntensity = frame.LightEstimate.PixelIntensity;
            var projectionMatrix = new float[16];
            camera.GetProjectionMatrix(projectionMatrix, 0, 0.1f, 100.0f);
            var viewMatrix = new float[16];
            camera.GetViewMatrix(viewMatrix, 0);

            foreach (var anchor in this.anchors.Keys)
            {
                if (anchor.TrackingState != TrackingState.Tracking)
                {
                    continue;
                }

                // Get the current combined pose of an Anchor and Plane in world space. The Anchor
                // and Plane poses are updated during calls to session.update() as ARCore refines
                // its estimate of the world.
                anchor.Pose.ToMatrix(anchorMatrix, 0);
                this.anchors[anchor].UpdateModelMatrix(anchorMatrix, scaleFactor);
                this.anchors[anchor].Draw(viewMatrix, projectionMatrix, lightIntensity);
            }
        }

    }

}
