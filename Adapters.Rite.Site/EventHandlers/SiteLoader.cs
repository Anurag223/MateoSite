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

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tlm.Fed.Adapters.Rite.Site.Services;
using Tlm.Fed.Framework.Common.Models;
using Tlm.Fed.Framework.Internal;
using Tlm.Fed.Models.Canonical.SiteDomain;
using Tlm.Sdk.Core.Models.Infrastructure;

namespace Tlm.Fed.Adapters.Rite.Site.EventHandlers
{
    [BulkLoader(CmmsId.RITE, "Site", ContextStrategy.SiteBySite)]
    [ContextStrategy(ContextStrategy.SiteBySite)]
    public class SiteLoader : IDataLoader<SiteBasedLoadQuery>
    {
        public SiteLoader(ISiteService serviceHandler, ITransformer<WorkCenterSite> transformer)
        {
            _serviceHandler = serviceHandler;
            _transformer = transformer;
        }

        public async Task<CacheLoadInfo> LoadAsync(SiteBasedLoadQuery query)
        {
            var list = new List<WorkCenterSite>();

            var siteRange = await _serviceHandler.GetSite(query.CommaSeparatedSiteCodes, query.HighWaterMark);
            if (siteRange != null && siteRange.Any())
                list = siteRange.ToList();
            //list.ForEach(x => { x.SyncDate = newHighWaterMark;  });

            var dataOperations = list.Select(x => new UpdateDataOperation<WorkCenterSite> { Entity = x }).ToArray();
            var res = new LoadFromSourceResult<WorkCenterSite> { DataOperations = dataOperations.ToArray() };
            var info = await _transformer.ConstructCacheLoadInfo(res).ConfigureAwait(false);

            return info;
        }

        private readonly ISiteService _serviceHandler;
        private readonly ITransformer<WorkCenterSite> _transformer;
    }
}