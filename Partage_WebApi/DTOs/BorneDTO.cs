namespace Partage.DTOs
{
    public class BorneDTO
    {
        public int BorneId { get; set; }
        public string? TypeConnecteur { get; set; }
        public int? Puissance { get; set; }
        public int NumCivique { get; set; }
        public string NomRue { get; set; }
        public string Ville { get; set; }
        public string Province { get; set; }
        public string CodePostal { get; set; }
        public List<EvaluationDTO> Evaluations { get; set; }
        public int FavorisId { get; set; }


    }
}
