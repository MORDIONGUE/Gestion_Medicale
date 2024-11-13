namespace gestionMedicale
{
    public class CPatient
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string DateNaissance { get; set; } = string.Empty;
        public string Adresse { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string Courriel { get; set; } = string.Empty;

        public List<ConsultationDetails> Consultations { get; private set; } = new List<ConsultationDetails>();
        public List<PrescriptionDetails> Prescriptions { get; private set; } = new List<PrescriptionDetails>();

        // Dictionnaire global pour stocker les patients
        private static Dictionary<int, CPatient> patients = new Dictionary<int, CPatient>();

        public static void AjouterPatient(CPatient patient)
        {
            if (!patients.ContainsKey(patient.Id))
            {
                patients[patient.Id] = patient;
            }
        }

        public static CPatient? ObtenirPatient(int id)
        {
            return patients.ContainsKey(id) ? patients[id] : null;
        }

        public static IEnumerable<CPatient> ObtenirTousLesPatients()
        {
            return patients.Values;
        }

        public void AjouterConsultation(ConsultationDetails consultation)
        {
            Consultations.Add(consultation);
        }


        public IEnumerable<ConsultationDetails> ObtenirConsultations()
        {
            return Consultations;
        }


        public void AjouterPrescription(PrescriptionDetails prescription)
        {
            Prescriptions.Add(prescription);
        }

    }
}
