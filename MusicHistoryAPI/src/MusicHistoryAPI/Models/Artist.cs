using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicHistoryAPI.Models
{
    public class Artist
    {
        [Key]
        public int ArtistId { get; set; }
        public string Name { get; set; }
    }
}
