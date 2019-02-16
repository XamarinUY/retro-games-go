using System;
using Plugin.SimpleAudioPlayer;

namespace RetroGamesGo.Core.Utils
{
    public static class Sounds
    {
        public static void Mario_Coin()
        {
            CrossSimpleAudioPlayer.Current.Load("mario_coin.mp3");
            CrossSimpleAudioPlayer.Current.Play();
        }

        public static void Pacman_WakaWaka()
        {
            CrossSimpleAudioPlayer.Current.Load("pacman_waka_waka.mp3");
            CrossSimpleAudioPlayer.Current.Play();
        }

        public static void StreetFighter_YogaFire()
        {
            CrossSimpleAudioPlayer.Current.Load("street_yogafire.wav");
            CrossSimpleAudioPlayer.Current.Play();
        }
    }
}
