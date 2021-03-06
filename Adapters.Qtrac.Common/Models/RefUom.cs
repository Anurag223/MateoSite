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
    ///     The TblRefUom Class
    ///     Represents the Database model for the TblRefUom table
    /// </summary>
    public class RefUom
    {
        public bool? Active { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string DisplayUom { get; set; }

        public string Factor { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public double Offset1 { get; set; }

        public double Offset2 { get; set; }

        public int RefUomId { get; set; }

        public int RefUomSiteId { get; set; }

        public string Si { get; set; }

        public string Uomcode { get; set; }

        public string Uomname { get; set; }
    }
}