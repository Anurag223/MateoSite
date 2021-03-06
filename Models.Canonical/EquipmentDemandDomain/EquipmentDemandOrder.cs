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
using System.Collections.Generic;
using Tlm.Sdk.Core.Models;

namespace Tlm.Fed.Models.Canonical.EquipmentDemandDomain
{
    [Key("{0:D}:{1}", @"^\d+:.+$", nameof(ActiveCmmsId), nameof(SourceSystemRecordId))]
    public class EquipmentDemandOrder : AttributedCmmsTrackedEntity
    {
        public const string AttributeNameToolName = "Toolname";

        public DateTime? AcceptedDate { get; set; }

        public string Comments { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public int? EquipmentRequestCount { get; set; }

        public DateTime? PlannedShipDate { get; set; }

        public DateTime? PlannedEndDate { get; set; }

        public DateTime? SubmittedDate { get; set; }

        public string RequestedEquipmentCode { get; set; }

        public ICollection<string> RequestedEquipmentFileCode { get; set; }

        public string EquipmentDemandOrderNumber { get; set; }

        public ICollection<EquipmentDemandFulfillment> Fulfillment { get; set; } = new List<EquipmentDemandFulfillment>();
    }
}