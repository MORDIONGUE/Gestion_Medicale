using System;
using System.Windows;
using System.Collections.ObjectModel;
using A_C.Domaine.Entites;
using A_C.Application.Interfaces;

namespace A_C.Presentation.Views
{
    public partial class GestionPrescriptionsView : Window
    {
        private readonly Patient _patient;
        private readonly IServicePrescription _servicePrescription;
        private readonly Medecin _medecinConnecte;
        public ObservableCollection<PrescriptionDetails> Prescriptions { get; private set; }

        public GestionPrescriptionsView(
            Patient patient, 
            Medecin medecinConnecte,
            IServicePrescription servicePrescription)
        {
            InitializeComponent();

            _patient = patient ?? throw new ArgumentNullException(nameof(patient));
            _medecinConnecte = medecinConnecte ?? throw new ArgumentNullException(nameof(medecinConnecte));
            _servicePrescription = servicePrescription ?? throw new ArgumentNullException(nameof(servicePrescription));

            Prescriptions = new ObservableCollection<PrescriptionDetails>();
            DataContext = this;

            InitialiserVue();
            ChargerPrescriptions();
        }

        private void InitialiserVue()
        {
            Title = $"Prescriptions - {_patient.Nom} - Dr. {_medecinConnecte.Nom}";
        }

        private async void ChargerPrescriptions()
        {
            try
            {
                var prescriptions = await _servicePrescription.ObtenirPrescriptionsPatient(_patient.Id);
                Prescriptions.Clear();
                foreach (var prescription in prescriptions)
                {
                    Prescriptions.Add(prescription);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des prescriptions : {ex.Message}",
                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void AjouterPrescription_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var prescription = new PrescriptionDetails
                {
                    Date = DateTime.Now,
                    MedecinId = _medecinConnecte.Id,
                    Medecin = _medecinConnecte,
                    PatientId = _patient.Id,
                    Patient = _patient,
                    Medicament = MedicamentTextBox.Text,
                    Dosage = DosageTextBox.Text,
                    Instructions = InstructionsTextBox.Text,
                    Duree = DureeTextBox.Text,
                    Etat = "En attente"
                };

                await _servicePrescription.AjouterPrescription(_patient.Id, prescription);
                
                MessageBox.Show("Prescription ajoutée avec succès.", 
                    "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                
                ChargerPrescriptions();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'ajout de la prescription : {ex.Message}",
                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValiderFormulaire()
        {
            return !string.IsNullOrWhiteSpace(MedicamentTextBox.Text) &&
                   !string.IsNullOrWhiteSpace(DosageTextBox.Text) &&
                   !string.IsNullOrWhiteSpace(InstructionsTextBox.Text) &&
                   !string.IsNullOrWhiteSpace(DureeTextBox.Text);
        }

        private void ViderFormulaire()
        {
            MedicamentTextBox.Clear();
            DosageTextBox.Clear();
            InstructionsTextBox.Clear();
            DureeTextBox.Clear();
        }
    }
}