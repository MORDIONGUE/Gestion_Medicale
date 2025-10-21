using System;
using System.Windows;
using A_C.Domaine.Entites;
using A_C.Application.Interfaces;

namespace A_C.Presentation.Views
{
    public partial class ModifierPatientView : Window
    {
        private readonly IServicePatient _servicePatient;
        public Patient Patient { get; private set; }
        public DateTime? DateNaissance { get; set; }

        public ModifierPatientView(Patient patient, IServicePatient servicePatient)
        {
            InitializeComponent();

            Patient = patient ?? throw new ArgumentNullException(nameof(patient));
            _servicePatient = servicePatient ?? throw new ArgumentNullException(nameof(servicePatient));

            InitialiserVue();
        }

        private void InitialiserVue()
        {
            Title = $"Modifier Patient - {Patient.Nom}";

            // Convertir la date de naissance string en DateTime
            if (DateTime.TryParseExact(Patient.DateNaissance, "dd/MM/yyyy", null, 
                System.Globalization.DateTimeStyles.None, out DateTime date))
            {
                DateNaissance = date;
            }

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

                // Mettre à jour la date de naissance
                if (DateNaissance.HasValue)
                {
                    Patient.DateNaissance = DateNaissance.Value.ToString("dd/MM/yyyy");
                }

                // Enregistrer les modifications via le service
                await _servicePatient.ModifierPatient(Patient);

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la modification : {ex.Message}",
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
            if (string.IsNullOrWhiteSpace(Patient.Nom))
            {
                AfficherErreur("Le nom est obligatoire.");
                return false;
            }

            if (!DateNaissance.HasValue)
            {
                AfficherErreur("La date de naissance est obligatoire.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(Patient.Adresse))
            {
                AfficherErreur("L'adresse est obligatoire.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(Patient.Telephone))
            {
                AfficherErreur("Le téléphone est obligatoire.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(Patient.Courriel))
            {
                AfficherErreur("Le courriel est obligatoire.");
                return false;
            }

            // Validation du format du courriel
            if (!IsValidEmail(Patient.Courriel))
            {
                AfficherErreur("Le format du courriel n'est pas valide.");
                return false;
            }

            return true;
        }

        private void AfficherErreur(string message)
        {
            MessageBox.Show(message, "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
} 