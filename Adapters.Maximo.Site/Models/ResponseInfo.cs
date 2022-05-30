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

using Newtonsoft.Json;

namespace Tlm.Fed.Adapters.Maximo.Site.Models
{
    public class ResponseInfo
    {
        [JsonProperty("nextPage")]
        public NextPage nextPage { get; set; }

        [JsonProperty("totalPages")]
        public int totalPages { get; set; }

        [JsonProperty("href")]
        public string href { get; set; }

        [JsonProperty("totalCount")]
        public int totalCount { get; set; }

        [JsonProperty("pagenum")]
        public int pagenum { get; set; }
    }
}