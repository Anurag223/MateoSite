﻿#region Header

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
using Tlm.Fed.Models.Canonical.MasterData;
using Tlm.Sdk.Core.Models.Querying;

namespace Tlm.Fed.Contexts.Site.Core.RepresentationModel
{
    [KeyReference(typeof(Facility))]
    [IsRoot]
    public class FacilityRepresentation : Entity
    {
        [CanBeFilteredOn]
        public override string Id { get => base.Id; set => base.Id = value; }

        /// <summary>
        ///     The Description of the facility
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Latitude of the facility located
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        ///     Longitude of hte facility located
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        ///     Describes the Name of the facility
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Describes the ownerShip of the facility
        /// </summary>
        public string Ownership { get; set; }

        /// <summary>
        ///     Describes the status
        /// </summary>
        public string Status { get; set; }

        public const string Version = "1.0";
    }
}