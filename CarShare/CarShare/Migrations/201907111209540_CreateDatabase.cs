namespace CarShare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ville = c.String(),
                        CodePostal = c.String(),
                        Rue = c.String(),
                        Latitude = c.String(),
                        Longitude = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Arrets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Horaire = c.DateTime(nullable: false),
                        Adresse_Id = c.Int(),
                        Trajet_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Adresses", t => t.Adresse_Id)
                .ForeignKey("dbo.Trajets", t => t.Trajet_Id)
                .Index(t => t.Adresse_Id)
                .Index(t => t.Trajet_Id);
            
            CreateTable(
                "dbo.Utilisateurs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsActif = c.Boolean(nullable: false),
                        Nom = c.String(),
                        Prenom = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Telephone = c.String(),
                        Promotion = c.String(),
                        IsAdmin = c.Boolean(nullable: false),
                        Description = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Voiture_Id = c.Int(),
                        Adresse_Id = c.Int(),
                        Ecole_Id = c.Int(),
                        Trajet_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Voitures", t => t.Voiture_Id)
                .ForeignKey("dbo.Adresses", t => t.Adresse_Id)
                .ForeignKey("dbo.Ecoles", t => t.Ecole_Id)
                .ForeignKey("dbo.Trajets", t => t.Trajet_Id)
                .Index(t => t.Voiture_Id)
                .Index(t => t.Adresse_Id)
                .Index(t => t.Ecole_Id)
                .Index(t => t.Trajet_Id);
            
            CreateTable(
                "dbo.Ecoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Adresse_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Adresses", t => t.Adresse_Id)
                .Index(t => t.Adresse_Id);
            
            CreateTable(
                "dbo.Voitures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NbPlaces = c.Int(nullable: false),
                        Immatriculation = c.String(),
                        Couleur = c.String(),
                        Marque = c.String(),
                        Modele = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Trajets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Arrive_Id = c.Int(),
                        Conducteur_Id = c.Int(),
                        Depart_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Adresses", t => t.Arrive_Id)
                .ForeignKey("dbo.Utilisateurs", t => t.Conducteur_Id)
                .ForeignKey("dbo.Adresses", t => t.Depart_Id)
                .Index(t => t.Arrive_Id)
                .Index(t => t.Conducteur_Id)
                .Index(t => t.Depart_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Utilisateurs", "Trajet_Id", "dbo.Trajets");
            DropForeignKey("dbo.Utilisateurs", "Ecole_Id", "dbo.Ecoles");
            DropForeignKey("dbo.Utilisateurs", "Adresse_Id", "dbo.Adresses");
            DropForeignKey("dbo.Trajets", "Depart_Id", "dbo.Adresses");
            DropForeignKey("dbo.Trajets", "Conducteur_Id", "dbo.Utilisateurs");
            DropForeignKey("dbo.Trajets", "Arrive_Id", "dbo.Adresses");
            DropForeignKey("dbo.Arrets", "Trajet_Id", "dbo.Trajets");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Utilisateurs", "Voiture_Id", "dbo.Voitures");
            DropForeignKey("dbo.Ecoles", "Adresse_Id", "dbo.Adresses");
            DropForeignKey("dbo.Arrets", "Adresse_Id", "dbo.Adresses");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Trajets", new[] { "Depart_Id" });
            DropIndex("dbo.Trajets", new[] { "Conducteur_Id" });
            DropIndex("dbo.Trajets", new[] { "Arrive_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Ecoles", new[] { "Adresse_Id" });
            DropIndex("dbo.Utilisateurs", new[] { "Trajet_Id" });
            DropIndex("dbo.Utilisateurs", new[] { "Ecole_Id" });
            DropIndex("dbo.Utilisateurs", new[] { "Adresse_Id" });
            DropIndex("dbo.Utilisateurs", new[] { "Voiture_Id" });
            DropIndex("dbo.Arrets", new[] { "Trajet_Id" });
            DropIndex("dbo.Arrets", new[] { "Adresse_Id" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Trajets");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Voitures");
            DropTable("dbo.Ecoles");
            DropTable("dbo.Utilisateurs");
            DropTable("dbo.Arrets");
            DropTable("dbo.Adresses");
        }
    }
}
