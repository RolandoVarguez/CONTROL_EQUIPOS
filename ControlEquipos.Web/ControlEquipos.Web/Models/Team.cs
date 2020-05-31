using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ControlEquipos.Web.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public DateTime? FoundationDate { get; set; }
        public int OwnerID { get; set; }
        [ForeignKey("OwnerID")]
        public Owner Owner { get; set; }

        public string Location { get; set; }
        public int Championships { get; set; }
        public byte[] Imagen { get; set; }

    }
}