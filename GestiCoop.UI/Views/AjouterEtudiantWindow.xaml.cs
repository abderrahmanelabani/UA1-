using System.Windows;
using GestiCoop.UI.Models;

namespace GestiCoop.UI.Views
{
    public partial class AjouterEtudiantWindow : Window
    {
        // Cette variable stockera l'étudiant créé pour l'envoyer à la fenêtre principale
        public Etudiant ResultatEtudiant { get; private set; }

        public AjouterEtudiantWindow()
        {
            InitializeComponent();
        }

        private void btnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            // On crée le nouvel étudiant avec les données saisies
            ResultatEtudiant = new Etudiant
            {
                Nom = txtNom.Text,
                Prenom = txtPrenom.Text,
                Programme = cboProgramme.Text,
                Courriel = txtCourriel.Text
            };

            // Indique que l'opération a réussi et ferme la fenêtre
            DialogResult = true;
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            // Indique l'annulation et ferme la fenêtre
            DialogResult = false;
        }
    }
}