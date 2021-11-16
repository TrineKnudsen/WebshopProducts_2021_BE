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
            _ctx.Products.Add(new ProductEntity {Name = "Ost"});
            _ctx.Products.Add(new ProductEntity {Name = "OsteKage"});
            _ctx.Products.Add(new ProductEntity {Name = "Brie"});
            _ctx.Products.Add(new ProductEntity {Name = "Ostesovs"});
            _ctx.SaveChanges();
        }

        public void SeedProduction()
        {
            _ctx.Database.EnsureCreated();
        }
    }
}