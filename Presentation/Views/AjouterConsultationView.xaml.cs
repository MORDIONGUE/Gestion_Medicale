using System;
using System.Windows;
using A_C.Domaine.Entites;
using A_C.Application.Interfaces;

namespace A_C.Presentation.Views
{
    public partial class AjouterConsultationView : Window
    {
        private readonly Patient _patient;
        private readonly IServiceConsultation _serviceConsultation;
        public ConsultationDetails NouvelleConsultation { get; private set; }

        public DateTime Date { get; set; }
        public string Motif { get; set; }
        public string Observation { get; set; }
        public string Diagnostic { get; set; }

        public AjouterConsultationView(Patient patient, IServiceConsultation serviceConsultation)
        {
            InitializeComponent();

            _patient = patient ?? throw new ArgumentNullException(nameof(patient));
            _serviceConsultation = serviceConsultation ?? throw new ArgumentNullException(nameof(serviceConsultation));

            Date = DateTime.Now;
            DataContext = this;
        }

        private async void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValiderDonnees())
                {
                    return;
                }

                NouvelleConsultation = new ConsultationDetails
                {
                    Date = Date,
                    Motif = Motif,
                    Observation = Observation,
                    Diagnostic = Diagnostic,
                    PatientId = _patient.Id,
                    Patient = _patient
                };

                await _serviceConsultation.AjouterConsultation(_patient.Id, NouvelleConsultation);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'enregistrement : {ex.Message}",
                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private bool ValiderDonnees()
        {
            if (Date == DateTime.MinValue)
            {
                MessageBox.Show("La date est obligatoire.",
                    "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(Motif))
            {
                MessageBox.Show("Le motif est obligatoire.",
                    "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(Observation))
            {
                MessageBox.Show("L'observation est obligatoire.",
                    "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(Diagnostic))
            {
                MessageBox.Show("Le diagnostic est obligatoire.",
                    "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
    }
}