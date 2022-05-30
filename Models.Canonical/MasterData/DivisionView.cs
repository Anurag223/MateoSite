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

using Tlm.Sdk.Core.Models;
using Tlm.Sdk.Core.Models.Metadata;

#pragma warning disable 1591

namespace Tlm.Fed.Models.Canonical.MasterData
{
    /// <summary>
    ///     SLOrg Division View represent the Division->BusinessLine-SubBusinessLine organizational units
    /// </summary>
    [Key("{0}", @"^.+$", nameof(Code))]
    [IsRoot()]
    public class DivisionView : Entity
    {
        public const string Version = "1.0";

        /// <summary>
        ///     MasterData Code representing the Unique Identity of the SLOrg unit
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     DivisionViews are geographical
        /// </summary>
        public string DVGroup { get; set; }

        public string LongName { get; set; }

        public string Definition { get; set; }

        public string ParentCode { get; set; }

        public string Type { get; set; }
    }
}