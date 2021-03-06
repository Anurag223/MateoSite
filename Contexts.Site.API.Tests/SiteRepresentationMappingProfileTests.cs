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

using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tlm.Fed.Contexts.Site.Core.RepresentationModel;
using Tlm.Fed.Models.Canonical.SiteDomain;
using Tlm.Sdk.Core.Models.Infrastructure;
using Tlm.Sdk.Testing.Unit;

namespace Tlm.Fed.Contexts.Site.API.Tests
{
    [TestClass]
    public class SiteRepresentationMappingProfileTests : UnitTestBase
    {
        private readonly MapperConfiguration _configuration;
        private readonly IMapper _mapper;

        #region FilePaths

        private const string SiteCanonicalDataFilePath = "Data\\SiteCanonicalData.json";
        private const string SiteCanonicalMaximoRiteQtracDataFilePath = "Data\\SiteCanonicalDataMaximoQtracRite.json";

        #endregion

        public SiteRepresentationMappingProfileTests()
        {
            _configuration = new MapperConfiguration(config => config.AddProfile(new SiteRepresentationMappingProfile()));
            _mapper = _configuration.CreateMapper();
        }

        /// <summary>
        ///     Validates whether mapper configurations are valid or not.
        /// </summary>
        [TestMethod]
        public void AssertConfigurationIsValid_NoExceptionThrown()
        {
            // Assert
            _configuration.AssertConfigurationIsValid();
        }

        /// <summary>
        ///     Given: Maximo site <see cref="WorkCenterSite" />
        ///     When: Mapper is configured.
        ///     Then: Returns Mateo WorkCenterSite Object <see cref="SiteRepresentation" />
        /// </summary>
        [TestMethod]
        [DeploymentItem(SiteCanonicalDataFilePath)]
        public void GivenMateoWorkCenterSiteObject_ReturnsMateoSiteRepresentationalObject()
        {
            // Arrange
            var workCenterSite = ReadJson<List<WorkCenterSite>>(SiteCanonicalDataFilePath);

            // Act
            var siteRepresentation = _mapper.Map<SiteRepresentation[]>(workCenterSite.ToArray());

            //Assert
            siteRepresentation.Length.Should().Be(workCenterSite.Count);
            for (var i = 0; i < siteRepresentation.Length; i++)
            {
                siteRepresentation[i].CountryCode.Should().Be(workCenterSite[i].CountryCode);
                siteRepresentation[i].City.Should().Be(workCenterSite[i].City);
                Assert.AreEqual(siteRepresentation[i].Attributes.Count, 1);
                var siteAttributes = siteRepresentation[i].Attributes.FirstOrDefault(x => x.Code == "DEFAULTLOCATION");
                var workCenterAttributes = workCenterSite[i].Attributes.FirstOrDefault(x => x.Code == "DEFAULTLOCATION");
                Assert.AreEqual(siteAttributes.Value, workCenterAttributes.Value);
                siteRepresentation[i].Code.Should().Be(workCenterSite[i].Code.Value);
                siteRepresentation[i].Description.Should().Be(workCenterSite[i].Description);
                siteRepresentation[i].Name.Should().Be(workCenterSite[i].Name);
                siteRepresentation[i].Facility.Id.Should().Be(workCenterSite[i].FacilityId);
                siteRepresentation[i].Facility.Name.Should().Be(workCenterSite[i].FacilityName);
                siteRepresentation[i].Facility.Longitude.Should().Be(workCenterSite[i].GetUnmanagedAttribute(Cmms.Systems[siteRepresentation[i].ActiveCmmsId].Code, CanonicalSiteConstant.Longitude)?.Value);
                siteRepresentation[i].Facility.Latitude.Should().Be(workCenterSite[i].GetUnmanagedAttribute(Cmms.Systems[siteRepresentation[i].ActiveCmmsId].Code, CanonicalSiteConstant.Latitude)?.Value);
                siteRepresentation[i].Classifications.Count.Should().BeGreaterOrEqualTo(1);
                siteRepresentation[i].Id.Should().Be(workCenterSite[i].Id);
                if (workCenterSite[i].SubSites != null)
                    siteRepresentation[i].SubSite.Count.Should().Be(workCenterSite[i].SubSites.Count);
            }
        }

        /// <summary>
        ///     Given: Maximo site <see cref="WorkCenterSite" />
        ///     When: Mapper is configured.
        ///     Then: Returns Mateo WorkCenterSite Object <see cref="SiteRepresentation" />
        /// </summary>
        [TestMethod]
        [DeploymentItem(SiteCanonicalMaximoRiteQtracDataFilePath)]
        public void GivenMateoWorkCenterSiteObject_WithClassification_ReturnsMateoSiteRepresentationalObject()
        {
            // Arrange
            var workCenterSite = ReadJson<List<WorkCenterSite>>(SiteCanonicalMaximoRiteQtracDataFilePath);

            // Act
            var siteRepresentation = _mapper.Map<SiteRepresentation[]>(workCenterSite.ToArray());

            //Assert
            siteRepresentation.Length.Should().Be(workCenterSite.Count);
            for (var i = 0; i < siteRepresentation.Length; i++)
            {
                siteRepresentation[i].Code.Should().Be(workCenterSite[i].Code.Value);
                siteRepresentation[i].Description.Should().Be(workCenterSite[i].Description);
                siteRepresentation[i].Name.Should().Be(workCenterSite[i].Name);
                siteRepresentation[i].Classifications.Count.Should().BeGreaterOrEqualTo(1);
                siteRepresentation[i].Id.Should().Be(workCenterSite[i].Id);
                if (workCenterSite[i].SubSites != null)
                    siteRepresentation[i].SubSite.Count.Should().Be(workCenterSite[i].SubSites.Count);
                var classification = siteRepresentation[i].Classifications;
                switch (siteRepresentation[i].ActiveCmmsId)
                {
                    case CmmsId.QTrac:
                    {
                        siteRepresentation[i].Facility.Id.Should().Be(workCenterSite[i].FacilityId);
                        siteRepresentation[i].Facility.Name.Should().Be(workCenterSite[i].FacilityName);
                        siteRepresentation[i].Facility.Longitude.Should().Be(workCenterSite[i].GetUnmanagedAttribute(Cmms.Systems[siteRepresentation[i].ActiveCmmsId].Code, CanonicalSiteConstant.Longitude)?.Value);
                        siteRepresentation[i].Facility.Latitude.Should().Be(workCenterSite[i].GetUnmanagedAttribute(Cmms.Systems[siteRepresentation[i].ActiveCmmsId].Code, CanonicalSiteConstant.Latitude)?.Value);

                        classification[WorkCenterSite.SiteClassificationValueSlOrgBusinessView].Should().Contain(x => x.Type == nameof(SiteClassificationType.SubSegment));
                        classification[WorkCenterSite.SiteClassificationValueSlOrgBusinessView].Should().Contain(x => x.Type == nameof(SiteClassificationType.Segment));

                        classification[WorkCenterSite.SiteClassificationValueSlOrgBusinessTeam].Should().Contain(x => x.Type == nameof(SiteClassificationType.SubGeoMarket));
                        classification[WorkCenterSite.SiteClassificationValueSlOrgBusinessTeam].Should().NotContain(x => x.Type == nameof(SiteClassificationType.GeoMarket));
                    }
                        break;
                    case CmmsId.RITE:
                    {
                        classification[WorkCenterSite.SiteClassificationValueSlOrgBusinessView].Should().Contain(x => x.Type == nameof(SiteClassificationType.SubSegment));
                        classification[WorkCenterSite.SiteClassificationValueSlOrgBusinessView].Should().Contain(x => x.Type == nameof(SiteClassificationType.Segment));

                        classification[WorkCenterSite.SiteClassificationValueSlOrgBusinessTeam].Should().Contain(x => x.Type == nameof(SiteClassificationType.SubGeoMarket));
                        classification[WorkCenterSite.SiteClassificationValueSlOrgBusinessTeam].Should().NotContain(x => x.Type == nameof(SiteClassificationType.GeoMarket));
                    }
                        break;
                    case CmmsId.Maximo:
                    {
                        siteRepresentation[i].Facility.Id.Should().Be(workCenterSite[i].FacilityId);
                        siteRepresentation[i].Facility.Name.Should().Be(workCenterSite[i].FacilityName);
                        siteRepresentation[i].Facility.Longitude.Should().Be(workCenterSite[i].GetUnmanagedAttribute(Cmms.Systems[siteRepresentation[i].ActiveCmmsId].Code, CanonicalSiteConstant.Longitude)?.Value);
                        siteRepresentation[i].Facility.Latitude.Should().Be(workCenterSite[i].GetUnmanagedAttribute(Cmms.Systems[siteRepresentation[i].ActiveCmmsId].Code, CanonicalSiteConstant.Latitude)?.Value);

                        classification[WorkCenterSite.SiteClassificationValueSlOrgBusinessView].Should().Contain(x => x.Type == nameof(SiteClassificationType.Segment));
                    }
                        break;
                }
            }
        }
    }
}