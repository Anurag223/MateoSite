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

using System.ComponentModel.DataAnnotations;
using Tlm.Sdk.Core.Models;
using Tlm.Sdk.Core.Models.Metadata;

namespace Tlm.Fed.Models.Canonical.MasterData
{
    [Sdk.Core.Models.Key("{0}:{1}", @"^.+:.+$", nameof(Source), nameof(SourceKey))]
    [IsRoot()]
    public class SiteTypeMap : Entity
    {
        public const string Version = "1.0";

        /// <summary>
        ///     Describes the source, for example "MAXIMO".
        /// </summary>
        [Display(Name = "Source")]
        public string Source { get; set; }

        /// <summary>
        ///     Describes the source key for the Site Type. Example "STORE"
        /// </summary>
        [Display(Name = "SourceKey")]
        public string SourceKey { get; set; }

        /// <summary>
        ///     Describes the target for the Site Type. For example "MATEO".
        /// </summary>
        [Display(Name = "Target")]
        public string Target { get; set; }

        /// <summary>
        ///     Describes the target key for SiteType. Example "OPERATION"
        /// </summary>
        [Display(Name = "TargetKey")]
        public string TargetKey { get; set; }
    }
}