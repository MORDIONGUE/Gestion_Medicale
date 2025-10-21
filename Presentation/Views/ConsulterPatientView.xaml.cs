using System;
using System.Windows;
using A_C.Domaine.Entites;
using A_C.Application.Interfaces;

namespace A_C.Presentation.Views
{
    public partial class ConsulterPatientView : Window
    {
        private readonly Patient _patient;
        private readonly IServicePatient _servicePatient;

        public ConsulterPatientView(Patient patient, IServicePatient servicePatient)
        {
            InitializeComponent();

            _patient = patient ?? throw new ArgumentNullException(nameof(patient));
            _servicePatient = servicePatient ?? throw new ArgumentNullException(nameof(servicePatient));

            InitialiserVue();
        }

        private void InitialiserVue()
        {
            // Définir le titre de la fenêtre
            Title = $"Patient - {_patient.Nom}";

            // Utiliser le binding pour afficher les données
            DataContext = this;
        }

        private void Fermer_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        // Propriété pour le binding
        public Patient Patient => _patient;
    }
} 