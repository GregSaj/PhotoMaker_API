using Microsoft.EntityFrameworkCore;
using PhotoMagic.Database.Entities;
using PhotoMagic.Models;

namespace PhotoMagic.Database
{
    public class PhotoMagicDbContext: DbContext
    {
        public PhotoMagicDbContext(DbContextOptions<PhotoMagicDbContext> option) : base(option)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
