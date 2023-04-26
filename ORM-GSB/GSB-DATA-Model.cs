using Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ORM_GSB
{
    public partial class GSB_DATA_Model : DbContext
    {
        public GSB_DATA_Model()
            : base("name=GSBDATAModel")
        {
        }

        public virtual DbSet<Departement> Departements { get; set; }
        public virtual DbSet<Medecin> Medecins { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departement>()
                .Property(e => e.dep_name)
                .IsUnicode(false);

            modelBuilder.Entity<Departement>()
                .Property(e => e.region_name)
                .IsUnicode(false);

            modelBuilder.Entity<Departement>()
                .HasMany(e => e.Medecins)
                .WithRequired(e => e.Departement)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Medecin>()
                .Property(e => e.Prenom)
                .IsUnicode(false);

            modelBuilder.Entity<Medecin>()
                .Property(e => e.Nom)
                .IsUnicode(false);

            modelBuilder.Entity<Medecin>()
                .Property(e => e.Adresse)
                .IsUnicode(false);

            modelBuilder.Entity<Medecin>()
                .Property(e => e.Telephone)
                .IsUnicode(false);
        }
    }
}
