using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tlm.Fed.Models.CrossDomain.Models.MasterData;
using Tlm.Sdk.Core.Models.Infrastructure;

namespace Tlm.Fed.Contexts.Common.Services
{
    public interface IMasterDataSiteTypeMappingService
    {
        string GetSiteTypeMappingName(string source, CmmsId cmmsId);
        string GetSiteTypeMappingDescription(string source, CmmsId cmmsId);
        void ClearCache();
    }
}
