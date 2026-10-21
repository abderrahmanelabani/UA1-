using System.Windows;
using GestiCoop.UI.ViewModels;

namespace GestiCoop.UI.Views
{
    public partial class AjouterEtudiantWindow : Window
    {
        public AjouterEtudiantWindow()
        {
            InitializeComponent();

            // On relie la fenêtre au ViewModel pour gérer la fermeture
            var viewModel = (AjouterEtudiantVM)this.DataContext;

            // Si le ViewModel déclenche "RequestClose", on exécute cette petite fonction
            viewModel.RequestClose += (bool resultat) =>
            {
                this.DialogResult = resultat; // True = succès, False = annulé
                this.Close(); // Ferme la fenêtre
            };
        }
    }
}