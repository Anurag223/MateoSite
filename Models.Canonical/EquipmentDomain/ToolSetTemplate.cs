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
using Tlm.Sdk.Core.Models;
using Tlm.Sdk.Core.Models.Hypermedia;
using Tlm.Sdk.Core.Models.Infrastructure;
using Tlm.Sdk.Core.Models.Metadata;

// ReSharper disable once CheckNamespace
namespace Tlm.Fed.Contexts.Equipment.Core.RepresentationModel
{
    /// <summary>
    ///     ToolSetTemplate is the Physical Entity that represents the entity
    ///     from MAXIMO
    /// </summary>
    [IsRoot()]
    [Key("{0:D}:{1}", @"^\d+:.+$", nameof(ActiveCmmsId), nameof(EquipmentCode))]
    public class ToolSetTemplate : Resource
    {
        public const string Version = "1.0";

        [BsonIgnore]
        public CmmsId ActiveCmmsId { get; set; }

        public string EquipmentCode { get; set; }

        public ICollection<BuildItem> BuildItems { get; set; } = new List<BuildItem>();
    }
}