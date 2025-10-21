using System;
using System.Windows;
using System.Collections.ObjectModel;
using A_C.Domaine.Entites;
using A_C.Application.Interfaces;

namespace A_C.Presentation.Views
{
    public partial class ModifierDossierMedicalView : Window
    {
        private readonly Patient _patient;
        private readonly Medecin _medecinConnecte;
        private readonly IServiceDossierMedical _serviceDossierMedical;
        public DossierMedical DossierMedical { get; private set; }
        public string TraitementSelectionne { get; set; }

        public ModifierDossierMedicalView(
            Patient patient,
            Medecin medecinConnecte,
            DossierMedical dossierMedical,
            IServiceDossierMedical serviceDossierMedical)
        {
            InitializeComponent();

            _patient = patient ?? throw new ArgumentNullException(nameof(patient));
            _medecinConnecte = medecinConnecte ?? throw new ArgumentNullException(nameof(medecinConnecte));
            DossierMedical = dossierMedical ?? throw new ArgumentNullException(nameof(dossierMedical));
            _serviceDossierMedical = serviceDossierMedical ?? throw new ArgumentNullException(nameof(serviceDossierMedical));

            InitialiserVue();
        }

        private void InitialiserVue()
        {
            Title = $"Modifier Dossier Médical - {_patient.Nom}";
            PatientInfo = $"Patient : {_patient.Nom} (ID: {_patient.Id}) - Dr. {_medecinConnecte.Nom}";
            DataContext = this;
        }

        private async void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValiderDonnees())
                {
                    await _serviceDossierMedical.ModifierDossier(DossierMedical);
                    DialogResult = true;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'enregistrement : {ex.Message}",
                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ModifierTraitement_Click(object sender, RoutedEventArgs e)
        {
            if (TraitementSelectionne == null)
            {
                MessageBox.Show("Veuillez sélectionner un traitement à modifier.",
                    "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var nouveauTraitement = Microsoft.VisualBasic.Interaction.InputBox(
                "Modifier le traitement :",
                "Modification du traitement",
                TraitementSelectionne);

            if (!string.IsNullOrWhiteSpace(nouveauTraitement) && nouveauTraitement != TraitementSelectionne)
            {
                var index = DossierMedical.Traitements.IndexOf(TraitementSelectionne);
                if (index != -1)
                {
                    DossierMedical.Traitements[index] = nouveauTraitement;
                    // La mise à jour de l'interface est automatique grâce à l'ObservableCollection
                }
            }
        }

        private void AjouterTraitement_Click(object sender, RoutedEventArgs e)
        {
            var nouveauTraitement = Microsoft.VisualBasic.Interaction.InputBox(
                "Entrez un nouveau traitement :",
                "Ajout de traitement");

            if (!string.IsNullOrWhiteSpace(nouveauTraitement))
            {
                DossierMedical.Traitements.Add(nouveauTraitement);
            }
        }

        private void SupprimerTraitement_Click(object sender, RoutedEventArgs e)
        {
            if (TraitementSelectionne == null)
            {
                MessageBox.Show("Veuillez sélectionner un traitement à supprimer.",
                    "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Voulez-vous vraiment supprimer le traitement : {TraitementSelectionne} ?",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                DossierMedical.Traitements.Remove(TraitementSelectionne);
            }
        }

        private bool ValiderDonnees()
        {
            if (string.IsNullOrWhiteSpace(DossierMedical.Antecedents))
            {
                MessageBox.Show("Veuillez renseigner les antécédents médicaux.",
                    "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (DossierMedical.Traitements.Count == 0)
            {
                MessageBox.Show("Veuillez ajouter au moins un traitement.",
                    "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        // Propriété pour le binding
        public string PatientInfo { get; private set; }
    }
}