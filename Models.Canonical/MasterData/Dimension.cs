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

using System.ComponentModel.DataAnnotations;
using Tlm.Sdk.Core.Models;
using Tlm.Sdk.Core.Models.Metadata;

namespace Tlm.Fed.Models.Canonical.MasterData
{
    [Sdk.Core.Models.Key("{0}:{1}", @"^.+:.+$", nameof(Name), nameof(DimensionalClass))]
    [IsRoot()]
    public class Dimension : Entity
    {
        public const string Version = "1.0";

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "DimensionalClass")]
        public string DimensionalClass { get; set; }
    }
}