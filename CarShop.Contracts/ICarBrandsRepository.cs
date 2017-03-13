using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CarShop.Models;

namespace CarShop.Contracts
{
    public interface ICarBrandsRepository
    {
        void Add(CarBrands carBrand);

        void Remove(Guid id);

        void Update(CarBrands carBrand);

        IEnumerable<CarBrands> Find(
            Expression<Func<CarBrands, bool>> predicate,
            Expression<Func<CarBrands, object>> imcludedProperties = null);

        CarBrands Get(Guid Id);
    }
}
