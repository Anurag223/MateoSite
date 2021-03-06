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

namespace Tlm.Fed.Contexts.Site.Core.DataStoreModel
{
    /// <summary>
    /// </summary>
    public class BusinessLineInfo
    {
        /// <summary>
        ///     Gets or sets the name of the business line.
        /// </summary>
        /// <value>
        ///     The name of the business line.
        /// </value>
        public string BusinessLineName { get; set; }

        /// <summary>
        ///     Gets or sets the business line code.
        /// </summary>
        /// <value>
        ///     The business line code.
        /// </value>
        public string BusinessLineCode { get; set; }

        /// <summary>
        ///     Gets or sets the sub business line code.
        /// </summary>
        /// <value>
        ///     The sub business line code.
        /// </value>
        public string SubBusinessLineCode { get; set; }

        /// <summary>
        ///     Gets or sets the name of the sub business line.
        /// </summary>
        /// <value>
        ///     The name of the sub business line.
        /// </value>
        public string SubBusinessLineName { get; set; }
    }
}