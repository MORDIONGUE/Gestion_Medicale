using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using A_C.Application.Interfaces;
using A_C.Domaine.Entites;
using A_C.Infrastructure.Services;

namespace A_C.Presentation.Views
{
    public partial class PrescriptionView : Window
    {
        private readonly Medecin _medecinConnecte;
        private readonly IServicePatient _servicePatient;
        private readonly IServicePrescription _servicePrescription;
        private bool isMenuCollapsed = false;

        public PrescriptionView(Medecin medecin, IServicePatient servicePatient, IServicePrescription servicePrescription)
        {
            InitializeComponent();
            _medecinConnecte = medecin;
            _servicePatient = servicePatient;
            _servicePrescription = servicePrescription;
            InitializeView();
            LoadPatientsWithConsultations();
        }

        private void InitializeView()
        {
            Title = $"Prescription - Dr. {_medecinConnecte.Nom}";
        }

        private async void LoadPatientsWithConsultations()
        {
            try
            {
                var patients = await _servicePatient.ObtenirTousLesPatients();
                var patientsWithConsultations = patients.Where(p => p.Consultations.Any()).ToList();

                if (!patientsWithConsultations.Any())
                {
                    MessageBox.Show("Aucun patient avec des consultations trouvé.", 
                        "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                PatientsListView.ItemsSource = patientsWithConsultations;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement : {ex.Message}", 
                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void AjouterPrescription_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Patient patient)
            {
                var ajoutPrescriptionView = new AjouterPrescriptionView(patient);
                if (ajoutPrescriptionView.ShowDialog() == true)
                {
                    await _servicePrescription.AjouterPrescription(patient.Id, ajoutPrescriptionView.NouvellePrescription);
                    MessageBox.Show($"Nouvelle prescription ajoutée pour {patient.Nom}.", 
                        "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadPatientsWithConsultations();
                }
            }
        }

        private async void ConsulterPatient_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Patient patient)
            {
                var prescriptions = await _servicePrescription.ObtenirPrescriptionsPatient(patient.Id);
                if (prescriptions.Any())
                {
                    PrescriptionsListView.ItemsSource = prescriptions;
                }
                else
                {
                    PrescriptionsListView.ItemsSource = null;
                    MessageBox.Show($"Le patient {patient.Nom} n'a aucune prescription enregistrée.",
                        "Aucune Prescription", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private async void ChangerEtat_Click(object sender, RoutedEventArgs e)
        {
            if (sender is TextBlock textBlock && textBlock.DataContext is PrescriptionDetails prescription)
            {
                prescription.Etat = prescription.Etat == "En attente" ? "Clôturée" : "En attente";
                textBlock.Foreground = prescription.Etat == "Clôturée" ? Brushes.Green : Brushes.Red;

                await _servicePrescription.ModifierPrescription(prescription);
                PatientsListView.Items.Refresh();
            }
        }

        private void ToggleMenu_Click(object sender, RoutedEventArgs e)
        {
            PrescriptionMenuColumn.Width = new GridLength(isMenuCollapsed ? 200 : 50);
            isMenuCollapsed = !isMenuCollapsed;
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
            var consultationView = new ConsultationView(
                _medecinConnecte,
                _servicePatient,
                new ServiceConsultation(_servicePatient));
            consultationView.Show();
            this.Close();
        }

        private void PrescriptionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Déjà sur la page prescription
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