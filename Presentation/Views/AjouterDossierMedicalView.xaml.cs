using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using A_C.Domaine.Entites;
using A_C.Application.Interfaces;

namespace A_C.Presentation.Views
{
    public partial class AjouterDossierMedicalView : Window
    {
        private readonly Patient _patient;
        private readonly IServiceDossierMedical _serviceDossierMedical;
        public DossierMedical NouveauDossier { get; private set; }

        public AjouterDossierMedicalView(Patient patient, IServiceDossierMedical serviceDossierMedical)
        {
            InitializeComponent();
            _patient = patient ?? throw new ArgumentNullException(nameof(patient));
            _serviceDossierMedical = serviceDossierMedical ?? throw new ArgumentNullException(nameof(serviceDossierMedical));

            InitialiserVue();
        }

        private void InitialiserVue()
        {
            // Afficher les informations du patient
            DataContext = this;
            PatientInfo = $"Patient : {_patient.Nom} (ID: {_patient.Id})";

            // Initialiser le dossier médical
            NouveauDossier = new DossierMedical
            {
                Patient = _patient,
                Traitements = new List<string>()
            };

            // Charger les traitements existants si le patient a déjà un dossier
            ChargerTraitementsExistants();
        }

        private async void ChargerTraitementsExistants()
        {
            var dossierExistant = await _serviceDossierMedical.ObtenirDossierParPatient(_patient.Id);
            if (dossierExistant != null)
            {
                NouveauDossier = dossierExistant;
                TraitementsListBox.ItemsSource = dossierExistant.Traitements;
            }
        }

        private void AjouterTraitement_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TraitementTextBox.Text))
            {
                MessageBox.Show("Veuillez entrer un traitement.", 
                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Ajouter le nouveau traitement
            NouveauDossier.Traitements.Add(TraitementTextBox.Text);

            // Mettre à jour l'affichage
            TraitementsListBox.ItemsSource = null;
            TraitementsListBox.ItemsSource = NouveauDossier.Traitements;

            // Vider le champ
            TraitementTextBox.Clear();
        }

        private async void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!NouveauDossier.Traitements.Any())
                {
                    MessageBox.Show("Veuillez ajouter au moins un traitement.", 
                        "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Enregistrer ou mettre à jour le dossier
                if (NouveauDossier.Id == 0)
                {
                    await _serviceDossierMedical.CreerDossier(NouveauDossier);
                }
                else
                {
                    await _serviceDossierMedical.ModifierDossier(NouveauDossier);
                }

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

        // Propriété pour le binding des informations du patient
        public string PatientInfo { get; private set; }
    }
} 