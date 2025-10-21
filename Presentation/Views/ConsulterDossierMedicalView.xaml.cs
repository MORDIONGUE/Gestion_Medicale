using System;
using System.Windows;
using A_C.Domaine.Entites;
using A_C.Application.Interfaces;

namespace A_C.Presentation.Views
{
    public partial class ConsulterDossierMedicalView : Window
    {
        private readonly Patient _patient;
        private readonly IServiceDossierMedical _serviceDossierMedical;
        private readonly IServiceConsultation _serviceConsultation;
        private readonly IServicePrescription _servicePrescription;
        private readonly Medecin _medecinConnecte;

        public ConsulterDossierMedicalView(
            Patient patient,
            Medecin medecinConnecte,
            IServiceDossierMedical serviceDossierMedical,
            IServiceConsultation serviceConsultation,
            IServicePrescription servicePrescription)
        {
            InitializeComponent();

            _patient = patient ?? throw new ArgumentNullException(nameof(patient));
            _medecinConnecte = medecinConnecte ?? throw new ArgumentNullException(nameof(medecinConnecte));
            _serviceDossierMedical = serviceDossierMedical ?? throw new ArgumentNullException(nameof(serviceDossierMedical));
            _serviceConsultation = serviceConsultation ?? throw new ArgumentNullException(nameof(serviceConsultation));
            _servicePrescription = servicePrescription ?? throw new ArgumentNullException(nameof(servicePrescription));

            InitialiserVue();
            ChargerDonnees();
        }

        private void InitialiserVue()
        {
            Title = $"Dossier Médical - {_patient.Nom} - Dr. {_medecinConnecte.Nom}";
            DataContext = this;
        }

        private async void ChargerDonnees()
        {
            try
            {
                // Charger le dossier médical
                var dossier = await _serviceDossierMedical.ObtenirDossierParPatient(_patient.Id);
                if (dossier != null)
                {
                    // Charger les consultations
                    var consultations = await _serviceConsultation.ObtenirConsultationsPatient(_patient.Id);
                    _patient.Consultations.Clear();
                    foreach (var consultation in consultations)
                    {
                        _patient.AjouterConsultation(consultation);
                    }

                    // Charger les prescriptions
                    var prescriptions = await _servicePrescription.ObtenirPrescriptionsPatient(_patient.Id);
                    _patient.Prescriptions.Clear();
                    foreach (var prescription in prescriptions)
                    {
                        _patient.AjouterPrescription(prescription);
                    }

                    // Rafraîchir l'interface
                    DataContext = null;
                    DataContext = this;
                }
                else
                {
                    MessageBox.Show("Aucun dossier médical trouvé pour ce patient.",
                        "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des données : {ex.Message}",
                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Propriété pour le binding
        public Patient Patient => _patient;
    }
} 