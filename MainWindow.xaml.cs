using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace gestionMedicale
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AuthentificationButton_Click(object sender, RoutedEventArgs e)
        {
            string identifiant = IdentifiantTextBox.Text;
            string motDePasse = MotDePasseBox.Password;

            var medecin = Medecins.Authentifier(identifiant, motDePasse);
            if (medecin != null)
            {
                // Ouvrir la fenêtre Compte_Utilisateur
                Compte_Utilisateur compteUtilisateur = new Compte_Utilisateur();
                compteUtilisateur.Show();

                // Fermer la fenêtre d'authentification actuelle
                this.Close();
            }
            else
            {
                MessageBox.Show("Identifiant ou mot de passe incorrect.");
            }
        }


        private void MotDePasseOublie_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MessageBox.Show("Appelez le service informatique pour réinitialiser votre mot de passe.");
        }


    }
}