using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Partage.Models
{
    public class Adresse
    {
        [Key]
        public int AdresseId { get; set; }
        public string NomRue { get; set; }
        public string CodePostal { get; set; }
        public int NumCivique { get; set;}
        public string Ville { get; set; }
        public string Province { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
}
