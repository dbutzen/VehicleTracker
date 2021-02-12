using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DTB.VehicleTracker.PL
{
    public partial class VehicleEntities : DbContext
    {
        public VehicleEntities()
        {
        }

        public VehicleEntities(DbContextOptions<VehicleEntities> options)
            : base(options)
        {
        }

        public virtual DbSet<tblColor> tblColors { get; set; }
        public virtual DbSet<tblMake> tblMakes { get; set; }
        public virtual DbSet<tblModel> tblModels { get; set; }
        public virtual DbSet<tblVehicle> tblVehicles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectsV13;Database=DTB.VehicleTracker.DB;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<tblColor>(entity =>
            {
                entity.ToTable("tblColor");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<tblMake>(entity =>
            {
                entity.ToTable("tblMake");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<tblModel>(entity =>
            {
                entity.ToTable("tblModel");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<tblVehicle>(entity =>
            {
                entity.ToTable("tblVehicle");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.VIN)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("VIN");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.tblVehicles)
                    .HasForeignKey(d => d.ColorId)
                    .HasConstraintName("tblVehicle_ColorId");

                entity.HasOne(d => d.Make)
                    .WithMany(p => p.tblVehicles)
                    .HasForeignKey(d => d.MakeId)
                    .HasConstraintName("tblVehicle_MakeId");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.tblVehicles)
                    .HasForeignKey(d => d.ModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tblVehicle_ModelId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
