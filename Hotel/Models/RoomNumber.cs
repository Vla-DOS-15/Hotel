using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel
{
    public class RoomNumber
    {
        [Key]
        public int IdRoom { get; set; }
        public int? CodeNumber { get; set; }
        public int RoomClassId { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("RoomClassId")]
        public RoomClass RoomClass { get; set; }
        public int? Price { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }

    }
}
