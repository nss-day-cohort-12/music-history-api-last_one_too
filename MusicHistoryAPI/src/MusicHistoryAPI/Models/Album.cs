using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicHistoryAPI.Models
{
    public class Album
    {
        [Key]
        public int AlbumId { get; set; }
        public int ArtistId { get; set; }
        public string AlbumTitle { get; set; }
        [DataType(DataType.Date)]
        public DateTime YearReleased { get; set; }
    }
}
