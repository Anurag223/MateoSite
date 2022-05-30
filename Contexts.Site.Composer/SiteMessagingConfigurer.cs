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
using Tlm.Fed.Contexts.Site.Core.RepresentationModel;
using Tlm.Fed.Framework.Common;
using Tlm.Fed.Framework.Internal;
using Tlm.Fed.Framework.Internal.Configurers;
using Tlm.Sdk.AspNetCore;
using Can_Site = Tlm.Fed.Models.Canonical.SiteDomain.Site;

namespace Tlm.Fed.Contexts.Site.Composer
{
    public class SiteMessagingConfigurer : ComposerConfigurer
    {
        protected override void RegisterComposer(Startup startup, ContainerBuilder builder)
        {
            builder.RegisterModule(new AutoMapperModule());

            builder.RegisterType<RepresentationBulkComposerAndSaver<Can_Site, SiteRepresentation>>().As<IRepresentationBulkComposerAndSaver<SiteRepresentation>>().InstancePerDependency();
            builder.RegisterType<SiteInboundComposer>().As<IInboundComposer<Can_Site, SiteRepresentation>>().InstancePerDependency();
            builder.RegisterType<WorkCenterSiteComposer>().As<IInboundAlternateComposer>().InstancePerDependency();

            builder.RegisterType<CacheLoadCompletedSender<Can_Site>>().As<ICacheLoadCompletedSender<Can_Site>>();
        }

        public override IEnumerable<Type> GetNonReflectedMessageHandlers()
        {
            yield return typeof(CacheLoadCompletedHandler<Can_Site>);
            yield return typeof(RefreshRepresentationsCommandHandler<Can_Site>);
        }
    }
}