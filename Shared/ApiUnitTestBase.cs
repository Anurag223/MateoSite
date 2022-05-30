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
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Tlm.Sdk.Api;
using Tlm.Sdk.AspNetCore;
using Tlm.Sdk.Core.Models;
using Tlm.Sdk.Core.Models.Hypermedia;
using Tlm.Sdk.Core.Models.Infrastructure;
using Tlm.Sdk.Core.Models.Querying;
using Tlm.Sdk.Data.Mongo;
using Tlm.Sdk.Testing.Unit;
using UriBuilder = Tlm.Sdk.AspNetCore.UriBuilder;

public abstract class ApiUnitTestBase : UnitTestBase
{
    protected (HostingConfig Config, UriBuilder Builder, MockContextAccessor Accessor) CreateUriBuilder(
        string configScheme,
        string configHost,
        string configBaseUrl,
        string requestScheme,
        string requestPath,
        string requestQueryString = null)
    {
        var config = new HostingConfig { Scheme = configScheme, HostName = configHost, BaseUrl = configBaseUrl };
        var accessor = new MockContextAccessor();
        accessor.HttpContext.Request.QueryString = requestQueryString != null ? new QueryString(requestQueryString) : QueryString.Empty;
        accessor.HttpContext.Request.Path = requestPath;
        accessor.HttpContext.Request.Scheme = requestScheme ?? configScheme;

        var b = new UriBuilder(
            config,
            accessor,
            new KeyGenerator<Request>(),
            new IUriPartProvider[]
            {
                new ConfigUriPartProvider(config),
                new ForwardedHeadersUriPartProvider(accessor, config),
                new MateoOriginsHeadersUriPartProvider(accessor, config),
                new XForwardedHeadersUriPartProvider(accessor, config),
                new XOriginalHeadersUriPartProvider(accessor, config),
                new HttpRequestUriPartProvider(accessor, config),
            });

        return (config, b, accessor);
    }

    protected (HostingConfig Config, UriBuilder Builder, MockContextAccessor Accessor) CreateUriBuilder(
        string requestScheme = "http",
        string requestPath = "",
        string requestQueryString = null)
    {
        var accessor = new MockContextAccessor();
        accessor.HttpContext.Request.QueryString = requestQueryString != null ? new QueryString(requestQueryString) : QueryString.Empty;
        accessor.HttpContext.Request.Path = requestPath;
        accessor.HttpContext.Request.Scheme = requestScheme ?? TestHostingConfig.Scheme;

        var b = new UriBuilder(
            TestHostingConfig,
            accessor,
            new KeyGenerator<Request>(),
            new IUriPartProvider[]
            {
                new ConfigUriPartProvider(TestHostingConfig),
                new ForwardedHeadersUriPartProvider(accessor, TestHostingConfig),
                new MateoOriginsHeadersUriPartProvider(accessor, TestHostingConfig),
                new XForwardedHeadersUriPartProvider(accessor, TestHostingConfig),
                new XOriginalHeadersUriPartProvider(accessor, TestHostingConfig),
                new HttpRequestUriPartProvider(accessor, TestHostingConfig),
            });

        return (TestHostingConfig, b, accessor);
    }

    protected IActionResult PrepareReturn<T, TController>(QuerySpec querySpec, IReadOnlyCollection<T> data)
        where T : Entity
        where TController : CoreController
    {
        TestHostingConfig.BaseUrl = "";
        var x = CreateUriBuilder(TestHostingConfig.Scheme, TestHostingConfig.HostName, TestHostingConfig.BaseUrl, "http", "");
        var qcs = new QueryCollectionStrategy<T>(null, new CollectionLinker<T>(x.Builder), new HypermediaLinker<T>(x.Builder));
        var res = new CollectionResult<T>(data);
        var returnObject = (CollectionResult<T>)qcs.CreateCollectionDocument(res, querySpec).Value;


    protected IQueryCollectionStrategy<T> PrepareQueryCollectionStrategy<T, TController>(IReadOnlyCollection<T> result)
        where T : Entity
        where TController : CoreController
    {
        var mock = Substitute.For<IQueryCollectionStrategy<T>>();

        mock
            .GetCollection(default)
            .ReturnsForAnyArgs(s => Task.FromResult(PrepareReturn<T, TController>((QuerySpec)s.Args()[0], result)));

        return mock;
    }

    protected IQueryCachedSingleResourceStrategy<TRepresentation> PrepareSingleStrategy<TRepresentation, TController>(TRepresentation representation)
        where TRepresentation : Entity
        where TController : CoreController
    {
        var mock = Substitute.For<IQueryCachedSingleResourceStrategy<TRepresentation>>();
        var doc = new HypermediaDocument<TRepresentation>(new ModelRepresentation<TRepresentation>(representation));

        mock
            .GetSingleRepresentation<TController>(default)
            .ReturnsForAnyArgs(s => Task.FromResult((IActionResult)new OkObjectResult(doc)));

        return mock;
    }

    protected IReadOnlyCollection<T> ValidateCollectionResult<T>(IActionResult result, IList<T> objects) where T : Entity
    {
        var value = ((OkObjectResult)result).Value;
        var r = (CollectionResult<T>)value;
        r.Collection.Count.Should().Be(objects.Count);
        return r.Collection;
    }

    protected void ValidateClassAttributes<TController>()
        where TController : CoreController
    {
        typeof(TController).Should().BeDecoratedWith<ProducesAttribute>(x => x.ContentTypes.Contains("application/json"));
        typeof(TController).Should().BeDecoratedWith<ApiVersionAttribute>
        (
            s => s.Versions.Count == 1 && s.Versions[0].ToString() == "2.0"
        );
        typeof(TController).Should().BeDecoratedWith<ApiExplorerSettingsAttribute>
        (
            s => s.GroupName == "v2"
        );
        typeof(TController).Should().BeDecoratedWith<RootPolicyAttribute>();
    }

    protected void ValidateGetMethodsAttributes<T, TController>()
        where T : Entity
        where TController : CoreController
    {
        typeof(TController).Methods()
            .ThatReturn<ActionResult>()
            .ThatAreNotDecoratedWith<HttpPostAttribute>()
            .Should().NotBeVirtual()
            .And.BeDecoratedWith<HttpGetAttribute>()
            .And.BeDecoratedWith<ProducesBadRequestResponseTypeAttribute>()
            .And.BeDecoratedWith<ProducesNotAcceptableResponseTypeAttribute>()
            .And.BeDecoratedWith<ProducesOkResponseTypeAttribute>(s => s.Type == typeof(CollectionResult<T>));
    }

    protected CollectionResult<T> GetCollection<T>(IActionResult result)
    {
        return result is ObjectResult o ? (CollectionResult<T>)o.Value : throw new Exception("Cannot get a value from the result");
    }

    protected T GetValue<T>(IActionResult result)
    {
        return result is ObjectResult o ? (T)o.Value : throw new Exception("Cannot get a value from the result");
    }
}