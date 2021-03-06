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

using System.Diagnostics.CodeAnalysis;
using Autofac;
using Tlm.Fed.Contexts.Site.Core.RepresentationModel;
using Tlm.Fed.Framework.Common.Configurers;
using Tlm.Fed.Models.Canonical.MasterData;
using Tlm.Fed.Models.Canonical.SiteDomain;
using Tlm.Sdk.AspNetCore;
using Tlm.Sdk.Data.Mongo;

namespace Tlm.Fed.Contexts.Site.Composer
{
    public class SiteDbConfigurer : MongoDbConfigurer
    {
        public override string Name => "SiteDbConfigurer";
        protected override void ConfigureRepos(Startup startup, ContainerBuilder builder)
        {
            builder
                .AddRepoSupport<SiteRepresentation>()
                .AddRepoSupport<FacilityRepresentation>()
                .AddRepoSupport<BusinessViewRepresentation>()
                .AddRepoSupport<DivisionViewRepresentation>()
                .AddRepoSupport<BasinViewRepresentation>()
                .AddRepoSupport<BusinessTeamRepresentation>()
                .AddRepoSupport<BusinessView>()
                .AddRepoSupport<DivisionView>()
                .AddRepoSupport<BusinessTeam>()
                .AddRepoSupport<BasinView>()
                .AddRepoSupport<SiteTypeMapRepresentation>()
                .AddRepoSupport<WorkCenterSite>()
                .AddRepoSupport<SegmentMapping>()
                .AddRepoSupport<Models.Canonical.SiteDomain.Site>();
        }
    }
}