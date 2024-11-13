using System.Windows;

namespace gestionMedicale
{
    public partial class Consultation : Window
    {
        private bool isMenuCollapsed = false;

        public Consultation()
        {
            InitializeComponent();
            LoadPatients();
        }

        //private void AjouterConsultationPourPatient_Click(object sender, RoutedEventArgs e)
        //{
        //    if (sender is FrameworkElement element && element.DataContext is CPatient patient)
        //    {
        //        var ajouterConsultationWindow = new AjouterConsultation();
        //        if (ajouterConsultationWindow.ShowDialog() == true)
        //        {
        //            var nouvelleConsultation = new ConsultationDetails
        //            {
        //                Id = patient.ObtenirConsultations().Count() + 1,
        //                Date = ajouterConsultationWindow.Date,
        //                Motif = ajouterConsultationWindow.Motif,
        //                Observation = ajouterConsultationWindow.Observation,
        //                Diagnostic = ajouterConsultationWindow.Diagnostic
        //            };

        //            // Ajouter la consultation au patient
        //            patient.AjouterConsultation(nouvelleConsultation);

        //            MessageBox.Show($"Consultation ajoutée pour {patient.Nom}.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

        //            // Rafraîchir la liste pour afficher les modifications
        //            PatientsListView.Items.Refresh();
        //        }
        //    }
        //}


        private void LoadPatients()
        {
            // Charger tous les patients
            var patientsWithConsultations = CPatient.ObtenirTousLesPatients()
                .Where(patient => patient.ObtenirConsultations().Any())
                .ToList();

            PatientsListView.ItemsSource = patientsWithConsultations;
        }


        private void AjouterConsultationPourPatient_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is CPatient patient)
            {
                var ajouterConsultationWindow = new AjouterConsultation();
                if (ajouterConsultationWindow.ShowDialog() == true)
                {
                    var nouvelleConsultation = new ConsultationDetails
                    {
                        Id = patient.ObtenirConsultations().Count() + 1,
                        Date = ajouterConsultationWindow.Date,
                        Motif = ajouterConsultationWindow.Motif,
                        Observation = ajouterConsultationWindow.Observation,
                        Diagnostic = ajouterConsultationWindow.Diagnostic
                    };

                    // Ajouter la consultation au patient
                    patient.AjouterConsultation(nouvelleConsultation);

                    MessageBox.Show($"Consultation ajoutée pour {patient.Nom}.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Recharger la liste des patients ayant des consultations
                    LoadPatients(); // Remplace LoadPatientsWithConsultations
                }
            }
        }



        private void ConsulterHistorique_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is CPatient patient)
            {
                var consultations = patient.ObtenirConsultations();
                if (consultations.Any())
                {
                    string details = $"Historique des consultations pour {patient.Nom}:\n\n";

                    foreach (var consultation in consultations)
                    {
                        details += $"- ID: {consultation.Id}, Date: {consultation.Date}, Motif: {consultation.Motif}\n";
                    }

                    MessageBox.Show(details, "Historique", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Aucune consultation pour {patient.Nom}.", "Historique", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }


        // Basculer le menu latéral
        private void ToggleMenu_Click(object sender, RoutedEventArgs e)
        {
            if (isMenuCollapsed)
            {
                ConsultationMenuColumn.Width = new GridLength(200); // Étendre le menu
                isMenuCollapsed = false;
            }
            else
            {
                ConsultationMenuColumn.Width = new GridLength(50); // Réduire le menu
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
            // Rien à faire, déjà sur la page Consultation
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
