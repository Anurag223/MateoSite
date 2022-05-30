using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tlm.Fed.Adapters.Maximo.Site.Models
{
    public class SlbLocSite
    {
        [JsonProperty("slb_locsiteid")]
        public string LocationSiteId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("slb_workstation")]
        public bool SlbWorkstation { get; set; }

        [JsonProperty("type_description")] 
        public string DescriptionType { get; set; }
    }
}
