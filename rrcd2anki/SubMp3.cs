using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace rrcd2anki
{
    class SubMp3
    {
        public string URL;
        private string mp3Base;
        private string mp3InTemp;
        public SubMp3(string url)
        {
            URL = url;
            string tempPath = Path.GetTempPath();
            Uri uri = new Uri(url);
            mp3Base = uri.Segments[uri.Segments.Length - 1];
            mp3InTemp = Path.Combine(tempPath, mp3Base);
            if(!File.Exists(mp3InTemp))
            {
                var webClient = new WebClient();
                webClient.DownloadFile(url, mp3InTemp);
            }
        }
        public void Play()
        {
            var wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = mp3InTemp;
            wplayer.controls.play();
        }
        public void Stop()
        {

        }
        public void SaveTo()
        {

        }
    }
}
