using Plugin.Maui.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGame
{
    public static class Audio
    {
        private static bool isInitialized = false;

        private static AudioPlayer player;
        private static AudioPlayer timer;

        private static double volume = 1.0;

        private static bool mute = false;

        public static async Task StartAudio()
        {
            if (isInitialized)
            {
                return;
            }

            player = (AudioPlayer)AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("soundgame.mp3"));
            player.Loop = true;

            timer = (AudioPlayer)AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("timer.mp3"));
            timer.Loop = false;

            isInitialized = true;
        }

        public static void Play()
        {
            if (!Muted && !player.IsPlaying)
            {
                player.Play();
            }
        }

        public static void Stop()
        {
            if (player.IsPlaying)
            {
                player.Stop();
            }
        }
        public static bool Muted
        {
            get
            {
                return mute;
            }
            set
            {
                mute = value;
                if (mute)
                {
                    Stop();
                }
                else
                {
                    if (volume > 0)
                    {
                        Play();
                    }
                }
            }
        }

        public static void timerSound()
        {
            if(Muted || volume <= 0)
            {
                return;
            }
            if (timer.IsPlaying)
            {
                timer.Stop();
            }
            timer.Play();
        }

        public static double Volume
        {
            get
            {
                return volume;
            }
            set
            {
                volume = value;
                if(player !=  null)
                {
                    player.Volume = volume;
                }
                if(volume <= 0.0)
                {
                    Stop();
                }
                else
                {
                    if (!Muted && !player.IsPlaying)
                    {
                        player.Play();
                    }
                }
            }
        }
    }
}
