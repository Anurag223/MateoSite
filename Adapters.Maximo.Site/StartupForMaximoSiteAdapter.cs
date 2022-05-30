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

using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Hosting;
using Tlm.Fed.Adapters.Maximo.Common.Configuration;
using Tlm.Fed.Framework.Internal;
using Tlm.Sdk.AspNetCore.Configurers;

namespace Tlm.Fed.Adapters.Maximo.Site
{
    public class StartupForMaximoSiteAdapter : StartupForAdapter<MaximoSiteAdapterConfigurer>
    {
        public StartupForMaximoSiteAdapter(IWebHostEnvironment env) : base(env)
        {
            AddConfigurer(new StronglyTypedConfigurationConfigurer<MaximoConfig>());
            AddConfigurer(new MaximoSiteAdapterConfigurer());
        }
    }
}