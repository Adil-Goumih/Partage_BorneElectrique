using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Partage.Models
{
    public class Favoris
    {
        [Key]
        public int FavorisId { get; set; }
        public bool IsFavoris { get; set; } = true;


        [ForeignKey("UserId")]
        public string UserId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

        [ForeignKey("BorneId")]
        public int BorneId { get; set; }

        public virtual Borne Borne { get; set; }

       

    }
}