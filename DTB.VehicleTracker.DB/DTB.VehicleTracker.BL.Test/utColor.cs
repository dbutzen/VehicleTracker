using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTB.VehicleTracker.BL;

namespace DTB.VehicleTracker.BL.Test
{
    [TestClass]
    public class utColor
    {
        [TestMethod]
        public void LoadTest()
        {
            
        }

        [TestMethod]
        public void InsertTest()
        {
            Task.Run(async () =>
            {
                int results = await ColorManager.Insert(new Models.Color { Code = -99, Description = "NewColor" }, true);
                Assert.IsTrue(results > 0);
            });
        }

        public void InsertFailedTest()
        {
            Task.Run(async () =>
            {
                int results = await ColorManager.Insert(new Models.Color { Code = -99 }, true);
                Assert.IsTrue(results == 0);
            });
        }

        [TestMethod]
        public void UpdateTest()
        {

        }
        [TestMethod]
        public void DeleteTest()
        {

        }
    }
}
