using EFExamples;
using System;

namespace EFExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new CarShopDataModel())
            {
                context.SaveChanges();
                foreach (var car in context.Cars)
                {
                    
                    var carFullName = $"{car.CarModels.CarBrands.Brand} {car.CarModels.Model} {car.Year.Year}";
                    Console.WriteLine(carFullName);
                }
            }

            Console.ReadLine();
        }
    }
}
