using System.Windows;

namespace gestionMedicale
{
    public partial class ConsulterFichePatient : Window
    {
        public ConsulterFichePatient(CPatient patient)
        {
            InitializeComponent();
            LoadPatientDetails(patient);
        }

        private void LoadPatientDetails(CPatient patient)
        {
            NomTextBlock.Text = patient.Nom;
            DateNaissanceTextBlock.Text = patient.DateNaissance;
            AdresseTextBlock.Text = patient.Adresse;
            TelephoneTextBlock.Text = patient.Telephone;
            CourrielTextBlock.Text = patient.Courriel;
        }

        private void Fermer_Click(object sender, RoutedEventArgs e)
        {
            Close(); // Ferme la fenêtre actuelle
        }
    }
}
