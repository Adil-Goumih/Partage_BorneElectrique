using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Partage.Models
{
    public class Disponibilite
    {

        public int DisponibiliteId { get; set; }

        [Column(TypeName = "date")] 
        public DateTime DateDeLaSemaine { get; set; }
        public string HeureDebut { get; set; }
        public string HeureFin { get; set; }

        [ForeignKey("BorneId")]
        public int BorneId { get; set; }

        public virtual Borne Borne { get; set; }


    }
}
