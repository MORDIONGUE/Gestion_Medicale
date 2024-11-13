using System.Windows;

namespace gestionMedicale
{
    public partial class ConsulterDossierMedical : Window
    {
        public ConsulterDossierMedical(Dossier dossier)
        {
            InitializeComponent();

            // Charger les informations du dossier
            IdTextBlock.Text = dossier.Identifiant.ToString();
            NomTextBlock.Text = dossier.Nom;
            TraitementsListBox.ItemsSource = dossier.Traitements;
        }

        private void Fermer_Click(object sender, RoutedEventArgs e)
        {
            Close(); // Ferme la fenêtre
        }
    }
}
