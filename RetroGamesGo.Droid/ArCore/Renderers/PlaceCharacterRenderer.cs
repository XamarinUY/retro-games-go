using System.Collections.Concurrent;
using System.Collections.Generic;
using Android.Content;
using Android.Opengl;
using Android.Views;
using Google.AR.Core;
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
        private readonly List<ObjModelRenderer> models = new List<ObjModelRenderer>();
        private readonly List<Anchor> anchors = new List<Anchor>();
        private readonly ConcurrentQueue<MotionEvent> queuedSingleTaps = new ConcurrentQueue<MotionEvent>();
        private readonly List<PlaneAttachmentHelper> touches = new List<PlaneAttachmentHelper>();
        private static readonly  float[] anchorMatrix = new float[16];
        private Character character;

        /// <summary>
        /// Creates the AR Renderer
        /// </summary>        
        public PlaceCharacterRenderer(Context context, GLSurfaceView surfaceView, Character character) : base(context, surfaceView)
        {
            this.character = character;
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


        //private void LoadModels()
        //{
        //    //var model = LoadModel("Luigi/luigi.obj", "Luigi/luigiD.jpg");
        //    //var model = LoadModel("Andy/andy.obj", "Andy/andy.png");
        //    var model = LoadModel("Mario/Mario2.obj", "Mario/Mario.png");
        //    this.models.Add(model);
        //}

        protected override void LoadArAssets()
        {
            try
            {
                //var model = new ObjModelRenderer();
                //model.CreateOnGlThread(this.context, modelName, textureName);
                //model.SetMaterialProperties(0.0f, 3.5f, 1.0f, 6.0f);

                ////mVirtualObjectShadow.CreateOnGlThread(_context,
                ////    "andy_shadow.obj", "andy_shadow.png");
                ////mVirtualObjectShadow.SetBlendMode(ObjectRenderer.BlendMode.Shadow);
                ////mVirtualObjectShadow.SetMaterialProperties(1.0f, 0.0f, 0.0f, 1.0f);
                //return model;
            }
            catch (Java.IO.IOException ex)
            {
                //Log.Error(TAG, "Failed to read obj file");
            }      
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
                    if (touches.Count >= 5)
                    {
                        this.anchors[0].Detach();
                        this.anchors.RemoveAt(0);
                    }                      
                    this.anchors.Add(hit.CreateAnchor());                        
                    break;
                }
            }
        }


        /// <summary>
        /// Renders the point cloud and the detected planes
        /// </summary>        
        protected override void RenderPlanes(Camera camera, Google.AR.Core.Frame frame)
        {
            var projectionMatrix = new float[16];
            camera.GetProjectionMatrix(projectionMatrix, 0, 0.1f, 100.0f);
            var viewMatrix = new float[16];
            camera.GetViewMatrix(viewMatrix, 0);
      
            // Renders the point cloud
            var pointCloud = frame.AcquirePointCloud();
            pointCloudRenderer.Update(pointCloud);
            pointCloudRenderer.Draw(camera.DisplayOrientedPose, viewMatrix, projectionMatrix);
            pointCloud.Release();

            // Check if we detected at least one plane. If so, hide the loading message.              
            var planes = new List<Plane>();
            foreach (var p in session.GetAllTrackables(Java.Lang.Class.FromType(typeof(Plane))))
            {
                var plane = (Plane)p;
                planes.Add(plane);
            }

            foreach (var plane in planes)
            {
                if (plane.GetType() == Plane.Type.HorizontalUpwardFacing
                    && plane.TrackingState == TrackingState.Tracking)
                {
                    // Todo: show a message while its tracking for a plane to put the model into and hide when a plane is found                        
                    break;
                }
            }

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

            foreach (var anchor in this.anchors)
            {

                if (anchor.TrackingState != TrackingState.Tracking)
                {
                    continue;
                }

                // Get the current combined pose of an Anchor and Plane in world space. The Anchor
                // and Plane poses are updated during calls to session.update() as ARCore refines
                // its estimate of the world.
                anchor.Pose.ToMatrix(anchorMatrix, 0);

                // Update and draw the model and its shadow.
                foreach (var model in this.models)
                {
                    model.UpdateModelMatrix(anchorMatrix, scaleFactor);
                    model.Draw(viewMatrix, projectionMatrix, lightIntensity);
                }
                //mVirtualObject.UpdateModelMatrix(mAnchorMatrix, scaleFactor);
                //mVirtualObjectShadow.UpdateModelMatrix(mAnchorMatrix, scaleFactor);
                //mVirtualObject.Draw(viewMatrix, projectionMatrix, lightIntensity);
                //mVirtualObjectShadow.Draw(viewMatrix, projectionMatrix, lightIntensity);

            }
        }

    }

}
