using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AnimalPassport.DataAccess
{
    public class DbContextFactory : IDesignTimeDbContextFactory<AnimalPassportDbContext>
    {
        public AnimalPassportDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AnimalPassportDbContext>();

            optionsBuilder.UseSqlServer("Server=.;Database=PetPassport;Trusted_Connection=True;");

            return new AnimalPassportDbContext(optionsBuilder.Options);
        }
    }
}