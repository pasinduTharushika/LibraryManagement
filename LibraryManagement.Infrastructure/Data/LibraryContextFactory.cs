using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LibraryManagement.Infrastructure.Data
{
    public class LibraryContextFactory : IDesignTimeDbContextFactory<LibraryContext>
    {
        public LibraryContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LibraryContext>();
            // Use your connection string here (same as in appsettings.json)
             optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=LIBRARY_DB;User Id=sa;Password=Pasi@123;TrustServerCertificate=True;");

            return new LibraryContext(optionsBuilder.Options);
        }
    }
}
