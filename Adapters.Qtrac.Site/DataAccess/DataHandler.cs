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
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Tlm.Fed.Contexts.Common.Services;
using Tlm.Fed.Framework.Internal;
using Tlm.Fed.Models.Canonical.SiteDomain;
using Tlm.Sdk.Core.Models;
using Tlm.Sdk.Core.Models.Infrastructure;
using QComm = Tlm.Fed.Adapters.Qtrac.Common;
using CanonicalSite = Tlm.Fed.Models.Canonical.SiteDomain.Site;
using static Tlm.Sdk.Core.Models.UnmanagedAttributeValue;

namespace Tlm.Fed.Adapters.Qtrac.Site.DataAccess
{
    public class DataHandler : IDataHandler
    {
        private readonly IMasterDataSiteTypeMappingService _masterDataSiteTypeMappingService;
        private static readonly IKeyGenerator<CanonicalSite> SiteKeyGenerator = new CompositeKeyGenerator<CanonicalSite>();

        public DataHandler(QComm.QtracDbContext.Factory dbContextFactory, SiteAdapterConfig siteAdapterConfig,
            IMasterDataSiteTypeMappingService masterDataSiteTypeMappingService)
        {
            _dbContextFactory = dbContextFactory;
            _siteAdapterConfig = siteAdapterConfig;
            _masterDataSiteTypeMappingService = masterDataSiteTypeMappingService;
        }

        public async Task<IReadOnlyCollection<WorkCenterSite>> GetSite(Expression<Func<WorkCenterSite, bool>> filter,
            Expression<Func<WorkCenterSite, bool>> subSiteFilter, string siteCode)
        {
            var context = _dbContextFactory();
            try
            {
                try
                {
                    context.Database.SetCommandTimeout(TimeSpan.FromMinutes(_siteAdapterConfig.ConnectionTimeout));
                }
                catch (InvalidOperationException e)
                {
                    _logger.Warning(e, e.Message + " You are likely using an in-memory database as in a unit test.");
                }

                var sites = await context.QueryInTransactionScope(async () =>
                    await PrepareSiteQuery(context).Where(filter).ToListAsync());

                if (sites != null && sites.Any())
                {
                    var allSubSites = await GetSubSite(context, subSiteFilter, siteCode);
                    foreach (var site in sites)
                    {
                        site.FacilityId = SiteKeyGenerator.ComposeKeyFromParts(((int)CmmsId.QTrac).ToString(), site.FacilityId);
                        site.SubSites = allSubSites.Where(x => x.Code.Value == site.Code.Value).ToList();
                    }
                }

                return sites;
            }
            finally
            {
                _logger.Debug("Clearing Connection pool");
                if (context.Database.IsSqlServer())
                {
                    context.Database.CloseConnection();
                    SqlConnection.ClearPool((SqlConnection)context.Database.GetDbConnection());
                }
            }
        }

        private readonly QComm.QtracDbContext.Factory _dbContextFactory;
        private readonly ILogger _logger = Log.Logger.ForContext<DataHandler>();
        private readonly SiteAdapterConfig _siteAdapterConfig;

        public async Task<IReadOnlyCollection<WorkCenterSite>> GetSubSite(QComm.QtracDbContext context,
            Expression<Func<WorkCenterSite, bool>> filter, string siteCode)
        {
            context.Database.SetCommandTimeout(TimeSpan.FromMinutes(_siteAdapterConfig.ConnectionTimeout));
            var subSites = await context.QueryInTransactionScope(async () =>
                await PrepareSubSiteQuery(context).Where(filter).ToListAsync());

            return subSites;
        }

        private IQueryable<WorkCenterSite> PrepareSiteQuery(QComm.QtracDbContext context)
        {
            var query = from workcentersite in context.Site
                        join subsites in context.Positions on workcentersite.SiteId equals subsites.SiteId
                            into sitedata
                        from siteinfo in sitedata.DefaultIfEmpty()
                        join facs in context.Facilities on workcentersite.FacilityId equals facs.FacilityID
                            into facdata
                        from facinfo in facdata.DefaultIfEmpty()
                        join segs in context.RefSegment on workcentersite.RefSegmentId equals segs.RefSegmentId
                            into segdata
                        from seginfo in segdata.DefaultIfEmpty()
                        join subsegs in context.RefBusinessLine on workcentersite.SubBusinessLine equals subsegs.RefBusinessLineId
                            into subsegsdata
                        from subsegsinfo in subsegsdata.DefaultIfEmpty()
                        join countryinfo in context.Country on facinfo.Country equals countryinfo.Name
                            into countries
                        from countrydata in countries.DefaultIfEmpty()
                        where workcentersite.Status == "Active" // filter sites on basis of status, we will get only active sites
                        select new WorkCenterSite
                        {
                            Code = workcentersite.SiteCode,
                            Description = workcentersite.SiteName,
                            Name = workcentersite.SiteName,
                            FacilityId = Convert.ToString(facinfo.FacilityID),
                            FacilityName = facinfo.FacilityName,
                            SiteType = _masterDataSiteTypeMappingService.GetSiteTypeMappingName(workcentersite.LeafType, CmmsId.QTrac),
                            GeoMarketCode = facinfo.GeoMarketName,
                            SegmentCode = seginfo.SegmentName,
                            SubBusinessLines = new List<string> { subsegsinfo.Code },
                            CreatedBy = workcentersite.CreatedBy,
                            CreatedDate = workcentersite.CreatedDate,
                            ModifiedBy = workcentersite.LastModifiedBy,
                            ModifiedDate = workcentersite.LastModifiedDate,
                            ActiveCmmsId = CmmsId.QTrac,
                            SourceSystemRecordId = Convert.ToString(workcentersite.SiteId),
                            Country = countrydata.Name,
                            CountryCode = countrydata.Code,
                            Attributes = AddQtracSiteAttributes(workcentersite.SapFlag,workcentersite.MupFlag.Value),
                            UnmanagedAttributes = new Dictionary<string, Dictionary<string, UnmanagedAttributeValue>>
                    {
                        {
                            Cmms.QTrac.Code, new Dictionary<string, UnmanagedAttributeValue>
                            {
                                {
                                    CanonicalSiteConstant.Area,
                                    new UnmanagedAttributeValue(CanonicalSiteConstant.Area, facinfo.AreaName)
                                },
                                {
                                    CanonicalSiteConstant.Ownership,
                                    new UnmanagedAttributeValue(CanonicalSiteConstant.Ownership,
                                        facinfo.FacilityOwnership)
                                },
                                {
                                    CanonicalSiteConstant.FacilityDescription,
                                    new UnmanagedAttributeValue(CanonicalSiteConstant.FacilityDescription,
                                        facinfo.FacilityType)
                                },
                                {
                                    CanonicalSiteConstant.FacilityStatus,
                                    new UnmanagedAttributeValue(CanonicalSiteConstant.FacilityStatus,
                                        facinfo.FacilityStatus)
                                },
                                {
                                    CanonicalSiteConstant.Latitude,
                                    new UnmanagedAttributeValue(CanonicalSiteConstant.Latitude, facinfo.Latitude)
                                },
                                {
                                    CanonicalSiteConstant.Longitude,
                                    new UnmanagedAttributeValue(CanonicalSiteConstant.Longitude, facinfo.Longitude)
                                }
                            }
                        }
                    }
                        };

            return query;
        }

        private IQueryable<WorkCenterSite> PrepareSubSiteQuery(QComm.QtracDbContext context)
        {
            var query = from subs in context.Positions
                        join posTypes in context.PositionTypes on subs.PositionTypeId equals posTypes.Id
                            into subitedata
                        from subiteinfo in subitedata.DefaultIfEmpty()
                        join posOptions in context.PositionOptions on subiteinfo.Id equals posOptions.Id
                            into posdata
                        from posTypeinfo in posdata.DefaultIfEmpty()
                        join refAttribute in context.RefInBaseAttribute on subs.PostionAttributeId equals refAttribute.Id
                            into attdata
                        from posAttributeinfo in attdata.DefaultIfEmpty()
                        join sites in context.Site on subs.SiteId equals sites.SiteId
                        select new WorkCenterSite
                        {
                            Code = sites.SiteCode,
                            Description = subs.Description,
                            Name = subs.Name,
                            SiteType = _masterDataSiteTypeMappingService.GetSiteTypeMappingName(posTypeinfo.Name, CmmsId.QTrac),
                            Attribute = posAttributeinfo.AttributeName,
                            CreatedBy = subs.CreatedBy,
                            CreatedDate = subs.CreatedDate,
                            ModifiedBy = subs.LastModifiedBy,
                            ModifiedDate = subs.LastModifiedDate,
                            ActiveCmmsId = CmmsId.QTrac,
                            SourceSystemRecordId = Convert.ToString(subs.Id)
                        };

            return query;
        }

        private static List<AttributeValue> AddQtracSiteAttributes(bool sapFlag,bool mupFlag)
        {
            List<AttributeValue> attributeList = new List<AttributeValue>();
            if (sapFlag)
            {
                attributeList.Add(new AttributeValue() { Name = WorkCenterSite.ManagedAttributes.SAPENABLED, Code = ToCode(WorkCenterSite.ManagedAttributes.SAPENABLED), Value = true.ToString() });
                attributeList.Add(new AttributeValue() { Name = WorkCenterSite.ManagedAttributes.OFSSTORE, Code = ToCode(WorkCenterSite.ManagedAttributes.OFSSTORE), Value = false.ToString() });
            }
            else
            {
                attributeList.Add(new AttributeValue() { Name = WorkCenterSite.ManagedAttributes.SAPENABLED, Code = ToCode(WorkCenterSite.ManagedAttributes.SAPENABLED), Value = false.ToString() });
                attributeList.Add(new AttributeValue() { Name = WorkCenterSite.ManagedAttributes.OFSSTORE, Code = ToCode(WorkCenterSite.ManagedAttributes.OFSSTORE), Value = true.ToString() });
            }

            attributeList.Add(mupFlag ? new AttributeValue() { Name = WorkCenterSite.ManagedAttributes.USEDPARTSTORE, Code = ToCode(WorkCenterSite.ManagedAttributes.USEDPARTSTORE), Value = true.ToString() } : new AttributeValue() { Name = WorkCenterSite.ManagedAttributes.USEDPARTSTORE, Code = ToCode(WorkCenterSite.ManagedAttributes.USEDPARTSTORE), Value = false.ToString() });
            return attributeList;
        }


    }
}