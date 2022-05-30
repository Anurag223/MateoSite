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
    public class SegmentInfo
    {
        /// <summary>
        ///     Gets or sets the sub segment code.
        /// </summary>
        /// <value>
        ///     The sub segment code.
        /// </value>
        public string SubSegmentCode { get; set; }

        /// <summary>
        ///     Gets or sets the name of the sub segment.
        /// </summary>
        /// <value>
        ///     The name of the sub segment.
        /// </value>
        public string SubSegmentName { get; set; }

        /// <summary>
        ///     Gets or sets the segment code.
        /// </summary>
        /// <value>
        ///     The segment code.
        /// </value>
        public string SegmentCode { get; set; }

        /// <summary>
        ///     Gets or sets the name of the segment.
        /// </summary>
        /// <value>
        ///     The name of the segment.
        /// </value>
        public string SegmentName { get; set; }

        /// <summary>
        ///     Gets or sets the xref identifier.
        /// </summary>
        /// <value>
        ///     The xref identifier.
        /// </value>
        public object XrefId { get; set; }
    }
}