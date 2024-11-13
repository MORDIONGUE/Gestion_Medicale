using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace gestionMedicale
{
    public partial class DossierMedical : Window
    {
        private List<Dossier> dossiers;

        public DossierMedical()
        {
            InitializeComponent();
            dossiers = new List<Dossier>();
            LoadDossiers();
        }

        private void LoadDossiers()
        {
            dossiers = new List<Dossier>{};

            DossierListView.ItemsSource = dossiers;
        }

        private void ToggleMenu_Click(object sender, RoutedEventArgs e)
        {
            DossierMenuColumn.Width = DossierMenuColumn.Width.Value == 200 ? new GridLength(50) : new GridLength(200);
        }

        // Naviguer vers l'accueil
        private void GoHome_Click(object sender, RoutedEventArgs e)
        {
            Compte_Utilisateur homeWindow = new Compte_Utilisateur();
            homeWindow.Show();
            this.Close(); // Fermer la fenêtre actuelle
        }

        // Naviguer vers la section "Patient"
        private void PatientMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Patient patientWindow = new Patient();
            patientWindow.Show();
            this.Close(); // Fermer la fenêtre actuelle
        }

        // Naviguer vers la section "Consultation"
        private void ConsultationMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Consultation consultationWindow = new Consultation();
            consultationWindow.Show();
            this.Close(); // Fermer la fenêtre actuelle
        }

        // Naviguer vers la section "Prescription"
        private void PrescriptionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Prescription prescriptionWindow = new Prescription();
            prescriptionWindow.Show();
            this.Close(); // Fermer la fenêtre actuelle
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Besoin d'aide ? Contactez le support.", "Aide", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SettingsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ouverture des paramètres.", "Paramètres", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Déconnexion
        private void DeconnexionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindow authWindow = new MainWindow();
            authWindow.Show();
            this.Close(); // Fermer la fenêtre actuelle
        }

        private void AjouterDossier_Click(object sender, RoutedEventArgs e)
        {
            var ajouterDossierWindow = new AjouterDossierMedical();
            if (ajouterDossierWindow.ShowDialog() == true)
            {
                dossiers.Add(ajouterDossierWindow.NouveauDossier);
                DossierListView.Items.Refresh();
            }
        }

        private void DossierMedicalMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Vous êtes déjà sur la page Dossier Médical.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ConsulterDossier_Click(object sender, RoutedEventArgs e)
        {
            // Vérifie si un dossier est sélectionné
            if (DossierListView.SelectedItem is Dossier selectedDossier)
            {
                // Ouvre une fenêtre de consultation avec les détails du dossier
                var consulterWindow = new ConsulterDossierMedical(selectedDossier);
                consulterWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un dossier à consulter.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private void ModifierDossier_Click(object sender, RoutedEventArgs e)
        {
            if (DossierListView.SelectedItem is Dossier selectedDossier)
            {
                var modifierWindow = new ModifierDossierMedical(selectedDossier);
                if (modifierWindow.ShowDialog() == true)
                {
                    DossierListView.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un dossier à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


    }

}
