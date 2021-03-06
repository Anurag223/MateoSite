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

using System.Collections.Generic;

namespace Tlm.Fed.Contexts.Site.Core.DataStoreModel
{
    /// <summary>
    /// </summary>
    public class PrimaryPlant
    {
        /// <summary>
        ///     Gets or sets the code.
        /// </summary>
        /// <value>
        ///     The code.
        /// </value>
        public string Code { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is primary.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is primary; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrimary { get; set; }

        /// <summary>
        ///     Gets or sets the company code.
        /// </summary>
        /// <value>
        ///     The company code.
        /// </value>
        public string CompanyCode { get; set; }

        /// <summary>
        ///     Gets or sets the source location.
        /// </summary>
        /// <value>
        ///     The source location.
        /// </value>
        public List<SourceLocation> SourceLocation { get; set; }

        /// <summary>
        ///     Gets or sets the sub segment.
        /// </summary>
        /// <value>
        ///     The sub segment.
        /// </value>
        public List<string> SubSegment { get; set; }
    }
}