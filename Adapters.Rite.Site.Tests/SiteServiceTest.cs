﻿#region Header

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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using NSubstitute;
using Tlm.Fed.Adapters.Rite.Common.Services.Interfaces;
using Tlm.Fed.Adapters.Rite.Common.Services.ServiceModels.Organization;
using Tlm.Fed.Adapters.Rite.Site;
using Tlm.Fed.Adapters.Rite.Site.Services;
using Tlm.Fed.Contexts.Site.Core.Services;
using Tlm.Fed.Models.Canonical.MasterData;
using Tlm.Fed.Models.Canonical.SiteDomain;
using Tlm.Sdk.Core.Data;
using Tlm.Sdk.Core.Models.Querying;
using Tlm.Sdk.Testing.Unit;

namespace Adapters.Rite.Site.Tests
{
    [TestClass]
    [UnitTestCategory]
    public class SiteServiceTest : UnitTestBase
    {
        private readonly IMapper _mapper;
        private MockRepository _mockRepository;
        private Mock<IOrganizationService> _riteOrganizationService;
        private Mock<ISegmentMappingService> _segmentMappingService;
        private Mock<IFacilityMasterDataService> _masterDataService;
        private Mock<IReadOnlyRepo<Facility>> _facilityRepo;


        public SiteServiceTest()
        {
            var configuration = new MapperConfiguration(config => config.AddProfile(new OrganizationToWorkCenterSiteMappingProfile()));
            _mapper = configuration.CreateMapper();
        }

        #region FilePaths

        private const string OrganizationResponseFilePath = "Data\\OrganizationDetailResponse.json";
        private const string MateoResponseFilePath = "Data\\MateoSiteList.json";
        private const string FacilityMasterData = "Data\\FacilityDummy.json";

        #endregion

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new MockRepository(MockBehavior.Loose);
            _riteOrganizationService = _mockRepository.Create<IOrganizationService>();
            _segmentMappingService = _mockRepository.Create<ISegmentMappingService>();
            _masterDataService = _mockRepository.Create<IFacilityMasterDataService>();
            _facilityRepo = _mockRepository.Create<IReadOnlyRepo<Facility>>();
        }

        [TestMethod]
        public async Task Should_return_Non_Deleted_Site_list()
        {
            var list = GetOrganizationDetails();
            _riteOrganizationService.Setup(g => g.GetOrganizationDetails(It.IsAny<string>()))
                .Returns(Task.FromResult(list));
            _segmentMappingService.Setup(x => x.UpdateSegments(It.IsAny<List<WorkCenterSite>>()))
                .Returns(Task.FromResult(GetWorkCentreSites()));

            var mockDummyFacility = GetFacilityDetails();
            _masterDataService.Setup(x => x.GetFacilityMasterData(It.IsAny<List<string>>()))
                .Returns(Task.FromResult(mockDummyFacility));
            var uat = GetSiteService(_riteOrganizationService.Object);
            var result = await uat.GetSite("test", null);
            result.Should().NotBeNull();
            result.Count.Should().Be(list.Where(x => x.Deleted == 0).ToList().Count);
        }

        [TestMethod]
        public async Task FiltersRecords_GivenSyncDate_ReturnsFilteredList()
        {
            var list = GetOrganizationDetails();
            _riteOrganizationService.Setup(g => g.GetOrganizationDetails(It.IsAny<string>()))
                .Returns(Task.FromResult(list));
            _segmentMappingService.Setup(x => x.UpdateSegments(It.IsAny<List<WorkCenterSite>>()))
                .ReturnsAsync((List<WorkCenterSite> items) => items);

            var mockDummyFacility = GetFacilityDetails();
            _masterDataService.Setup(x => x.GetFacilityMasterData(It.IsAny<List<string>>()))
                .Returns(Task.FromResult(mockDummyFacility));
            var uat = GetSiteService(_riteOrganizationService.Object);
            var result = await uat.GetSite("test", null);
            result.Should().NotBeNull();
            result.Count.Should().Be(3684);
            result.Count.Should().Be(list.Where(x => x.Deleted == 0).ToList().Count);

            _segmentMappingService.Setup(x => x.UpdateSegments(It.IsAny<List<WorkCenterSite>>()))
                .ReturnsAsync((List<WorkCenterSite> items) => items);
            result = await uat.GetSite("test", new DateTime(2018, 10, 08, 0, 0, 0).ToUniversalTime());
            result.Should().NotBeNull();
            result.Count.Should().Be(2635);
        }

        [TestMethod]
        public async Task Should_Return_Null_Without_SubSegmentList()
        {
            var list = GetOrganizationDetails();
            _riteOrganizationService.Setup(g => g.GetOrganizationDetails(It.IsAny<string>()))
                .Returns(Task.FromResult(list));
            var mockDummyFacility = GetFacilityDetails();
            _masterDataService.Setup(x => x.GetFacilityMasterData(It.IsAny<List<string>>()))
                .Returns(Task.FromResult(mockDummyFacility));
            var uat = GetSiteService(_riteOrganizationService.Object);
             var result = await uat.GetSite("test", (new System.DateTime(2018, 10, 08, 0, 0, 0)).ToUniversalTime());
            result.Should().BeNull();
        }

        [TestMethod]
        public void Should_Return_FacilityMasterData()
        {
            var orgList = GetOrganizationDetails();
            _riteOrganizationService.Setup(_ => _.GetOrganizationDetails(It.IsAny<string>()))
                .Returns(Task.FromResult(orgList));
            var facilityDetails = GetFacilityDetails();
            var result = _masterDataService.Setup(x => x.GetFacilityMasterData(It.IsAny<List<string>>()))
                .Returns(Task.FromResult(facilityDetails));
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task Test_Should_FillOrganizationalDataWithFacilityMasterData()
        {
            var orgList = GetOrganizationDetails();
            _riteOrganizationService.Setup(_ => _.GetOrganizationDetails(It.IsAny<string>()))
                .Returns(Task.FromResult(orgList));
            _segmentMappingService.Setup(x => x.UpdateSegments(It.IsAny<List<WorkCenterSite>>()))
                .Returns(Task.FromResult(GetWorkCentreSites()));

            var mockDummyFacility = GetFacilityDetails();
            _masterDataService.Setup(x => x.GetFacilityMasterData(It.IsAny<List<string>>()))
                .Returns(Task.FromResult(mockDummyFacility));

            var uat = GetSiteService(_riteOrganizationService.Object);
            var result = await uat.GetSite("adk", null);
            result.Should().NotBeNull();
        }

        private ISiteService GetSiteService(IOrganizationService organizationService)
        {
            return new SiteService(organizationService, _mapper, _segmentMappingService.Object, _masterDataService.Object);
        }

        private List<WorkCenterSite> GetWorkCentreSites()
        {
            var workCenterSites = ReadJson<List<WorkCenterSite>>(MateoResponseFilePath);
            return workCenterSites;
        }

        private List<OrganizationDetail> GetOrganizationDetails()
        {
            var data = ReadJson<OrganizationResponse>(OrganizationResponseFilePath);
            return data.OrganizationDetails.ToList();
        }

        private List<Facility> GetFacilityDetails()
        {
            var data = ReadJson<List<Facility>>(FacilityMasterData);
            return data;
        }

        

    }
}