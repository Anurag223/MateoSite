

using Castle.Core.Internal;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tlm.Fed.Adapters.Qtrac.Site.DataAccess;
using Tlm.Fed.Contexts.Site.Core.Services;
using Tlm.Fed.Framework.Common.Models;
using Tlm.Fed.Framework.Internal;
using Tlm.Fed.Models.Canonical.SiteDomain;
using Tlm.Sdk.Core.Models.Infrastructure;
using Tlm.Sdk.Testing.Unit;
using SiteLoader = Tlm.Fed.Adapters.Qtrac.Site.EventHandlers.SiteLoader;

namespace Tlm.Fed.Adapters.Qtrac.Site.Tests.EventHandlers
{
    [TestClass]
    public class SiteLoaderTest : UnitTestBase
    {
        #region Initialize

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new MockRepository(MockBehavior.Loose);
            _mockDataHandler = _mockRepository.Create<IDataHandler>();
            _mockTransformer = _mockRepository.Create<ITransformer<WorkCenterSite>>();
            _mockSegmentMappingService = _mockRepository.Create<ISegmentMappingService>();
            _logger = CreateLogger(_mockRepository);
        }

        #endregion
        [TestMethod]
        public void Instantiate_Component_NoExceptionThrown()
        {
            Assert.IsNotNull(TryInstantiateComponent<SiteLoader, StartupForQtracSiteAdapter, SiteAdapterConfig>(
                additionalConfigurations: builder => builder.AddJsonFile("testsettings.shared.json", false)));
        }
        [TestMethod]
        public async Task SiteLoader_Should_Set_Site_Criteria()
        {
            //Arrange
            var m = new Mock<Expression<Func<WorkCenterSite, bool>>>();
            IReadOnlyCollection<WorkCenterSite> d = new List<WorkCenterSite>();
            var cacheLoadInfo = new CacheLoadInfo();
            cacheLoadInfo.OverrideDefaultHighWaterMark(DateTime.UtcNow);

            _mockDataHandler.Setup(q => q.GetSite(It.IsAny<Expression<Func<WorkCenterSite, bool>>>(), It.IsAny<Expression<Func<WorkCenterSite, bool>>>(), It.IsAny<string>())).Returns(Task.FromResult(d));
            _mockTransformer.Setup(x => x.ConstructCacheLoadInfo(It.IsAny<LoadFromSourceResult<WorkCenterSite>>())).Returns(Task.FromResult(cacheLoadInfo));

            var query = new SiteBasedLoadQuery { CommaSeparatedSiteCodes = null };
            var siteLoader = CreateSiteLoader();

            //act
            var result = await siteLoader.LoadAsync(query);

            //asset
            HasSequenceSiteLoadStrategy(siteLoader).Should().BeTrue("SiteBasedLoader should have ContextStrategy= SiteLoadStrategy.Sequence");
            result.Should().NotBeNull();
            result.HighWaterMark.Should().BeNull();
        }

        [TestMethod]
        public async Task SiteLoader_CacheLoadInfo_NotNull()
        {
            //Arrange
            var m = new Mock<Expression<Func<WorkCenterSite, bool>>>();
            IReadOnlyCollection<WorkCenterSite> d = new List<WorkCenterSite>();
            var cacheLoadInfo = new CacheLoadInfo();

            _mockDataHandler.Setup(q => q.GetSite(It.IsAny<Expression<Func<WorkCenterSite, bool>>>(), It.IsAny<Expression<Func<WorkCenterSite, bool>>>(), It.IsAny<string>())).Returns(Task.FromResult(d));
            _mockTransformer.Setup(x => x.ConstructCacheLoadInfo(It.IsAny<LoadFromSourceResult<WorkCenterSite>>())).Returns(Task.FromResult(cacheLoadInfo));

            var query = new SiteBasedLoadQuery { CommaSeparatedSiteCodes = "1234,5334" };
            var sut = CreateSiteLoader();

            //act
            var result = await sut.LoadAsync(query);

            //asset
            HasSequenceSiteLoadStrategy(sut).Should().BeTrue("SiteBasedLoader should have ContextStrategy= SiteLoadStrategy.Sequence");
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task SiteLoader_ItemsDeleted_Zero()
        {
            //Arrange
            var m = new Mock<Expression<Func<WorkCenterSite, bool>>>();
            IReadOnlyCollection<WorkCenterSite> d = new List<WorkCenterSite>();
            var cacheLoadInfo = new CacheLoadInfo();

            _mockDataHandler.Setup(q => q.GetSite(It.IsAny<Expression<Func<WorkCenterSite, bool>>>(), It.IsAny<Expression<Func<WorkCenterSite, bool>>>(), It.IsAny<string>())).Returns(Task.FromResult(d));
            _mockTransformer.Setup(x => x.ConstructCacheLoadInfo(It.IsAny<LoadFromSourceResult<WorkCenterSite>>())).Returns(Task.FromResult(cacheLoadInfo));

            var query = new SiteBasedLoadQuery { CommaSeparatedSiteCodes = null };
            var sut = CreateSiteLoader();

            //act
            var result = await sut.LoadAsync(query);

            //asset
            HasSequenceSiteLoadStrategy(sut).Should().BeTrue("SiteBasedLoader should have ContextStrategy= SiteLoadStrategy.Sequence");
            result.ItemsDeleted.Count().Should().Be(0);
        }

        [TestMethod]
        public async Task SiteLoader_With_HighwaterMark()
        {
            //Arrange
            var m = new Mock<Expression<Func<WorkCenterSite, bool>>>();
            var d = Builder<WorkCenterSite>.CreateListOfSize(10).Build().ToList();


            _mockDataHandler.Setup(q => q.GetSite(It.IsAny<Expression<Func<WorkCenterSite, bool>>>(), It.IsAny<Expression<Func<WorkCenterSite, bool>>>(), It.IsAny<string>())).ReturnsAsync(d);
            _mockTransformer.Setup(x => x.ConstructCacheLoadInfo(It.IsAny<LoadFromSourceResult<WorkCenterSite>>())).ReturnsAsync(GetCacheLoadInfo);

            var query = new SiteBasedLoadQuery { CommaSeparatedSiteCodes = "1234", HighWaterMark = DateTime.Now };
            var sut = CreateSiteLoader();

            //act
            var result = await sut.LoadAsync(query);

            //asset
            HasSequenceSiteLoadStrategy(sut).Should().BeTrue("SiteBasedLoader should have ContextStrategy= SiteLoadStrategy.Sequence");
            result.ItemsLoaded.Count().Should().Be(10);
            result.ItemsDeleted.Count().Should().Be(0);
        }

        #region Variables

        private MockRepository _mockRepository;
        private ILogger _logger;
        private Mock<IDataHandler> _mockDataHandler;
        private Mock<ITransformer<WorkCenterSite>> _mockTransformer;
        private Mock<ISegmentMappingService> _mockSegmentMappingService;

        #endregion

        #region Private Methods

        private static ILogger CreateLogger(MockRepository mockRepo)
        {
            var loggerMock = mockRepo.Create<ILogger>(MockBehavior.Loose);
            loggerMock.Setup(x => x.ForContext<SiteLoader>()).Returns(() => loggerMock.Object);
            var logger = loggerMock.Object;
            return logger;
        }

        private SiteLoader CreateSiteLoader()
        {
            return new SiteLoader(_mockDataHandler.Object, _mockTransformer.Object, _mockSegmentMappingService.Object);
        }

        private bool HasSequenceSiteLoadStrategy(SiteLoader sut)
        {
            var attribute = sut.GetType().GetAttributes<ContextStrategyAttribute>().FirstOrDefault();
            return attribute != null && attribute.Strategy == ContextStrategy.SiteBySite;
        }

        private CacheLoadInfo GetCacheLoadInfo()
        {
            var cacheLoadInfo = new CacheLoadInfo();
            cacheLoadInfo.AddItemsLoaded(new string[] { "Test1", "Test2", "Test3", "Test4", "Test5", "Test6", "Test7", "Test8", "Test9", "Test10" });
            return cacheLoadInfo;
        }
        #endregion
    }
}
