using LibVLCSharp.Shared;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace RadioPlayer
{
    public static class StreamManager
    {
        static LibVLC LibVLC;
        static MediaPlayer MediaPlayer;
        static MainWindow MainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        static int volume = 50;
        public static int Volume { get { return volume; } set { volume = value; UpdateVolume(); } }
        static string CurrentStation;
        static string LastSong;
        public static ObservableCollection<string[]> StreamHistory = new();

        static StreamManager()
        {
            Core.Initialize();
            LibVLC = new LibVLC(enableDebugLogs: true);
            MediaPlayer = new MediaPlayer(LibVLC);
            MediaPlayer.Stopped += MediaPlayer_Stopped;
            MediaPlayer.Playing += MediaPlayer_Playing;
            // TODO: Add events and bind them to StatusBar [Playing, etc]
        }

        private static void MediaPlayer_Playing(object sender, EventArgs e)
        {
            UpdateVolume();
        }

        private static void MediaPlayer_Stopped(object sender, EventArgs e)
        {
            MediaPlayer.Media.ParseStop();
            CurrentStation = null;
        }

        static void UpdateVolume()
        {
            if (MediaPlayer is null) return;

            MediaPlayer.Volume = Volume;
            //Debug.WriteLine("");
        }

        public static void StreamURL(string url, string displayName)
        {
            try
            {
                CurrentStation = displayName;
                var media = new Media(LibVLC, url, FromType.FromLocation);
                media.MetaChanged += Media_MetaChanged;
                media.Parse(MediaParseOptions.ParseNetwork, -1);
                PlayMedia(media);

                MainWindow.Dispatcher.Invoke(() => {
                    MainWindow.LeftText = $"Streaming: {displayName}";
                });
            } catch (Exception e) {
                MessageBox.Show($"An error occurred while trying to start a stream!\n\nError Message:\n{e.Message}");
            }
        }

        private static void Media_MetaChanged(object sender, MediaMetaChangedEventArgs e)
        {
            if (e.MetadataType == MetadataType.NowPlaying && !String.IsNullOrWhiteSpace(MediaPlayer.Media.Meta(MetadataType.NowPlaying)))
            {
                var newSong = MediaPlayer.Media.Meta(MetadataType.NowPlaying);

                if (LastSong != newSong)
                {
                    LastSong = newSong;

                    MainWindow.Dispatcher.Invoke(() => {
                        MainWindow.CenterText = LastSong;
                        StreamHistory.Insert(0, new string[] { CurrentStation ?? "N/A", LastSong ?? "N/A", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") });
                        MainWindow.AddHistoryEntry();
                    });
                }
            }
        }

        public static void PlayMedia(Media media)
        {
            if (MediaPlayer.IsPlaying)
                Stop();

            bool success = MediaPlayer.Play(media);

            if (success)
            {
                // TODO: Update StatusBar, Title? - Start parsing data here
            }
        }

        public static void PlayPause()
        {
            if (MediaPlayer.CanPause && MediaPlayer.IsPlaying)
                MediaPlayer.Pause();

            if (MediaPlayer.WillPlay && !MediaPlayer.IsPlaying)
                MediaPlayer.Play();
        }

        public static void Stop()
        {
            MediaPlayer.Stop();
        }

        public static void SetVolume(int newLevel)
        {
            // TODO: Testing, levels above 100? Imagine possible since VLC
            MediaPlayer.Volume = Math.Clamp(newLevel, 0, 100);
        }
    }
}
