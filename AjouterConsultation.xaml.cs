using System;
using System.Windows;

namespace gestionMedicale
{
    public partial class AjouterConsultation : Window
    {
        public int Id { get; private set; }
        public DateTime Date { get; private set; }
        public string Motif { get; private set; } = string.Empty;
        public string Observation { get; private set; } = string.Empty;
        public string Diagnostic { get; private set; } = string.Empty;

        public AjouterConsultation()
        {
            InitializeComponent();
        }

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            // Vérifiez que les champs obligatoires sont renseignés
            if (string.IsNullOrWhiteSpace(ConsultationIdTextBox.Text) ||
                ConsultationDatePicker.SelectedDate == null ||
                string.IsNullOrWhiteSpace(ConsultationMotifTextBox.Text) ||
                string.IsNullOrWhiteSpace(ConsultationObservationTextBox.Text) ||
                string.IsNullOrWhiteSpace(ConsultationDiagnosticTextBox.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Récupération des valeurs des champs
            if (!int.TryParse(ConsultationIdTextBox.Text, out int id))
            {
                MessageBox.Show("L'identifiant doit être un nombre.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Id = id;
            Date = ConsultationDatePicker.SelectedDate.Value;
            Motif = ConsultationMotifTextBox.Text;
            Observation = ConsultationObservationTextBox.Text;
            Diagnostic = ConsultationDiagnosticTextBox.Text;

            DialogResult = true;
            Close();
        }
    }
}
