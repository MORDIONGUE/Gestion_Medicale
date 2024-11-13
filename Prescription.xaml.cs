using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace gestionMedicale
{
    public partial class Prescription : Window
    {
        private bool isMenuCollapsed = false; // État du menu latéral

        public Prescription()
        {
            InitializeComponent();
            LoadPatientsWithConsultations(); // Charger uniquement les patients ayant des consultations
        }


        private void LoadPatientsWithConsultations()
        {
            // Charger les patients ayant au moins une consultation
            var patientsWithConsultations = CPatient.ObtenirTousLesPatients()
                .Where(patient => patient.Consultations.Any()) // Filtre basé sur les consultations
                .ToList();

            // Associer les données filtrées à l'élément PatientsListView
            PatientsListView.ItemsSource = patientsWithConsultations;
        }

        private void AjouterPrescription_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is CPatient patient)
            {
                var nouvellePrescription = new PrescriptionDetails
                {
                    Id = patient.Prescriptions.Count + 1, // Générer un nouvel ID
                    Date = DateTime.Now,
                    Medicament = "Paracétamol",
                    Dosage = "500mg",
                    Instructions = "2 fois par jour",
                    Etat = "En attente"
                };

                patient.AjouterPrescription(nouvellePrescription);

                MessageBox.Show($"Prescription ajoutée pour {patient.Nom}.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

                // Rafraîchir la liste après ajout
                PatientsListView.Items.Refresh();
            }
        }

        private void ConsulterPatient_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is CPatient patient)
            {
                string details = $"Consultations de {patient.Nom}:\n\n";
                foreach (var consultation in patient.ObtenirConsultations())
                {
                    details += $"- ID: {consultation.Id}, Date: {consultation.Date}, Motif: {consultation.Motif}\n";
                }

                MessageBox.Show(details, "Consultations", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ChangerEtat_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is TextBlock textBlock && textBlock.DataContext is PrescriptionDetails prescription)
            {
                if (prescription.Etat == "En attente")
                {
                    prescription.Etat = "Validée";
                    textBlock.Foreground = Brushes.Green;
                }
                else
                {
                    prescription.Etat = "En attente";
                    textBlock.Foreground = Brushes.Red;
                }

                // Rafraîchir l'interface
                PatientsListView.Items.Refresh();
            }
        }


        // Basculer le menu latéral
        private void ToggleMenu_Click(object sender, RoutedEventArgs e)
        {
            if (isMenuCollapsed)
            {
                PrescriptionMenuColumn.Width = new GridLength(200); // Étendre le menu
                isMenuCollapsed = false;
            }
            else
            {
                PrescriptionMenuColumn.Width = new GridLength(50); // Réduire le menu
                isMenuCollapsed = true;
            }
        }

        // Naviguer vers l'accueil
        private void GoHome_Click(object sender, RoutedEventArgs e)
        {
            Compte_Utilisateur homeWindow = new Compte_Utilisateur();
            homeWindow.Show();
            this.Close();
        }

        // Naviguer vers la section "Patient"
        private void PatientMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Patient patientWindow = new Patient();
            patientWindow.Show();
            this.Close();
        }

        // Naviguer vers la section "Dossier Médical"
        private void DossierMedicalMenuItem_Click(object sender, RoutedEventArgs e)
        {
            DossierMedical dossierWindow = new DossierMedical();
            dossierWindow.Show();
            this.Close();
        }

        // Naviguer vers la section "Consultation"
        private void ConsultationMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Consultation consultationWindow = new Consultation();
            consultationWindow.Show();
            this.Close();
        }

        // Naviguer vers la section "Prescription"
        private void PrescriptionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Rien à faire, déjà sur la page Prescription
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
            this.Close();
        }
    }
}
