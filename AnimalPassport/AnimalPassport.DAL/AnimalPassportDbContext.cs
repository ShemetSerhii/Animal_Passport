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

        public DbSet<Animal> Animal { get; set; }

        public DbSet<MedicalOperation> MedicalOperation { get; set; }

        public DbSet<Attachment> Attachment { get; set; }
    }
}