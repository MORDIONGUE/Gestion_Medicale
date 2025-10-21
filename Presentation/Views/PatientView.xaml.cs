using System.Windows;
using System.Windows.Controls;
using A_C.Application.Interfaces;
using A_C.Domaine.Entites;
using A_C.Infrastructure.Services;
using System.Collections.ObjectModel;
using System;

namespace A_C.Presentation.Views
{
    public partial class PatientView : Window
    {
        private readonly Medecin _medecinConnecte;
        private readonly IServicePatient _servicePatient;
        private bool isMenuCollapsed = false;
        public ObservableCollection<Patient> Patients { get; private set; }

        public PatientView(Medecin medecin, IServicePatient servicePatient)
        {
            InitializeComponent();
            _medecinConnecte = medecin;
            _servicePatient = servicePatient ?? throw new ArgumentNullException(nameof(servicePatient));
            Patients = new ObservableCollection<Patient>();
            DataContext = this;
            InitializeView();
            ChargerPatients();
        }

        private async void ChargerPatients()
        {
            try
            {
                var patients = await _servicePatient.ObtenirTousLesPatients();
                Patients.Clear();
                foreach (var patient in patients)
                {
                    Patients.Add(patient);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des patients : {ex.Message}",
                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void AjouterPatient_Click(object sender, RoutedEventArgs e)
        {
            var ajouterPatientView = new AjouterPatientView(_servicePatient);
            if (ajouterPatientView.ShowDialog() == true)
            {
                ChargerPatients();
            }
        }

        private async void Consulter_Click(object sender, RoutedEventArgs e)
        {
            if (PatientListView.SelectedItem is Patient selectedPatient)
            {
                // TODO: Créer ConsulterPatientView
                MessageBox.Show($"Consultation du patient : {selectedPatient.Nom}");
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un patient.", "Attention", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ModifierPatient_Click(object sender, RoutedEventArgs e)
        {
            var patient = ((FrameworkElement)sender).DataContext as Patient;
            if (patient != null)
            {
                var modifierPatientView = new ModifierPatientView(patient, _servicePatient);
                if (modifierPatientView.ShowDialog() == true)
                {
                    ChargerPatients();
                }
            }
        }

        private async void SupprimerPatient_Click(object sender, RoutedEventArgs e)
        {
            var patient = ((FrameworkElement)sender).DataContext as Patient;
            if (patient != null)
            {
                var result = MessageBox.Show($"Voulez-vous vraiment supprimer le patient {patient.Nom} ?",
                    "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        await _servicePatient.SupprimerPatient(patient.Id);
                        ChargerPatients();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erreur lors de la suppression : {ex.Message}",
                            "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void InitializeView()
        {
            // Initialiser les données de la vue
            Title = $"Patient - Dr. {_medecinConnecte.Nom}";
        }

        private void ToggleMenu_Click(object sender, RoutedEventArgs e)
        {
            // Logique pour le menu
            PatientMenuColumn.Width = PatientMenuColumn.Width == 200 ? 50 : 200;
        }

        private void GoHome_Click(object sender, RoutedEventArgs e)
        {
            // Navigation vers l'accueil
        }

        private void PatientMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Déjà sur la page patient
        }

        private void DossierMedicalMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dossierMedicalView = new DossierMedicalView(
                _medecinConnecte, 
                new ServiceDossierMedical(_servicePatient), 
                _servicePatient);
            dossierMedicalView.Show();
            this.Close();
        }

        private void ConsultationMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Navigation vers consultation
        }

        private void PrescriptionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Navigation vers prescription
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Aide non disponible pour le moment.", "Aide", 
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SettingsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Navigation vers paramètres
        }

        private void DeconnexionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Voulez-vous vraiment vous déconnecter ?", "Déconnexion",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                new MainWindow().Show();
                this.Close();
            }
        }
    }
} 