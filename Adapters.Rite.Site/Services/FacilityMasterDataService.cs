using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tlm.Fed.Models.Canonical.MasterData;
using Tlm.Sdk.Core.Data;
using Tlm.Sdk.Core.Models.Querying;

namespace Tlm.Fed.Adapters.Rite.Site.Services
{
    public class FacilityMasterDataService : IFacilityMasterDataService
    {
        private readonly IReadOnlyRepo<Facility> _facilityRepo;

        public FacilityMasterDataService(IReadOnlyRepo<Facility> facilityRepo)
        {
            _facilityRepo = facilityRepo;
        }
        public async Task<List<Facility>> GetFacilityMasterData(List<string> facilitiesId)
        {
            var data = facilitiesId.Where(x => !string.IsNullOrEmpty(x)).ToList();
            var query = QuerySpec.ByValues("fmdcommonid", data);
            var collectionData = (await _facilityRepo.QueryManyAsync(query))?.Collection?.ToList();
            return collectionData;
        }
    }
}
