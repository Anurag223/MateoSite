using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tlm.Fed.Adapters.Maximo.Site.Concrete;
using Tlm.Fed.Adapters.Maximo.Site.Models;
using Tlm.Fed.Framework.Common;
using Tlm.Fed.Framework.Common.Models;
using Tlm.Sdk.Testing.Unit;

namespace Adapters.Maximo.Site.Tests.Concrete
{
    [TestClass]
    [UnitTestCategory]
    public class GetMaximoSiteTest : UnitTestBase
    {
        private const string MaximoServiceResponseFilePath = "Data\\MaximoSiteServiceResponse.json";

        private Mock<IMaximoSiteTransformer> _mockMaximoTransformer;
        private Mock<IActionHandler<MaximoSiteQuery, SAP_R_LOCATIONS_LOCATIONSType>> _mockSiteHandler;

        [TestInitialize]
        public void Initialize()
        {
            var mockProvider = new MockRepository(MockBehavior.Loose);
            _mockMaximoTransformer = mockProvider.Create<IMaximoSiteTransformer>();
            _mockSiteHandler = mockProvider.Create<IActionHandler<MaximoSiteQuery, SAP_R_LOCATIONS_LOCATIONSType>>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockMaximoTransformer = null;
            _mockSiteHandler = null;
        }

        [TestMethod]
        public async Task Should_Load_Sites()
        {
            //Arrange
            var maximoSiteQuery = Builder<MaximoSiteQuery>.CreateNew().Build();
            var maximoSites  = ReadJson<SAP_R_LOCATIONS_LOCATIONSType>(MaximoServiceResponseFilePath);
            maximoSites.responseInfo.pagenum = 377;
            _mockSiteHandler.Setup(x => x.Handle(It.IsAny<MaximoSiteQuery>())).ReturnsAsync(maximoSites);
            _mockMaximoTransformer.Setup(x => x.Transform(maximoSites.maximoLocation)).ReturnsAsync(GetCacheLoadInfo);

            //Act
            var obj = new GetMaximoSite(_mockMaximoTransformer.Object, _mockSiteHandler.Object);
            var result = await obj.Handle(maximoSiteQuery);

            //Assert
            result.Should().NotBeNull();
            result.ItemsLoaded.Count.Should().Be(10);
        }

        private CacheLoadInfo GetCacheLoadInfo()
        {
            var cacheLoadInfo = new CacheLoadInfo();
            cacheLoadInfo.AddItemsLoaded(new string[] { "Test1", "Test2", "Test3", "Test4", "Test5", "Test6", "Test7", "Test8", "Test9", "Test10" });
            return cacheLoadInfo;
        }
    }
}
