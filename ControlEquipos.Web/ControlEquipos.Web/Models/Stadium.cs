using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ControlEquipos.Web.Models
{
    public class Stadium
    {
        public int Id { get; set; }
        public string StadiumName { get; set; }
        public DateTime? InaugurationDate { get; set; }
        public int Capacity { get; set; }

        public int OwnerID { get; set; }
        [ForeignKey("OwnerID")]
        public  Owner Owner { get; set; }
        public byte[] Imagen { get; set; }
        public string About { get; set; }

    }
}