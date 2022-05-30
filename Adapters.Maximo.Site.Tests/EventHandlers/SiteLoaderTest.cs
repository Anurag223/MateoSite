using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tlm.Fed.Adapters.Maximo.Site.Concrete;
using Tlm.Fed.Adapters.Maximo.Site.EventHandlers;
using Tlm.Fed.Framework.Internal;
using Tlm.Fed.Framework.Common;
using Tlm.Fed.Framework.Common.Models;
using Tlm.Sdk.Testing.Unit;

namespace Adapters.Maximo.Site.Tests.EventHandlers
{
    [TestClass]
    [UnitTestCategory]
    public class SiteLoaderTest : UnitTestBase
    {
        private Mock<IActionHandler<MaximoSiteQuery, CacheLoadInfo>> _mockMaximoSites;
        [TestInitialize]
        public void Initialize()
        {
            var mockRepository = new MockRepository(MockBehavior.Loose);
            _mockMaximoSites = mockRepository.Create<IActionHandler<MaximoSiteQuery, CacheLoadInfo>>();
        }

        [TestCleanup]
        public void CleanUp()
        {
            _mockMaximoSites = null;
        }

        [TestMethod]
        public async Task Should_Load_Sites()
        {
            //Arrange
            MaximoSiteQuery maximoSiteQuery = null;
            _mockMaximoSites.Setup(x => x.Handle(It.IsAny<MaximoSiteQuery>()))
                            .Callback<MaximoSiteQuery>(x => maximoSiteQuery = x)
                            .ReturnsAsync(GetCacheLoadInfo);
            //Act
            var loader = new SiteLoader(_mockMaximoSites.Object);

            // Full load scenario: no high water mark set.
            var query = new SiteBasedLoadQuery() { CommaSeparatedSiteCodes = "2016", HighWaterMark = null };
            var result = await loader.LoadAsync(query);

            //Assert
            maximoSiteQuery.Should().NotBeNull();
            maximoSiteQuery.SubBusinessLine.Should().Be(query.CommaSeparatedSiteCodes);
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
