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

namespace Tlm.Fed.Models.Canonical.EquipmentDomain
{
    /// <summary>
    ///     Status of the equipment.
    /// </summary>
    public class Status
    {
        #region Rite

        public const string ActivityStatusType = "Activity";
        public const string LifeStatusType = "Life";
        public const string MovementStatusType = "Movement";
        public const string ModificationStatusType = "Modification";
        public const string RepairStatusType = "Repair";
        public const string ServiceStatusType = "Service";
        public const string CertificationStatusType = "Certification";

        #endregion

        #region Common

        public const string TechnicalStatusType = "Technical"; //QTrac, Rite
        public const string LogisticsStatusType = "Logistics"; // Maximo

        #endregion

        /// <summary>
        ///     The Status Code.
        /// </summary>
        public string StatusCode { get; set; }

        /// <summary>
        ///     The Value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        ///     MovementStatus, TechnicalStatus, RepairStatus
        /// </summary>
        public string StatusType { get; set; }
    }
}