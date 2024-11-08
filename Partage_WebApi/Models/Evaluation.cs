using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Partage.Models
{
    public class Evaluation
    {
        public int EvaluationId { get; set; }
        public int Note { get; set; }
        public string Commentaire { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

        [ForeignKey("BorneId")]
        public int BorneId { get; set; }

        public virtual Borne Borne { get; set; }
    }

}
