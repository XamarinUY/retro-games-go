using System.Diagnostics;
using RetroGamesGo.Core.Game;
using Urho.Droid;
using Com.Google.AR.Core;
using Urho;

namespace RetroGamesGo.Droid.Game
{
    public class DroidArGame : ArGame
    {
        protected ARCoreComponent ArCore { get; private set; }

        public DroidArGame(ApplicationOptions options = null) : base(options)
        {
            UnhandledException += (s, e) =>
            {                
                e.Handled = true;
            };

        }

        protected override void InitializePlatform()
        {
            this.ArCore = scene.CreateComponent<ARCoreComponent>();
            ArCore.ARFrameUpdated += OnARFrameUpdated;

            ArCore.ConfigRequested += ArCore_ConfigRequested;
            ArCore.Run();
        }


        private void ArCore_ConfigRequested(Config config)
        {
            config.SetPlaneFindingMode(Config.PlaneFindingMode.Horizontal);
            config.SetLightEstimationMode(Config.LightEstimationMode.AmbientIntensity);
            config.SetUpdateMode(Config.UpdateMode.LatestCameraImage); //non blocking
        }


        /// <summary>
        ///  called by the update loop
        /// </summary>        
        void OnARFrameUpdated(Frame arFrame)
        {
            var currentFrame = arFrame;
            var anchors = arFrame.UpdatedAnchors;

            //TODO: visulize anchors (don't forget ARCore uses RHD coordinate system)
            // Adjust our ambient light based on the light estimates ARCore provides each frame
            var lightEstimate = arFrame.LightEstimate;
            //fps.AdditionalText = lightEstimate?.PixelIntensity.ToString("F1");
            //  zone.AmbientColor = new Color(1, 1, 1) * ((lightEstimate?.PixelIntensity ?? 0.2f) / 2f);
            //
        }

    }

}
