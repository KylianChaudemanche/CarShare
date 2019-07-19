using CarShare.BO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarShare.Models
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        

        public ApplicationDbContext()
            : base("ApplicationDbContext", throwIfV1Schema: false)
        {
            this.Database.CreateIfNotExists();
        }

        public DbSet<Emplacement> Emplacements { get; set; }
        public DbSet<Arret> Arrets { get; set; }
        public DbSet<Ecole> Ecoles { get; set; }
        public DbSet<trajet> Trajets { get; set; }
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

            modelBuilder.Entity<trajet>().HasMany(t => t.Arrets).WithOptional(a => a.Trajet);

            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.EmplacementsFavoris);
            modelBuilder.Entity<ApplicationUser>().HasOptional(u => u.Ecole).WithMany(e => e.ListeEleves);
            modelBuilder.Entity<ApplicationUser>().HasOptional(c => c.Voiture).WithOptionalPrincipal(v => v.Proprietaire);
            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.ListeTrajetsPassager).WithMany(t => t.Passagers);
            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.ListeTrajetsConducteur).WithOptional(t => t.Conducteur);

            modelBuilder.Entity<trajet>().HasMany(t => t.Passagers).WithMany(p => p.ListeTrajetsPassager);
            modelBuilder.Entity<trajet>().HasOptional(t => t.Conducteur).WithMany(c => c.ListeTrajetsConducteur);



            modelBuilder.Entity<Voiture>().HasOptional(v => v.Proprietaire).WithOptionalDependent(c => c.Voiture);


        }
    }
}