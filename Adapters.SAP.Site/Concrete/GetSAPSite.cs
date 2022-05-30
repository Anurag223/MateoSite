using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;
using Tlm.Fed.Adapters.SAP.Site.Models;
using Tlm.Fed.Framework.Common;
using Tlm.Fed.Framework.Common.Models;

namespace Tlm.Fed.Adapters.SAP.Site.Concrete
{
    public class GetSAPSite : IActionHandler<SAPSiteQuery, CacheLoadInfo>
    {
        private readonly ISAPSiteTransformer _transformer;
        private readonly IActionHandler<SAPSiteQuery, SAPSiteResponse> _siteHandler;
        private readonly ILogger _logger;

        public GetSAPSite(ISAPSiteTransformer transformer, IActionHandler<SAPSiteQuery,
            SAPSiteResponse> siteHandler)
        {
            _logger = Log.Logger.ForContext(GetType());
            _transformer = transformer;
            _siteHandler = siteHandler;
        }


        public async Task<CacheLoadInfo> Handle(SAPSiteQuery query)
        {
            var cacheLoadInfo = new CacheLoadInfo();
            _logger.Debug($"Fetching sites for SAP");
            var sapSites = await _siteHandler.Handle(query);
            _logger.Debug($"Fetched '{sapSites?.SAPSites?.Count}' sites for SAP");
            cacheLoadInfo.Add(await _transformer.Transform(sapSites.SAPSites));
            return cacheLoadInfo;
        }
    }
}