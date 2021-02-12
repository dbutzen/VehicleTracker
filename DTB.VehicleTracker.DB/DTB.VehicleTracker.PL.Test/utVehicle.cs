using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTB.VehicleTracker.PL;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;

namespace DTB.VehicleTracker.PL.Test
{
    [TestClass]
    public class utVehicle
    {
        protected VehicleEntities dc;
        protected IDbContextTransaction transaction;

        [TestInitialize]
        public void TestInitialize()
        {
            dc = new VehicleEntities();
            transaction = dc.Database.BeginTransaction();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            transaction.Rollback();
            transaction.Dispose();
        }



        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(2, dc.tblVehicles.Count());
        }

        [TestMethod]
        public void InsertTest()
        {

            tblVehicle newrow = new tblVehicle();
            newrow.Id = Guid.NewGuid();
            newrow.ColorId = dc.tblColors.FirstOrDefault().Id;
            newrow.MakeId = dc.tblMakes.FirstOrDefault().Id;
            newrow.ModelId = dc.tblModels.FirstOrDefault().Id;
            newrow.VIN = "NEWVIN";
            newrow.Year = 1992;

            dc.tblVehicles.Add(newrow);
            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            tblVehicle existingrow = dc.tblVehicles.FirstOrDefault(c => c.Year == 1992);

            if (existingrow != null)
            {
                existingrow.Year = 1950;
                dc.SaveChanges();
            }

            tblVehicle row = dc.tblVehicles.FirstOrDefault(c => c.Year == 1950);

            Assert.AreEqual(existingrow.Year, row.Year);

        }

        [TestMethod]
        public void DeleteTest()
        {

            InsertTest();

            tblVehicle row = dc.tblVehicles.FirstOrDefault(c => c.Year == 1992);

            if (row != null)
            {
                dc.tblVehicles.Remove(row);
                dc.SaveChanges();
            }

            tblVehicle deletedrow = dc.tblVehicles.FirstOrDefault(c => c.Year == 1992);

            Assert.IsNull(deletedrow);
        }
    }
}
