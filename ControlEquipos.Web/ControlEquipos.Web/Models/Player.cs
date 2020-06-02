using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ControlEquipos.Web.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public string PlayerLastName { get; set; }
        public string Nationality { get; set; }
        public string Position { get; set; }
        public string BornDate { get; set; }
        public int TeamID { get; set; }
        [ForeignKey("TeamID")]
        public Team Team { get; set; }
        public byte[] Imagen { get; set; }
        public string About { get; set; }

    }
}