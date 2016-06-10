using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicHistoryAPI.Models
{
    public class Album
    {
        public int AlbumId { get; set; }
        public int ArtistId { get; set; }
        public string Title { get; set; }
        public DateTime YearReleased { get; set; }
    }
}
