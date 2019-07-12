using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CarShare.Models
{
    // Vous pouvez ajouter des données de profil pour l'utilisateur en ajoutant d'autres propriétés à votre classe ApplicationUser. Pour en savoir plus, consultez https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Notez qu'authenticationType doit correspondre à l'élément défini dans CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Ajouter les revendications personnalisées de l’utilisateur ici
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ApplicationDbContext", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<BO.Emplacement> Emplacements { get; set; }

        public System.Data.Entity.DbSet<BO.Arret> Arrets { get; set; }

        public System.Data.Entity.DbSet<BO.Conducteur> Conducteurs { get; set; }

        public System.Data.Entity.DbSet<BO.Ecole> Ecoles { get; set; }

        public System.Data.Entity.DbSet<BO.Trajet> Trajets { get; set; }

        public System.Data.Entity.DbSet<BO.Utilisateur> Utilisateurs { get; set; }

        public System.Data.Entity.DbSet<BO.Voiture> Voitures { get; set; }
    }
}