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

namespace Tlm.Fed.Models.Canonical.MasterData
{
    [Key("{0}:{1}", @"^.+:.+$", nameof(CMMS), nameof(Status))]
    [IsRoot]
    public class EquipmentStatusMapping : Entity
    {
        public string CMMS { get; set; }

        public string Description { get; set; }

        public string FMPDescription { get; set; }

        public string FMPStatus { get; set; }

        public string Status { get; set; }

        public string StatusType { get; set; }

        public string TargetSystem { get; set; }

        public const string Version = "1.0";
    }
}