using EFExamples;
using System;
using CarShop.Models;
using CarShop.Services.Repos;

namespace EFExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new CarBrandRepos();
            
            var mercedes = new CarBrands
            {
                Id = Guid.NewGuid(),
                Brand = "Mercedes"
            };
            service.Add(mercedes);

            var bmw = new CarBrands
            {
                Id = Guid.NewGuid(),
                Brand = "BMW"
            };
            service.Add(bmw);

            var toyota = new CarBrands
            {
                Id = Guid.NewGuid(),
                Brand = "Toyota"
            };
            service.Add(toyota);

            var model22 = service.Find(c => c.Brand == "Toyota" || c.Brand == "BMW");
            Console.ReadLine();
        }
    }
}
