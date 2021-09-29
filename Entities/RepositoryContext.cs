using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Entreprise> Entreprises { get; set; }
        public DbSet<Telephone> Telephones { get; set; }
        public DbSet<Email> Email { get; set; }
        public DbSet<EntrepriseUser> EntrepriseUsers { get; set; }
        public DbSet<Livraison> Livraisons { get; set; }
        public DbSet<Payement> Payements { get; set; }
        public DbSet<Offre> Offres { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Marque> Marques { get; set; }
        public DbSet<Taille> Tailles { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<Gamme> Gammes { get; set; }
        public DbSet<Achat_Matiere> Achat_Matieres { get; set; }
        public DbSet<Conso_Matiere> Conso_Matieres { get; set; }
        public DbSet<Matière_Premiere> Matière_Premieres { get; set; }
        public DbSet<Vente> Ventes { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<ExternalLogin> ExternalLogins { get; set; }
        public DbSet<Panier> Paniers { get; set; }
        public DbSet<Num_Payement> Num_Payements { get; set; }
        public DbSet<Num_Vente> num_Ventes { get; set; }
        public DbSet<Avarier> Avariers { get; set; }
        public DbSet<Produit_Fini> Produit_Finis { get; set; }
        public DbSet<Stock_Produit> Stock_Produits { get; set; }
        public DbSet<Quartier> Quartiers { get; set; }
        public DbSet<Pays> Pays { get; set; }
        public DbSet<Ville> Villes { get; set; }
        public DbSet<Commune> Communes { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<Continent> Continents { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Fournisseur> Fournisseurs { get; set; }
    }
}
