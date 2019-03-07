using System.Windows.Input;
using ARKit;
using RetroGamesGo.iOS.Delegates;
using RetroGamesGo.iOS.Pages;
using RetroGamesGo.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CapturePage), typeof(CapturePageRenderer))]
namespace RetroGamesGo.iOS.Renderers
{  
    /// <summary>
    /// Custom renderer for supporting ARKit on iOS
    /// </summary>
    public class CapturePageRenderer: PageRenderer, IARSCNViewDelegate
    {
        private ARSCNView sceneView;

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
                Delegate = new CaptureDelegate((s)=> (Element as CapturePage).ImageCapturedCommand?.Execute(s)),
                Session =
                {
                    Delegate = new ArSessionDelegate()
                },
            };
            this.View.AddSubview(this.sceneView);
        }


        /// <summary>
        /// Load the resources for the scene
        /// </summary>        
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            var configuration = new ARImageTrackingConfiguration();
            var referenceImages = ARReferenceImage.GetReferenceImagesInGroup("RetroGamesGoImages", null);
            configuration.TrackingImages = referenceImages;
            configuration.MaximumNumberOfTrackedImages = 100;
            this.sceneView.Session.Run(configuration);
        }

      
        /// <summary>
        /// Pause the AR scene
        /// </summary>        
        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            this.sceneView.Session.Pause();
        }
    }
}