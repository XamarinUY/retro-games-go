//using System;
//using MvvmCross;
//using RetroGamesGo.Core.Interfaces;

//namespace RetroGamesGo.Core.Game
//{
//    using Urho;

//    /// <summary>
//    /// AR Game class
//    /// </summary>
//    public class ArGame : Urho.Application
//    {
//        protected Zone zone;
//        //Node mutantNode;
//        //MonoDebugHud fps;
//        //bool gammaCorrected;
//        //bool scaling;
//        protected Scene scene;
//        protected Viewport viewport;

//        //Frame currentFrame;
//        //private IArPlatform arPlaform;

//        public ArGame(ApplicationOptions options = null) : base(options)
//        {
//        }

//        protected override void Start()
//        {
//            try
//            {

//                // 3d scene with octree and ambient light
//                this.scene = new Scene(Context);
//                var octree = scene.CreateComponent<Octree>();
//                zone = scene.CreateComponent<Zone>();
//                zone.AmbientColor = new Color(1, 1, 1) * 0.2f;

//                // Camera
//                var cameraNode = scene.CreateChild(name: "Camera");
//                var camera = cameraNode.CreateComponent<Urho.Camera>();

//                // Viewport
//                this.viewport = new Viewport(Context, scene, camera, null);
//                Renderer.SetViewport(0, this.viewport);

//                InitializePlatform();

//                //scene.CreateComponent(this.arPlaform.Component.Type);

//                //LoadModel();

//                //fps = new MonoDebugHud(this);
//                //fps.Show(Color.Blue, 20);

//                // Add some post-processing (also, see CorrectGamma())
//                viewport.RenderPath.Append(CoreAssets.PostProcess.FXAA2);



//                //Input.TouchBegin += OnTouchBegin;
//                //Input.TouchEnd += OnTouchEnd;

//            }
//            catch (Exception ex)
//            {
//                var e = ex.Message;
//            }
//        }

//        protected virtual void InitializePlatform()
//        {
//        }

//        private void LoadModel()
//        {
//            //mutantNode = this.scene.CreateChild();
//            //mutantNode.Position = new Vector3(0, -0.5f, 0.5f); // 50cm Y, 50cm Z
//            //mutantNode.SetScale(0.3f);

//            //var model = mutantNode.CreateComponent<AnimatedModel>();
//            //model.CastShadows = true;
//            //model.Model = ResourceCache.GetModel("Models/Mutant.mdl");
//            //model.Material = ResourceCache.GetMaterial("Materials/mutant_M.xml");
//            //var ani = mutantNode.CreateComponent<AnimationController>();
//            //ani.Play("Animations/Mutant_HipHop1.ani", 0, true, 1f);
//        }
//    }
//}
