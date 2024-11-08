using System.ComponentModel.DataAnnotations;

namespace Partage.DTOs
{
    public class LoginDTO
    {
        [Required]
       
        public string Courriel { get; set; } = null!;
        [Required]
        public string MotDePasse { get; set; } = null!;
    }
}
