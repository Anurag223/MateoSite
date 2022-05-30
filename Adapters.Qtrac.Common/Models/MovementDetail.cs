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
    ///     MovementDetail Entity Class
    /// </summary>
    public class MovementDetail
    {
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? EquipmentId { get; set; }

        public int? FinancialOwner { get; set; }

        public int? FtlMovementHdrId { get; set; }

        public int? HoldingsiteIdMovement { get; set; }

        public bool IsLatestShipment { get; set; }

        public bool IsToolset { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public int MovementDtlId { get; set; }

        public int MovementHdrId { get; set; }

        public int? OwnersiteIdMovement { get; set; }

        public string ParentContainer { get; set; }

        public int? State { get; set; }

        public int? StauseAtTimeOfTransfer { get; set; }

        public int? ToolsetHistoryHdrId { get; set; }

        public DateTime? TransferOutdate { get; set; }

        public string TransferredBy { get; set; }

        public decimal? Weight { get; set; }
    }
}