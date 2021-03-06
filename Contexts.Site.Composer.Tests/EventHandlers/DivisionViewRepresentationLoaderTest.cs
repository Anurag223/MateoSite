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

using System.Linq;
using Castle.Core.Internal;
using FluentAssertions;

using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tlm.Fed.Contexts.Site.Composer.EventHandlers;
using Tlm.Fed.Framework.Internal;
using Tlm.Sdk.Testing.Unit;

namespace Tlm.Fed.Contexts.Site.Composer.Tests.EventHandlers
{
    [TestClass]
    public class DivisionViewRepresentationLoaderTest : UnitTestBase
    {
        [TestMethod]
        public void Instantiate_Component_NoExceptionThrown()
        {
            Assert.IsNotNull(TryInstantiateComponent<DivisionViewRepresentationLoader, StartupForSiteComposer, SiteComposerConfiguration>(
                additionalConfigurations: builder => builder.AddJsonFile("testsettings.shared.json", false)));
        }

        [TestMethod]
        public void Validate_Attribute()
        {
            var sut = new DivisionViewRepresentationLoader(null, null);
            var result = HasBulkRepresentationLoader(sut, "MasterData.DivisionView");
            result.Should().BeTrue();
        }

        [TestMethod]
        public void Should_Implemented_IBulkRepresentationLoader_Interface()
        {
            var sut = new DivisionViewRepresentationLoader(null, null);
            var result = sut.GetType().GetInterface("IBulkRepresentationLoader");
            result.Should().NotBeNull();
        }

        private bool HasBulkRepresentationLoader(IBulkRepresentationLoader sut, string contextName)
        {
            var attribute = sut.GetType().GetAttributes<BulkRepresentationLoaderAttribute>().FirstOrDefault();

            return attribute != null && attribute.Context == contextName;
        }
    }
}