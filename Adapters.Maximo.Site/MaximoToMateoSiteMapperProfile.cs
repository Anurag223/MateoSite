﻿#region Header

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
using AutoMapper;
using Tlm.Sdk.Core;
using Tlm.Sdk.Core.Models.Infrastructure;
using Tlm.Fed.Models.Canonical;
using Tlm.Fed.Models.Canonical.SiteDomain;
using Tlm.Fed.Models;
using Tlm.Fed.Adapters.Maximo.Site.Models;
using Tlm.Sdk.Core.Models;
using System.Collections.Generic;
using Tlm.Fed.Contexts.Common.Services;
using CanonicalSite = Tlm.Fed.Models.Canonical.SiteDomain.Site;
using static Tlm.Sdk.Core.Models.UnmanagedAttributeValue;
using static Tlm.Fed.Models.Canonical.SiteDomain.WorkCenterSite;
using Tlm.Fed.Contexts.Common;

namespace Tlm.Fed.Adapters.Maximo.Site
{
    public class MaximoToMateoSiteMapperProfile : Profile
    {
        private static readonly IKeyGenerator<CanonicalSite> SiteKeyGenerator = new CompositeKeyGenerator<CanonicalSite>();

        public MaximoToMateoSiteMapperProfile(IMasterDataSiteTypeMappingService masterDataSiteTypeMappingService, FeatureFlag flags)
        {
            CreateMap<MaximoLocation, WorkCenterSite>()
                .ForMember(x => x.Description, opts => opts.MapFrom(src => src.Description.Sanitize()))
                .ForMember(x => x.Name, opts => opts.MapFrom(src => src.Description.Sanitize()))
                .ForMember(x => x.FacilityId, opts => opts.MapFrom(src => SiteKeyGenerator.ComposeKeyFromParts(((int)CmmsId.Maximo).ToString(), src.SlbDistrict)))
                .ForMember(x => x.FacilityName, opts => opts.MapFrom(src => src.SLBDistrictName.Sanitize()))
                .ForMember(x => x.SubGeoMarketCode, opts => opts.Ignore())
                .ForMember(x => x.SiteType, opts => opts.MapFrom(src => masterDataSiteTypeMappingService.GetSiteTypeMappingName(src.Type.Sanitize(), CmmsId.Maximo)))
                .ForMember(x => x.SegmentCode, opts => opts.MapFrom(src => src.OldProductLine.Sanitize()))
                .ForMember(x => x.Code, opts => opts.MapFrom(src => src.Location.Sanitize()))
                .ForMember(x => x.CreatedDate, opts => opts.MapFrom(src => DateTime.UtcNow))
                .ForMember(x => x.ActiveCmmsId, opts => opts.MapFrom(src => CmmsId.Maximo))
                .ForMember(x => x.SourceSystemRecordId, opts => opts.MapFrom(src => src.Location.Sanitize()))
                .ForMember(x => x.SubBusinessLines, opts => opts.MapFrom(src => new List<string> { src.SlbSubBusinessLine.Sanitize() }))
                .ForMember(x => x.GeoMarketCode, opts => opts.MapFrom(src => src.SLBGeoUnit.Sanitize()))
                .ForMember(x => x.SubSites, opts => opts.MapFrom(src=>src.SlbLocSiteDetails))
                .ForMember(x => x.UpdateWorkstation, opts => opts.Ignore())
                .ForMember(x => x.Country, opts => opts.Ignore())
                .ForMember(x => x.CreatedBy, opts => opts.Ignore())
                .ForMember(x => x.CreatedDate, opts => opts.Ignore())
                .ForMember(x => x.Id, opts => opts.Ignore())
                .ForMember(x => x.ModifiedBy, opts => opts.Ignore())
                .ForMember(x => x.ModifiedDate, opts => opts.Ignore())
                .ForMember(x => x.Links, opts => opts.Ignore())
                .MapSyncDate()
                .ForMember(x => x.Attributes, opts => opts.Ignore())
                .ForMember(x => x.Attribute, opts => opts.Ignore())
                .ForMember(x => x.AlternateIdentities, opts => opts.Ignore())
                .ForMember(x => x.UnmanagedAttributes, opts => opts.Ignore())
                .ForMember(x => x.Links, opts => opts.Ignore())
                .AfterMap((a, e) =>
                {
                    e.Attributes.Add(new AttributeValue
                    {
                        Code = "DEFAULTLOCATION",
                        Name = "DefaultLocation",
                        Value = a.SLBDefault?.ToLower()
                    });
                    if (flags.StoreInformationFlag)
                    {
                        e.Attributes.Add(new AttributeValue
                        {
                            Code = ToCode(ManagedAttributes.OFSSTORE),
                            Name = ManagedAttributes.OFSSTORE,
                            Value = Convert.ToString(a.IsStoreLocation)
                        });
                        e.Attributes.Add(new AttributeValue
                        {
                            Code = ToCode(ManagedAttributes.ITTSTORE),
                            Name = ManagedAttributes.ITTSTORE,
                            Value = Convert.ToString(a.IsSapLocation)
                        });
                        e.Attributes.Add(new AttributeValue
                        {
                            Code = ToCode(ManagedAttributes.USEDPARTSTORE),
                            Name = ManagedAttributes.USEDPARTSTORE,
                            Value = Convert.ToString(a.IsUsedPartsLocation)
                        });
                        e.Attributes.Add(new AttributeValue
                        {
                            Code = ToCode(ManagedAttributes.OPTIMUSSTORE),
                            Name = ManagedAttributes.OPTIMUSSTORE,
                            Value = Convert.ToString(a.IsOptimusLocation)
                        }); 
                    }
                })
                .ForAllOtherMembers(opts => opts.UseDestinationValue());

            CreateMap<SlbLocSite, WorkCenterSite>()
                .ForMember(x => x.Name, opts => opts.MapFrom(src => src.LocationSiteId))
                .ForMember(x => x.Description, opts => opts.MapFrom(src => src.Description))
                .ForMember(x => x.SiteType, opts => opts.MapFrom(src => src.DescriptionType))
                .ForMember(x => x.UpdateWorkstation, opts => opts.MapFrom(src => src.SlbWorkstation))
                .ForMember(x => x.ActiveCmmsId, opts => opts.MapFrom(src => CmmsId.Maximo))
                .ForAllOtherMembers(opts => opts.Ignore());
        }
    }
}