using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tlm.Fed.Adapters.Sap.Common.CustomException;
using Tlm.Fed.Adapters.SAP.Common.Configuration;
using Tlm.Fed.Adapters.SAP.Site.Concrete;
using Tlm.Fed.Adapters.SAP.Site.Models;
using Tlm.Fed.Framework.Common.ServiceClient;
using Tlm.Sdk.Testing.Unit;

namespace Adapters.Sap.Site.Tests.Concrete
{
    [TestClass]
    [UnitTestCategory]
    public class GetSapSiteChunkTest : UnitTestBase
    {
        private const string SapServiceResponseFilePath = "Data\\SAPSiteServiceResponse.json";

        private Mock<IHttpService> _mockHttpService;
        private Mock<SAPEndpointConfig> _mockSapConfig;

        [TestInitialize]
        public void Initialize()
        {
            var mockProvider = new MockRepository(MockBehavior.Loose);
            _mockHttpService = mockProvider.Create<IHttpService>();
            _mockSapConfig = mockProvider.Create<SAPEndpointConfig>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockHttpService = null;
            _mockSapConfig = null;
        }

        [TestMethod]
        public async Task Should_Return_SapLocation()
        {
            //Arrange
            _mockSapConfig.Object.Url = "http://www.google.com";
            var sapSiteQuery = Builder<SAPSiteQuery>.CreateNew().Build();
            var sapLocation = ReadJson<SAPSiteResponse>(SapServiceResponseFilePath);
            //var sapLocation = Builder<SAPSite>.CreateListOfSize(10).Build().ToList();
            //var sapResponse = Builder<SAPSiteResponse>.CreateNew()
            //                                            .With(x => x.SAPSites = sapLocation)
            //                                            .Build();
            var response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            response.Content = new StringContent(JsonConvert.SerializeObject(sapLocation));
            _mockHttpService.Setup(x => x.ServiceCaller(It.IsAny<HttpRequestMessage>(), It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);


            //Act
            var obj = new GetSAPSiteChunk(_mockHttpService.Object, _mockSapConfig.Object);
            var result = await obj.Handle(sapSiteQuery);

            //Assert
            result.Should().NotBeNull();
            result.SAPSites.Should().NotBeEmpty();
        }

        [TestMethod]
        public async Task Should_Throws_SapIntegrationException()
        {
            //Arrange
            _mockSapConfig.Object.Url = "http://www.google.com";
            var sapSiteQuery = Builder<SAPSiteQuery>.CreateNew().Build();
            HttpRequestMessage request = null;
            var response = new HttpResponseMessage();
            response.Content = new StringContent("Unable to get http response from site  api with Http  Reason: Bad Request");
            response.StatusCode = HttpStatusCode.BadRequest;
            _mockHttpService.Setup(x => x.ServiceCaller(It.IsAny<HttpRequestMessage>(),
                It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>()))
                .Callback<HttpRequestMessage, TimeSpan, CancellationToken>((req, timeSpan, token) =>  request = req)
                .ReturnsAsync(response);

            //Act
            var obj = new GetSAPSiteChunk(_mockHttpService.Object, _mockSapConfig.Object);
            Func<Task> result = async () => { await obj.Handle(sapSiteQuery); };

            //Assert
            await result
                .Should()
                .ThrowAsync<SapIntegrationException>()
                .WithMessage($"Error occured while creating Site for request {JsonConvert.SerializeObject(request)} with httpStatus code {response.StatusCode} and error message {await response.Content?.ReadAsStringAsync()}");
        }
    }
}
