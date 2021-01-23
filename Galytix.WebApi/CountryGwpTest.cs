using Galytix.WebApi.Models;
using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;

namespace Galytix.WebApi
{
    public class CountryGwpTest
    {
        private CountryGwp _countryGwp;
        private Mock<CountryGwp> _countryGwpMock;
        [SetUp]
        public virtual void SetUp()
        {
            _countryGwpMock = new Mock<CountryGwp>();
            _countryGwp = new CountryGwp();

        }

        /// <summary>
        /// Tears down.
        /// </summary>
        [TearDown]
        public virtual void TearDown()
        {
        }
        [Test]
        [TestCase("ao","liability,transport")]
        public void CountryLOBTest(string country, string lobs)
        {
            _countryGwpMock.Setup(x => x.GetCountriesData(It.IsAny<string>(), It.IsAny<string[]>())).Returns(
                new List<GwpModel>{ new GwpModel() {
                Country = country,
                variableName = lobs.Split(",").FirstOrDefault(),
                YearGwpValue = Enumerable.Range(1,16).Select(x=>x.ToString()).ToList()
                }
                });
            decimal  average = 0;
            Enumerable.Range(8, 7).ToList().ForEach(s => average += s);

            var expectedResult = new Dictionary<string, string> { { lobs.Split(",").FirstOrDefault(), average.ToString() }};

            var result = _countryGwp.GWP(country, lobs.Split(","));
            Assert.AreEqual(expectedResult, result);
        }
    }
}
