using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel
{
    public class Reservation
    {
        [Key]
        public int IdReservation { get; set; }
        public int? ClientListId { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ClientListId")]
        public ClientList ClientList { get; set; }
        public int? RoomNumberId { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("RoomNumberId")]
        public RoomNumber RoomNumber { get; set; }
        public DateTime? DateReservation { get; set; }
        public DateTime? DateOfEntry { get; set; }
        public DateTime? DateDeparture { get; set; }

    }
}
