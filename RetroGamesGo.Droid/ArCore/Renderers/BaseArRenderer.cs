using Android.Content;
using Android.Opengl;
using Android.Util;
using Android.Views;
using Google.AR.Core;
using Javax.Microedition.Khronos.Opengles;
using RetroGamesGo.Droid.ArCore.Helpers;
using Xamarin.Forms;
using EGLConfig = Javax.Microedition.Khronos.Egl.EGLConfig;
using Frame = Xamarin.Forms.Frame;

namespace RetroGamesGo.Droid.ArCore.Renderers
{
    /// <summary>
    /// Base ARCore rendering class
    /// </summary>
    public class BaseArRenderer : Java.Lang.Object, GLSurfaceView.IRenderer
    {
        protected readonly Session session;
        protected readonly Context context;
        protected readonly GLSurfaceView surfaceView;
        private readonly DisplayRotationHelper displayRotationHelper;
        protected readonly BackgroundRenderer backgroundRenderer = new BackgroundRenderer();
        protected Google.AR.Core.Config config;

        /// <summary>
        /// Creates the AR Renderer
        /// </summary>        
        public BaseArRenderer(Context context, GLSurfaceView surfaceView)
        {
            this.context = context;
            this.surfaceView = surfaceView;
            this.session = new Session(this.context);
            this.displayRotationHelper = new DisplayRotationHelper(this.context);
            config = new Google.AR.Core.Config(this.session);
            if (!session.IsSupported(config))
            {
                return;
            }

            this.surfaceView.PreserveEGLContextOnPause = true;
            this.surfaceView.SetEGLContextClientVersion(2);
            this.surfaceView.SetEGLConfigChooser(8, 8, 8, 8, 16, 0);
        }


        /// <summary>
        /// Draw a frame
        /// </summary>        
        public void OnDrawFrame(IGL10 gl)
        {            
            GLES20.GlClear(GLES20.GlColorBufferBit | GLES20.GlDepthBufferBit);
            if (this.session == null) return;

            // Notify ARCore session that the view size changed so that the perspective matrix
            // and the video background can be properly adjusted
            this.displayRotationHelper.UpdateSessionIfNeeded(this.session);

            try
            {              
                var frame = this.session.Update();
                var camera = frame.Camera;
                CheckDetectedImages(frame);
                // Handle taps. Handling only one tap per frame, as taps are usually low frequency
                // compared to frame rate.


                /* if (tap != null && camera.TrackingState == TrackingState.Tracking)
                {
                    foreach (var hit in frame.HitTest(tap))
                    {
                        var trackable = hit.Trackable;

                        // Check if any plane was hit, and if it was hit inside the plane polygon.
                        if (trackable is Plane && ((Plane) trackable).IsPoseInPolygon(hit.HitPose))
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
                }*/

                // Draw background.
                this.backgroundRenderer.Draw(frame);
                if (camera.TrackingState == TrackingState.Paused) return;

              /*  var projectionMatrix = new float[16];
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
                    var plane = (Plane) p;
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
                }*/
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
            this.backgroundRenderer.CreateOnGlThread(this.context);
            this.session.SetCameraTextureName(this.backgroundRenderer.TextureId);
            LoadArAssets();
            this.session.Resume();
        }


        /// <summary>
        /// Load any assets related to thi AR session
        /// </summary>
        protected virtual void LoadArAssets()
        {

        }


        /// <summary>
        /// Checks if some augmented images were detected
        /// </summary>
        protected virtual void CheckDetectedImages(Google.AR.Core.Frame frame)
        {

        }
    }

}
