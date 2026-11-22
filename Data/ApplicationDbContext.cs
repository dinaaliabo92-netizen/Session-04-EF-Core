using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Session_04_EF_Core.Models;

namespace Session_04_EF_Core.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<Branch> Branches { get; set; } = null!;

        public DbSet<Manager> Managers { get; set; } = null!;

        public DbSet<Account> Accounts { get; set; } = null!;

        public DbSet<Customer> Customers { get; set; } = null!;

        public DbSet<Transaction> Transactions { get; set; } = null!;

        public DbSet<CustomerAccount> CustomerAccounts { get; set; } = null!;

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=.;Database=NationalBankDB;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed Branches
            modelBuilder.Entity<Branch>().HasData(
                new Branch
                {
                    Code = 1,
                    Name = "Cairo Branch",
                    Address = "Nasr City, Cairo",
                    PhoneNumber = "01012345678"
                },
                new Branch
                {
                    Code = 2,
                    Name = "Alexandria Branch",
                    Address = "Smouha, Alexandria",
                    PhoneNumber = "01112345678"
                }
            );

            // Seed Managers
            modelBuilder.Entity<Manager>().HasData(
                new Manager
                {
                    Id = 1,
                    FullName = "Ahmed Mohamed",
                    Email = "ahmed@bank.com",
                    PhoneNumber = "01212345678",
                    HireDate = new DateTime(2020, 1, 1),
                    BranchCode = 1
                },
                new Manager
                {
                    Id = 2,
                    FullName = "Mona Ali",
                    Email = "mona@bank.com",
                    PhoneNumber = "01512345678",
                    HireDate = new DateTime(2021, 5, 1),
                    BranchCode = 2
                }
            );
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.HasKey(b => b.Code);

                entity.Property(b => b.Name)
                      .IsRequired();

                entity.Property(b => b.Address)
                      .IsRequired();

                entity.Property(b => b.PhoneNumber)
                      .IsRequired();
            });


            modelBuilder.Entity<Manager>(entity =>
            {
                entity.HasKey(m => m.Id);

                entity.Property(m => m.FullName)
                      .IsRequired();

                entity.Property(m => m.Email)
                      .IsRequired();

                entity.Property(m => m.PhoneNumber)
                      .IsRequired();

                entity.HasIndex(m => m.BranchCode)
                      .IsUnique();
            });


            modelBuilder.Entity<Branch>()
                .HasOne(b => b.Manager)
                .WithOne(m => m.Branch)
                .HasForeignKey<Manager>(m => m.BranchCode)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(a => a.AccountNumber);

                entity.Property(a => a.AccountNumber)
                      .IsRequired();

                entity.Property(a => a.CurrentBalance)
                      .HasPrecision(18, 2);

                entity.Property(a => a.AccountType)
                      .IsRequired();
            });


            modelBuilder.Entity<Account>()
                .HasOne(a => a.Branch)
                .WithMany(b => b.Accounts)
                .HasForeignKey(a => a.BranchCode)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.FullName)
                      .IsRequired();

                entity.Property(c => c.Address)
                      .IsRequired();

                entity.Property(c => c.Email)
                      .IsRequired();

                entity.Property(c => c.PhoneNumber)
                      .IsRequired();

                entity.Property(c => c.NationalId)
                      .IsRequired();

                entity.Property(c => c.CustomerType)
                      .IsRequired();
            });


            modelBuilder.Entity<CustomerAccount>(entity =>
            {
                entity.HasKey(ca => new
                {
                    ca.AccountNumber,
                    ca.CustomerId
                });

                entity.Property(ca => ca.OwnershipType)
                      .IsRequired();

                entity.Property(ca => ca.OwnershipStartDate)
                      .IsRequired();

                entity.Property(ca => ca.AccountStatus)
                      .IsRequired();
            });

            modelBuilder.Entity<CustomerAccount>()
                .HasOne(ca => ca.Account)
                .WithMany(a => a.CustomerAccounts)
                .HasForeignKey(ca => ca.AccountNumber)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CustomerAccount>()
                .HasOne(ca => ca.Customer)
                .WithMany(c => c.CustomerAccounts)
                .HasForeignKey(ca => ca.CustomerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(t => t.TransactionNumber);

                entity.Property(t => t.Amount)
                      .HasPrecision(18, 2);

                entity.Property(t => t.Note)
                      .IsRequired();

                entity.Property(t => t.TransactionType)
                      .IsRequired();
            });

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.AccountNumber)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }

    }

}
