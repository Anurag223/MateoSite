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

using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tlm.Fed.Contexts.Common;
using Tlm.Fed.Contexts.Site.Core.RepresentationModel;
using Tlm.Fed.Models.Canonical.MasterData;
using Tlm.Fed.Models.Canonical.SiteDomain;
using Tlm.Sdk.Core.Data;
using Tlm.Sdk.Core.Models;
using Tlm.Sdk.Core.Models.Infrastructure;
using Tlm.Sdk.Core.Models.Querying;
using Tlm.Sdk.Testing.Unit;

namespace Tlm.Fed.Contexts.Site.Composer.Tests
{
    [TestClass]
    public class SiteInboundComposerTest : UnitTestBase
    {
        private readonly IMapper _mapper;
        private const string SiteCanonicalDataFilePath = "Data\\SiteCanonicalData.json";
        private const string SiteCanonicalMaximoRiteQtracDataFilePath = "Data\\SiteCanonicalDataMaximoQtracRite.json";
        private const string BusinessTeamDataFilePath = "Data\\BusinessTeam.json";
        private const string BusinessViewDataFilePath = "Data\\BusinessView.json";
        private const string DivisionViewDataFilePath = "Data\\DivisionView.json";
        private const string BasinViewDataFilePath = "Data\\BasinView.json";

        public SiteInboundComposerTest()
        {
            var configuration = new MapperConfiguration(config => config.AddProfile(new SiteRepresentationMappingProfile()));
            _mapper = configuration.CreateMapper();
        }

        private static IReadOnlyRepo<T> CreateMockRepo<T>(CollectionResult<T> canonicalModels)
            where T : Entity
        {
            var repoMock = new Mock<IReadOnlyRepo<T>>(MockBehavior.Loose);
            repoMock.Setup(x => x.QueryManyAsync(It.IsAny<QuerySpec>())).ReturnsAsync(canonicalModels);
            return repoMock.Object;
        }

        [TestMethod]
        public void Instantiate_Component_NoExceptionThrown()
        {
            Assert.IsNotNull(TryInstantiateComponent<SiteInboundComposer, StartupForSiteComposer, SiteComposerConfiguration>(
                additionalConfigurations: builder => builder.AddJsonFile("testsettings.shared.json", false)));
        }

        [TestMethod]
        [DeploymentItem(SiteCanonicalDataFilePath)]
        public async Task Compose_Should_Generate_Representation_And_Update_Classification_Parent_Code()
        {
            //arrange
            var workCenterSite = ReadJson<List<WorkCenterSite>>(SiteCanonicalDataFilePath);
            var siteCanonicalRepo = CreateMockRepo(new CollectionResult<WorkCenterSite>(workCenterSite));
            var businessViewRepo = GetBusinessViewRepo();
            var divisionViewRepo = GetDivisionViewRepo();
            var basinViewRepo = GetBasinViewRepo();
            var businessTeamRepo = GetBusinessTeamRepo();
            var segmentRepo = GetSegmentMappingTypeRepo();
            var facilityRepo = CreateMockRepo(GetFacilityMapData());
            var uat = new SiteInboundComposer(_mapper, siteCanonicalRepo, businessViewRepo, divisionViewRepo, basinViewRepo, businessTeamRepo, segmentRepo, facilityRepo, GetComposerConfig());

            //act
            var result = await uat.ComposeAsync(workCenterSite.ToList());

            //assert
            result.Count.Should().Be(workCenterSite.Count);
            var classification = result.First().Classifications.ToList();
            classification.Count.Should().BeLessOrEqualTo(2);
            var businessTeam = classification.FirstOrDefault(s => s.Key == WorkCenterSite.SiteClassificationValueSlOrgBusinessTeam);
            var businessView = classification.FirstOrDefault(s => s.Key == WorkCenterSite.SiteClassificationValueSlOrgBusinessView);
            businessTeam.Should().NotBeNull();
            businessView.Should().NotBeNull();
        }

        private static SiteComposerConfiguration GetComposerConfig()
        {
            return new SiteComposerConfiguration
            {
                FeatureFlags = new FeatureFlag
                {
                    ServeBasinView = true,
                    ServeBusinessTeam = true
                }
            };
        }

        [TestMethod]
        [DeploymentItem(SiteCanonicalMaximoRiteQtracDataFilePath)]
        public async Task Compose_Should_Generate_Representation_And_Update_Classification_Parent_Code_For_SubSegment()
        {
            //arrange
            var workCenterSite = ReadJson<List<WorkCenterSite>>(SiteCanonicalMaximoRiteQtracDataFilePath);
            var siteCanonicalRepo = CreateMockRepo(new CollectionResult<WorkCenterSite>(workCenterSite));
            var businessViewRepo = GetBusinessViewRepo();
            var divisionViewRepo = GetDivisionViewRepo();
            var basinViewRepo = GetBasinViewRepo();
            var businessTeamRepo = GetBusinessTeamRepo();
            var segmentRepo = GetSegmentMappingTypeRepo();
            var facilityRepo = CreateMockRepo(GetFacilityMapData());
            var uat = new SiteInboundComposer(_mapper, siteCanonicalRepo, businessViewRepo, divisionViewRepo, basinViewRepo, businessTeamRepo, segmentRepo, facilityRepo, GetComposerConfig());

            //act
            var result = await uat.ComposeAsync(workCenterSite.ToList());

            //assert
            result.Count.Should().Be(workCenterSite.Count);
            foreach (var siteRepresentation in result)
            {
                var classification = siteRepresentation.Classifications;

                switch (siteRepresentation.ActiveCmmsId)
                {
                    case CmmsId.QTrac:
                    {
                        classification.Count.Should().BeLessOrEqualTo(4);
                        var businessTeam = classification[WorkCenterSite.SiteClassificationValueSlOrgBusinessTeam];
                        var businessView = classification[WorkCenterSite.SiteClassificationValueSlOrgBusinessView];
                        var divisionView = classification[WorkCenterSite.SiteClassificationValueSlOrgDivisionView];

                        businessTeam.Should().NotBeNull();
                        businessView.Should().NotBeNull();
                        divisionView.Should().NotBeNull();

                        businessTeam.Count.Should().Be(6);
                        businessView.Count.Should().Be(3);
                        divisionView.Count.Should().Be(3);

                        businessView.Should().Contain(x => x.Type == nameof(SiteClassificationType.SubSegment));
                        businessView.Should().Contain(x => x.Type == nameof(SiteClassificationType.Segment));
                        businessView.Should().Contain(x => x.Type == nameof(SiteClassificationType.Group));

                        divisionView.Should().Contain(x => x.Type == nameof(SiteClassificationType.SubBusinessLine));
                        divisionView.Should().Contain(x => x.Type == "BusinessLine");
                        divisionView.Should().Contain(x => x.Type == "Division");

                        businessTeam.Should().Contain(x => x.Type == nameof(SiteClassificationType.SubGeoMarket));
                        businessTeam.Should().Contain(x => x.Type == "Geomarket");
                        businessTeam.Should().Contain(x => x.Type == "Area Operations");
                        businessTeam.Should().Contain(x => x.Type == "Hemisphere");
                        businessTeam.Should().Contain(x => x.Type == "Global Operations & Support");
                        businessTeam.Should().Contain(x => x.Type == "Corporate");
                        break;
                    }

                    case CmmsId.RITE:
                    {
                        classification.Count.Should().BeLessOrEqualTo(4);
                        var businessTeam = classification[WorkCenterSite.SiteClassificationValueSlOrgBusinessTeam];
                        var businessView = classification[WorkCenterSite.SiteClassificationValueSlOrgBusinessView];
                        var divisionView = classification[WorkCenterSite.SiteClassificationValueSlOrgDivisionView];

                        businessTeam.Should().NotBeNull();
                        businessView.Should().NotBeNull();
                        divisionView.Should().NotBeNull();

                        businessTeam.Count.Should().Be(6);
                        businessView.Count.Should().Be(3);
                        divisionView.Count.Should().Be(3);

                        businessView.Should().Contain(x => x.Type == nameof(SiteClassificationType.SubSegment));
                        businessView.Should().Contain(x => x.Type == nameof(SiteClassificationType.Segment));
                        businessView.Should().Contain(x => x.Type == nameof(SiteClassificationType.Group));

                        divisionView.Should().Contain(x => x.Type == nameof(SiteClassificationType.SubBusinessLine));
                        divisionView.Should().Contain(x => x.Type == "BusinessLine");
                        divisionView.Should().Contain(x => x.Type == "Division");

                        businessTeam.Should().Contain(x => x.Type == nameof(SiteClassificationType.SubGeoMarket));
                        businessTeam.Should().Contain(x => x.Type == "Geomarket");
                        businessTeam.Should().Contain(x => x.Type == "Area Operations");
                        businessTeam.Should().Contain(x => x.Type == "Hemisphere");
                        businessTeam.Should().Contain(x => x.Type == "Global Operations & Support");
                        businessTeam.Should().Contain(x => x.Type == "Corporate");
                        break;
                    }

                    case CmmsId.Maximo:
                    {
                        classification.Count.Should().BeLessOrEqualTo(1);
                        classification.ContainsKey(WorkCenterSite.SiteClassificationValueSlOrgBusinessTeam).Should().Be(false);
                        var businessView = classification[WorkCenterSite.SiteClassificationValueSlOrgBusinessView];

                        businessView.Should().NotBeNull();
                        businessView.Count.Should().Be(2);

                        businessView.Should().Contain(x => x.Type == nameof(SiteClassificationType.Segment));
                        businessView.Should().Contain(x => x.Type == nameof(SiteClassificationType.Group));
                        break;
                    }
                }
            }
        }

        [TestMethod]
        public async Task Compose_Should_Generate_Representation_And_Update_Classification_Parent_Code_For_Segment_Where_NoMappingFoundFor_SubSegment()
        {
            //arrange
            var workCenterSite = new List<WorkCenterSite>
            {
                new WorkCenterSite
                {
                    Id = "990:KARESC",
                    CreatedBy = "RRANE",
                    CreatedDate = new DateTime(2019, 05, 21, 09, 37, 23, DateTimeKind.Utc),
                    ModifiedBy = "datafix_1278862",
                    ModifiedDate = new DateTime(2019, 05, 21, 09, 37, 23, DateTimeKind.Utc),
                    SyncDate = new DateTime(2019, 05, 21, 09, 37, 23, DateTimeKind.Utc),
                    ActiveCmmsId = CmmsId.QTrac,
                    SourceSystemRecordId = "1000",
                    Attributes = new List<AttributeValue>(),
                    UnmanagedAttributes =
                        new Dictionary<string, Dictionary<string, UnmanagedAttributeValue>>
                        {
                            {
                                "earaa990",
                                new Dictionary<string, UnmanagedAttributeValue>
                                {
                                    { "area", new UnmanagedAttributeValue("area", null, null) },
                                    { "ownership", new UnmanagedAttributeValue("ownership", null, null) },
                                    { "facility_description", new UnmanagedAttributeValue("facility_description", null, null) },
                                    { "Facility_status", new UnmanagedAttributeValue("Facility_status", null, null) },
                                    { "latitude", new UnmanagedAttributeValue("latitude", null, null) },
                                    { "longitude", new UnmanagedAttributeValue("longitude", null, null) }
                                }
                            }
                        },
                    Code = "KARESC",
                    Description = "China Equipment Sales",
                    Name = "China Equipment Sales",
                    AlternateIdentities = new List<AlternateIdentity>(),
                    FacilityId = null,
                    FacilityName = null,
                    SubGeoMarketCode = null,
                    GeoMarketCode = null,
                    SegmentCode = "D & M",
                    SubBusinessLines = new List<string> { "XTRE_RANDOM" },
                    SiteType = "BASE      ",
                    Attribute = null,
                    CountryCode = null,
                    City = null,
                    SubSites = new List<WorkCenterSite>()
                }
            };

            var siteCanonicalRepo = CreateMockRepo(new CollectionResult<WorkCenterSite>(workCenterSite));
            var businessViewRepo = GetBusinessViewRepo();
            var divisionViewRepo = GetDivisionViewRepo();
            var basinViewRepo = GetBasinViewRepo();
            var businessTeamRepo = GetBusinessTeamRepo();
            var segmentRepo = GetSegmentMappingTypeRepo();
            var facilityRepo = CreateMockRepo(GetFacilityMapData());
            var uat = new SiteInboundComposer(_mapper, siteCanonicalRepo, businessViewRepo, divisionViewRepo, basinViewRepo, businessTeamRepo, segmentRepo, facilityRepo, GetComposerConfig());

            //act
            var result = await uat.ComposeAsync(workCenterSite.ToList());

            //assert
            result.Count.Should().Be(workCenterSite.Count);
            foreach (var siteRepresentation in result)
            {
                var classification = siteRepresentation.Classifications;
                classification.Count.Should().BeLessOrEqualTo(2);
                {
                    var businessView = classification[WorkCenterSite.SiteClassificationValueSlOrgBusinessView];

                    businessView.Should().NotBeNull();

                    businessView.Count.Should().Be(2);

                    businessView.Should().Contain(x => x.Type == nameof(SiteClassificationType.SubSegment));
                    businessView.Should().Contain(x => x.Type == nameof(SiteClassificationType.Segment));
                    businessView.Should().NotContain(x => x.Type == nameof(SiteClassificationType.Group));
                }
                {
                    var divisionView = classification[WorkCenterSite.SiteClassificationValueSlOrgDivisionView];

                    divisionView.Should().NotBeNull();

                    divisionView.Count.Should().Be(1);

                    divisionView.Should().Contain(x => x.Type == nameof(SiteClassificationType.SubBusinessLine));
                    divisionView.Should().NotContain(x => x.Type == nameof(SiteClassificationType.SubSegment));
                    divisionView.Should().NotContain(x => x.Type == nameof(SiteClassificationType.Segment));
                    divisionView.Should().NotContain(x => x.Type == nameof(SiteClassificationType.Group));
                }
            }
        }

        [TestMethod]
        public async Task Compose_Should_Generate_More_Than_One_SubbusinessLines_Classification_For_SAP()
        {
            //arrange
            var workCenterSite = new List<WorkCenterSite>
            {
                new WorkCenterSite
                {
                    Id = "2796:1000",
                    CreatedBy = "RRANE",
                    CreatedDate = new DateTime(2019, 05, 21, 09, 37, 23, DateTimeKind.Utc),
                    ModifiedBy = "datafix_1278862",
                    ModifiedDate = new DateTime(2019, 05, 21, 09, 37, 23, DateTimeKind.Utc),
                    SyncDate = new DateTime(2019, 05, 21, 09, 37, 23, DateTimeKind.Utc),
                    ActiveCmmsId = CmmsId.SAP,
                    SourceSystemRecordId = "1000",
                    Code = "1000",
                    Description = "China Plant",
                    Name = "China Plant",
                    FacilityId = null,
                    FacilityName = null,
                    SubGeoMarketCode = null,
                    GeoMarketCode = null,
                    SegmentCode = "ALS",
                    SubBusinessLines = new List<string> { "HPS", "RTAL", "PCP", "ASNS", "SRP", "TALS", "ESP", "AGL" },
                    SiteType = "BASE",
                    Attribute = null,
                    CountryCode = null,
                    City = null,
                    SubSites = new List<WorkCenterSite>()
                }
            };

            var siteCanonicalRepo = CreateMockRepo(new CollectionResult<WorkCenterSite>(workCenterSite));
            var businessViewRepo = GetBusinessViewRepo();
            var divisionViewRepo = GetDivisionViewRepo();
            var basinViewRepo = GetBasinViewRepo();
            var businessTeamRepo = GetBusinessTeamRepo();
            var segmentRepo = GetSegmentMappingTypeRepo();
            var facilityRepo = CreateMockRepo(GetFacilityMapData());
            var uat = new SiteInboundComposer(_mapper, siteCanonicalRepo, businessViewRepo, divisionViewRepo, basinViewRepo, businessTeamRepo, segmentRepo, facilityRepo, GetComposerConfig());

            //act
            var result = await uat.ComposeAsync(workCenterSite.ToList());

            //assert
            result.Count.Should().Be(workCenterSite.Count);
            foreach (var siteRepresentation in result)
            {
                var classification = siteRepresentation.Classifications;
                classification.Count.Should().BeLessOrEqualTo(2);
                var businessView = classification[WorkCenterSite.SiteClassificationValueSlOrgBusinessView];
                businessView.Should().NotBeNull();
                businessView.Count.Should().Be(3);
                businessView.Should().Contain(x => x.Type == nameof(SiteClassificationType.SubSegment));
                businessView.Should().Contain(x => x.Type == nameof(SiteClassificationType.Segment));
                businessView.Should().Contain(x => x.Type == nameof(SiteClassificationType.Group));

                var divisionView = classification[WorkCenterSite.SiteClassificationValueSlOrgDivisionView];
                divisionView.Should().NotBeNull();
                divisionView.Count.Should().Be(11);
                divisionView.Should().Contain(x => x.Type == nameof(SiteClassificationType.SubBusinessLine));
                divisionView.Should().NotContain(x => x.Type == nameof(SiteClassificationType.SubSegment));
                divisionView.Should().NotContain(x => x.Type == nameof(SiteClassificationType.Segment));
                divisionView.Should().NotContain(x => x.Type == nameof(SiteClassificationType.Group));
            }
        }

        [TestMethod]
        public async Task Composer_Should_Not_Generate_Classification_For_DivisionView_Where_SubSegment_IsNull()
        {
            //arrange
            var workCenterSite = new List<WorkCenterSite>
            {
                new WorkCenterSite
                {
                    Id = "990:KARESC",
                    CreatedBy = "RRANE",
                    CreatedDate = new DateTime(2019, 05, 21, 09, 37, 23, DateTimeKind.Utc),
                    ModifiedBy = "datafix_1278862",
                    ModifiedDate = new DateTime(2019, 05, 21, 09, 37, 23, DateTimeKind.Utc),
                    SyncDate = new DateTime(2019, 05, 21, 09, 37, 23, DateTimeKind.Utc),
                    ActiveCmmsId = CmmsId.QTrac,
                    SourceSystemRecordId = "1000",
                    Attributes = new List<AttributeValue>(),
                    UnmanagedAttributes =
                        new Dictionary<string, Dictionary<string, UnmanagedAttributeValue>>
                        {
                            {
                                "earaa990",
                                new Dictionary<string, UnmanagedAttributeValue>
                                {
                                    { "area", new UnmanagedAttributeValue("area", null, null) },
                                    { "ownership", new UnmanagedAttributeValue("ownership", null, null) },
                                    { "facility_description", new UnmanagedAttributeValue("facility_description", null, null) },
                                    { "Facility_status", new UnmanagedAttributeValue("Facility_status", null, null) },
                                    { "latitude", new UnmanagedAttributeValue("latitude", null, null) },
                                    { "longitude", new UnmanagedAttributeValue("longitude", null, null) }
                                }
                            }
                        },
                    Code = "KARESC",
                    Description = "China Equipment Sales",
                    Name = "China Equipment Sales",
                    AlternateIdentities = new List<AlternateIdentity>(),
                    FacilityId = null,
                    FacilityName = null,
                    SubGeoMarketCode = null,
                    GeoMarketCode = null,
                    SegmentCode = "D & M",
                    SubBusinessLines = new List<string>(),
                    SiteType = "BASE      ",
                    Attribute = null,
                    CountryCode = null,
                    City = null,
                    SubSites = new List<WorkCenterSite>()
                }
            };

            var siteRepo = CreateMockRepo(new CollectionResult<WorkCenterSite>(workCenterSite));
            var businessViewRepo = GetBusinessViewRepo();
            var divisionViewRepo = GetDivisionViewRepo();
            var businessTeamRepo = GetBusinessTeamRepo();
            var basinViewRepo = GetBasinViewRepo();
            var segmentRepo = GetSegmentMappingTypeRepo();
            var facilityRepo = CreateMockRepo(GetFacilityMapData());
            var uat = new SiteInboundComposer(_mapper, siteRepo, businessViewRepo, divisionViewRepo, basinViewRepo, businessTeamRepo, segmentRepo, facilityRepo, GetComposerConfig());

            //act
            var result = await uat.ComposeAsync(workCenterSite.ToList());

            //assert
            result.Count.Should().Be(workCenterSite.Count);
            foreach (var siteRepresentation in result)
            {
                var classification = siteRepresentation.Classifications;
                classification.Count.Should().BeLessOrEqualTo(2);

                var businessView = classification[WorkCenterSite.SiteClassificationValueSlOrgBusinessView];
                classification.ContainsKey(WorkCenterSite.SiteClassificationValueSlOrgBusinessView).Should().BeTrue();
                classification.ContainsKey(WorkCenterSite.SiteClassificationValueSlOrgDivisionView).Should().BeFalse();
                businessView.Should().NotBeNull();

                businessView.Count.Should().Be(1);

                businessView.Should().NotContain(x => x.Type == nameof(SiteClassificationType.SubSegment));
                businessView.Should().Contain(x => x.Type == nameof(SiteClassificationType.Segment));
                businessView.Should().NotContain(x => x.Type == nameof(SiteClassificationType.Group));
            }
        }

        private IReadOnlyRepo<BusinessView> GetBusinessViewRepo()
        {
            var list = ReadJson<List<BusinessView>>(BusinessViewDataFilePath);
            return new InMemoryRepo<BusinessView>(list);
        }

        private IReadOnlyRepo<DivisionView> GetDivisionViewRepo()
        {
            var list = ReadJson<List<DivisionView>>(DivisionViewDataFilePath);
            return new InMemoryRepo<DivisionView>(list);
        }

        private IReadOnlyRepo<BasinView> GetBasinViewRepo()
        {
            var list = ReadJson<List<BasinView>>(BasinViewDataFilePath);
            return new InMemoryRepo<BasinView>(list);
        }

        private IReadOnlyRepo<BusinessTeam> GetBusinessTeamRepo()
        {
            var list = ReadJson<List<BusinessTeam>>(BusinessTeamDataFilePath);
            return new InMemoryRepo<BusinessTeam>(list);
        }

        private IReadOnlyRepo<SegmentMapping> GetSegmentMappingTypeRepo()
        {
            return new InMemoryRepo<SegmentMapping>();
        }

        private CollectionResult<FacilityRepresentation> GetFacilityMapData()
        {
            return new CollectionResult<FacilityRepresentation>(Enumerable.Empty<FacilityRepresentation>());
        }
    }
}