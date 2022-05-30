using System;
using System.Collections.Generic;
using System.Threading.Tasks;
 using Serilog;
using Tlm.Fed.Adapters.Maximo.Common;
using Tlm.Fed.Adapters.Maximo.Site.Concrete;
using Tlm.Fed.Adapters.Maximo.Site.Models;
using Tlm.Fed.Framework.Common;
using Tlm.Fed.Framework.Common.Models;

namespace Tlm.Fed.Adapters.Maximo.Site.Concrete
{
    

    public class GetMaximoSite :  IActionHandler<MaximoSiteQuery, CacheLoadInfo>
    {
        private readonly IMaximoSiteTransformer _transformer;
        private readonly IActionHandler<MaximoSiteQuery, SAP_R_LOCATIONS_LOCATIONSType> _siteHandler;
        private readonly ILogger _logger;
        public GetMaximoSite(IMaximoSiteTransformer transformer , IActionHandler<MaximoSiteQuery, SAP_R_LOCATIONS_LOCATIONSType> siteHandler)
        {
            _logger = Log.Logger.ForContext(GetType());
            _transformer = transformer;
            _siteHandler = siteHandler;
        }


        public async Task<CacheLoadInfo> Handle(MaximoSiteQuery query)
        {
#if DEBUG
            const int chunkSize = 50;
#else
            const int chunkSize = 100;
#endif

            var cacheLoadInfo = new CacheLoadInfo();
            int pageNumber = 1;
            SAP_R_LOCATIONS_LOCATIONSType maximoSites = null;
            do
            {
                var chunkQuery = new MaximoSiteQuery(query);
                chunkQuery.MaxItems = chunkSize;
                chunkQuery.PageNumber = pageNumber;

                _logger.Debug($"Fetching {chunkSize} sites on '{pageNumber}' page number for businessLine '{query.SubBusinessLine}'");
                maximoSites = await _siteHandler.Handle(chunkQuery);
                _logger.Debug($"Fetched '{pageNumber}' Page out of '{maximoSites?.responseInfo?.totalPages}' Pages for businessLine '{query.SubBusinessLine}'");
              
                var cacheLoadInfoTemp = await _transformer.Transform(maximoSites.maximoLocation);

                cacheLoadInfo.Add(cacheLoadInfoTemp);
                pageNumber++;
            }
            while (maximoSites.responseInfo.totalPages > 0 && maximoSites.responseInfo.totalPages != maximoSites.responseInfo.pagenum);

            return cacheLoadInfo;
        }

       

        
    }
}