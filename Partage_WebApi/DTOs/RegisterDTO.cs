
using System.ComponentModel.DataAnnotations;

namespace Partage.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        [Required]
        public string Pseudo { get; set; }

        [Required]
        [EmailAddress]
        public string Courriel { get; set; }

        [Required]
        public string MotDePasse { get; set; }

        [Required]
        public string MotDePasseConfirm { get; set; }
    }
}
