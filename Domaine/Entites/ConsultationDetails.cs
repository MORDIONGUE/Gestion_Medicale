using System;

namespace A_C.Domaine.Entites
{
    public class ConsultationDetails : IEntite
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Motif { get; set; }
        public string Observation { get; set; }
        public string Diagnostic { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
} 