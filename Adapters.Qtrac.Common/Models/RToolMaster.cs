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
    ///     The RToolMaster Class
    ///     Represents the Database model for the R_TOOL_MASTER table
    /// </summary>
    public class RToolMaster
    {
        public string Abbreviation { get; set; }

        public bool? Active { get; set; }

        public DateTime? Created { get; set; }

        public string Description { get; set; }

        public int? DisplayOrder { get; set; }

        public int? EtraceId { get; set; }

        public int Id { get; set; }

        public double? InnerDiameter { get; set; }

        public double? Length { get; set; }

        public double? MaxOuterDiameter { get; set; }

        public string Name { get; set; }

        public double? OuterDiameter { get; set; }

        public int ProductGroupId { get; set; }

        public int? ToolImageId { get; set; }

        public string ToolSize { get; set; }

        public int? TubularTool { get; set; }

        public DateTime? Updated { get; set; }
    }
}