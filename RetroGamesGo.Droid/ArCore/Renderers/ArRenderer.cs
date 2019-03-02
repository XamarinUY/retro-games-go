using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using Android.Content;
using Android.Opengl;
using Android.Util;
using Android.Views;
using Google.AR.Core;
using Javax.Microedition.Khronos.Opengles;
using RetroGamesGo.Droid.ArCore.Helpers;
using Xamarin.Forms;
using EGLConfig = Javax.Microedition.Khronos.Egl.EGLConfig;

namespace RetroGamesGo.Droid.ArCore.Renderers
{
    public class ArRenderer : Java.Lang.Object, GLSurfaceView.IRenderer, Android.Views.View.IOnTouchListener
    {
        private readonly Session session;
        private readonly Context context;
        private readonly GestureDetector gestureDetector;
        private readonly GLSurfaceView surfaceView;
        private readonly DisplayRotationHelper displayRotationHelper;
        private readonly BackgroundRenderer backgroundRenderer = new BackgroundRenderer();
        private PointCloudRenderer pointCloudRenderer = new PointCloudRenderer();
        private List<ObjModelRenderer> models = new List<ObjModelRenderer>();
        private List<Anchor> anchors = new List<Anchor>();
        private ConcurrentQueue<MotionEvent> queuedSingleTaps = new ConcurrentQueue<MotionEvent>();
        private List<PlaneAttachmentHelper> touches = new List<PlaneAttachmentHelper>();
        private static float[] mAnchorMatrix = new float[16];

        /// <summary>
        /// Creates the AR Renderer
        /// </summary>        
        public ArRenderer(Context context, GLSurfaceView surfaceView)
        {            
            this.context = context;
            this.surfaceView = surfaceView;

            try
            {
                this.session = new Session(this.context);
                this.displayRotationHelper = new DisplayRotationHelper(this.context);

                var config = new Google.AR.Core.Config(this.session);
                if (!session.IsSupported(config))
                {
                    return;
                }

                
                this.gestureDetector = new GestureDetector(this.context, new TapGestureDetectorHelper
                {
                    SingleTapUpHandler = (arg) =>
                    {
                        OnSingleTap(arg);
                        return true;
                    },
                    DownHandler = (arg) => true
                });

                //Set ups renderer.
                this.surfaceView.SetOnTouchListener(this);
                this.surfaceView.PreserveEGLContextOnPause = true;
                this.surfaceView.SetEGLContextClientVersion(2);
                this.surfaceView.SetEGLConfigChooser(8, 8, 8, 8, 16, 0);
            }
            catch (Java.Lang.Exception ex)
            {
                // Log somewhere
            }
            catch (System.Exception ex)
            {
                // Log somewhere
            }
        }


        /// <summary>
        /// Draw a frame
        /// </summary>        
        public void OnDrawFrame(IGL10 gl)
        {                   
            // Clear screen to notify driver it should not load any pixels from previous frame.
            GLES20.GlClear(GLES20.GlColorBufferBit | GLES20.GlDepthBufferBit);
            if (this.session == null) return;

            // Notify ARCore session that the view size changed so that the perspective matrix and the video background
            // can be properly adjusted
            this.displayRotationHelper.UpdateSessionIfNeeded(this.session);

            try
            {
                // Obtain the current frame from ARSession. When the configuration is set to
                // UpdateMode.BLOCKING (it is by default), this will throttle the rendering to the
                // camera framerate.

                var frame = this.session.Update();
                var camera = frame.Camera;

                // Handle taps. Handling only one tap per frame, as taps are usually low frequency
                // compared to frame rate.
                MotionEvent tap = null;
                queuedSingleTaps.TryDequeue(out tap);

                
                if (tap != null && camera.TrackingState == TrackingState.Tracking)
                {
                    foreach (var hit in frame.HitTest(tap))
                    {
                        var trackable = hit.Trackable;

                        // Check if any plane was hit, and if it was hit inside the plane polygon.
                        if (trackable is Plane && ((Plane)trackable).IsPoseInPolygon(hit.HitPose))
                        {
                            // Cap the number of objects created. This avoids overloading both the
                            // rendering system and ARCore.
                            if (touches.Count >= 16)
                            {
                                this.anchors[0].Detach();
                                this.anchors.RemoveAt(0);
                            }
                            // Adding an Anchor tells ARCore that it should track this position in
                            // space. This anchor will be used in PlaneAttachment to place the 3d model
                            // in the correct position relative both to the world and to the plane.
                            //mTouches.Add(new PlaneAttachment(
                            //    ((PlaneHitResult)hit).Plane,
                            //    mSession.AddAnchor(hit.HitPose)));
                            this.anchors.Add(hit.CreateAnchor());

                            // Hits are sorted by depth. Consider only closest hit on a plane.
                            break;
                        }
                    }
                }

                // Draw background.
                this.backgroundRenderer.Draw(frame);
                if (camera.TrackingState == TrackingState.Paused) return;

                var projectionMatrix = new float[16];
                camera.GetProjectionMatrix(projectionMatrix, 0, 0.1f, 100.0f);
                var viewMatrix = new float[16];
                camera.GetViewMatrix(viewMatrix, 0);
                var lightIntensity = frame.LightEstimate.PixelIntensity;

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
                


               // Visualize planes.
               // mPlaneRenderer.DrawPlanes(planes, camera.DisplayOrientedPose, projectionMatrix);

               // Visualize anchors created by touch.
               var scaleFactor = 0.1f;
                foreach (var anchor in this.anchors)
                {

                    if (anchor.TrackingState != TrackingState.Tracking)
                    {
                        continue;
                    }


                    // Get the current combined pose of an Anchor and Plane in world space. The Anchor
                    // and Plane poses are updated during calls to session.update() as ARCore refines
                    // its estimate of the world.
                    anchor.Pose.ToMatrix(mAnchorMatrix, 0);

                    // Update and draw the model and its shadow.
                    foreach (var model in this.models)
                    {
                        model.UpdateModelMatrix(mAnchorMatrix, scaleFactor);
                        model.Draw(viewMatrix, projectionMatrix, lightIntensity);
                    }
                    //mVirtualObject.UpdateModelMatrix(mAnchorMatrix, scaleFactor);
                    //mVirtualObjectShadow.UpdateModelMatrix(mAnchorMatrix, scaleFactor);
                    //mVirtualObject.Draw(viewMatrix, projectionMatrix, lightIntensity);
                    //mVirtualObjectShadow.Draw(viewMatrix, projectionMatrix, lightIntensity);

                }
            }
            catch (System.Exception ex)
            {
                // Avoid crashing the application due to unhandled exceptions.
                //Log.Error("ArCore", "Exception on the OpenGL thread", ex);
            }
        }



        /// <summary>
        /// Notify ARCore session that the view size changed so that the perspective matrix and
        /// the video background can be properly adjusted.</summary>
        public void OnSurfaceChanged(IGL10 gl, int width, int height)
        {
            this.displayRotationHelper.OnSurfaceChanged(width, height);
            GLES20.GlViewport(0, 0, width, height);
        }


        /// <summary>
        /// Called when the OpenGL surface its created
        /// </summary>
        public void OnSurfaceCreated(IGL10 gl, EGLConfig config)
        {
            GLES20.GlClearColor(0.1f, 0.1f, 0.1f, 1.0f);

            // Create the texture and pass it to ARCore session to be filled during update().
            this.backgroundRenderer.CreateOnGlThread(this.context);
            this.session.SetCameraTextureName(this.backgroundRenderer.TextureId);
         
            //try
            //{
            //    mPlaneRenderer.CreateOnGlThread(_context, "trigrid.png");
            //}
            //catch (Java.IO.IOException ex)
            //{
            //    Log.Error(TAG, "Failed to read plane texture");
            //}
            this.pointCloudRenderer.CreateOnGlThread(this.context);

            this.session.Resume(); ;
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


        private void LoadModels()
        {
            //var model = LoadModel("Luigi/luigi.obj", "Luigi/luigiD.jpg");
            //var model = LoadModel("Andy/andy.obj", "Andy/andy.png");
            var model = LoadModel("Mario/Mario2.obj", "Mario/Mario.png");
            this.models.Add(model);
        }


        private ObjModelRenderer LoadModel(string modelName, string textureName, string modelShadowName = "", string textureShadowName = "")
        {
            
            try
            {
                var model = new ObjModelRenderer();
                model.CreateOnGlThread(this.context, modelName, textureName);
                model.SetMaterialProperties(0.0f, 3.5f, 1.0f, 6.0f);

                //mVirtualObjectShadow.CreateOnGlThread(_context,
                //    "andy_shadow.obj", "andy_shadow.png");
                //mVirtualObjectShadow.SetBlendMode(ObjectRenderer.BlendMode.Shadow);
                //mVirtualObjectShadow.SetMaterialProperties(1.0f, 0.0f, 0.0f, 1.0f);
                return model;
            }
            catch (Java.IO.IOException ex)
            {
               //Log.Error(TAG, "Failed to read obj file");
            }
            return null;
        }
    }

}
