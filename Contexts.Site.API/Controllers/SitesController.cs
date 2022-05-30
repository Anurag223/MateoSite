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

using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tlm.Fed.Contexts.Site.Core.RepresentationModel;
using Tlm.Sdk.Api;
using Tlm.Sdk.Core.Models.Querying;

namespace Tlm.Fed.Contexts.Site.API.Controllers
{
    [ProducesJson]
    [ApiVersion("2.0")]
    [ApiExplorerSettings(GroupName = "v2")]
    [RootPolicy]
    public class SitesController : CoreController
    {
        private readonly IGetCollectionStrategy<SiteRepresentation> _multiResourceGetter;
        private readonly IGetStrategy<SiteRepresentation> _singleResourceGetter;

        public SitesController(IGetCollectionStrategy<SiteRepresentation> multiResourceGetter, IGetStrategy<SiteRepresentation> singleResourceGetter)
        {
            _multiResourceGetter = multiResourceGetter;
            _singleResourceGetter = singleResourceGetter;
        }

		/// <summary>
		///     Get the site using the specified query parameters.
		/// </summary>
		[HttpGet]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		[ProducesResponseType(typeof(CollectionResult<SiteRepresentation>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> Get([QuerySpecBinder(typeof(SiteRepresentation), Key = nameof(SitesController), ExclusionPolicies = QueryExclusionPolicies.ExcludeFieldsAndIncludeablesInMemory)] QuerySpec querySpec)
		{
			return await _multiResourceGetter.GetCollection(querySpec);
		}

        /// <summary>
        ///     Gets the Site identified by the specified ID.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="querySpec">A specification about what parts of the Site to include.</param>
        /// <returns>The Site with the specified Id.</returns>
        [HttpGet("{id}")]
        [ProducesNotFoundResponseType]
        [ProducesOkResponseType(typeof(SiteRepresentation))]
        public async Task<IActionResult> GetById(string id, [QuerySpecBinder(typeof(SiteRepresentation), Key = nameof(SitesController), ExclusionPolicies = QueryExclusionPolicies.ExcludeFieldsAndIncludeablesInMemory)] QuerySpec querySpec)
        {
            querySpec = SpecBuilder.FromQuery<SiteRepresentation>(querySpec ?? QuerySpec.ForEverything).WithExclusionPolicies(QueryExclusionPolicies.ExcludeFieldsAndIncludeablesInRepo);

            return await _singleResourceGetter.GetResourceById<SitesController>(id, querySpec);
        }
    }
}