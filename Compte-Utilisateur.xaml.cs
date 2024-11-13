using System.Windows;

namespace gestionMedicale
{
    public partial class Compte_Utilisateur : Window
    {
        private bool isMenuCollapsed = false;

        public Compte_Utilisateur()
        {
            InitializeComponent();
        }

        // Basculer le menu latéral
        private void ToggleMenu_Click(object sender, RoutedEventArgs e)
        {
            if (isMenuCollapsed)
            {
                CompteMenuColumn.Width = new GridLength(200); // Étendre le menu
                isMenuCollapsed = false;
            }
            else
            {
                CompteMenuColumn.Width = new GridLength(50); // Réduire le menu
                isMenuCollapsed = true;
            }
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

        // Naviguer vers la section "Dossier Médical"
        private void DossierMedicalMenuItem_Click(object sender, RoutedEventArgs e)
        {
            DossierMedical dossierWindow = new DossierMedical();
            dossierWindow.Show();
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

        // Afficher l'aide
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Aide : Contactez le support ou consultez la documentation.", "Aide", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Naviguer vers les paramètres
        private void SettingsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Section Paramètres sélectionnée.", "Paramètres", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Déconnexion
        private void DeconnexionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindow authWindow = new MainWindow();
            authWindow.Show();
            this.Close(); // Fermer la fenêtre actuelle
        }
    }
}
