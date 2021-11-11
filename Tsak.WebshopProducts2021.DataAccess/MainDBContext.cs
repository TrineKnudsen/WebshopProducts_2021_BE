using Microsoft.EntityFrameworkCore;
using Tsak.WebshopProducts2021.DataAccess.Entities;

namespace Tsak.WebshopProducts2021.DataAccess
{
    public class MainDBContext: DbContext
    {
        public MainDBContext(DbContextOptions<MainDBContext> options): base(options) { }
        public DbSet<ProductEntity> Products { get; set; }
        
    }
}