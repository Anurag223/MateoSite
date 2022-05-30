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

namespace Tlm.Fed.Contexts.Site.Core.DataStoreModel
{
    /// <summary>
    /// </summary>
    public class MasterDistrict
    {
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        ///     Gets or sets the wk identifier.
        /// </summary>
        /// <value>
        ///     The wk identifier.
        /// </value>
        public string WkId { get; set; }

        /// <summary>
        ///     Gets or sets the created date.
        /// </summary>
        /// <value>
        ///     The created date.
        /// </value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        ///     Gets or sets the created by.
        /// </summary>
        /// <value>
        ///     The created by.
        /// </value>
        public object CreatedBy { get; set; }

        /// <summary>
        ///     Gets or sets the last modified date.
        /// </summary>
        /// <value>
        ///     The last modified date.
        /// </value>
        public DateTime LastModifiedDate { get; set; }

        /// <summary>
        ///     Gets or sets the last modified by.
        /// </summary>
        /// <value>
        ///     The last modified by.
        /// </value>
        public string LastModifiedBy { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the country.
        /// </summary>
        /// <value>
        ///     The country.
        /// </value>
        public Country Country { get; set; }

        /// <summary>
        ///     Gets or sets the uom.
        /// </summary>
        /// <value>
        ///     The uom.
        /// </value>
        public string UOM { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is remote district.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is remote district; otherwise, <c>false</c>.
        /// </value>
        public bool IsRemoteDistrict { get; set; }

        /// <summary>
        ///     Gets or sets the time zone.
        /// </summary>
        /// <value>
        ///     The time zone.
        /// </value>
        public TimeZone TimeZone { get; set; }

        /// <summary>
        ///     Gets or sets the sales org.
        /// </summary>
        /// <value>
        ///     The sales org.
        /// </value>
        public SalesOrg SalesOrg { get; set; }

        /// <summary>
        ///     Gets or sets the type of the document.
        /// </summary>
        /// <value>
        ///     The type of the document.
        /// </value>
        public string DocumentType { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is dc enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is dc enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsDCEnabled { get; set; }

        /// <summary>
        ///     Gets or sets the erp system.
        /// </summary>
        /// <value>
        ///     The erp system.
        /// </value>
        public string ERPSystem { get; set; }

        /// <summary>
        ///     Gets or sets the FDP deployment status.
        /// </summary>
        /// <value>
        ///     The FDP deployment status.
        /// </value>
        public string FDPDeploymentStatus { get; set; }

        /// <summary>
        ///     Gets or sets the legacy path.
        /// </summary>
        /// <value>
        ///     The legacy path.
        /// </value>
        public string LegacyPath { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is shared pool.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is shared pool; otherwise, <c>false</c>.
        /// </value>
        public bool IsSharedPool { get; set; }

        /// <summary>
        ///     Gets or sets the sub geo market.
        /// </summary>
        /// <value>
        ///     The sub geo market.
        /// </value>
        public List<SubGeoMarket> SubGeoMarket { get; set; }

        /// <summary>
        ///     Gets or sets the segment information.
        /// </summary>
        /// <value>
        ///     The segment information.
        /// </value>
        public List<SegmentInfo> SegmentInfo { get; set; }

        /// <summary>
        ///     Gets or sets the primary plant.
        /// </summary>
        /// <value>
        ///     The primary plant.
        /// </value>
        public List<PrimaryPlant> PrimaryPlant { get; set; }

        /// <summary>
        ///     Gets or sets the secondary plant.
        /// </summary>
        /// <value>
        ///     The secondary plant.
        /// </value>
        public List<object> SecondaryPlant { get; set; }

        /// <summary>
        ///     Gets or sets the functional location.
        /// </summary>
        /// <value>
        ///     The functional location.
        /// </value>
        public object FunctionalLocation { get; set; }

        /// <summary>
        ///     Gets or sets the resource planning node.
        /// </summary>
        /// <value>
        ///     The resource planning node.
        /// </value>
        public List<ResourcePlanningNode> ResourcePlanningNode { get; set; }

        /// <summary>
        ///     Gets or sets the accounting units.
        /// </summary>
        /// <value>
        ///     The accounting units.
        /// </value>
        public List<AccountingUnit> AccountingUnits { get; set; }

        /// <summary>
        ///     Gets or sets the sales locations.
        /// </summary>
        /// <value>
        ///     The sales locations.
        /// </value>
        public List<string> SalesLocations { get; set; }

        /// <summary>
        ///     Gets or sets the external ops locations.
        /// </summary>
        /// <value>
        ///     The external ops locations.
        /// </value>
        public List<ExternalOpsLocation> ExternalOpsLocations { get; set; }

        /// <summary>
        ///     Gets or sets the remote ops locations.
        /// </summary>
        /// <value>
        ///     The remote ops locations.
        /// </value>
        public object RemoteOpsLocations { get; set; }

        /// <summary>
        ///     Gets or sets the hr org units.
        /// </summary>
        /// <value>
        ///     The hr org units.
        /// </value>
        public object HROrgUnits { get; set; }

        /// <summary>
        ///     Gets or sets the maintenance locations.
        /// </summary>
        /// <value>
        ///     The maintenance locations.
        /// </value>
        public List<MaintenanceLocation> MaintenanceLocations { get; set; }

        /// <summary>
        ///     Gets or sets the ofs stores.
        /// </summary>
        /// <value>
        ///     The ofs stores.
        /// </value>
        public object OFSStores { get; set; }

        /// <summary>
        ///     Gets or sets the business process.
        /// </summary>
        /// <value>
        ///     The business process.
        /// </value>
        public string BusinessProcess { get; set; }

        /// <summary>
        ///     Gets or sets the management countries.
        /// </summary>
        /// <value>
        ///     The management countries.
        /// </value>
        public List<ManagementCountry> ManagementCountries { get; set; }

        /// <summary>
        ///     Gets or sets the business line infos.
        /// </summary>
        /// <value>
        ///     The business line infos.
        /// </value>
        public List<BusinessLineInfo> BusinessLineInfos { get; set; }

        /// <summary>
        ///     Gets or sets the path.
        /// </summary>
        /// <value>
        ///     The path.
        /// </value>
        public string Path { get; set; }
    }
}