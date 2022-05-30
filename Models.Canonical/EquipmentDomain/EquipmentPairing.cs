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
using Tlm.Sdk.Core.Models;
using Tlm.Sdk.Core.Models.Metadata;
using Tlm.Sdk.Core.Models.Querying;

namespace Tlm.Fed.Models.Canonical.EquipmentDomain
{
    [IsRoot()]
    public class EquipmentPairing : CmmsTrackedEntity
    {
        public const string Version = "1.0";

        /// <summary>
        ///     The key for the control site associated with the equipment.
        /// </summary>
        [CanBeFilteredOn]
        public string Equipment1Id { get; set; }

        /// <summary>
        ///     The key for the control site associated with the equipment.
        /// </summary>
        [CanBeFilteredOn]
        public string Equipment2Id { get; set; }

        /// <summary>
        /// </summary>
        public DateTime PairedStartDate { get; set; }

        /// <summary>
        /// </summary>
        public DateTime? PairedEndDate { get; set; }

        /// <summary>
        /// </summary>
        public string PairedBy { get; set; }
    }
}