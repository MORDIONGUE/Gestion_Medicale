using System.Windows;

namespace gestionMedicale
{
    public partial class Patient : Window
    {
        private bool isMenuCollapsed = false;

        public Patient()
        {
            InitializeComponent();
            LoadPatients();
        }

        private void LoadPatients()
        {
            // Charger les patients depuis le dictionnaire
            PatientListView.ItemsSource = CPatient.ObtenirTousLesPatients().ToList();
        }

        private void AjouterPatient_Click(object sender, RoutedEventArgs e)
        {
            var ajouterFicheWindow = new AjouterFichePatient();
            if (ajouterFicheWindow.ShowDialog() == true)
            {
                var nouveauPatient = ajouterFicheWindow.NouveauPatient;
                CPatient.AjouterPatient(nouveauPatient);

                // Rafraîchir la vue
                PatientListView.ItemsSource = null;
                PatientListView.ItemsSource = CPatient.ObtenirTousLesPatients().ToList();
            }
        }

        private void Consulter_Click(object sender, RoutedEventArgs e)
        {
            if (PatientListView.SelectedItem is CPatient selectedPatient)
            {
                var consulterWindow = new ConsulterFichePatient(selectedPatient);
                consulterWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un patient pour consulter sa fiche.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Modifier_Click(object sender, RoutedEventArgs e)
        {
            if (PatientListView.SelectedItem is CPatient selectedPatient)
            {
                var modifierWindow = new ModifierFichePatient(selectedPatient);
                if (modifierWindow.ShowDialog() == true)
                {
                    PatientListView.Items.Refresh();
                    MessageBox.Show($"Fiche de {selectedPatient.Nom} modifiée avec succès.", "Modification réussie", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un patient pour modifier sa fiche.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //private void LoadPatients()
        //{
        //var patients = new List<CPatient>
        //{
        //};

        //PatientListView.ItemsSource = patients;
        //}

        // Basculer le menu latéral
        private void ToggleMenu_Click(object sender, RoutedEventArgs e)
        {
            if (isMenuCollapsed)
            {
                PatientMenuColumn.Width = new GridLength(200); // Étendre le menu
                isMenuCollapsed = false;
            }
            else
            {
                PatientMenuColumn.Width = new GridLength(50); // Réduire le menu
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

        // Naviguer vers la section "Patient" (reste sur la même page)
        private void PatientMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Rien à faire, déjà sur la page Patient
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
