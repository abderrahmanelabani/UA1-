using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GestiCoop.UI.Models;

namespace GestiCoop.UI.ViewModels
{
    public class EtudiantsViewModel : ViewModelBase
    {
        // Liste complète en mémoire (pour ne pas perdre de données quand on filtre)
        private List<Etudiant> _tousLesEtudiants = new List<Etudiant>();

        public ObservableCollection<Etudiant> Etudiants { get; } = new ObservableCollection<Etudiant>();

        private string _programmeFiltre = "Tous les programmes";
        public string ProgrammeFiltre
        {
            get => _programmeFiltre;
            set
            {
                if (Set(ref _programmeFiltre, value))
                {
                    AppliquerFiltre(); // Appelé automatiquement à chaque changement du menu
                }
            }
        }

        public ICommand AjouterCommand { get; }

        public EtudiantsViewModel()
        {
            AjouterCommand = new RelayCommand(
                execute: obj => OuvrirFenetreAjout(),
                canExecute: obj => true
            );

            // Remplissage de la liste de sauvegarde
            _tousLesEtudiants.Add(new Etudiant { Nom = "Tremblay", Prenom = "Sophie", Programme = "Informatique", Courriel = "s.tremblay@etud.lacite.ca" });
            _tousLesEtudiants.Add(new Etudiant { Nom = "Nguyen", Prenom = "Minh", Programme = "Informatique", Courriel = "m.nguyen@etud.lacite.ca" });
            _tousLesEtudiants.Add(new Etudiant { Nom = "Dubois", Prenom = "Élodie", Programme = "Génie logiciel", Courriel = "e.dubois@etud.lacite.ca" });
            _tousLesEtudiants.Add(new Etudiant { Nom = "Okonkwo", Prenom = "Chidi", Programme = "Cybersécurité", Courriel = "c.okonkwo@etud.lacite.ca" });
            _tousLesEtudiants.Add(new Etudiant { Nom = "Lavoie", Prenom = "Xavier", Programme = "Informatique", Courriel = "x.lavoie@etud.lacite.ca" });

            // Affichage initial
            AppliquerFiltre();
        }

        private void AppliquerFiltre()
        {
            Etudiants.Clear();
            foreach (var etudiant in _tousLesEtudiants)
            {
                if (ProgrammeFiltre == "Tous les programmes" || etudiant.Programme == ProgrammeFiltre || ProgrammeFiltre == null)
                {
                    Etudiants.Add(etudiant);
                }
            }
        }

        private void OuvrirFenetreAjout()
        {
            var fenetre = new GestiCoop.UI.Views.AjouterEtudiantWindow();

            // Si l'utilisateur clique sur Enregistrer (DialogResult == true)
            if (fenetre.ShowDialog() == true)
            {
                // On ajoute le nouvel étudiant à notre liste complète
                _tousLesEtudiants.Add(fenetre.ResultatEtudiant);

                // On met à jour l'affichage dans le tableau
                AppliquerFiltre();
            }
        }
    }
}