using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTB.VehicleTracker.PL;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;

namespace DTB.VehicleTracker.PL.Test
{
    [TestClass]
    public class utMake
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
            Assert.AreEqual(3, dc.tblMakes.Count());
        }

        [TestMethod]
        public void InsertTest()
        {

            tblMake newrow = new tblMake();
            newrow.Id = Guid.NewGuid();
            newrow.Description = "NewModel";

            dc.tblMakes.Add(newrow);
            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            tblMake existingrow = dc.tblMakes.FirstOrDefault(c => c.Description == "NewModel");

            if (existingrow != null)
            {
                existingrow.Description = "UpdatedModel";
                dc.SaveChanges();
            }

            tblMake row = dc.tblMakes.FirstOrDefault(c => c.Description == "UpdatedModel");

            Assert.AreEqual(existingrow.Description, row.Description);

        }

        [TestMethod]
        public void DeleteTest()
        {

            InsertTest();

            tblMake row = dc.tblMakes.FirstOrDefault(c => c.Description == "NewModel");

            if (row != null)
            {
                dc.tblMakes.Remove(row);
                dc.SaveChanges();
            }

            tblMake deletedrow = dc.tblMakes.FirstOrDefault(c => c.Description == "NewModel");

            Assert.IsNull(deletedrow);
        }
    }
}

