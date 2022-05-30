using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tlm.Fed.Adapters.SAP.Site.Concrete;
using Tlm.Fed.Adapters.SAP.Site.Models;
using Tlm.Fed.Framework.Common;
using Tlm.Fed.Framework.Common.Models;
using Tlm.Sdk.Testing.Unit;

namespace Adapters.Sap.Site.Tests.Concrete
{
    [TestClass]
    [UnitTestCategory]
    public class GetSapSiteTest : UnitTestBase
    {
        private const string SapServiceResponseFilePath = "Data\\SAPSiteServiceResponse.json";

        private Mock<ISAPSiteTransformer> _mockSapTransformer;
        private Mock<IActionHandler<SAPSiteQuery, SAPSiteResponse>> _mockSiteHandler;

        [TestInitialize]
        public void Initialize()
        {
            var mockProvider = new MockRepository(MockBehavior.Loose);
            _mockSapTransformer = mockProvider.Create<ISAPSiteTransformer>();
            _mockSiteHandler = mockProvider.Create<IActionHandler<SAPSiteQuery, SAPSiteResponse>>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockSapTransformer = null;
            _mockSiteHandler = null;
        }

        [TestMethod]
        public async Task Should_Load_Sites()
        {
            //Arrange
            var sapSiteQuery = Builder<SAPSiteQuery>.CreateNew().Build();
            var sapSites  = ReadJson<SAPSiteResponse>(SapServiceResponseFilePath);
            _mockSiteHandler.Setup(x => x.Handle(It.IsAny<SAPSiteQuery>())).ReturnsAsync(sapSites);
            _mockSapTransformer.Setup(x => x.Transform(sapSites.SAPSites)).ReturnsAsync(GetCacheLoadInfo);

            //Act
            var obj = new GetSAPSite(_mockSapTransformer.Object, _mockSiteHandler.Object);
            var result = await obj.Handle(sapSiteQuery);

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
