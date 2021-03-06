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

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Tlm.Fed.Contexts.Common.Configuration;
using Tlm.Fed.Contexts.Site.Core.DataStoreModel;

namespace Tlm.Fed.Contexts.Common.Services
{
    [ExcludeFromCodeCoverage]
    public class MasterDistrictDataService : IMasterDistrictDataService
    {
        private readonly HttpClient _masterDistrictClient;
        private readonly MasterDistrictConfig _masterDistrictConfig;
        private const string districtEndPoint = "fdp/opsdistricts?wkeid=";

        public MasterDistrictDataService(HttpClient masterDistrictClient,
            MasterDistrictConfig masterDistrictConfig)
        {
            _masterDistrictClient = masterDistrictClient;
            _masterDistrictConfig = masterDistrictConfig;
        }

        public async Task<MasterDistrict> CheckMasterDistrictDataAsync(string wkeId)
        {
            _masterDistrictClient.DefaultRequestHeaders.Add("x-apikey", _masterDistrictConfig.MasterDistrictConfigApiKey);
            _masterDistrictClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string url = _masterDistrictConfig.MasterDistrictConfigBaseUrl + districtEndPoint + wkeId;

            var httpResponse = await _masterDistrictClient.GetAsync(url).ConfigureAwait(false);
            if (httpResponse.IsSuccessStatusCode)
            {
                var json = await httpResponse.Content.ReadAsStringAsync();
                var masterDistrict = JsonConvert.DeserializeObject<MasterDistrict>(json);
                return masterDistrict;
            }
            else
            {
                throw new Exception($"Unable to get http response from master data api. Reason: {httpResponse.ReasonPhrase}");
            }
        }
    }
}
