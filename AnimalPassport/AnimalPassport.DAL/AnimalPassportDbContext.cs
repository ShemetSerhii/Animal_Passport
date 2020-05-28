using System;
using AnimalPassport.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnimalPassport.DataAccess
{
    public class AnimalPassportDbContext : DbContext
    {
        public AnimalPassportDbContext(DbContextOptions<AnimalPassportDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<Animal> Animal { get; set; }

        public DbSet<MedicalOperation> MedicalOperation { get; set; }

        public DbSet<Attachment> Attachment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role { Id = Guid.NewGuid(), Name = "Власник домашньої тварини"});
            modelBuilder.Entity<Role>().HasData(new Role { Id = Guid.NewGuid(), Name = "Ветеринар" });
            modelBuilder.Entity<Role>().HasData(new Role { Id = Guid.NewGuid(), Name = "Член контрольних органів" });
        }
    }
}