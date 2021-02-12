using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTB.VehicleTracker.PL;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;

namespace DTB.VehicleTracker.PL.Test
{
    [TestClass]
    public class utModel
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
            Assert.AreEqual(3, dc.tblModels.Count());
        }

        [TestMethod]
        public void InsertTest()
        {

            tblModel newrow = new tblModel();
            newrow.Id = Guid.NewGuid();
            newrow.Description = "NewModel";

            dc.tblModels.Add(newrow);
            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            tblModel existingrow = dc.tblModels.FirstOrDefault(c => c.Description == "NewModel");

            if (existingrow != null)
            {
                existingrow.Description = "UpdatedModel";
                dc.SaveChanges();
            }

            tblModel row = dc.tblModels.FirstOrDefault(c => c.Description == "UpdatedModel");

            Assert.AreEqual(existingrow.Description, row.Description);

        }

        [TestMethod]
        public void DeleteTest()
        {

            InsertTest();

            tblModel row = dc.tblModels.FirstOrDefault(c => c.Description == "NewModel");

            if (row != null)
            {
                dc.tblModels.Remove(row);
                dc.SaveChanges();
            }

            tblModel deletedrow = dc.tblModels.FirstOrDefault(c => c.Description == "NewModel");

            Assert.IsNull(deletedrow);
        }
    }
}
