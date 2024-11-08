using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Partage.Models
{
    public class User : IdentityUser
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }

        [ValidateNever]
        public virtual List<Borne> Bornes { get; set; } = null!;
        [ValidateNever]
        public virtual List<Favoris> ListFavoris { get; set; }
        [ValidateNever]
        public virtual List<Evaluation> Evaluations { get; set; }

        internal static object FindFirstValue(string nameIdentifier)
        {
            throw new NotImplementedException();
        }
    }
}
