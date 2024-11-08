namespace Partage.DTOs
{
    public class DisponibiliteDTO
    {

        public int DisponibiliteId { get; set; }
        public int BorneId { get; set; }
        public DateTime DateDeLaSemaine { get; set; }
        public string HeureDebut { get; set; }
        public string HeureFin { get; set; }

    }
}
