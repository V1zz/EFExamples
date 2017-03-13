using System.Data.Entity;
using CarShop.DAL;
using CarShop.Models;

namespace CarShop.Tests.Fake
{
    public class FakeDbContext : CarShopContext
    {
        public override DbSet<CarBrands> CarBrands => new FakeDbSet<CarBrands>();

        public override int SaveChanges()
        {
            return 1; 
        }
    }
}
