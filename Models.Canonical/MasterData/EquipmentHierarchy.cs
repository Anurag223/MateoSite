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
using Newtonsoft.Json;
using Tlm.Sdk.Core.Models;

namespace Tlm.Fed.Models.Canonical.MasterData
{
    [JsonObject(ItemReferenceLoopHandling = ReferenceLoopHandling.Ignore)]
    [Key("{0:D}:{1}", @"^[\d]+:.+$", nameof(Type), nameof(Code))]
    public class EquipmentHierarchy : Entity
    {
        [JsonProperty]
        public string Code { get; set; }

        [JsonIgnore]
        public string ChildKey { get; set; }

        [JsonIgnore]
        public override string Id { get; set; }

        [JsonProperty]
        public string Name { get; set; }

        [JsonIgnore]
        public string Type { get; set; }

        [JsonIgnore]
        public string ParentKey { get; set; }

        public ReferenceObject ReferenceObject { get; set; }

        public IReadOnlyCollection<EquipmentHierarchy> Children { get; set; } = new List<EquipmentHierarchy>();
    }
}