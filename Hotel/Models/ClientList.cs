using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel
{
    public class ClientList
    {
        [Key]
        public int IdClient { get; set; }

        public string FullNameClient { get; set; }
        public DateTime? DateOfBirth { get; set; }        
        public int? PassportID { get; set; }
        public int GenderId { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("GenderId")]
        public Gender Gender { get; set; }       
        public virtual ICollection<Reservation> Reservations { get; set; }


    }
}
