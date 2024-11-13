namespace gestionMedicale
{
    public class ConsultationDetails
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Motif { get; set; } = string.Empty;
        public string Observation { get; set; } = string.Empty;
        public string Diagnostic { get; set; } = string.Empty;
    }
}
