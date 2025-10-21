using System;  // Pour DateTime

namespace A_C.Domaine.Entites
{
    public class PrescriptionDetails : IEntite
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Medicament { get; set; } = string.Empty;
        public string Dosage { get; set; } = string.Empty;
        public string Instructions { get; set; } = string.Empty;
        public string Duree { get; set; } = string.Empty;
        public string Etat { get; set; } = "En attente";

        // Ajout des propriétés manquantes
        public int MedecinId { get; set; }
        public Medecin Medecin { get; set; }

        // Relations existantes
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
} 