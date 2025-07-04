using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RealEstate.Infrastructure.Context;

public class ApplicationContextFactory: IDesignTimeDbContextFactory<ApplicationContext>
{
    public ApplicationContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

        optionsBuilder.UseSqlServer("Data Source=DESKTOP-DM2MVOJ;Database=RealEstate;Trusted_Connection=true;Encrypt=False;TrustServerCertificate=True;");

        return new ApplicationContext(optionsBuilder.Options);
    }
}