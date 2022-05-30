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
using System.Collections.Generic;
using Tlm.Fed.Models.Canonical.SiteDomain;
using Tlm.Sdk.Core.Models;

namespace Tlm.Fed.Models.Canonical.MovementDomain
{
    [Key("{0:D}:{1}", @"^[\d]+:.+$", nameof(ActiveCmmsId), nameof(SourceSystemRecordId))]
    public class Movement : CmmsTrackedEntity
    {
        /// <summary>
        ///     ActualArrivalDate of the movement.
        /// </summary>
        public DateTime? ActualArrivalDate { get; set; }

        /// <summary>
        ///     Describes actual shipment Date
        /// </summary>
        public DateTime? ActualShipmentDate { get; set; }

        /// <summary>
        ///     Control Site
        /// </summary>
        public SiteId ControlSiteId { get; set; }

        /// <summary>
        ///     Describes comment of the movement.
        /// </summary>
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        /// <summary>
        ///     Describes dispatch Site
        /// </summary>
        public SiteId DispatchSiteId { get; set; }

        /// <summary>
        ///     The ID of the Equipment being moved
        /// </summary>
        public string EquipmentId { get; set; }

        /// <summary>
        ///     Describes FTL shipment flag
        /// </summary>
        public bool FTLShipmentIndicator { get; set; }

        /// <summary>
        ///     to check whether tool flag is set o not
        /// </summary>
        public bool ToolSetIndicator { get; set; }

        /// <summary>
        ///     Describes from location of the movement
        /// </summary>
        public SiteId MovementFromSiteId { get; set; }

        /// <summary>
        ///     Description of the from location
        /// </summary>
        public string MovementFromDescription { get; set; }

        /// <summary>
        ///     Describes target location of the movement
        /// </summary>
        public SiteId MovementToSiteId { get; set; }

        /// <summary>
        ///     Description of the target location
        /// </summary>
        public string MovementToDescription { get; set; }

        /// <summary>
        ///     Describes type is of the movement base, job , internal
        /// </summary>
        public string MovementType { get; set; }

        /// <summary>
        ///     Describes parent shipment number
        /// </summary>
        public string ParentShipmentNumber { get; set; }

        /// <summary>
        ///     Describes estimated end date
        /// </summary>
        public DateTime? PlannedArrivalDate { get; set; }

        /// <summary>
        ///     Describes estimated start date
        /// </summary>
        public DateTime? PlannedShipmentDate { get; set; }

        /// <summary>
        ///     Describes released date
        /// </summary>
        public DateTime? ReleasedDate { get; set; }

        /// <summary>
        ///     Describes movement requested By
        /// </summary>
        public string RequestedBy { get; set; }

        /// <summary>
        ///     Describes movement requested date
        /// </summary>
        public DateTime RequestedDate { get; set; }

        /// <summary>
        ///     Describes scheduled arrival date
        /// </summary>
        public DateTime? ScheduledArrivalDate { get; set; }

        /// <summary>
        ///     Describes scheduled shipment Date
        /// </summary>
        public DateTime? ScheduledShipmentDate { get; set; }

        /// <summary>
        ///     Describes date of shipment
        /// </summary>
        public DateTime? ShipmentDate { get; set; }

        /// <summary>
        ///     Describes the movementnumber
        /// </summary>
        public string ShipmentNumber { get; set; }

        /// <summary>
        ///     Describes the status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        ///     Describes the total quantity
        /// </summary>
        public string TotalQuantity { get; set; }

        /// <summary>
        ///     Describes type is  of permanant, loan
        /// </summary>
        public string TransferType { get; set; }
    }
}