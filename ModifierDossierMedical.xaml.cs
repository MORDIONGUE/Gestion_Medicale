using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace gestionMedicale
{
    public partial class ModifierDossierMedical : Window
    {
        public Dossier DossierModifie { get; private set; }

        public ModifierDossierMedical(Dossier dossier)
        {
            InitializeComponent();

            // Charger les informations du dossier
            DossierModifie = dossier;
            IdTextBlock.Text = dossier.Identifiant.ToString();
            NomTextBox.Text = dossier.Nom;
            TraitementsListBox.ItemsSource = dossier.Traitements;
        }

        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            // Mettre à jour les informations
            DossierModifie.Nom = NomTextBox.Text;

            // Enregistrer les modifications
            DialogResult = true;
            Close();
        }

        private void ModifierTraitement_Click(object sender, RoutedEventArgs e)
        {
            if (TraitementsListBox.SelectedItem is string traitement)
            {
                var nouveauTraitement = Microsoft.VisualBasic.Interaction.InputBox(
                    "Modifier le traitement sélectionné :",
                    "Modification du traitement",
                    traitement);

                if (!string.IsNullOrWhiteSpace(nouveauTraitement) && nouveauTraitement != traitement)
                {
                    int index = DossierModifie.Traitements.IndexOf(traitement);
                    if (index != -1)
                    {
                        DossierModifie.Traitements[index] = nouveauTraitement;
                        TraitementsListBox.Items.Refresh();
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un traitement à modifier.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void AjouterTraitement_Click(object sender, RoutedEventArgs e)
        {
            var nouveauTraitement = Microsoft.VisualBasic.Interaction.InputBox(
                "Entrez un nouveau traitement :",
                "Ajout de traitement");

            if (!string.IsNullOrWhiteSpace(nouveauTraitement))
            {
                DossierModifie.Traitements.Add(nouveauTraitement);
                TraitementsListBox.Items.Refresh();
            }
        }

        private void SupprimerTraitement_Click(object sender, RoutedEventArgs e)
        {
            if (TraitementsListBox.SelectedItem is string traitement)
            {
                var result = MessageBox.Show(
                    $"Voulez-vous vraiment supprimer le traitement : {traitement} ?",
                    "Confirmation",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    DossierModifie.Traitements.Remove(traitement);
                    TraitementsListBox.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un traitement à supprimer.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
