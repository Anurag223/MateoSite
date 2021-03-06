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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AutoMapper;
using Tlm.Fed.Models.Canonical.SiteDomain;
using Tlm.Sdk.Core.Models;
using Tlm.Sdk.Core.Models.Hypermedia;
using Tlm.Sdk.Core.Models.Infrastructure;

namespace Tlm.Fed.Models.Canonical
{
    [ExcludeFromCodeCoverage]
    public abstract class ProfileBase : Profile
    {
        protected SiteCode ToSiteCode(SiteId siteId)
        {
            if (siteId?.Value == null) return null;

            if (!siteId.Value.Contains(':'))
                throw new InvalidOperationException("Invalid Site ID: " + siteId.Value);

            return new SiteCode(siteId.Value.Split(':')[1]);
        }

        public IMappingExpression<TSource, TDestination> CreateMapToRepresentation<TSource, TDestination>()
            where TSource : Entity
            where TDestination : Resource
        {
            var map = CreateMap<TSource, TDestination>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.OutOfDateDate, opt => opt.Ignore())
                .ForMember(x => x.UpdateStatus, opt => opt.Ignore())
                .ForMember(x => x.ModifiedBy, opt => opt.MapFrom(src => src.ModifiedBy))
                .ForMember(x => x.ModifiedDate, opt => opt.MapFrom(src => src.ModifiedDate))
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(x => x.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy));

            map.ForAllMembers(options => options.Condition((source, dest, mem) => mem != null));
            return map;
        }

        public IMappingExpression<TSource, TDestination> CreateMapToCmmsTrackedRepresentation<TSource, TDestination>()
            where TSource : CmmsTrackedEntity
            where TDestination : CmmsTrackedResource
        {
            // Workaround to avoid releasing the SDK just to add a CMMS.
            // The SDK should not contain such things normally.
            var cmmsSystems = new Dictionary<CmmsId, Cmms>(Cmms.Systems);
            if (cmmsSystems.Keys.All(x => (int)x != 6382))
                cmmsSystems.Add((CmmsId)6382, new Cmms { SystemId = (CmmsId)6382 });

            return CreateMapToRepresentation<TSource, TDestination>()
                .ForMember(x => x.ActiveCmms, opt => opt.MapFrom(x => cmmsSystems[x.ActiveCmmsId].SsrId));
        }

        public IMappingExpression<TSource, TDestination> CreateMapToEntity<TSource, TDestination>()
            where TSource : Entity
            where TDestination : Entity
        {
            var map = CreateMap<TSource, TDestination>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.ModifiedBy, opt => opt.MapFrom(src => src.ModifiedBy))
                .ForMember(x => x.ModifiedDate, opt => opt.MapFrom(src => src.ModifiedDate))
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(x => x.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy));

            map.ForAllMembers(options => options.Condition((source, dest, mem) => mem != null && !Equals(mem, 0)));
            return map;
        }

        public IMappingExpression<TSource, TDestination> CreateMapToCmmsTrackedEntity<TSource, TDestination>()
            where TSource : CmmsTrackedResource
            where TDestination : CmmsTrackedEntity
        {
            return CreateMapToEntity<TSource, TDestination>()
                .ForMember(x => x.ActiveCmmsId, opt => opt.MapFrom(x => Cmms.ToId(x.ActiveCmms)));
        }
    }
}