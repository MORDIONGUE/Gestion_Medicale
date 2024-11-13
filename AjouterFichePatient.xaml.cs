using System;
using System.Windows;

namespace gestionMedicale
{
    public partial class AjouterFichePatient : Window
    {
        public CPatient NouveauPatient { get; private set; }

        public AjouterFichePatient()
        {
            InitializeComponent();
        }

        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            // Validation des champs
            if (string.IsNullOrWhiteSpace(NomTextBox.Text) ||
                string.IsNullOrWhiteSpace(AdresseTextBox.Text) ||
                string.IsNullOrWhiteSpace(TelephoneTextBox.Text) ||
                string.IsNullOrWhiteSpace(CourrielTextBox.Text) ||
                !DateNaissancePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Création d'une nouvelle fiche patient
            NouveauPatient = new CPatient
            {
                Id = new Random().Next(1000, 9999), // Génération d'un ID unique
                Nom = NomTextBox.Text,
                DateNaissance = DateNaissancePicker.SelectedDate.Value.ToString("dd/MM/yyyy"),
                Adresse = AdresseTextBox.Text,
                Telephone = TelephoneTextBox.Text,
                Courriel = CourrielTextBox.Text
            };

            // Clôturer la fenêtre avec succès
            DialogResult = true;
            Close();
        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            // Annuler l'opération et fermer la fenêtre
            DialogResult = false;
            Close();
        }
    }
}
