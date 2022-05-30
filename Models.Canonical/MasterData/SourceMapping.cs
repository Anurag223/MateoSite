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

using System.ComponentModel.DataAnnotations;
using Tlm.Sdk.Core.Models;
using Tlm.Sdk.Core.Models.Metadata;

namespace Tlm.Fed.Models.Canonical.MasterData
{
    /// <inheritdoc />
    /// <summary>
    ///     Describes the mapping between a source structure/system and a target system.
    /// </summary>
    [Sdk.Core.Models.Key("{0}:{1}:{2}:{3}", @"^.+:.+:.+:.+$", nameof(Source), nameof(SourceKey), nameof(Target), nameof(TargetKey))]
    [IsRoot()]
    public class SourceMapping : Entity
    {
        public const string Version = "1.0";

        /// <summary>
        ///     Describes the source, for example "SLOrg".
        /// </summary>
        [Display(Name = "Source")]
        public string Source { get; set; }

        /// <summary>
        ///     Describes the source key, for example "D&amp;M".
        /// </summary>
        [Display(Name = "SourceKey")]
        public string SourceKey { get; set; }

        /// <summary>
        ///     Describes the target. For example "CMMS".
        /// </summary>
        [Display(Name = "Target")]
        public string Target { get; set; }

        /// <summary>
        ///     Describes the target key, for example "QTrac".
        /// </summary>
        [Display(Name = "TargetKey")]
        public string TargetKey { get; set; }
    }
}