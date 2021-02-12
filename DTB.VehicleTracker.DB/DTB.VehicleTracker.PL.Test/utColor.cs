using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTB.VehicleTracker.PL;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;

namespace DTB.VehicleTracker.PL.Test
{
    [TestClass]
    public class utColor
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
            Assert.AreEqual(3, dc.tblColors.Count());
        }

        [TestMethod]
        public void InsertTest()
        {

            tblColor newrow = new tblColor();
            newrow.Id = Guid.NewGuid();
            newrow.Code = -1;
            newrow.Description = "NewColor";

            dc.tblColors.Add(newrow);
            int result = dc.SaveChanges();

            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void UpdateTest()
        {
            InsertTest();

            tblColor existingrow = dc.tblColors.FirstOrDefault(c => c.Code == -1);

            if (existingrow != null)
            {
                existingrow.Description = "UpdatedColor";
                dc.SaveChanges();
            }

            tblColor row = dc.tblColors.FirstOrDefault(c => c.Code == -1);

            Assert.AreEqual(existingrow.Description, row.Description);

        }

        [TestMethod]
        public void DeleteTest()
        {

            InsertTest();

            tblColor row = dc.tblColors.FirstOrDefault(c => c.Code == -1);

            if (row != null)
            {
                dc.tblColors.Remove(row);
                dc.SaveChanges();
            }

            tblColor deletedrow = dc.tblColors.FirstOrDefault(c => c.Code == -1);

            Assert.IsNull(deletedrow);
        }
    }
}
