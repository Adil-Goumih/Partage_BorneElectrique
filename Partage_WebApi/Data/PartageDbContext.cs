using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Partage.Models;
using Microsoft.AspNetCore.Identity;

namespace Partage.Data
{
    public class PartageDbContext : IdentityDbContext<User>
    {
        public DbSet<Adresse> Adresses { get; set; }
        public DbSet<Borne> Bornes { get; set; }
        public DbSet<Disponibilite> Disponibilites { get; set; }
        public DbSet<Favoris> Favoris { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        


        public PartageDbContext(DbContextOptions<PartageDbContext> options) : base(options)
        {


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.ApplyConfigurationsFromAssembly(typeof(PartageDbContext).Assembly);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Adresse>().HasData(
    // Adresses pour Longueuil
    new Adresse { AdresseId = 1, NomRue = "Joliette", CodePostal = "J4K4V8", NumCivique = 1033, Ville = "Longueuil", Province = "Québec", Latitude = 45.5249722, Longitude = -73.5004433 },
    
    // Adresses pour Saint-Bruno-de-Montarville
    new Adresse { AdresseId = 2, NomRue = "Dunant", CodePostal = "J3V2K4", NumCivique = 1775, Ville = "Saint-Bruno-de-Montarville", Province = "Québec", Latitude = 45.5166687, Longitude = -73.3453169 },
    
    // Adresses pour Montreal
    new Adresse { AdresseId = 3, NomRue = "Saint-andré", CodePostal = "H2L3T1", NumCivique = 1245, Ville = "Montréal", Province = "Québec", Latitude = 45.516144, Longitude = -73.557228 },
    new Adresse { AdresseId = 4, NomRue = "Côte-des-Neiges", CodePostal = "H3V1A2", NumCivique = 3200, Ville = "Montréal", Province = "Québec", Latitude = 45.502123, Longitude = -73.619888 },
    new Adresse { AdresseId = 5, NomRue = "de Maisonneuve", CodePostal = "H3H1J8", NumCivique = 2200, Ville = "Montréal", Province = "Québec", Latitude = 45.495789, Longitude = -73.577678 },
    new Adresse { AdresseId = 6, NomRue = "Bishop", CodePostal = "H3G2E8", NumCivique = 1400, Ville = "Montréal", Province = "Québec", Latitude = 45.499302, Longitude = -73.576673 },

    // Adresses pour Laval
    new Adresse { AdresseId = 7, NomRue = "Saint-Martin Ouest", CodePostal = "H7S1N2", NumCivique = 2550, Ville = "Laval", Province = "Québec", Latitude = 45.565456, Longitude = -73.752045 },
    new Adresse { AdresseId = 8, NomRue = "Boulevard Curé-Labelle", CodePostal = "H7P2P1", NumCivique = 1600, Ville = "Laval", Province = "Québec", Latitude = 45.618724, Longitude = -73.791781 },

    // Adresses pour Delson
    new Adresse { AdresseId = 9, NomRue = "Montée Saint-Régis", CodePostal = "J5B1X6", NumCivique = 230, Ville = "Delson", Province = "Québec", Latitude = 45.362993, Longitude = -73.541481 },
    new Adresse { AdresseId = 10, NomRue = "Rue Beauvais", CodePostal = "J5B1Y1", NumCivique = 150, Ville = "Delson", Province = "Québec", Latitude = 45.365138, Longitude = -73.542956 },

    // Adresses pour Dorval
    new Adresse { AdresseId = 11, NomRue = "Boulevard des Sources", CodePostal = "H9S3G3", NumCivique = 55, Ville = "Dorval", Province = "Québec", Latitude = 45.446678, Longitude = -73.759451 },
    new Adresse { AdresseId = 12, NomRue = "Rue Elm", CodePostal = "H9S1R7", NumCivique = 1, Ville = "Dorval", Province = "Québec", Latitude = 45.448937, Longitude = -73.750785 },

    // Adresses pour Saint Hubert
    new Adresse { AdresseId = 13, NomRue = "Rue Marmier", CodePostal = "J3Y3M8", NumCivique = 2085, Ville = "Saint Hubert", Province = "Québec", Latitude = 45.502896, Longitude = -73.423557 },
    new Adresse { AdresseId = 14, NomRue = "Rue Victoria", CodePostal = "J3Y6V5", NumCivique = 2120, Ville = "Saint Hubert", Province = "Québec", Latitude = 45.508716, Longitude = -73.415841 },

    // Adresses pour Saint Julie
    new Adresse { AdresseId = 15, NomRue = "Rue Principale", CodePostal = "J3E1Y2", NumCivique = 1985, Ville = "Saint Julie", Province = "Québec", Latitude = 45.590802, Longitude = -73.318055 },
    new Adresse { AdresseId = 16, NomRue = "Rue Saint-Joseph", CodePostal = "J3E1W9", NumCivique = 105, Ville = "Saint Julie", Province = "Québec", Latitude = 45.593841, Longitude = -73.319756 }

            );

            modelBuilder.Entity<Borne>().HasData(

                new Borne {BorneId = 1, TypeConnecteur = "J1772",Puissance = 50,AdresseId = 1},
                new Borne {BorneId = 2, TypeConnecteur = "J1772", Puissance = 150, AdresseId = 2},
                new Borne {BorneId = 3, TypeConnecteur = "J1772", Puissance = 250, AdresseId = 3},
                new Borne {BorneId = 4, TypeConnecteur = "NACS", Puissance = 50, AdresseId = 4},
                new Borne { BorneId = 5, TypeConnecteur = "NACS", Puissance = 150, AdresseId = 5},
                new Borne { BorneId = 6, TypeConnecteur = "NACS", Puissance = 250, AdresseId = 6},
                new Borne { BorneId = 7, TypeConnecteur = "J1772", Puissance = 50, AdresseId = 7 },
                new Borne { BorneId = 8, TypeConnecteur = "NACS", Puissance = 150, AdresseId = 8 },
                new Borne { BorneId = 9, TypeConnecteur = "J1772", Puissance = 250, AdresseId = 9 },
                new Borne { BorneId = 10, TypeConnecteur = "NACS", Puissance = 50, AdresseId = 10 },
                new Borne { BorneId = 11, TypeConnecteur = "J1772", Puissance = 150, AdresseId = 11 },
                new Borne { BorneId = 12, TypeConnecteur = "NACS", Puissance = 250, AdresseId = 12 },
                new Borne { BorneId = 13, TypeConnecteur = "J1772", Puissance = 7, AdresseId = 13 },
                new Borne { BorneId = 14, TypeConnecteur = "NACS", Puissance = 25, AdresseId = 14 },
                new Borne { BorneId = 15, TypeConnecteur = "J1772", Puissance = 50, AdresseId = 15 },
                new Borne { BorneId = 16, TypeConnecteur = "NACS", Puissance = 150, AdresseId = 16 }
            );



            modelBuilder.Entity<User>().HasData(

                new User{
                    Id = "1",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<User>().HashPassword(null, "Admin1234!"),
                    SecurityStamp = string.Empty,
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Nom = "Adil",
                    Prenom = "Allessi"
                });


        }

    }
}
