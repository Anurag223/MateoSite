#region Header

// Schlumberger Private
// Copyright 2020 Schlumberger.  All rights reserved in Schlumberger
// authored and generated code (including the selection and arrangement of
// the source code base regardless of the authorship of individual files),
// but not including any copyright interest(s) owned by a third party
// related to source code or object code authored or generated by
// non-Schlumberger personnel.
// This source code includes Schlumberger confidential and/or proprietary
// information and may include Schlumberger trade secrets. Any use,
// disclosure and/or reproduction is prohibited unless authorized in
// writing.

#endregion

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Tlm.Fed.Contexts.Common.Services;
using Tlm.Fed.Framework.Common.ServiceClient;
using Tlm.Sdk.AspNetCore;
using Tlm.Sdk.Core;
using Tlm.Sdk.Core.Models.Infrastructure;
using Tlm.Sdk.Testing.Unit;

namespace Contexts.Common.Tests
{
    [TestClass]
    public class MasterDataSiteTypeMappingServiceTests : UnitTestBase
    {
        private readonly Mock<IHttpService> _httpService;
        private readonly Mock<IMateoAuthenticationService> _mateoAuthenticationService;
        private readonly Mock<IInternalUriBuilder> _uriBuilder;

        #region FilePaths
        private const string MasterDataSiteTypeMappingResponsePath = "Data\\MasterSiteTypeMappingResponse.json";
        #endregion

        public MasterDataSiteTypeMappingServiceTests()
        {
            var mockRepository = new MockRepository(MockBehavior.Strict);
            _httpService = mockRepository.Create<IHttpService>();
            _mateoAuthenticationService = mockRepository.Create<IMateoAuthenticationService>();
            _uriBuilder = mockRepository.Create<IInternalUriBuilder>();
        }

        [TestMethod]
        public void GivenValidSourceAndCMMSId_ReturnsMatchedSiteMapName()
        {
            //Arrange
            _httpService.Setup(x => x.ServiceCaller(It.IsAny<HttpRequestMessage>(), It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(GetHttpResponse()));

            _mateoAuthenticationService.Setup(x => x.GetAccessToken(It.IsAny<string>())).Returns(Task.FromResult("testToken"));
            _uriBuilder.Setup(x => x.MakeInternalUriBuilder()).Returns(GetUriBuilderEx());
            var masterDataService = new MasterDataSiteTypeMappingService(_httpService.Object, _mateoAuthenticationService.Object, _uriBuilder.Object);

            //Act
            var result = masterDataService.GetSiteTypeMappingName("0001", CmmsId.SAP);

            //Assert
            result.Should().Be("Operations");
        }

        [TestMethod]
        public void GivenValidSource_InAnyCase_WithSpace_AndCMMSId_ReturnsMatchedSiteMapName()
        {
            //Arrange
            _httpService.Setup(x => x.ServiceCaller(It.IsAny<HttpRequestMessage>()
                , It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(GetHttpResponse()));
            _mateoAuthenticationService.Setup(x => x.GetAccessToken(It.IsAny<string>())).Returns(Task.FromResult("testToken"));
            _uriBuilder.Setup(x => x.MakeInternalUriBuilder()).Returns(GetUriBuilderEx());
            var masterDataService = new MasterDataSiteTypeMappingService(_httpService.Object, _mateoAuthenticationService.Object, _uriBuilder.Object);

            //Act
            var result = masterDataService.GetSiteTypeMappingName("Base", CmmsId.QTrac);
            var result1 = masterDataService.GetSiteTypeMappingName("BASE", CmmsId.QTrac);
            var result2 = masterDataService.GetSiteTypeMappingName("BaSE", CmmsId.QTrac);
            var result3 = masterDataService.GetSiteTypeMappingName("BaSE ", CmmsId.QTrac);

            //Assert
            result.Should().Be("WorkCenter");
            result.Should().Be(result1);
            result1.Should().Be(result2);
            result2.Should().Be(result3);
        }

        [TestMethod]
        public void GivenValidSource_InAnyCase_WithSpace_AndCMMSId_ReturnsMatchedSiteTypeDescription()
        {
            //Arrange
            _httpService
                .Setup(x => x.ServiceCaller(It.IsAny<HttpRequestMessage>(), It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(GetHttpResponse()));

            _mateoAuthenticationService.Setup(x => x.GetAccessToken(It.IsAny<string>())).Returns(Task.FromResult("testToken"));
            _uriBuilder.Setup(x => x.MakeInternalUriBuilder()).Returns(GetUriBuilderEx());
            var masterDataService = new MasterDataSiteTypeMappingService(_httpService.Object, _mateoAuthenticationService.Object, _uriBuilder.Object);

            //Act
            var result = masterDataService.GetSiteTypeMappingDescription("Base", CmmsId.QTrac);
            var result1 = masterDataService.GetSiteTypeMappingDescription("BASE", CmmsId.QTrac);
            var result2 = masterDataService.GetSiteTypeMappingDescription("BaSE", CmmsId.QTrac);
            var result3 = masterDataService.GetSiteTypeMappingDescription("BaSE ", CmmsId.QTrac);

            //Assert
            result.Should().Be("This is dummy description");
            result.Should().Be(result1);
            result1.Should().Be(result2);
            result2.Should().Be(result3);
        }

        [TestMethod]
        public void GivenInValidSourceAndCMMSId_ReturnsMatchedSiteMapName()
        {
            //Arrange
            _httpService
                .Setup(x => x.ServiceCaller(It.IsAny<HttpRequestMessage>(), It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(GetHttpResponse()));

            _mateoAuthenticationService.Setup(x => x.GetAccessToken(It.IsAny<string>())).Returns(Task.FromResult("testToken"));
            _uriBuilder.Setup(x => x.MakeInternalUriBuilder()).Returns(GetUriBuilderEx());
            var masterDataService = new MasterDataSiteTypeMappingService(_httpService.Object, _mateoAuthenticationService.Object, _uriBuilder.Object);

            //Act
            var result = masterDataService.GetSiteTypeMappingName("6788", CmmsId.None);

            //Assert
            result.Should().Be("6788");
        }

        [TestMethod]
        public void GivenNullSourceAndCMMSId_ReturnsMatchedSiteMapName()
        {
            //Arrange
            _httpService.Setup(x => x.ServiceCaller(It.IsAny<HttpRequestMessage>()
                , It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(GetHttpResponse()));
            _mateoAuthenticationService.Setup(x => x.GetAccessToken(It.IsAny<string>())).Returns(Task.FromResult("testToken"));
            _uriBuilder.Setup(x => x.MakeInternalUriBuilder()).Returns(GetUriBuilderEx());
            var masterDataService = new MasterDataSiteTypeMappingService(_httpService.Object, _mateoAuthenticationService.Object, _uriBuilder.Object);

            //Act
            var result = masterDataService.GetSiteTypeMappingName(null, CmmsId.None);

            //Assert
            result.Should().Be("Unknown");
        }

        [TestMethod]
        public void GivenValidSourceAndCMMSId_ReturnsMatchedSiteMapDescription()
        {
            //Arrange
            _httpService.Setup(x => x.ServiceCaller(It.IsAny<HttpRequestMessage>()
                , It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(GetHttpResponse()));
            _mateoAuthenticationService.Setup(x => x.GetAccessToken(It.IsAny<string>())).Returns(Task.FromResult("testToken"));
            _uriBuilder.Setup(x => x.MakeInternalUriBuilder()).Returns(GetUriBuilderEx());
            var masterDataService = new MasterDataSiteTypeMappingService(_httpService.Object, _mateoAuthenticationService.Object, _uriBuilder.Object);

            //Act
            var result = masterDataService.GetSiteTypeMappingDescription("0001", CmmsId.SAP);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Machine", result);
        }

        [TestMethod]
        public void GivenInValidSourceAndCMMSId_ReturnsMatchedSiteMapDescription()
        {
            //Arrange
            _httpService.Setup(x => x.ServiceCaller(It.IsAny<HttpRequestMessage>()
                , It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(GetHttpResponse()));
            _mateoAuthenticationService.Setup(x => x.GetAccessToken(It.IsAny<string>())).Returns(Task.FromResult("testToken"));
            _uriBuilder.Setup(x => x.MakeInternalUriBuilder()).Returns(GetUriBuilderEx());
            var masterDataService = new MasterDataSiteTypeMappingService(_httpService.Object, _mateoAuthenticationService.Object, _uriBuilder.Object);

            //Act
            var result = masterDataService.GetSiteTypeMappingDescription("6788", CmmsId.None);

            //Assert
            result.Should().Be("NA");
        }

        [TestMethod]
        public void GivenNullSourceAndCMMSId_ReturnsMatchedSiteMapDescription()
        {
            //Arrange
            _httpService.Setup(x => x.ServiceCaller(It.IsAny<HttpRequestMessage>()
                , It.IsAny<TimeSpan>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(GetHttpResponse()));
            _mateoAuthenticationService.Setup(x => x.GetAccessToken(It.IsAny<string>())).Returns(Task.FromResult("testToken"));
            _uriBuilder.Setup(x => x.MakeInternalUriBuilder()).Returns(GetUriBuilderEx());
            var masterDataService = new MasterDataSiteTypeMappingService(_httpService.Object, _mateoAuthenticationService.Object, _uriBuilder.Object);

            //Act
            var result = masterDataService.GetSiteTypeMappingDescription(null, CmmsId.None);

            //Assert
            result.Should().Be("NA");
        }

        private HttpResponseMessage GetHttpResponse()
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,

                Content = new StringContent(File.ReadAllText(MasterDataSiteTypeMappingResponsePath))
            };
        }

        private UriBuilderEx GetUriBuilderEx()
        {
            return new UriBuilderEx("testUri");
        }
    }
}