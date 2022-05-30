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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Autofac;
using Tlm.Fed.Adapters.SAP.Site.Concrete;
using Tlm.Fed.Adapters.SAP.Site.EventHandlers;
using Tlm.Fed.Adapters.SAP.Site.Models;
using Tlm.Fed.Contexts.Common.Services;
using Tlm.Fed.Framework.Common;
using Tlm.Fed.Framework.Common.Models;
using Tlm.Fed.Framework.Common.ServiceClient;
using Tlm.Fed.Framework.Internal;
using Tlm.Fed.Framework.Internal.Configurers;
using Tlm.Fed.Models.Canonical.MasterData;
using Tlm.Fed.Models.Canonical.SiteDomain;
using Tlm.Sdk.AspNetCore;
using Tlm.Sdk.Data.Mongo;
using UriBuilder = Tlm.Sdk.AspNetCore.UriBuilder;

namespace Tlm.Fed.Adapters.SAP.Site
{
    public class SAPSiteAdapterConfigurer : AdapterConfigurer
    {
        public override IEnumerable<Type> GetNonReflectedMessageHandlers()
        {
            yield return typeof(LoadCacheCommandHandler<Fed.Models.Canonical.SiteDomain.Site>);
        }

        protected override void RegisterAdapter(Startup startup, ContainerBuilder builder)
        {
            builder.RegisterModule(new AutoMapperModule());
            builder.RegisterType<GetSAPSite>().As<IActionHandler<SAPSiteQuery, CacheLoadInfo>>();
            builder.RegisterType<GetSAPSiteChunk>().As<IActionHandler<SAPSiteQuery, SAPSiteResponse>>();
            builder.RegisterType<SAPSiteTransformer>().As<ISAPSiteTransformer>();
            builder.RegisterType<SiteLoader>().As<IDataLoader>();
            builder.RegisterType<Transformer<WorkCenterSite>>().As<ITransformer<WorkCenterSite>>();
            builder.RegisterType<HttpService>().As<IHttpService>();
            builder.RegisterType<MateoAuthenticationService>().As<IMateoAuthenticationService>();
            builder.RegisterType<UriBuilder>().As<IUriBuilder>().SingleInstance();
            builder.RegisterType<MasterDataSiteTypeMappingService>().As<IMasterDataSiteTypeMappingService>();

            builder.AddRepoSupport<WorkCenterSite>()
                .AddRepoSupport<SegmentMapping>();

            RegisterLoading<Fed.Models.Canonical.SiteDomain.Site>(builder);
        }
    }
}