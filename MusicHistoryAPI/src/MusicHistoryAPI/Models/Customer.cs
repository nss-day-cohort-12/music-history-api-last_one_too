using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicHistoryAPI.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public IQueryable<object> FavoriteTracks { get; set; }
    }
}
