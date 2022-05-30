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

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tlm.Fed.Adapters.Qtrac.Common.Models
{
    /// <summary>
    ///     The Job Class
    ///     Represents the Database model for the Job table
    /// </summary>
    [Table("JOB")]
    public class JobEdp
    {
        public MapJobSiteEdp DmLocation { get; set; }

        public int DmLocationId { get; set; }

        public int Id { get; set; }

        public string JobNumber { get; set; }

        public string JobType { get; set; }

        public int? SourceId { get; set; }

        public virtual ICollection<LoadoutEdp> Loadouts { get; set; }
    }
}