namespace Advertisements.Data
{
    using Advertisements.Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    public interface IAdsData
    {
        IRepository<ApplicationUser> Users { get; }

        IRepository<IdentityRole> UserRoles { get; }

        IRepository<Advertisement> Advertisements { get; }

        IRepository<Town> Towns { get; }

        IRepository<Category> Categories { get; }

        int SaveChanges();
    }
}
