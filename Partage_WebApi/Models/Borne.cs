using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace Partage.Models
{
    public class Borne
    {
        [Key]
        public int BorneId { get; set; }
        public string TypeConnecteur { get; set; }
        public int Puissance { get; set; }
        
        [ForeignKey("AdresseId")]
        public int AdresseId { get; set; }
        [ValidateNever]
        public virtual Adresse Adresse { get; set; }

        [ForeignKey("UserId")]
        public string? UserId { get; set; }

        [ValidateNever]
        [JsonIgnore]
        public virtual User? User { get; set; }

        [ValidateNever]
        [JsonIgnore]
        public virtual List<Favoris>? ListFavoris { get; set; }

        [ValidateNever]
        [JsonIgnore]
        public virtual List<Evaluation>? Evaluations { get; set; }
        [ValidateNever]
        public virtual ICollection<Disponibilite>? Disponibilites { get; set; }


    }
}
