using CarShare.BO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarShare.Models
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {


        public ApplicationDbContext()
            : base("ApplicationDbContext", throwIfV1Schema: false)
        {
            if (this.Database.CreateIfNotExists())
            {

                // Créations Emplacements
                Emplacement emplacementEniRennes = new Emplacement() { Id = 1, Description = "Campus de Chartre-de-Bretagne", Intitule = "ENI Rennes", Latitude = 48.038919F, Longitude = -1.692393F };
                Emplacement emplacementToto = new Emplacement() { Id = 1, Description = "Rennes Ouest (Cleunay)", Intitule = "Chez moi", Latitude = 48.103505F, Longitude = -1.711650F };
                Emplacement emplacement1 = new Emplacement() { Id = 2, Description = "Au niveau du LIDL", Intitule = "arret passager1", Latitude = 48.104660F, Longitude = -1.732394F };
                Emplacement emplacement2 = new Emplacement() { Id = 3, Description = "Sur le parking d'Alma", Intitule = "arret passager2", Latitude = 48.082034F, Longitude = -1.678468F };
               
                // Création Ecole
                Ecole ecoleEniRennes = new Ecole() { Id = 1, Emplacement = emplacementEniRennes, Nom = "Chartre-de-Bretagne" };

                // Création Conducteur
                ApplicationUser toto = new ApplicationUser()
                {
                    Id = "1",
                    Email = "toto@eni.fr",
                    Nom = "TOTO",
                    Prenom = "Toto",
                    UserName = "toto@eni.fr",
                    Telephone = "0123456798",
                    EmplacementsFavoris = new List<Emplacement>() { emplacementToto },
                    Description = "Je ne supporte pas la cigarette",
                    Ecole = ecoleEniRennes
                };
               
                // Création de la Voiture
                Voiture totoMobile = new Voiture() { Id = 1, Couleur = "Bleu", Immatriculation = "Y0L0SW4G", Marque = "Bugatti", Modele = "Chiron", NbPlaces = 2, Proprietaire = toto };
                toto.Voiture = totoMobile;

                // Création passagers
                ApplicationUser passager1 = new ApplicationUser()
                {
                    Id = "2",
                    Email = "turlute@eni.fr",
                    Nom = "TUR",
                    Prenom = "Lute",
                    UserName = "turlute@eni.fr",
                    Description = "Calme et Discret",
                    Ecole = ecoleEniRennes,
                    Telephone = "987654321"
                };

                ApplicationUser passager2 = new ApplicationUser()
                {
                    Id = "3",
                    Email = "bolognaise@eni.fr",
                    Nom = "GNAISE",
                    Prenom = "Bolo",
                    UserName = "bolognaise@eni.fr",
                    Description = "Un peu de musique ne fait pas de mal",
                    Ecole = ecoleEniRennes,
                    Telephone = "147258369"
                };
                    System.Collections.Generic.List<ApplicationUser> listePassagers = new List<ApplicationUser> { passager1, passager2 };


                // Création Arrets
                Arret departToto = new Arret() { Id = 1, Emplacement = emplacementToto, EtatArret = 1, Horaire = DateTime.Now };
                Arret arret1 = new Arret() { Id = 2, Emplacement = emplacement1, EtatArret = 2, Horaire = DateTime.Now };
                Arret arret2 = new Arret() { Id = 3, Emplacement = emplacement2, EtatArret = 0, Horaire = DateTime.Now };

                List<Arret> listeArrets = new List<Arret> { arret1, arret2 };

                // Création Trajet
                Trajet trajetToto = new Trajet() { Id = 1, Depart = emplacementToto, Arrive = toto.Ecole.Emplacement, Arrets = listeArrets, Conducteur = toto, Passagers = listePassagers, Date = DateTime.Now };

                // Ajout du Trajet dans les Arrets
                departToto.Trajet = trajetToto;
                arret1.Trajet = trajetToto;
                arret2.Trajet = trajetToto;

                this.Trajets.Add(trajetToto);
                try
                {
                    this.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }

            }
        }

        public DbSet<Emplacement> Emplacements { get; set; }
        public DbSet<Arret> Arrets { get; set; }
        public DbSet<Ecole> Ecoles { get; set; }
        public DbSet<Trajet> Trajets { get; set; }
        public DbSet<Voiture> Voitures { get; set; }


        

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Ne pas mettre de Required, sinon EF fait nimporte quoi avec les FK sur des PK et c'est incompréhensible
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Arret>().HasOptional(a => a.Emplacement).WithOptionalDependent(e => e.Arret);
            modelBuilder.Entity<Arret>().HasOptional(a => a.Trajet).WithMany(t => t.Arrets);


            modelBuilder.Entity<Ecole>().HasOptional(e => e.Emplacement).WithOptionalDependent(e => e.Ecole);

            modelBuilder.Entity<Emplacement>().HasOptional(e => e.Arret).WithOptionalPrincipal(a => a.Emplacement);
            modelBuilder.Entity<Emplacement>().HasOptional(e => e.Ecole).WithOptionalPrincipal(e => e.Emplacement);

            modelBuilder.Entity<Trajet>().HasMany(t => t.Arrets).WithOptional(a => a.Trajet);

            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.EmplacementsFavoris);
            modelBuilder.Entity<ApplicationUser>().HasOptional(u => u.Ecole).WithMany(e => e.ListeEleves);
            modelBuilder.Entity<ApplicationUser>().HasOptional(c => c.Voiture).WithOptionalPrincipal(v => v.Proprietaire);
            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.ListeTrajetsPassager).WithMany(t => t.Passagers);
            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.ListeTrajetsConducteur).WithOptional(t => t.Conducteur);

            modelBuilder.Entity<Trajet>().HasMany(t => t.Passagers).WithMany(p => p.ListeTrajetsPassager);
            modelBuilder.Entity<Trajet>().HasOptional(t => t.Conducteur).WithMany(c => c.ListeTrajetsConducteur);



            modelBuilder.Entity<Voiture>().HasOptional(v => v.Proprietaire).WithOptionalDependent(c => c.Voiture);


        }
    }
}