using System;
using System.Collections.Generic;  // Pour List<>

namespace A_C.Domaine.Entites
{
    public class Patient : IEntite 
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string DateNaissance { get; set; }
        public string Adresse { get; set; }
        public string Telephone { get; set; }
        public string Courriel { get; set; }
        public List<ConsultationDetails> Consultations { get; private set; } = new List<ConsultationDetails>();
        public List<PrescriptionDetails> Prescriptions { get; private set; } = new List<PrescriptionDetails>();

        public void AjouterConsultation(ConsultationDetails consultation)
        {
            if (consultation == null)
                throw new ArgumentNullException(nameof(consultation));
            Consultations.Add(consultation);
        }

        public void AjouterPrescription(PrescriptionDetails prescription)
        {
            if (prescription == null)
                throw new ArgumentNullException(nameof(prescription));
            Prescriptions.Add(prescription);
        }
    }
} 