using System;
using System.Collections.Generic;
using System.Windows;

namespace gestionMedicale
{
    public partial class AjouterDossierMedical : Window
    {
        public Dossier NouveauDossier { get; private set; }

        public AjouterDossierMedical()
        {
            InitializeComponent();

            // Générer un identifiant unique
            var random = new Random();
            int id = random.Next(1000, 9999);
            IdTextBlock.Text = id.ToString();

            NouveauDossier = new Dossier
            {
                Identifiant = id,
                Traitements = new List<string>()
            };
        }

        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            // Vérifier que les champs sont remplis
            if (!string.IsNullOrWhiteSpace(NomTextBox.Text) && !string.IsNullOrWhiteSpace(TraitementTextBox.Text))
            {
                NouveauDossier.Nom = NomTextBox.Text;
                NouveauDossier.Traitements.Add(TraitementTextBox.Text);

                DialogResult = true; // Confirmer l'ajout
                Close();
            }
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Annuler l'opération
            Close();
        }
    }
}
