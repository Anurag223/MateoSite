using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tlm.Fed.Adapters.Rite.Common.Configuration;
using Tlm.Fed.Adapters.Rite.Common.Services.Implementers;
using Tlm.Fed.Adapters.Rite.Common.Services.ServiceModels.Organization;
using Tlm.Fed.Framework.Common.ServiceClient;
using Tlm.Sdk.Testing.Unit;

namespace Adapters.Rite.Site.Tests
{
    [TestClass]
    [UnitTestCategory]
    public class OrganizationServiceTest : UnitTestBase
    {
        private Mock<RiteEndpointConfig> _mockRiteEndpointConfig;
        private Mock<IHttpService> _mockHttpService;


        [TestInitialize]
        public void Initialize()
        {
            var mockProvider = new MockRepository(MockBehavior.Loose);
            _mockHttpService = mockProvider.Create<IHttpService>();
            _mockRiteEndpointConfig = mockProvider.Create<RiteEndpointConfig>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockHttpService = null;
            _mockRiteEndpointConfig = null;
        }

        [TestMethod]
        public async Task Should_Return_Organizationdetails()
        {
            //Arrange
            _mockRiteEndpointConfig.Object.BaseUri = "http://www.google.com";
            var organizationDetails = Builder<OrganizationDetail>.CreateListOfSize(10).Build().ToArray();
            var organizationResponse = Builder<OrganizationResponse>.CreateNew()
                                                                   .With(x => x.OrganizationDetails = organizationDetails)
                                                                   .Build();
            var response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            response.Content = new StringContent(JsonConvert.SerializeObject(organizationResponse));
            _mockHttpService.Setup(x => x.ServiceCaller(It.IsAny<HttpRequestMessage>(), It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            //Act
            var orgService = new OrganizationService(_mockRiteEndpointConfig.Object, _mockHttpService.Object);
            var result = await orgService.GetOrganizationDetails("test");

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(organizationDetails.Length);
        }

        [TestMethod]
        public async Task Should_Return_Null()
        {
            //Arrange
            _mockRiteEndpointConfig.Object.BaseUri = "http://www.google.com";
          
            var response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.InternalServerError;
            _mockHttpService.Setup(x => x.ServiceCaller(It.IsAny<HttpRequestMessage>(), It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            //Act
            var orgService = new OrganizationService(_mockRiteEndpointConfig.Object, _mockHttpService.Object);
            var result = await orgService.GetOrganizationDetails("test");

            //Assert
            result.Should().BeNull();
        }
    }
}
