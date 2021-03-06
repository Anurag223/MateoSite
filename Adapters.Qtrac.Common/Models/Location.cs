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
    /// <summary>
    ///     The Location Class
    ///     Represents the Database model for the Location table
    /// </summary>
    public class Location
    {
        public bool? Active { get; set; }

        public int? BusinessUnit { get; set; }

        public int? CategoryTypeId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string Description { get; set; }

        public int? DistrictId { get; set; }

        public bool? Districtmigrated { get; set; }

        public bool? EnableSap { get; set; }

        public int? EtraceId { get; set; }

        public int Id { get; set; }

        public int? IdistrictId { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public int? Level { get; set; }

        public string Location1 { get; set; }

        public int? ParentId { get; set; }

        public bool? RigPacketQcRequired { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string UpdateUser { get; set; }

        public double? UtcDifference { get; set; }
    }
}