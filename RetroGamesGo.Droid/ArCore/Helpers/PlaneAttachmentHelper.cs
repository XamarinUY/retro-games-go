namespace RetroGamesGo.Droid.ArCore.Helpers
{
    using Google.AR.Core;

    /// <summary>
    /// ¨Plane attachment, taken form the HelloAR demo
    /// </summary>
    public class PlaneAttachmentHelper
    {
        private Plane plane;
        private Anchor anchor;
        private float[] mPoseTranslation = new float[3];
        private float[] mPoseRotation = new float[4];


        /// <summary>
        /// Returns if the plane if being tracked
        /// </summary>
        public bool IsTracking => plane.TrackingState == TrackingState.Tracking
                                  && anchor.TrackingState == TrackingState.Tracking;


        public PlaneAttachmentHelper(Plane plane, Anchor anchor)
        {
            this.plane = plane;
            this.anchor = anchor;
        }


        public Pose GetPose()
        {
            var pose = anchor.Pose;
            pose.GetTranslation(mPoseTranslation, 0);
            pose.GetRotationQuaternion(mPoseRotation, 0);
            mPoseTranslation[1] = plane.CenterPose.Ty();
            return new Pose(mPoseTranslation, mPoseRotation);
        }


        public Anchor GetAnchor()
        {
            return anchor;
        }

    }
}