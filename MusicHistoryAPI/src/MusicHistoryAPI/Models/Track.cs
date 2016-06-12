using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicHistoryAPI.Models
{
    public class Track
    {
        [Key]
        public int TrackId { get; set; }
        public int AlbumId { get; set; }
        public string Genre { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
    }
}
