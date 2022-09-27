using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data.Contexts
{
    public sealed class TemplateDbContext : DbContext
    {
        public TemplateDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<OperationClaim>(o =>
           {
               o.ToTable("OperationClaims").HasKey(u => u.Id);
               o.Property(o => o.Id).HasColumnName("Id");
               o.Property(o => o.Name).HasColumnName("Name");
           });


            modelBuilder.Entity<User>(u =>
            {
                u.ToTable("Users").HasKey(u => u.Id);
                u.Property(u => u.Id).HasColumnName("Id");
                u.Property(u => u.Email).HasColumnName("Email").IsRequired();
                u.Property(u => u.FirstName).HasColumnName("FirstName").IsRequired();
                u.Property(u => u.LastName).HasColumnName("LastName").IsRequired();
                u.Property(u => u.UserName).HasColumnName("UserName").IsRequired();
                u.Property(u => u.Status).HasColumnName("Status").HasDefaultValue(true);
                u.Property(u => u.PasswordHash).HasColumnName("PasswordHash").IsRequired();
                u.Property(u => u.PasswordSalt).HasColumnName("PasswordSalt").IsRequired();


                // u.HasMany(e=>e.UserOperationClaims).WithOne(u=>u.User).HasForeignKey(uop=>uop.UserId);

                u.HasMany(e => e.UserOperationClaims);

            });

            modelBuilder.Entity<UserOperationClaim>(uop =>
            {
                uop.ToTable("UserOperationClaims").HasKey(uop => uop.Id);

                uop.Property(uop => uop.Id).HasColumnName("Id");
                uop.Property(uop => uop.UserId).HasColumnName("UserId").IsRequired();
                uop.Property(uop => uop.OperationClaimId).HasColumnName("OperationClaimId").IsRequired();

                // uop.HasOne(e=>e.User).WithOne().HasForeignKey<UserOperationClaim>(uop=>uop.UserId);
                uop.HasOne(e => e.User);

                // uop.HasOne(e=>e.OperationClaim).WithOne().HasForeignKey<UserOperationClaim>(uop=>uop.OperationClaimId);
                uop.HasOne(e => e.OperationClaim);
            });



            OperationClaim[] operationClaimSeedData = { new("3de010d3-2133-46d9-837b-4daa114d5f01", "Admin"), new("482741e3-36a5-4193-9b5d-4be5de5bfa2d", "User") };

            modelBuilder.Entity<OperationClaim>().HasData(operationClaimSeedData);
        }

    }
}
