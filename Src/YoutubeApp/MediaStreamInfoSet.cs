using System;
using System.Collections.Generic;

namespace YTApp.Classes
{
    public class MediaStreamInfoSet
    {
        public AudioOption[] Audio;
        public VideoQuality[] Video;
        public VideoQuality[] Muxed;

        public List<VideoQuality> GetAllVideoQualities()
        {
            return default;
        }
    }
}