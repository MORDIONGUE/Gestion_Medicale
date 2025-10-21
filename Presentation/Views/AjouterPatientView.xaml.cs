using System;
using System.Windows;
using A_C.Domaine.Entites;
using A_C.Application.Interfaces;

namespace A_C.Presentation.Views
{
    public partial class AjouterPatientView : Window
    {
        private readonly IServicePatient _servicePatient;
        public Patient NouveauPatient { get; private set; }

        public AjouterPatientView(IServicePatient servicePatient)
        {
            InitializeComponent();
            _servicePatient = servicePatient ?? throw new ArgumentNullException(nameof(servicePatient));
            
            // Initialisation du patient et du DataContext
            NouveauPatient = new Patient();
            DataContext = NouveauPatient;

            // Définir la date par défaut
            DateNaissancePicker.SelectedDate = DateTime.Now.AddYears(-30); // Date par défaut
        }

        private async void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValiderFormulaire())
                {
                    return;
                }

                var nouveauPatient = new Patient
                {
                    Nom = NomTextBox.Text,
                    DateNaissance = DateNaissancePicker.SelectedDate?.ToString("dd/MM/yyyy") ?? "",
                    Adresse = AdresseTextBox.Text,
                    Telephone = TelephoneTextBox.Text,
                    Courriel = CourrielTextBox.Text
                };

                bool resultat = await _servicePatient.AjouterPatient(nouveauPatient);
                if (resultat)
                {
                    DialogResult = true;
                    Close();
                }
                else
                {
                    MessageBox.Show("Erreur lors de l'ajout du patient.", 
                        "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", 
                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValiderFormulaire()
        {
            return !string.IsNullOrWhiteSpace(NouveauPatient.Nom) &&
                   !string.IsNullOrWhiteSpace(NouveauPatient.Adresse) &&
                   !string.IsNullOrWhiteSpace(NouveauPatient.Telephone) &&
                   !string.IsNullOrWhiteSpace(NouveauPatient.Courriel) &&
                   DateNaissancePicker.SelectedDate.HasValue;
        }

        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
} 