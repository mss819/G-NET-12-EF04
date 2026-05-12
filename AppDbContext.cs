using G_NET_12_EF04.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_NET_12_EF04
{
    internal class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=Bank_System;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manager>(En =>
            {
                En.ToTable("Managers").HasKey(e => e.Id);
                En.Property(e => e.FullName).HasMaxLength(50).IsRequired();
                En.Property(e => e.Email).HasMaxLength(50).IsRequired();
                En.Property(e => e.HireDate).IsRequired();
            });

            modelBuilder.Entity<Branch>()
               .HasOne(b => b.Manager)
               .WithOne(m => m.Branch)
               .HasForeignKey<Manager>(m => m.BranchCode);


            modelBuilder.Entity<Account>()
                .HasOne(a => a.Branch)
                .WithMany(b => b.Accounts)
                .HasForeignKey(a => a.BranchCode);

            modelBuilder.Entity<AccountCustomer>()
                .HasKey(ca => new { ca.CustomerId, ca.AccountNumber });

            modelBuilder.Entity<AccountCustomer>()
                .HasOne(ca => ca.Customer)
                .WithMany(c => c.AccountCustomers)
                .HasForeignKey(ca => ca.CustomerId);

            modelBuilder.Entity<AccountCustomer>()
                .HasOne(ca => ca.Account)
                .WithMany(a => a.AccountCustomers)
                .HasForeignKey(ca => ca.AccountNumber);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.AccountNumber);

            modelBuilder.Entity<Branch>().HasData(
                new Branch
                {
                    Code = "B001",
                    Name = "Cairo Branch",
                    Address = "Nasr City",
                    PhoneNumber = "0135677000"
                },
                new Branch
                {
                    Code = "B002",
                    Name = "Alex Branch",
                    Address = "Smouha",
                    PhoneNumber = "011345311"
                });
        }

        // DbSet properties must be inside the AppDbContext class
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<AccountCustomer> AccountCustomers { get; set; }
    }
}
