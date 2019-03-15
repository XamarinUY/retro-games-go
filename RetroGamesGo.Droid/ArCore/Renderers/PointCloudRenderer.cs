namespace RetroGamesGo.Droid.ArCore.Renderers
{
    using Android.Content;
    using Android.Opengl;
    using Helpers;
    using Google.AR.Core;

    /// <summary>
    /// PointCloud renderer, taken from the HelloAR demo
    /// </summary>
    public class PointCloudRenderer
    {
        private const string TAG = "POINTCLOUDRENDERER";
        private const int BYTES_PER_FLOAT = Java.Lang.Float.Size / 8;
        private const int FLOATS_PER_POINT = 4;  // X,Y,Z,confidence.
        private const int BYTES_PER_POINT = BYTES_PER_FLOAT * FLOATS_PER_POINT;
        private const int INITIAL_BUFFER_POINTS = 1000;
        private int mVbo;
        private int mVboSize;
        private int mProgramName;
        private int mPositionAttribute;
        private int mModelViewProjectionUniform;
        private int mColorUniform;
        private int mPointSizeUniform;
        private int mNumPoints = 0;
        private PointCloud mLastPointCloud = null;



        /// <summary>
        /// Allocates and initializes OpenGL resources needed by the plane renderer.  Must be
        /// called on the OpenGL thread, typically in
        /// {@link GLSurfaceView.Renderer#onSurfaceCreated(GL10, EGLConfig)}.
        /// </summary>
        public void CreateOnGlThread(Context context)
        {
            //ShaderHelper.CheckGLError(TAG, "before create");

            var buffers = new int[1];
            GLES20.GlGenBuffers(1, buffers, 0);
            mVbo = buffers[0];
            GLES20.GlBindBuffer(GLES20.GlArrayBuffer, mVbo);

            mVboSize = INITIAL_BUFFER_POINTS * BYTES_PER_POINT;
            GLES20.GlBufferData(GLES20.GlArrayBuffer, mVboSize, null, GLES20.GlDynamicDraw);
            GLES20.GlBindBuffer(GLES20.GlArrayBuffer, 0);

            var vertexShader = ShaderHelper.Load(TAG, context,
                GLES20.GlVertexShader, Resource.Raw.point_cloud_vertex);
            var passthroughShader = ShaderHelper.Load(TAG, context,
                GLES20.GlFragmentShader, Resource.Raw.passthrough_fragment);

            mProgramName = GLES20.GlCreateProgram();
            GLES20.GlAttachShader(mProgramName, vertexShader);
            GLES20.GlAttachShader(mProgramName, passthroughShader);
            GLES20.GlLinkProgram(mProgramName);
            GLES20.GlUseProgram(mProgramName);

   
            mPositionAttribute = GLES20.GlGetAttribLocation(mProgramName, "a_Position");
            mColorUniform = GLES20.GlGetUniformLocation(mProgramName, "u_Color");
            mModelViewProjectionUniform = GLES20.GlGetUniformLocation(
                mProgramName, "u_ModelViewProjection");
            mPointSizeUniform = GLES20.GlGetUniformLocation(mProgramName, "u_PointSize");
           
        }


        /// <summary>
        ///  Updates the OpenGL buffer contents to the provided point.  Repeated calls with the same
        ///  point cloud will be ignored.
        /// </summary>
        public void Update(PointCloud cloud)
        {
            if (mLastPointCloud == cloud)
            {
                return;
            }
         
            GLES20.GlBindBuffer(GLES20.GlArrayBuffer, mVbo);
            mLastPointCloud = cloud;

            // If the VBO is not large enough to fit the new point cloud, resize it.
            mNumPoints = mLastPointCloud.Points.Remaining() / FLOATS_PER_POINT;
            if (mNumPoints * BYTES_PER_POINT > mVboSize)
            {
                while (mNumPoints * BYTES_PER_POINT > mVboSize)
                {
                    mVboSize *= 2;
                }
                GLES20.GlBufferData(GLES20.GlArrayBuffer, mVboSize, null, GLES20.GlDynamicDraw);
            }
            GLES20.GlBufferSubData(GLES20.GlArrayBuffer, 0, mNumPoints * BYTES_PER_POINT,
                mLastPointCloud.Points);
            GLES20.GlBindBuffer(GLES20.GlArrayBuffer, 0);
            
        }


        /// <summary>
        /// Renders the point cloud.        
        /// @param pose the current point cloud pose, from {@link Frame#getPointCloudPose()}.
        /// @param cameraView the camera view matrix for this frame, typically from
        ///     {@link Frame#getViewMatrix(float[], int)}.
        /// @param cameraPerspective the camera projection matrix for this frame, typically from
        ///     {@link Session#getProjectionMatrix(float[], int, float, float)}.        
        /// </summary>
        public void Draw(Pose pose, float[] cameraView, float[] cameraPerspective)
        {
            var modelMatrix = new float[16];
            pose.ToMatrix(modelMatrix, 0);

            var modelView = new float[16];
            var modelViewProjection = new float[16];
            Matrix.MultiplyMM(modelView, 0, cameraView, 0, modelMatrix, 0);
            Matrix.MultiplyMM(modelViewProjection, 0, cameraPerspective, 0, modelView, 0);           

            GLES20.GlUseProgram(mProgramName);
            GLES20.GlEnableVertexAttribArray(mPositionAttribute);
            GLES20.GlBindBuffer(GLES20.GlArrayBuffer, mVbo);
            GLES20.GlVertexAttribPointer(
                mPositionAttribute, 4, GLES20.GlFloat, false, BYTES_PER_POINT, 0);
            GLES20.GlUniform4f(mColorUniform, 31.0f / 255.0f, 188.0f / 255.0f, 210.0f / 255.0f, 1.0f);
            GLES20.GlUniformMatrix4fv(mModelViewProjectionUniform, 1, false, modelViewProjection, 0);
            GLES20.GlUniform1f(mPointSizeUniform, 5.0f);

            GLES20.GlDrawArrays(GLES20.GlPoints, 0, mNumPoints);
            GLES20.GlDisableVertexAttribArray(mPositionAttribute);
            GLES20.GlBindBuffer(GLES20.GlArrayBuffer, 0);         
        }
    }
}