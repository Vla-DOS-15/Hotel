using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel
{
    public class RoomClass
    {
        [Key]
        public int IdClass { get; set; }
        public string Class { get; set; }
        public virtual ICollection<RoomNumber> RoomNumbers { get; set; }
    }
}
