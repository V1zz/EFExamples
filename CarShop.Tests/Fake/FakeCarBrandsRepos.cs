using CarShop.DAL;
using CarShop.Services.Repos;

namespace CarShop.Tests.Fake
{
    public class FakeCarBrandsRepos : CarBrandRepos
    {
        private static readonly FakeDbContext Context = new FakeDbContext();

        protected CarShopContext GetCarShopContext()
        {
            return Context;
        }
    }
}
