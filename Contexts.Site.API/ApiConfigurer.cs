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

using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Tlm.Fed.Contexts.Common.Services;
using Tlm.Fed.Contexts.Site.Core.DataStoreModel;
using Tlm.Fed.Contexts.Site.Core.RepresentationModel;
using Tlm.Fed.Framework.Common.Configurers;
using Tlm.Fed.Framework.Common.ServiceClient;
using Tlm.Sdk.Api;
using Tlm.Sdk.AspNetCore;
using Tlm.Sdk.Data.Mongo;

namespace Tlm.Fed.Contexts.Site.API
{
    public class ApiConfigurer : MessagingConfigurer
    {
        public override void ConfigureServicesViaAutofac(Startup startup, ContainerBuilder builder)
        {
            base.ConfigureServicesViaAutofac(startup, builder);

            builder.AddRepoSupport<SiteRepresentation>();
            builder.RegisterType<HypermediaLinker<SiteRepresentation>>().As<IHypermediaLinker<SiteRepresentation>>();
            builder.RegisterMateoHttpServiceClient<MasterDistrict>("MasterDistrictService");
        }

        public override void ConfigureServices(Startup startup, IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddHttpClient<IAuthenticationService, AuthenticationService>();
            services.AddHttpClient<IMasterDistrictDataService, MasterDistrictDataService>();
        }
    }
}