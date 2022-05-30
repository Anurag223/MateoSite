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

namespace Tlm.Fed.Adapters.Qtrac.Common.Models
{
    public class LoadoutModificationDetails
    {
        public string Action { get; set; }

        public int? AttributeId { get; set; }

        public int? EtraceId { get; set; }

        public int Id { get; set; }

        public string Label { get; set; }

        public int LogId { get; set; }

        public string NewValue { get; set; }

        public string OldValue { get; set; }

        public int TCOToolsetId { get; set; }

        public string Type { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string UpdateUser { get; set; }
    }
}