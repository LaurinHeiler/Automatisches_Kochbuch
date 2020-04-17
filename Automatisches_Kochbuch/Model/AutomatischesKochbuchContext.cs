using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Automatisches_Kochbuch.Model
{
    public partial class AutomatischesKochbuchContext : DbContext
    {
        public AutomatischesKochbuchContext()
        {
        }

        public AutomatischesKochbuchContext(DbContextOptions<AutomatischesKochbuchContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LnkTabRezeptZutaten> LnkTabRezeptZutaten { get; set; }
        public virtual DbSet<LnkTabUserRezepte> LnkTabUserRezepte { get; set; }
        public virtual DbSet<LnkTabUserZutaten> LnkTabUserZutaten { get; set; }
        public virtual DbSet<TabRezepte> TabRezepte { get; set; }
        public virtual DbSet<TabUser> TabUser { get; set; }
        public virtual DbSet<TabZutaten> TabZutaten { get; set; }
        public virtual DbSet<TabZutatenEinheit> TabZutatenEinheit { get; set; }
        public virtual DbSet<TabZutatenKategorien> TabZutatenKategorien { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("DataSource=localhost;DataBase=automatisches_kochbuch;UserID=root;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LnkTabRezeptZutaten>(entity =>
            {
                entity.ToTable("lnk_tab_rezept_zutaten");

                entity.HasIndex(e => e.IdRezept)
                    .HasName("fk_rezept");

                entity.HasIndex(e => e.IdZutaten)
                    .HasName("fk_zutaten");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdRezept)
                    .HasColumnName("ID_rezept")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdZutaten)
                    .HasColumnName("ID_zutaten")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Menge).HasColumnType("decimal(11,3)");

                entity.HasOne(d => d.IdRezeptNavigation)
                    .WithMany(p => p.LnkTabRezeptZutaten)
                    .HasForeignKey(d => d.IdRezept)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("lnk_tab_rezept_zutaten_ibfk_1");

                entity.HasOne(d => d.IdZutatenNavigation)
                    .WithMany(p => p.LnkTabRezeptZutaten)
                    .HasForeignKey(d => d.IdZutaten)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("lnk_tab_rezept_zutaten_ibfk_2");
            });

            modelBuilder.Entity<LnkTabUserRezepte>(entity =>
            {
                entity.ToTable("lnk_tab_user_rezepte");

                entity.HasIndex(e => e.IdRezepte)
                    .HasName("fk_rezepte");

                entity.HasIndex(e => e.IdUser)
                    .HasName("fk_user");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AnzahlPortionen)
                    .HasColumnName("Anzahl_Portionen")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdRezepte)
                    .HasColumnName("ID_rezepte")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUser)
                    .HasColumnName("ID_user")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdRezepteNavigation)
                    .WithMany(p => p.LnkTabUserRezepte)
                    .HasForeignKey(d => d.IdRezepte)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("lnk_tab_user_rezepte_ibfk_1");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.LnkTabUserRezepte)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("lnk_tab_user_rezepte_ibfk_2");
            });

            modelBuilder.Entity<LnkTabUserZutaten>(entity =>
            {
                entity.ToTable("lnk_tab_user_zutaten");

                entity.HasIndex(e => e.IdUser)
                    .HasName("fk_user");

                entity.HasIndex(e => e.IdZutaten)
                    .HasName("fk_zutaten");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUser)
                    .HasColumnName("ID_user")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdZutaten)
                    .HasColumnName("ID_zutaten")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Menge).HasColumnType("int(11)");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.LnkTabUserZutaten)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("lnk_tab_user_zutaten_ibfk_1");

                entity.HasOne(d => d.IdZutatenNavigation)
                    .WithMany(p => p.LnkTabUserZutaten)
                    .HasForeignKey(d => d.IdZutaten)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("lnk_tab_user_zutaten_ibfk_2");
            });

            modelBuilder.Entity<TabRezepte>(entity =>
            {
                entity.ToTable("tab_rezepte");

                entity.HasIndex(e => e.Rezeptname)
                    .HasName("Rezeptname")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Bild)
                    .IsRequired()
                    .HasColumnType("mediumblob");

                entity.Property(e => e.Glutenfrei)
                    .HasColumnName("glutenfrei")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.Rezeptname)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Vegan)
                    .HasColumnName("vegan")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.Vegetarisch)
                    .HasColumnName("vegetarisch")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.Zubereitung)
                    .IsRequired()
                    .HasColumnType("text");
            });

            modelBuilder.Entity<TabUser>(entity =>
            {
                entity.ToTable("tab_user");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AnzahlPortionen)
                    .HasColumnName("Anzahl Portionen")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Glutenfrei)
                    .HasColumnName("glutenfrei")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.Nachname)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Passwort)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Veganer).HasColumnType("tinyint(1)");

                entity.Property(e => e.Vegetarier).HasColumnType("tinyint(1)");

                entity.Property(e => e.Vorname)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<TabZutaten>(entity =>
            {
                entity.ToTable("tab_zutaten");

                entity.HasIndex(e => e.Zutat)
                    .HasName("Zutat")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Glutenfrei)
                    .HasColumnName("glutenfrei")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.IdZutatEinheit)
                    .HasColumnName("ID_Zutat_Einheit")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdZutatKategorie)
                    .HasColumnName("ID_Zutat_Kategorie")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Vegan)
                    .HasColumnName("vegan")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Vegetarisch)
                    .HasColumnName("vegetarisch")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Zutat)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<TabZutatenEinheit>(entity =>
            {
                entity.ToTable("tab_zutaten_einheit");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Einheit)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.EinheitKuerzel)
                    .IsRequired()
                    .HasColumnName("Einheit_Kuerzel")
                    .HasColumnType("varchar(15)");
            });

            modelBuilder.Entity<TabZutatenKategorien>(entity =>
            {
                entity.ToTable("tab_zutaten_kategorien");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Kategorie)
                    .IsRequired()
                    .HasColumnType("varchar(100)");
            });
        }
    }
}
