using CarShare.BO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace CarShare.Models
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {


        public ApplicationDbContext()
            : base("ApplicationDbContext", throwIfV1Schema: false)
        {
            if (this.Database.CreateIfNotExists())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(this));
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this));

                if (!roleManager.RoleExists("SuperAdmin"))
                {
                    var role = new Role();
                    role.Name = "SuperAdmin";
                    role.Force = 0;
                    roleManager.Create(role);

                }
                this.SaveChanges();
                if (!roleManager.RoleExists("Admin"))
                {
                    var role = new Role();
                    role.Name = "Admin";
                    role.Force = 1;
                    roleManager.Create(role);

                }
                this.SaveChanges();
                if (!roleManager.RoleExists("Utilisateur"))
                {
                    var role = new Role();
                    role.Name = "Utilisateur";
                    role.Force = 2;
                    roleManager.Create(role);

                }
                this.SaveChanges();
                if (!roleManager.RoleExists("AncienUtilisateur"))
                {
                    var role = new Role();
                    role.Name = "AncienUtilisateur";
                    role.Force = 99;
                    roleManager.Create(role);

                }
                this.SaveChanges();

                var emplacement = new Emplacement() { Id = 1, Intitule = "ENI Rennes", Description = "L'école ENI de Chartres de Bretagne", Latitude = (long)48.038909, Longitude = (long)-1.692360 };
                List<Ecole> EcolesDispo = new List<Ecole>();
                EcolesDispo.Add(new Ecole() { Id = 1, Nom = "ENI RENNES", Emplacement = emplacement });
                var user = new ApplicationUser { Id = "1", Nom = "Admin", Prenom = "Admin", PhoneNumber = "1234567890", Ecole = EcolesDispo[0], UserName = "admin@admin.fr", Email = "admin@admin.fr", PasswordHash = "ACKvy/muH2GcO6HZB37rG4zlvaMhb3yGSse97Sc6j5a4sAOIAjkzquhLL8i0W/xrEQ==", SecurityStamp = "AMexbWsgbv606c/Gk7dpovwTd9KdAbzb381tiGTvfVwOd4fCaIfKgVFFjLi0Im7sPg==" };
                this.Users.Add(user);
                this.SaveChanges();
                UserManager.AddToRole(user.Id, "SuperAdmin");
                UserManager.AddToRole(user.Id, "Admin");
                UserManager.AddToRole(user.Id, "Utilisateur");
                this.SaveChanges();

                // Créations Emplacements
                Emplacement emplacementEniRennes = new Emplacement() { Id = 1, Description = "Campus de Chartre-de-Bretagne", Intitule = "ENI Rennes", Latitude = 48.038919F, Longitude = -1.692393F };
                Emplacement emplacementEniNantes = new Emplacement() { Id = 1, Description = "Campus de Saint-Herblain", Intitule = "ENI Nantes", Latitude = 47.226717F, Longitude = -1.620898F };
                Emplacement emplacementEniNiort = new Emplacement() { Id = 1, Description = "Campus de Niort", Intitule = "ENI Niort", Latitude = 46.316156F, Longitude = -0.471065F };

                // Création Ecole
                Ecole ecoleEniRennes = new Ecole() { Id = 1, Emplacement = emplacementEniRennes, Nom = "Chartre-de-Bretagne" };
                Ecole ecoleEniNantes = new Ecole() { Id = 1, Emplacement = emplacementEniNantes, Nom = "Saint-Herblain" };
                Ecole ecoleEniNiort = new Ecole() { Id = 1, Emplacement = emplacementEniNiort, Nom = "Niort" };

                this.Ecoles.Add(ecoleEniRennes);
                this.Ecoles.Add(ecoleEniNantes);
                this.Ecoles.Add(ecoleEniNiort);
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
        public DbSet<Role> Role { get; set; }




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
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.ListeVoitures).WithOptional(v => v.Proprietaire);
            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.ListeTrajetsPassager).WithMany(t => t.Passagers);
            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.ListeTrajetsConducteur).WithOptional(t => t.Conducteur);

            modelBuilder.Entity<Trajet>().HasMany(t => t.Passagers).WithMany(p => p.ListeTrajetsPassager);
            modelBuilder.Entity<Trajet>().HasOptional(t => t.Conducteur).WithMany(c => c.ListeTrajetsConducteur);

            modelBuilder.Entity<Voiture>().HasOptional(v => v.Proprietaire).WithMany(c => c.ListeVoitures);


        }
    }
}