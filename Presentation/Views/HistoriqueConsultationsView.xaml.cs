using System;
using System.Collections.ObjectModel;
using System.Windows;
using A_C.Domaine.Entites;
using A_C.Application.Interfaces;
using System.Linq;

namespace A_C.Presentation.Views
{
    public partial class HistoriqueConsultationsView : Window
    {
        private readonly Patient _patient;
        private readonly IServiceConsultation _serviceConsultation;
        public ObservableCollection<ConsultationDetails> Consultations { get; private set; }

        public HistoriqueConsultationsView(Patient patient, IServiceConsultation serviceConsultation)
        {
            InitializeComponent();

            _patient = patient ?? throw new ArgumentNullException(nameof(patient));
            _serviceConsultation = serviceConsultation ?? throw new ArgumentNullException(nameof(serviceConsultation));

            Consultations = new ObservableCollection<ConsultationDetails>();
            DataContext = this;

            InitialiserVue();
            ChargerConsultations();
        }

        private void InitialiserVue()
        {
            Title = $"Historique des Consultations - {_patient.Nom}";
            PatientInfo = $"Patient : {_patient.Nom} (ID: {_patient.Id})";
        }

        private async void ChargerConsultations()
        {
            try
            {
                var consultations = await _serviceConsultation.ObtenirConsultationsPatient(_patient.Id);
                Consultations.Clear();
                foreach (var consultation in consultations)
                {
                    Consultations.Add(consultation);
                }

                if (!Consultations.Any())
                {
                    MessageBox.Show("Aucune consultation trouvée pour ce patient.",
                        "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des consultations : {ex.Message}",
                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Propriété pour le binding de l'information du patient
        public string PatientInfo { get; private set; }
    }
} 