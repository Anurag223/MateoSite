using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Tlm.Fed.Adapters.SAP.Common.Configuration;
using Tlm.Fed.Adapters.SAP.Site.Concrete;
using Tlm.Sdk.Testing.Unit;

namespace Adapters.Sap.Site.Tests.Concrete
{
    [TestClass]
    [UnitTestCategory]
    public class SapSiteRequestBuilderTest : UnitTestBase
    {
        private Mock<SAPEndpointConfig> _mockSapEndPoint;

        [TestInitialize]
        public void Initialize()
        {
            var mockProvider = new MockRepository(MockBehavior.Loose);
            _mockSapEndPoint = mockProvider.Create<SAPEndpointConfig>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockSapEndPoint = null;
        }

        [TestMethod]
        public void Verify_Sap_Site_Request()
        {
            //Arrange
            _mockSapEndPoint.Object.Url = "http://www.google.com";

            //Act
            var sapRequestBuilder = new SAPSiteRequestBuilder(_mockSapEndPoint.Object);

            var httpRequestMessage = sapRequestBuilder.Build();

            //Assert
            httpRequestMessage.Should().NotBeNull();
            httpRequestMessage.RequestUri.Should().NotBeNull();
            httpRequestMessage.RequestUri.AbsoluteUri.Should().NotBeNull();
            httpRequestMessage.RequestUri.AbsoluteUri.Should().Be("http://www.google.com/eamplantdetails/plantDetailsSet?$expand=plantToWrkcntr($expand=wrkcntrToPerson)&$count=true");
        }
    }
}
