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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tlm.Sdk.Testing.Integration;

namespace Adapters.Rite.IntegrationTests
{
    [TestClass]
    [TestCategory("EndToEnd")]
    public class SiteLoaderTests : IntegrationTestBase
    {
        /*[TODO] 3.1
        [TestMethod]
        public async Task LoadFromSourceAsync_Incremental_CanonicalModelPopulated1()
        {
            try
            {
                await LoadData(true, ContextStrategy.Multisite);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        [TestMethod]
        public async Task LoadFromSourceAsync_NotIncremental_CanonicalModelPopulated1()
        {
            await LoadData(false, ContextStrategy.SiteBySite);
        }

        private async Task<CacheLoadCompleted> LoadData(bool isIncremental, ContextStrategy strategy)
        {
            var request = BulkLoaderTestSupport.CreateCacheLoadRequestedEvent(Cmms.Rite.SystemId, "Site", "WL", isIncremental, strategy);

            using (RiteAdapterMicroservices.CreateSiteServer())
            {
                var res = await BulkLoaderTestSupport.PostEventRequestAndWaitForResponses<CacheLoadRequested, CacheLoadCompleted>(
                    request, Timeout);

#if DEBUG
                var context = CreateTestDbContext<WorkCenterSite>();
                await WriteCollectionToFile(context.Database, "Site", "c:\\temp\\rite\\site.json");
#endif
                return CacheLoadCompleted.Aggregate(res);
            }
        }
        */
    }
}