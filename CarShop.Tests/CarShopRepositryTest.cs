using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarShop.Contracts;
using CarShop.Models;
using CarShop.Services.Repos;
using CarShop.Tests.Fake;

namespace CarShop.Tests
{
    [TestClass]
    public class CarShopRepositryTest
    {
        private Guid addTestCarBrandId;

        private Guid[] findTestCarBrandId;

        private ICarBrandsRepository repository;

        private Guid updateTestCarBrandId;

        [TestInitialize]
        public void Setup()
        {
            this.repository = new CarBrandRepos();
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Cleanup database
            if (repository.GetType() == typeof(FakeCarBrandsRepos))
            {
                return;
            }

            if (this.addTestCarBrandId != Guid.Empty)
            {
                this.repository.Remove(this.addTestCarBrandId);
            }

            if (this.updateTestCarBrandId != Guid.Empty)
            {
                this.repository.Remove(this.updateTestCarBrandId);
            }

            if (this.findTestCarBrandId == null)
            {
                return;
            }

            foreach (var id in this.findTestCarBrandId)
            {
                this.repository.Remove(id);
            }
        }

        [TestMethod]
        public void Repository_Add_Test()
        {
            var carBrand = new CarBrands
            {
                Id = Guid.NewGuid(),
                Brand = "Ferrari"
            };
            this.repository.Add(carBrand);

            var result = repository.Get(carBrand.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, carBrand.Id);
        }

        [TestMethod]
        public void RemoveTest()
        {
            // arrange
            var carBrand = new CarBrands { Id = Guid.NewGuid(), Brand = "Toyota" };
            this.repository.Add(carBrand);
            var addedCarBrand = this.repository.Get(carBrand.Id);
            Assert.IsNotNull(addedCarBrand);

            // act
            this.repository.Remove(carBrand.Id);

            // assert
            var result = this.repository.Get(carBrand.Id);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Repository_Update_Test()
        {
            // arrange
            this.updateTestCarBrandId = Guid.NewGuid();
            var carBrand = new CarBrands { Id = this.updateTestCarBrandId, Brand = "BMW" };
            this.repository.Add(carBrand);
            var addedCarBrand = this.repository.Get(carBrand.Id);
            Assert.IsNotNull(addedCarBrand);

            // act
            carBrand.Brand = "Ferrari";
            this.repository.Update(carBrand);

            // assert
            var result = this.repository.Get(carBrand.Id);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Brand, carBrand.Brand);
        }

        [TestMethod]
        public void Repository_Find_Test()
        {
            // arrange
            var volksCarBrand = new CarBrands { Id = Guid.NewGuid(), Brand = "Volkswagen" };
            var lexusCarBrand = new CarBrands { Id = Guid.NewGuid(), Brand = "Lexus" };
            this.findTestCarBrandId = new[] { volksCarBrand.Id, lexusCarBrand.Id };
            this.repository.Add(volksCarBrand);
            this.repository.Add(lexusCarBrand);

            // act
            var findCarBrandsById =
                this.repository.Find(c => c.Id == volksCarBrand.Id || c.Id == lexusCarBrand.Id).ToList();
            var findCarBrandsByNameAndId =
                this.repository.Find(c => c.Brand == lexusCarBrand.Brand && c.Id == lexusCarBrand.Id).ToList();

            // assert
            Assert.IsNotNull(findCarBrandsById);
            Assert.AreEqual(findCarBrandsById.Count, 2);
            Assert.AreEqual(findCarBrandsById.First(c => c.Id == volksCarBrand.Id).Id, volksCarBrand.Id);

            Assert.IsNotNull(findCarBrandsByNameAndId);
            Assert.AreEqual(findCarBrandsByNameAndId.Count, 1);
            Assert.AreEqual(findCarBrandsByNameAndId.First().Brand, lexusCarBrand.Brand);
        }
    }
}
