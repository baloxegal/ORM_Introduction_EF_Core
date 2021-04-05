using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ORM_Introduction_EF_Core
{
    public partial class ACDBContext : DbContext
    {
        public ACDBContext()
        {
            //It is if we create a database with CodFirst method
            Database.EnsureCreated();

            //If we use DatabaseFirst method (reverse engineering) - use it:
            //Scaffold-DbContext "Server=(localdb)\mssqllocaldb;Database=ACDBC;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer
        }

        public ACDBContext(DbContextOptions<ACDBContext> options)
            : base(options)
        {           
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<PackGrade> PackGrades { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<Sector> Sectors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ACDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customers");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_Id");

                entity.Property(e => e.BirthDate)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Birth_Date");

                entity.Property(e => e.City)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("fax");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("First_Name");

                entity.Property(e => e.JoinDate)
                    .HasColumnType("date")
                    .HasColumnName("Join_Date");

                entity.Property(e => e.LastName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Last_Name");

                entity.Property(e => e.MainPhoneNum)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("main_phone_num");

                entity.Property(e => e.MonthlyDiscount)
                    .HasColumnType("decimal(4, 2)")
                    .HasColumnName("monthly_discount");

                entity.Property(e => e.PackId).HasColumnName("pack_id");

                entity.Property(e => e.SecondaryPhoneNum)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("secondary_phone_num");

                entity.Property(e => e.State)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.Pack)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.PackId)
                    .HasConstraintName("pck_id_fk");
            });

            modelBuilder.Entity<PackGrade>(entity =>
            {
                entity.HasKey(e => e.GradeId)
                    .HasName("grade_id_pk");

                entity.ToTable("pack_grades");

                entity.Property(e => e.GradeId).HasColumnName("grade_id");

                entity.Property(e => e.GradeName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("grade_name");

                entity.Property(e => e.MaxPrice).HasColumnName("max_price");

                entity.Property(e => e.MinPrice).HasColumnName("min_price");
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.HasKey(e => e.PackId)
                    .HasName("pack_id_pk");

                entity.ToTable("packages");

                entity.Property(e => e.PackId).HasColumnName("pack_id");

                entity.Property(e => e.MonthlyPayment).HasColumnName("monthly_payment");

                entity.Property(e => e.SectorId).HasColumnName("sector_id");

                entity.Property(e => e.Speed)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("speed");

                entity.Property(e => e.StrtDate)
                    .HasColumnType("date")
                    .HasColumnName("strt_date");

                entity.HasOne(d => d.Sector)
                    .WithMany(p => p.Packages)
                    .HasForeignKey(d => d.SectorId)
                    .HasConstraintName("sec_id_fk");
            });

            modelBuilder.Entity<Sector>(entity =>
            {
                entity.ToTable("sectors");

                entity.Property(e => e.SectorId).HasColumnName("sector_id");

                entity.Property(e => e.SectorName)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("sector_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
