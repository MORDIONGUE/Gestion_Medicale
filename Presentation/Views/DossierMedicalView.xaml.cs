using System.Windows;
using A_C.Application.Interfaces;
using A_C.Domaine.Entites;
using A_C.Infrastructure.Services;

namespace A_C.Presentation.Views
{
    public partial class DossierMedicalView : Window
    {
        private readonly Medecin _medecinConnecte;
        private readonly IServiceDossierMedical _serviceDossierMedical;
        private readonly IServicePatient _servicePatient;
        private bool isMenuCollapsed = false;

        public DossierMedicalView(Medecin medecin, IServiceDossierMedical serviceDossierMedical, IServicePatient servicePatient)
        {
            InitializeComponent();
            _medecinConnecte = medecin;
            _serviceDossierMedical = serviceDossierMedical;
            _servicePatient = servicePatient;
            InitializeView();
            LoadDossiers();
        }

        private async void LoadDossiers()
        {
            var dossiers = await _serviceDossierMedical.ObtenirTousLesDossiers();
            DossierListView.ItemsSource = dossiers;
        }

        private void InitializeView()
        {
            Title = $"Dossier Médical - Dr. {_medecinConnecte.Nom}";
        }

        private async void ConsulterDossier_Click(object sender, RoutedEventArgs e)
        {
            if (DossierListView.SelectedItem is DossierMedical dossier)
            {
                // TODO: Implémenter la consultation du dossier
                MessageBox.Show($"Consultation du dossier : {dossier.Nom}");
            }
        }

        private async void ModifierDossier_Click(object sender, RoutedEventArgs e)
        {
            if (DossierListView.SelectedItem is DossierMedical dossier)
            {
                // TODO: Implémenter la modification du dossier
                MessageBox.Show($"Modification du dossier : {dossier.Nom}");
            }
        }

        private async void AjouterDossier_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implémenter l'ajout d'un nouveau dossier
            MessageBox.Show("Ajout d'un nouveau dossier");
        }

        private void ToggleMenu_Click(object sender, RoutedEventArgs e)
        {
            if (isMenuCollapsed)
            {
                DossierMenuColumn.Width = new GridLength(200);
                isMenuCollapsed = false;
            }
            else
            {
                DossierMenuColumn.Width = new GridLength(50);
                isMenuCollapsed = true;
            }
        }

        private void GoHome_Click(object sender, RoutedEventArgs e)
        {
            var patientView = new PatientView(_medecinConnecte, _servicePatient);
            patientView.Show();
            this.Close();
        }

        // Autres méthodes de navigation...

        private void DeconnexionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Voulez-vous vraiment vous déconnecter ?", "Déconnexion",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                new MainWindow().Show();
                this.Close();
            }
        }

        private void ConsultationMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var consultationView = new ConsultationView(
                _medecinConnecte, 
                _servicePatient, 
                new ServiceConsultation(_servicePatient));
            consultationView.Show();
            this.Close();
        }

        private void PatientMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var patientView = new PatientView(_medecinConnecte, _servicePatient);
            patientView.Show();
            this.Close();
        }

        private void PrescriptionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var prescriptionView = new PrescriptionView(
                _medecinConnecte, 
                _servicePatient, 
                new ServicePrescription(_servicePatient));
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

        private void DossierMedicalMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Déjà sur la page dossier médical
        }
    }
} 