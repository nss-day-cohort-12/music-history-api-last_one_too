using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicHistoryAPI.Models
{
    public class TrackInfo
    {
        [Key]
        public string AlbumTitle {get; set;}
        public DateTime YearReleased {get; set;}
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Title { get; set; }
    }
}
