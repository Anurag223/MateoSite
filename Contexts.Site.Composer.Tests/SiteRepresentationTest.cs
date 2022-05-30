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

using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tlm.Fed.Contexts.Site.Core.RepresentationModel;
using Tlm.Fed.Models.Canonical.SiteDomain;
using Tlm.Sdk.Core.Models;
using Tlm.Sdk.Core.Models.Hypermedia;
using Tlm.Sdk.Core.Models.Metadata;
using Tlm.Sdk.Core.Models.Querying;
using Tlm.Sdk.Testing.Unit;

namespace Tlm.Fed.Contexts.Site.Composer.Tests
{
    [TestClass]
    public class SiteRepresentationTest : UnitTestBase
    {
        [TestMethod]
        public void SiteRepresentation_should_be_sub_type_of_Entity()
        {
            var uat = new SiteRepresentation();
            var result = uat.GetType().IsSubclassOf(typeof(AttributedCmmsTrackedResource));
            result.Should().BeTrue();
        }

        [TestMethod]
        public void SiteRepresentation_should_have_IsRoot_Attribute()
        {
            var uat = new SiteRepresentation();
            var result = uat.GetType().GetCustomAttributes(typeof(IsRootAttribute), false);
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void SiteRepresentation_should_have_KeyReference_Attribute_of_WorkCenterSite()
        {
            var uat = new SiteRepresentation();
            var result = uat.GetType().GetCustomAttributesData();
            result.Should().NotBeNull();
            var keyRefAttribute = result.FirstOrDefault(x => x.AttributeType == typeof(KeyReferenceAttribute));
            keyRefAttribute.ConstructorArguments.Count.Should().Be(1);
            keyRefAttribute.ConstructorArguments[0].Value.Should().Be(typeof(WorkCenterSite));
        }

        public void SiteRepresentation_Should_have_Classification_Dictionary_With_Include_Attribute()
        {
            var uat = new SiteRepresentation();
            var classificationsPropInfo = uat.GetPropertyInfo(p => p.Classifications);
            var result = classificationsPropInfo.GetCustomAttributesData();
            result.Should().NotBeNull();
            var includeAttribute = result.FirstOrDefault(x => x.AttributeType == typeof(IncludeOnlyIfRequestedAttribute));
            includeAttribute.Should().NotBeNull();
        }
    }
}