using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Tlm.Sdk.AspNetCore;

namespace Tlm.Fed.Adapters.SAP.Site
{
    [ExcludeFromCodeCoverage]
    public class Program : ProgramForMicroservice<StartupForSAPSiteAdapter>
    {
        public static async Task Main(string[] args)
        {
            await BaseMainAsync(args);
        }
    }
}
