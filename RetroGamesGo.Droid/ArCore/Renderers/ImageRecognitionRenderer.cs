using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Android.Content;
using Android.Graphics;
using Android.Opengl;
using Android.Support.Design.Widget;
using Google.AR.Core;
using MvvmCross;
using RetroGamesGo.Core.Repositories;
using RetroGamesGo.Droid.Activities;

namespace RetroGamesGo.Droid.ArCore.Renderers
{
    /// <summary>
    /// AR Core renderer for image recognition
    /// </summary>
    public class ImageRecognitionRenderer : BaseArRenderer
    {
        protected AugmentedImageDatabase imageDatabase;
        private List<Core.Models.Character> characters;
        private Action<string> imageCapturedAction;

        /// <summary>
        /// Creates the AR Renderer
        /// </summary>        
        public ImageRecognitionRenderer(Context context, GLSurfaceView surfaceView, Action<string> imageCapturedAction) : base(context, surfaceView)
        {
            this.imageCapturedAction = imageCapturedAction;
        }


        /// <summary>
        /// Loads the images for image recognition
        /// </summary>
        protected override async void LoadArAssets()
        {
            var characterRepository = Mvx.IoCProvider.Resolve<ICharacterRepository>();
            this.characters = await characterRepository.GetAll();

            var assets = MainActivity.Instance.Assets;
            this.imageDatabase = new AugmentedImageDatabase(this.session);
            foreach (var character in this.characters)
            {
                character.Captured = false;
                using (var streamReader = new StreamReader(assets.Open(character.AssetSticker)))
                {

                    var image = BitmapFactory.DecodeStream(streamReader.BaseStream);
                    imageDatabase.AddImage(character.Name, image, 0.15f);
                }
            }
        
            config.AugmentedImageDatabase = this.imageDatabase;
            session.Configure(config);
        }


        /// <summary>
        /// Checks if some augmented images were detected
        /// </summary>
        protected override void CheckDetectedImages(Google.AR.Core.Frame frame)
        {
            var updatedAugmentedImages = frame.GetUpdatedTrackables(Java.Lang.Class.FromType(typeof(AugmentedImage)));
            foreach (var image in updatedAugmentedImages)
            {
                var imageName = ((AugmentedImage) image).Name;
                imageCapturedAction?.Invoke(imageName);
                var character = this.characters.FirstOrDefault(x => x.Name == imageName);
                if (character!=null && !character.Captured)
                {
                    character.Captured = true;
                    var characterRepository = Mvx.IoCProvider.Resolve<ICharacterRepository>();
                    characterRepository.UpdateCharacter(character);

                    var view = MainActivity.Instance.FindViewById(Android.Resource.Id.Content);
                    var snackBar = Snackbar.Make(view, $"Capturaste a {((AugmentedImage) image).Name}", Snackbar.LengthIndefinite);
                    snackBar.SetDuration(5000);
                    snackBar.Show();                    
                }
            }
        }
    }
}