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
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tlm.Fed.Adapters.Sap.Common.CustomException;
using Tlm.Fed.Adapters.SAP.Common.Configuration;
using Tlm.Fed.Adapters.SAP.Site.Models;
using Tlm.Fed.Framework.Common;
using Tlm.Fed.Framework.Common.ServiceClient;

namespace Tlm.Fed.Adapters.SAP.Site.Concrete
{
    public class GetSAPSiteChunk : IActionHandler<SAPSiteQuery, SAPSiteResponse>
    {
        private readonly IHttpService _httpService;
        private readonly SAPEndpointConfig _sapConfig;
        private const string SAPALSPlant = "art";
        
        public GetSAPSiteChunk(IHttpService httpService, SAPEndpointConfig sapConfig)
        {
            _httpService = httpService;
            _sapConfig = sapConfig;
        }

        public async Task<SAPSiteResponse> Handle(SAPSiteQuery query)
        {
            var sapSiteRequestBuilder = new SAPSiteRequestBuilder(_sapConfig);

            var request = sapSiteRequestBuilder.Build();

            var result = await _httpService.ServiceCaller(request, _sapConfig.Timeout);
            if (result.IsSuccessStatusCode)
            {
                var stringContent = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<SAPSiteResponse>(stringContent);
                //Filtering ALS Plants
                response.SAPSites = response.SAPSites.Where(x => string.Equals(x.LocationType, SAPALSPlant, StringComparison.InvariantCultureIgnoreCase)).ToList();
                return response;
            }
            var errorMessage = await result.Content.ReadAsStringAsync();
            throw new SapIntegrationException("Site", request, result.StatusCode.ToString(), errorMessage);
        }
    }
}