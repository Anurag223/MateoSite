#region Header

// Schlumberger Private
// Copyright 2018 Schlumberger.  All rights reserved in Schlumberger
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
using MongoDB.Bson;
using Tlm.Fed.Adapters.Rite.Common.Services.Interfaces;
using Tlm.Fed.Adapters.Rite.Common.Services.ServiceModels.Organization;
using Tlm.Fed.Contexts.Site.Core.Services;
using Tlm.Fed.Models.Canonical.MasterData;
using Tlm.Fed.Models.Canonical.SiteDomain;
using Tlm.Sdk.Api;
using Tlm.Sdk.Core.Data;
using Tlm.Sdk.Core.Models.Querying;

namespace Tlm.Fed.Adapters.Rite.Site.Services
{
    public class SiteService : ISiteService
    {
        public SiteService(IOrganizationService orgService, IMapper mapper, ISegmentMappingService segmentMappingService, IFacilityMasterDataService facilityMasterDataService)
        {
            _orgService = orgService;
            _mapper = mapper;
            _segmentMappingService = segmentMappingService;
            _facilityMasterDataService = facilityMasterDataService;
        }

        private static readonly IList<string> SubSegmentList = new List<string>();
        private readonly IMapper _mapper;
        private readonly IOrganizationService _orgService;
        private readonly ISegmentMappingService _segmentMappingService;
        private readonly IFacilityMasterDataService _facilityMasterDataService;

        public async Task<IReadOnlyCollection<WorkCenterSite>> GetSite(string segment, DateTime? syncDate)
        {
            var retList = new List<WorkCenterSite>();
            var orgList = await _orgService.GetOrganizationDetails(segment);
            var result = orgList.Select(x => x.FacilitiesID).ToList();
            var filteredData = await _facilityMasterDataService.GetFacilityMasterData(result);
            if (orgList != null)
                retList = await SiteListFromOrganizatons(orgList, syncDate, filteredData);
            return retList;
        }

        private async Task<List<WorkCenterSite>> SiteListFromOrganizatons(List<OrganizationDetail> orgList, DateTime? syncDate, List<Facility> facilityMasterData)
        {
            var retList = new List<WorkCenterSite>();
            orgList = orgList.FindAll(x => x.Deleted == null || x.Deleted.Value <= 0);

            if (syncDate != null)
                orgList = orgList.FindAll(x =>
                {
                    if (!SubSegmentList.Contains(x.SubSegment))
                        return true;
                    if (string.IsNullOrEmpty(x.LastModified))
                        return false;

                    var count = x.LastModified.Count(y => y == ':');
                    if (count == 3)
                        x.LastModified = x.LastModified.Substring(0, x.LastModified.LastIndexOf(":"));

                    var isCorrectDate = DateTime.TryParse(x.LastModified, out var convertDate);
                    if (isCorrectDate && convertDate > syncDate)
                        return true;

                    return false;
                });

            var distinctList = orgList.Select(x => x.SubSegment).Distinct();
            if (distinctList.Any())
                foreach (var distItem in distinctList)
                    if (!SubSegmentList.Contains(distItem))
                        SubSegmentList.Add(distItem);

            FillOrganizationWithFacilityMasterData(orgList, facilityMasterData);
            retList = _mapper.Map<WorkCenterSite[]>(orgList).ToList();

            retList = await _segmentMappingService.UpdateSegments(retList);

            return retList;
        }

        public void FillOrganizationWithFacilityMasterData(List<OrganizationDetail> orgList, List<Facility> facilityMasterData)
        {
            if (facilityMasterData.Any())
            {
                orgList.ForEach(y =>
                {
                    var data = facilityMasterData.Where(x => x.FmdCommonId == y.FacilitiesID).FirstOrDefault();
                    if (data != null)
                    {
                        y.FacilityName = data.Name;
                        y.Latitude = data.Latitude;
                        y.Longitude = data.Longitude;
                        y.Status = data.Status;
                        y.Description = data.Description;
                        y.Ownership = data.Ownership;
                        y.Country = data.CountryName;
                        y.CountryCode = data.CountryCode;
                    }
                });
            }
            
        }

    }
}