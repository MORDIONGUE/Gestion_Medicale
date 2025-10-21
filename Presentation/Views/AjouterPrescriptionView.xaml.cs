using System.Windows;
using A_C.Domaine.Entites;

namespace A_C.Presentation.Views
{
    public partial class AjouterPrescriptionView : Window
    {
        private readonly Patient _patient;
        public PrescriptionDetails NouvellePrescription { get; private set; }

        public AjouterPrescriptionView(Patient patient)
        {
            InitializeComponent();
            _patient = patient;
            Title = $"Nouvelle Prescription - {_patient.Nom}";
        }

        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MedicamentTextBox.Text))
            {
                MessageBox.Show("Le médicament est obligatoire.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NouvellePrescription = new PrescriptionDetails
            {
                Medicament = MedicamentTextBox.Text,
                Dosage = DosageTextBox.Text,
                Instructions = InstructionsTextBox.Text,
                Duree = DureeTextBox.Text,
                Etat = "En attente"
            };

            DialogResult = true;
            Close();
        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
} 