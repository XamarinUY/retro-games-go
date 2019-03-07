using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ARKit;
using CoreGraphics;
using Foundation;
using MvvmCross;
using RetroGamesGo.Core.Repositories;
using SceneKit;
using UIKit;

namespace RetroGamesGo.iOS.Delegates
{
    /// <summary>
    /// Delegate for AR Rendering
    /// </summary>
    public class CaptureDelegate : ARSCNViewDelegate
    {
        private ICharacterRepository characterRepository = Mvx.IoCProvider.Resolve<ICharacterRepository>();

        public override async void DidAddNode(ISCNSceneRenderer renderer, SCNNode node, ARAnchor anchor)
        {
            if (anchor != null && anchor is ARImageAnchor)
            {
                var imageAnchor = (ARImageAnchor)anchor;
                var imageName = imageAnchor.ReferenceImage.Name;
                var characters = await characterRepository.GetAll();
                var character = characters.FirstOrDefault(x => x.AssetSticker.Contains(imageName));
                if (character != null && !character.Captured)
                {
                    character.Captured = true;
                    await characterRepository.UpdateCharacter(character);

                    InvokeOnMainThread(() => {
                        var okAlertController = UIAlertController.Create(string.Empty, $"Capturaste a {character.Name}", UIAlertControllerStyle.Alert);
                        okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                        UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(okAlertController, true, null);
                    });
                }
            }
        }

        public override void DidFail(ARSession session, NSError error)
        {
           // Handle the error
        }

    }
}