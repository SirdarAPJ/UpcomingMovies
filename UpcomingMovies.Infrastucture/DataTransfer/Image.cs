using System;
using System.Collections.Generic;
using System.Text;

namespace UpcomingMovies.Infrastucture.DataTransfer
{
    public class ImageResult
    {
        public Image images { get; set; }
        public string[] change_keys { get; set; }
    }

    public class Image
    {
        public string base_url { get; set; }
        public string secure_base_url { get; set; }
        public string[] backdrop_sizes { get; set; }
        public string[] logo_sizes { get; set; }
        public string[] poster_sizes { get; set; }
        public string[] profile_sizes { get; set; }
        public string[] still_sizes { get; set; }
    }
}
