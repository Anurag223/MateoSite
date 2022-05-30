using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Tlm.Fed.Adapters.Maximo.Common.Configuration;
using Tlm.Fed.Adapters.Maximo.Site.Concrete;
using Tlm.Sdk.Testing.Unit;

namespace Adapters.Maximo.Site.Tests.Concrete
{
    [TestClass]
    [UnitTestCategory]
    public class MaximoSiteRequestBuilderTest : UnitTestBase
    {
        private Mock<MaximoEndpointConfig> _mockMaximoEndPoint;

        [TestInitialize]
        public void Initialize()
        {
            var mockProvider = new MockRepository(MockBehavior.Loose);
            _mockMaximoEndPoint = mockProvider.Create<MaximoEndpointConfig>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockMaximoEndPoint = null;
        }

        [TestMethod]
        public void Verify_Maximo_Site_Request()
        {
            //Arrange
            _mockMaximoEndPoint.Object.Url = "http://www.google.com";

            //Act
            var maximoRequestBuilder = new MaximoSiteRequestBuilder(_mockMaximoEndPoint.Object);

            var httpRequestMessage = maximoRequestBuilder.Build();

            //Assert
            httpRequestMessage.Should().NotBeNull();
            httpRequestMessage.RequestUri.Should().NotBeNull();
            httpRequestMessage.RequestUri.AbsoluteUri.Should().NotBeNull();
            httpRequestMessage.RequestUri.AbsoluteUri.Should().Be("http://www.google.com/maximo/api/os/SLB_R_LOCATION_API01?lean=1&oslc.select=*&_dropnull=0&collectioncount=1");
        }

        [TestMethod]
        public void Verify_Maximo_Site_Request_All_Parameters()
        {
            //Arrange
            _mockMaximoEndPoint.Object.Url = "http://www.google.com";

            //Act
            var maximoRequestBuilder = new MaximoSiteRequestBuilder(_mockMaximoEndPoint.Object);

            var httpRequestMessage = maximoRequestBuilder.WithMaxItems(50).WithStartIndex(1).WithSubBusinessLineBuilder("test").Build();

            //Assert
            httpRequestMessage.Should().NotBeNull();
            httpRequestMessage.RequestUri.Should().NotBeNull();
            httpRequestMessage.RequestUri.AbsoluteUri.Should().NotBeNull();
            httpRequestMessage.RequestUri.AbsoluteUri.Should().Be("http://www.google.com/maximo/api/os/SLB_R_LOCATION_API01?lean=1&oslc.select=*&_dropnull=0&collectioncount=1&oslc.pageSize=50&pageno=1&oslc.where=slb_sub_bl=%22test%22");
        }

        [TestMethod]
        public void Verify_Maximo_Site_Request_All_Parameters_Except_SubBusinessLine()
        {
            //Arrange
            _mockMaximoEndPoint.Object.Url = "http://www.google.com";

            //Act
            var maximoRequestBuilder = new MaximoSiteRequestBuilder(_mockMaximoEndPoint.Object);

            var httpRequestMessage = maximoRequestBuilder.WithMaxItems(50).WithStartIndex(1).WithSubBusinessLineBuilder("").Build();

            //Assert
            httpRequestMessage.Should().NotBeNull();
            httpRequestMessage.RequestUri.Should().NotBeNull();
            httpRequestMessage.RequestUri.AbsoluteUri.Should().NotBeNull();
            httpRequestMessage.RequestUri.AbsoluteUri.Should().Be("http://www.google.com/maximo/api/os/SLB_R_LOCATION_API01?lean=1&oslc.select=*&_dropnull=0&collectioncount=1&oslc.pageSize=50&pageno=1");
        }
    }
}
