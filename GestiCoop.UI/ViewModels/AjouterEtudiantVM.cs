using System;
using System.Text.RegularExpressions;
using System.Windows.Input;
using GestiCoop.UI.Models;

namespace GestiCoop.UI.ViewModels
{
    public class AjouterEtudiantVM : ViewModelBase
    {
        private string _nom;
        public string Nom
        {
            get => _nom;
            set
            {
                if (Set(ref _nom, value))
                {
                    OnPropertyChanged(nameof(EstValide));
                    (EnregistrerCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        private string _prenom;
        public string Prenom
        {
            get => _prenom;
            set
            {
                if (Set(ref _prenom, value))
                {
                    OnPropertyChanged(nameof(EstValide));
                    (EnregistrerCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        private string _courriel;
        public string Courriel
        {
            get => _courriel;
            set
            {
                if (Set(ref _courriel, value))
                {
                    OnPropertyChanged(nameof(EstCourrielInvalide));
                    OnPropertyChanged(nameof(EstValide));
                    (EnregistrerCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        private string _programme;
        public string Programme
        {
            get => _programme;
            set
            {
                if (Set(ref _programme, value))
                {
                    OnPropertyChanged(nameof(EstValide));
                    (EnregistrerCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        private string _numeroEtudiant;
        public string NumeroEtudiant
        {
            get => _numeroEtudiant;
            set => Set(ref _numeroEtudiant, value);
        }

        // --- Logique de validation ---

        public bool EstCourrielValide => !string.IsNullOrWhiteSpace(Courriel) && Regex.IsMatch(Courriel, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        // Propriété utilisée pour afficher le message d'erreur en rouge
        public bool EstCourrielInvalide => !string.IsNullOrWhiteSpace(Courriel) && !EstCourrielValide;

        public bool EstValide
        {
            get
            {
                bool nomRempli = !string.IsNullOrWhiteSpace(Nom);
                bool prenomRempli = !string.IsNullOrWhiteSpace(Prenom);
                bool programmeSelectionne = !string.IsNullOrWhiteSpace(Programme);

                return nomRempli && prenomRempli && programmeSelectionne && EstCourrielValide;
            }
        }

        // L'étudiant qui sera créé si l'enregistrement réussit
        public Etudiant NouvelEtudiant { get; private set; }

        public ICommand EnregistrerCommand { get; }
        public ICommand AnnulerCommand { get; }

        // Événement pour demander à la vue de se fermer
        public event Action<bool> RequestClose;

        public AjouterEtudiantVM()
        {
            EnregistrerCommand = new RelayCommand(
                execute: obj => Enregistrer(),
                canExecute: obj => EstValide // Le bouton sera grisé si EstValide est faux
            );

            AnnulerCommand = new RelayCommand(
                execute: obj => RequestClose?.Invoke(false)
            );
        }

        private void Enregistrer()
        {
            NouvelEtudiant = new Etudiant
            {
                Nom = this.Nom,
                Prenom = this.Prenom,
                Programme = this.Programme,
                Courriel = this.Courriel
                // Note: Le numéro d'étudiant n'est pas dans le modèle actuel, je le laisse de côté pour l'instant
            };

            RequestClose?.Invoke(true); // Indique un succès
        }
    }

    // Ajout d'une petite méthode à RelayCommand pour forcer la mise à jour du CanExecute
    public partial class RelayCommand
    {
        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}