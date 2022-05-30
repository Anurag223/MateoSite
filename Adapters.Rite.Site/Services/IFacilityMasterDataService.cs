using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tlm.Fed.Models.Canonical.MasterData;

namespace Tlm.Fed.Adapters.Rite.Site.Services
{
    public interface IFacilityMasterDataService
    {
        Task<List<Facility>> GetFacilityMasterData(List<string> facilitiesId);
    }
}
