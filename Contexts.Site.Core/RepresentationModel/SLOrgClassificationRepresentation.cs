#region Header

// Schlumberger Private
// Copyright 2018 Schlumberger.  All rights reserved in Schlumberger
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

using Tlm.Sdk.Core.Models;
using Tlm.Sdk.Core.Models.Metadata;
using Tlm.Sdk.Core.Models.Querying;
using Tlm.Fed.Models.Canonical.MasterData;

namespace Tlm.Fed.Contexts.Site.Core.RepresentationModel
{
    [KeyReference(typeof(SLOrgClassification))]
    [IsRoot]
    public class SlOrgClassificationRepresentation : Entity
    {
        /// <summary>
        ///     Area of the Classification
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        ///     GeoMarket of the Classification
        /// </summary>
        public string GeoMarket { get; set; }

        /// <summary>
        ///     Group of the Classification
        /// </summary>
        /// ///
        public string Group { get; set; }

        /// <summary>
        ///     GeoMarket of the Classification
        /// </summary>
        [CanBeFilteredOn]
        public string ProductLine { get; set; }

        public const string Version = "1.0";
    }
}