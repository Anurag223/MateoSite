﻿#region Header

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
    ///     The Well Class
    ///     Represents the Database model for the well table
    /// </summary>
    public class Well
    {
        public bool? Active { get; set; }

        public string Country { get; set; }

        public string County { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedUser { get; set; }

        public string FieldName { get; set; }

        public int Id { get; set; }

        public string Latitude { get; set; }

        public string Location { get; set; }

        public string Longitude { get; set; }

        public string Name { get; set; }

        public string SiebelId { get; set; }

        public int? SourceId { get; set; }

        public string SourceSystem { get; set; }

        public DateTime? SpudDate { get; set; }

        public string State { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string UpdateUser { get; set; }

        public string Uwi { get; set; }

        public string WellNumber { get; set; }

        public string WellType { get; set; }
    }
}