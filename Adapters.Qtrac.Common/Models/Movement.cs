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
using System.ComponentModel.DataAnnotations.Schema;

namespace Tlm.Fed.Adapters.Qtrac.Common.Models
{
    /// <summary>
    ///     Movement properties.
    /// </summary>
    public class Movement
    {
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? DateReceived { get; set; }

        public DateTime? EstimatedArrivalDate { get; set; }

        public bool HasReceived { get; set; }

        public int? HazMatFormId { get; set; }

        public bool? IsFtlInitiated { get; set; }

        public bool? IsHazMatIdRecievedFromRmis { get; set; }

        public string ItemNo { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public DateTime? LoadInDate { get; set; }

        public DateTime? LoadOutDate { get; set; }

        public int? MovementFromId { get; set; }

        public int MovementHdrId { get; set; }

        public int MovementHdrSiteId { get; set; }

        public int? MovementToId { get; set; }

        public ReferenceMovementType MovementType { get; set; }

        public string ParentShipmentNo { get; set; }

        public DateTime? ReceiveDate { get; set; }

        public int RefMovementStatusId { get; set; }

        [ForeignKey(nameof(MovementType))]
        public int? RefMovementTypeId { get; set; }

        public DateTime? RentalOffDate { get; set; }

        public DateTime? RentalOnDate { get; set; }

        public DateTime? ShipmentDate { get; set; }

        public string ShipmentNumber { get; set; }

        public string ToRmisDepot { get; set; }

        public string TrackingNumber { get; set; }

        public string TransferType { get; set; }
    }
}