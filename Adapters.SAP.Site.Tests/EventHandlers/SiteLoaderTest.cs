using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tlm.Fed.Adapters.SAP.Site.Concrete;
using Tlm.Fed.Adapters.SAP.Site.EventHandlers;
using Tlm.Fed.Framework.Common;
using Tlm.Fed.Framework.Common.Models;
using Tlm.Fed.Framework.Internal;
using Tlm.Sdk.Testing.Unit;

namespace Adapters.SAP.Site.Tests.EventHandlers
{
    [TestClass]
    [UnitTestCategory]
    public class SiteLoaderTest : UnitTestBase
    {
        private Mock<IActionHandler<SAPSiteQuery, CacheLoadInfo>> _mockSapSites;
        [TestInitialize]
        public void Initialize()
        {
            var mockRepository = new MockRepository(MockBehavior.Loose);
            _mockSapSites = mockRepository.Create<IActionHandler<SAPSiteQuery, CacheLoadInfo>>();
        }

        [TestCleanup]
        public void CleanUp()
        {
            _mockSapSites = null;
        }

        [TestMethod]
        public async Task Should_Load_Sites()
        {
            //Arrange
            SAPSiteQuery sapSiteQuery = null;
            _mockSapSites.Setup(x => x.Handle(It.IsAny<SAPSiteQuery>()))
                            .Callback<SAPSiteQuery>(x => sapSiteQuery = x)
                            .ReturnsAsync(GetCacheLoadInfo);
            //Act
            var loader = new SiteLoader(_mockSapSites.Object);

            // Full load scenario: no high water mark set.
            var query = new SiteBasedLoadQuery() { CommaSeparatedSiteCodes = "2016", HighWaterMark = null };
            var result = await loader.LoadAsync(query);

            //Assert
            sapSiteQuery.Should().NotBeNull();
            result.LoadErrors.Should().BeEmpty("No errors occurred!");
            result.HighWaterMark.Should().BeNull();
            result.ItemsDeleted.Should().BeEmpty();
        }

        private CacheLoadInfo GetCacheLoadInfo()
        {
            var cacheLoadInfo = new CacheLoadInfo();
            cacheLoadInfo.AddItemsLoaded(new string[] { "Test1", "Test2", "Test3", "Test4", "Test5", "Test6", "Test7", "Test8", "Test9", "Test10" });
            return cacheLoadInfo;
        }
    }
}
