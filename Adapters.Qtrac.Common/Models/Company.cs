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

using System;

namespace Tlm.Fed.Adapters.Qtrac.Common.Models
{
    public class Company
    {
        public bool? Active { get; set; }

        public string Address { get; set; }

        public string BillingFlag { get; set; }

        public string City { get; set; }

        public string CorporateAlias { get; set; }

        public string Country { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedUser { get; set; }

        public string CrmId { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Parent { get; set; }

        public string ParentSite { get; set; }

        public string Region { get; set; }

        public string Site { get; set; }

        public string Source { get; set; }

        public int SourceId { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string UpdateUser { get; set; }
    }
}