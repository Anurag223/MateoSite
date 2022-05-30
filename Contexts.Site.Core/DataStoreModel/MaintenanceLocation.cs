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

namespace Tlm.Fed.Contexts.Site.Core.DataStoreModel
{
    /// <summary>
    /// </summary>
    public class MaintenanceLocation
    {
        /// <summary>
        ///     Gets or sets the code.
        /// </summary>
        /// <value>
        ///     The code.
        /// </value>
        public string Code { get; set; }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>
        ///     The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets the sales location.
        /// </summary>
        /// <value>
        ///     The sales location.
        /// </value>
        public string SalesLocation { get; set; }

        /// <summary>
        ///     Gets or sets the sub segment.
        /// </summary>
        /// <value>
        ///     The sub segment.
        /// </value>
        public string SubSegment { get; set; }

        /// <summary>
        ///     Gets or sets the sub sub segment.
        /// </summary>
        /// <value>
        ///     The sub sub segment.
        /// </value>
        public string SubSubSegment { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is primary.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is primary; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrimary { get; set; }
    }
}