using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using A_C.Infrastructure.Services;
using A_C.Presentation.Views;

namespace A_C.Presentation
{
    public partial class MainWindow : Window
    {
        private readonly ServiceMedecin _serviceMedecin;

        public MainWindow()
        {
            InitializeComponent();
            _serviceMedecin = new ServiceMedecin();
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var medecin = await _serviceMedecin.Authentifier(IdentifiantTextBox.Text, MotDePasseBox.Password);
                if (medecin != null)
                {
                    // Initialiser tous les services nécessaires
                    var servicePatient = new ServicePatient();
                    var serviceConsultation = new ServiceConsultation(servicePatient);
                    var servicePrescription = new ServicePrescription(servicePatient);
                    var serviceDossierMedical = new ServiceDossierMedical(servicePatient);

                    // Créer et afficher la vue d'accueil avec tous les services nécessaires
                    var accueilView = new AccueilView(
                        medecin,
                        servicePatient,
                        serviceConsultation,
                        servicePrescription,
                        serviceDossierMedical);

                    accueilView.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Identifiant ou mot de passe incorrect.", 
                        "Erreur d'authentification", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'authentification : {ex.Message}",
                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AuthentificationButton_Click(object sender, RoutedEventArgs e)
        {
            Login_Click(sender, e);
        }

        private void MotDePasseOublie_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Veuillez contacter le service informatique pour réinitialiser votre mot de passe.",
                "Mot de passe oublié", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}