using System;
using System.Windows;

namespace gestionMedicale
{
    public partial class ModifierFichePatient : Window
    {
        public CPatient Patient { get; private set; } // Référence au patient à modifier

        public ModifierFichePatient(CPatient patient)
        {
            InitializeComponent();
            Patient = patient;
            LoadPatientDetails();
        }

        private void LoadPatientDetails()
        {
            // Charger les informations du patient dans les champs
            NomTextBox.Text = Patient.Nom;
            AdresseTextBox.Text = Patient.Adresse;
            TelephoneTextBox.Text = Patient.Telephone;
            CourrielTextBox.Text = Patient.Courriel;

        }

        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            // Mettre à jour les données du patient
            Patient.Nom = NomTextBox.Text;
            Patient.Adresse = AdresseTextBox.Text;
            Patient.Telephone = TelephoneTextBox.Text;
            Patient.Courriel = CourrielTextBox.Text;

            if (DateNaissancePicker.SelectedDate.HasValue)
            {
                Patient.DateNaissance = DateNaissancePicker.SelectedDate.Value.ToString("dd/MM/yyyy");
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une date de naissance valide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DialogResult = true; // Fermer la fenêtre et indiquer que la modification est réussie
            Close();
        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Annuler la modification
            Close();
        }
    }
}
