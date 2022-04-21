using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel
{
    public class Gender
    {
        [Key]
        public int IdGender { get; set; }
        public string GenderName { get; set; }
        public virtual ICollection<ClientList> ClientLists { get; set; }

    }
}
