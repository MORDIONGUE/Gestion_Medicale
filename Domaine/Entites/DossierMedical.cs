using System.Collections.Generic;  // Pour List<>

namespace A_C.Domaine.Entites
{
    public class DossierMedical : IEntite
    {
        public int Id { get; set; }  // Implémentation de IEntite
        public string Nom { get; set; } = string.Empty;
        public string Antecedents { get; set; } = string.Empty;
        public List<string> Traitements { get; set; } = new List<string>();
        public int PatientId { get; set; }
        public Patient Patient { get; set; }  // Référence au patient
        public List<ConsultationDetails> Consultations => Patient?.Consultations ?? new List<ConsultationDetails>();
        public List<PrescriptionDetails> Prescriptions => Patient?.Prescriptions ?? new List<PrescriptionDetails>();
    }
} 