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

using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using Tlm.Fed.Models.Canonical;

namespace Tlm.Fed.Contexts.Equipment.Core.RepresentationModel
{
    public class BuildItem : AttributedEntity
    {
        [BsonIgnoreIfNull]
        public string EquipmentCode { get; set; }

        [BsonIgnoreIfNull]
        public string MaterialNumber { get; set; }

        public ICollection<Alternate> Alternates { get; set; } = new List<Alternate>();
    }
}