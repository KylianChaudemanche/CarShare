using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarShare.BO
{
    public class JeuxDeDonnees
    {
        // Création Ecole
        static Emplacement emplacementEniRennes = new Emplacement() { Id = 1, Description = "Campus de Chartre-de-Bretagne", Intitule = "ENI Rennes", Latitude = (long)48.038919, Longitude = (long)-1.692393 };
        static Ecole ecoleEniRennes = new Ecole() { Id = 1, Emplacement = emplacementEniRennes, Nom = "Chartre-de-Bretagne" };

        // Création Conducteur
        static ApplicationUser toto = new ApplicationUser()
        {
            Id = "1",
            Email = "toto@eni.fr",
            IsActif = true,
            Nom = "TOTO",
            Prenom = "Toto",
            Description = "Je ne supporte pas la cigarette",
            Ecole = ecoleEniRennes,
            IsAdmin = false,
            Password = "Pa$$w0rd",
            Telephone = "0123456798",
            Voiture = new Voiture() { Id = 1, Couleur = "Bleu", Immatriculation = "Y0L0SW4G", Marque = "Bugatti", Modele = "Chiron", NbPlaces = 2, Proprietaire = toto }
        };

        // Création passagers
        static ApplicationUser passager1 = new ApplicationUser()
        {
            Id = "2",
            Email = "turlute@eni.fr",
            IsActif = true,
            Nom = "TUR",
            Prenom = "Lute",
            Description = "Calme et Discret",
            Ecole = ecoleEniRennes,
            IsAdmin = false,
            Password = "Pa$$w0rd",
            Telephone = "987654321"
        };

        static ApplicationUser passager2 = new ApplicationUser()
        {
            Id = "3",
            Email = "bolognaise@eni.com",
            IsActif = true,
            Nom = "GNAISE",
            Prenom = "Bolo",
            Description = "Un peu de musique ne fait pas de mal",
            Ecole = ecoleEniRennes,
            IsAdmin = false,
            Password = "Pa$$w0rd",
            Telephone = "147258369"
        };
        static List<ApplicationUser> listePassagers = new List<ApplicationUser> { passager1, passager2 };

        // Créations Emplacements
        static Emplacement emplacementToto = new Emplacement() { Id = 1, Description = "Mon départ quotidien", Intitule = "Départ", Longitude = (long)48.103505, Latitude = (long)-1.711650 };
        static Emplacement emplacement1 = new Emplacement() { Id = 2, Description = "Au niveau du LIDL", Intitule = "arret passager1", Longitude = (long)48.104660, Latitude = (long)-1.732394 };
        static Emplacement emplacement2 = new Emplacement() { Id = 3, Description = "Sur le parking d'Alma", Intitule = "arret passager2", Longitude = (long)48.082034, Latitude = (long)-1.678468 };

        // Création Arrets
        static Arret departToto = new Arret() { Id = 1, Emplacement = emplacementToto, EtatArret = 1, Horaire = DateTime.Now };
        static Arret arret1 = new Arret() { Id = 2, Emplacement = emplacement1, EtatArret = 2, Horaire = DateTime.Now };
        static Arret arret2 = new Arret() { Id = 3, Emplacement = emplacement2, EtatArret = 0, Horaire = DateTime.Now };

        static List<Arret> listeArrets = new List<Arret> { arret1, arret2 };

        // Création Trajet
        static Trajet totoDaily = new Trajet() { Id = 1, Depart = emplacementToto, Arrive = toto.Ecole.Emplacement, Arrets = listeArrets, Conducteur = toto, Passagers = listePassagers, Date = DateTime.Now };

        // Ajout du Trajet dans les Arrets
        public JeuxDeDonnees()
        {
            departToto.Trajet = totoDaily;
            arret1.Trajet = totoDaily;
            arret2.Trajet = totoDaily;
        }
    }
}