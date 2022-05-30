using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tlm.Fed.Adapters.Maximo.Common.Configuration;
using Tlm.Fed.Adapters.Maximo.Common.CustomException;
using Tlm.Fed.Adapters.Maximo.Site.Concrete;
using Tlm.Fed.Adapters.Maximo.Site.Models;
using Tlm.Fed.Framework.Common.ServiceClient;
using Tlm.Sdk.Testing.Unit;

namespace Adapters.Maximo.Site.Tests.Concrete
{
    [TestClass]
    [UnitTestCategory]
    public class GetMaximoSiteChunkTest : UnitTestBase
    {
        private const string MaximoServiceResponseFilePath = "Data\\MaximoSiteServiceResponse.json";

        private Mock<IHttpService> _mockHttpService;
        private Mock<MaximoEndpointConfig> _mockMaximoConfig;

        [TestInitialize]
        public void Initialize()
        {
            var mockProvider = new MockRepository(MockBehavior.Loose);
            _mockHttpService = mockProvider.Create<IHttpService>();
            _mockMaximoConfig = mockProvider.Create<MaximoEndpointConfig>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockHttpService = null;
            _mockMaximoConfig = null;
        }

        [TestMethod]
        public async Task Should_Return_MaximoLocation()
        {
            //Arrange
            _mockMaximoConfig.Object.Url = "http://www.google.com";
            var maximoSiteQuery = Builder<MaximoSiteQuery>.CreateNew().Build();
            var maximoLocation = ReadJson<SAP_R_LOCATIONS_LOCATIONSType>(MaximoServiceResponseFilePath);
            var response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            response.Content = new StringContent(JsonConvert.SerializeObject(maximoLocation));
            _mockHttpService.Setup(x => x.ServiceCaller(It.IsAny<HttpRequestMessage>(), It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);


            //Act
            var obj = new GetMaximoSiteChunk(_mockHttpService.Object, _mockMaximoConfig.Object);
            var result = await obj.Handle(maximoSiteQuery);

            //Assert
            result.Should().NotBeNull();
            result.maximoLocation.Should().NotBeEmpty();
        }

        [TestMethod]
        public async Task Should_Throws_MaximoIntegrationException()
        {
            //Arrange
            _mockMaximoConfig.Object.Url = "http://www.google.com";
            var maximoSiteQuery = new MaximoSiteQuery(MaximoSiteQuery.For("test"));
            HttpRequestMessage request = null;
            var response = new HttpResponseMessage();
            response.Content = new StringContent("Unable to get http response from site  api with Http  Reason: Bad Request");
            response.StatusCode = HttpStatusCode.BadRequest;
            _mockHttpService.Setup(x => x.ServiceCaller(It.IsAny<HttpRequestMessage>(),
                It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>()))
                .Callback<HttpRequestMessage, TimeSpan, CancellationToken>((req, timeSpan, token) =>  request = req)
                .ReturnsAsync(response);

            //Act
            var obj = new GetMaximoSiteChunk(_mockHttpService.Object, _mockMaximoConfig.Object);
            Func<Task> result = async () => { await obj.Handle(maximoSiteQuery); };

            //Assert
            await result
                .Should()
                .ThrowAsync<MaximoIntegrationException>()
                .WithMessage($"Error occured while creating Site for request {JsonConvert.SerializeObject(request)} with httpStatus code {response.StatusCode} and error message {await response.Content?.ReadAsStringAsync()}");
        }
    }
}
