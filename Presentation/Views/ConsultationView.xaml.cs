using System.Windows;
using System.Linq;
using A_C.Application.Interfaces;
using A_C.Domaine.Entites;
using A_C.Infrastructure.Services;

namespace A_C.Presentation.Views
{
    public partial class ConsultationView : Window
    {
        private readonly Medecin _medecinConnecte;
        private readonly IServicePatient _servicePatient;
        private readonly IServiceConsultation _serviceConsultation;
        private bool isMenuCollapsed = false;

        public ConsultationView(Medecin medecin, IServicePatient servicePatient, IServiceConsultation serviceConsultation)
        {
            InitializeComponent();
            _medecinConnecte = medecin;
            _servicePatient = servicePatient;
            _serviceConsultation = serviceConsultation;
            InitializeView();
            LoadPatients();
        }

        private void InitializeView()
        {
            Title = $"Consultation - Dr. {_medecinConnecte.Nom}";
        }

        private async void LoadPatients()
        {
            var patients = await _servicePatient.ObtenirTousLesPatients();
            PatientsListView.ItemsSource = patients;
        }

        private async void AjouterConsultationPourPatient_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Patient patient)
            {
                var ajouterConsultationView = new AjouterConsultationView(
                    patient,
                    _serviceConsultation);
                
                if (ajouterConsultationView.ShowDialog() == true)
                {
                    var nouvelleConsultation = new ConsultationDetails
                    {
                        Date = ajouterConsultationView.Date,
                        Motif = ajouterConsultationView.Motif,
                        Observation = ajouterConsultationView.Observation,
                        Diagnostic = ajouterConsultationView.Diagnostic
                    };

                    await _serviceConsultation.AjouterConsultation(patient.Id, nouvelleConsultation);
                    MessageBox.Show($"Consultation ajoutée pour {patient.Nom}.", "Succès", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadPatients();
                }
            }
        }

        private async void ConsulterHistorique_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Patient patient)
            {
                var consultations = await _serviceConsultation.ObtenirConsultationsPatient(patient.Id);
                if (consultations.Any())
                {
                    // TODO: Créer HistoriqueConsultationsView
                    var historiqueView = new HistoriqueConsultationsView(patient, _serviceConsultation);
                    historiqueView.ShowDialog();
                }
                else
                {
                    MessageBox.Show($"Le patient {patient.Nom} n'a aucune consultation enregistrée.",
                        "Historique des consultations", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void ToggleMenu_Click(object sender, RoutedEventArgs e)
        {
            if (isMenuCollapsed)
            {
                ConsultationMenuColumn.Width = new GridLength(200);
                isMenuCollapsed = false;
            }
            else
            {
                ConsultationMenuColumn.Width = new GridLength(50);
                isMenuCollapsed = true;
            }
        }

        private void GoHome_Click(object sender, RoutedEventArgs e)
        {
            var patientView = new PatientView(_medecinConnecte, _servicePatient);
            patientView.Show();
            this.Close();
        }

        private void PatientMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var patientView = new PatientView(_medecinConnecte, _servicePatient);
            patientView.Show();
            this.Close();
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
            // Déjà sur la page consultation
        }

        private void PrescriptionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implémenter la navigation vers PrescriptionView
            MessageBox.Show("Navigation vers Prescription à implémenter");
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Aide : Contactez le support ou consultez la documentation.", 
                "Aide", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SettingsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Section Paramètres à implémenter", 
                "Paramètres", MessageBoxButton.OK, MessageBoxImage.Information);
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