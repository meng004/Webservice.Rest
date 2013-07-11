using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebService.Rest.Models;
using WebService.Rest.Controllers;

namespace WebService.Rest.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DbContext()
        {
            var context = new CityDataContext();

            var result = context.Provinces.AsEnumerable();

            Assert.IsNotNull(result.Count() > 0);
        }

        [TestMethod]
        public void GetAllProvince()
        {
            var ctl = new ProvincesController();
            var result = ctl.GetProvinces();
            Assert.IsNotNull(result.Count() > 0);
        }
    }
}
