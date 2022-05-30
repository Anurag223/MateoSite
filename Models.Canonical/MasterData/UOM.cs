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

using System.ComponentModel.DataAnnotations;
using Tlm.Sdk.Core.Models;
using Tlm.Sdk.Core.Models.Metadata;

namespace Tlm.Fed.Models.Canonical.MasterData
{
    [Sdk.Core.Models.Key("{0}:{1}", @"^.+:.+$", nameof(Name), nameof(QuantityType))]
    [IsRoot()]
    public class UOM : Entity
    {
        public const string Version = "1.0";

        [Display(Name = "BasicAuthority")]
        public string BasicAuthority { get; set; }

        [Display(Name = "CatalogName")]
        public string CatalogName { get; set; }

        [Display(Name = "CatalogSymbol")]
        public string CatalogSymbol { get; set; }

        [Display(Name = "CatelogId")]
        public string CatelogId { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "QuantityType")]
        public string QuantityType { get; set; }

        [Display(Name = "RP66Symbol")]
        public string RP66Symbol { get; set; }
    }
}