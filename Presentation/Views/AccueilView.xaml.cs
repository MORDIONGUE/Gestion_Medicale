using System.Windows;
using A_C.Application.Interfaces;
using A_C.Domaine.Entites;

namespace A_C.Presentation.Views
{
    public partial class AccueilView : Window
    {
        private readonly Medecin _medecinConnecte;
        private readonly IServicePatient _servicePatient;
        private readonly IServiceConsultation _serviceConsultation;
        private readonly IServicePrescription _servicePrescription;
        private readonly IServiceDossierMedical _serviceDossierMedical;
        private bool isMenuCollapsed = false;

        public AccueilView(
            Medecin medecin,
            IServicePatient servicePatient,
            IServiceConsultation serviceConsultation,
            IServicePrescription servicePrescription,
            IServiceDossierMedical serviceDossierMedical)
        {
            InitializeComponent();
            _medecinConnecte = medecin;
            _servicePatient = servicePatient;
            _serviceConsultation = serviceConsultation;
            _servicePrescription = servicePrescription;
            _serviceDossierMedical = serviceDossierMedical;
            InitializeView();
            LoadStatistics();
        }

        private void InitializeView()
        {
            WelcomeText.Text = $"Bienvenue Dr. {_medecinConnecte.Nom}";
        }

        private async void LoadStatistics()
        {
            var patients = await _servicePatient.ObtenirTousLesPatients();
            PatientsCount.Text = patients.Count.ToString();

            var consultations = await _serviceConsultation.ObtenirToutesLesConsultations();
            ConsultationsCount.Text = consultations.Count.ToString();

            var prescriptions = await _servicePrescription.ObtenirToutesLesPrescriptions();
            PrescriptionsCount.Text = prescriptions.Count.ToString();

            var dossiers = await _serviceDossierMedical.ObtenirTousLesDossiers();
            DossiersCount.Text = dossiers.Count.ToString();
        }

        private void ToggleMenu_Click(object sender, RoutedEventArgs e)
        {
            isMenuCollapsed = !isMenuCollapsed;
            AccueilMenuColumn.Width = new GridLength(isMenuCollapsed ? 50 : 200);
        }

        private void GoHome_Click(object sender, RoutedEventArgs e)
        {
            // Déjà sur la page d'accueil
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
                _serviceDossierMedical,
                _servicePatient);
            dossierMedicalView.Show();
            this.Close();
        }

        private void ConsultationMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var consultationView = new ConsultationView(
                _medecinConnecte,
                _servicePatient,
                _serviceConsultation);
            consultationView.Show();
            this.Close();
        }

        private void PrescriptionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var prescriptionView = new PrescriptionView(
                _medecinConnecte,
                _servicePatient,
                _servicePrescription);
            prescriptionView.Show();
            this.Close();
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