using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CarShop.Contracts;
using CarShop.Models;
using EFExamples;
using System.Data.Entity;
using CarShop.DAL;

namespace CarShop.Services.Repos
{
    public class CarBrandRepos : ICarBrandsRepository
    {
        //    public CarBrandRepos()
        //    {

        //    }
        public void Add(CarBrands carBrand)
        {
            using (var context = new CarShopContext())
            {
                context.CarBrands.Add(carBrand);
                context.SaveChanges();
            }
        }

        public void Remove(Guid id)
        {
            using (var context = new CarShopContext())
            {
                CarBrands carBrand = this.Find(x => x.Id == id).FirstOrDefault(); //this.Get(Id);
                using (var db = this.GetContext())
                {
                    // db.CarBrands.Remove(carBrand); - 
                    // We can't use remove as we retrieved entity in other db context that is already closed
                    // Rather we can attach entry to a current dbContext and mark it as deleted
                    db.Entry(carBrand).State = EntityState.Deleted;
                    db.SaveChanges();
                }
            }
        }
        
        public IEnumerable<CarBrands> Find(Expression<Func<CarBrands, bool>> predicate,
                                           Expression<Func<CarBrands, object>> includedProperties = null)
        {
            using (var context = new CarShopContext())
            {
                return includedProperties != null 
                    ? context.CarBrands.Include(includedProperties).Where(predicate).ToArray() 
                    : context.CarBrands.Where(predicate).ToArray();
            }
        }

        public CarBrands Get(Guid Id)
        {
            using (var db = this.GetContext())
            {
                return db.CarBrands.FirstOrDefault(c => c.Id == Id);
            }
        }

        public void Update(CarBrands carBrand)
        {
            using (var context = new CarShopContext())
            {
                var entity = context.CarBrands.Find(carBrand.Id);
                context.Entry(entity).CurrentValues.SetValues(carBrand);
                context.SaveChanges();
            }
        }

        protected virtual CarShopContext GetContext()
        {
            // Will be replaced later by DI injection
            return new CarShopContext();
        }
    }
}
