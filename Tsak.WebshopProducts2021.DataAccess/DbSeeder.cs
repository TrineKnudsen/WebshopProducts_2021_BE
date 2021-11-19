using Tsak.WebshopProducts2021.DataAccess.Entities;

namespace Tsak.WebshopProducts2021.DataAccess
{
    public class DbSeeder
    {
        private readonly MainDBContext _ctx;
        public DbSeeder(MainDBContext ctx)
        {
            _ctx = ctx;
        }

        public void SeedDevelopment()
        {
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();
            _ctx.Users.Add(new UserEntity { Name = "Bilbo" });
            _ctx.SaveChanges();
            _ctx.Products.AddRange(new ProductEntity {Name = "Ost", OwnerId = 1});
            _ctx.Products.AddRange(new ProductEntity {Name = "OsteKage", OwnerId = 1});
            _ctx.Products.AddRange(new ProductEntity {Name = "Brie", OwnerId = 1});
            _ctx.SaveChanges();
        }

        public void SeedProduction()
        {
            _ctx.Database.EnsureCreated();
        }
    }
}