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

using Tlm.Sdk.Core.Models;
using Tlm.Sdk.Core.Models.Metadata;

namespace Tlm.Fed.Models.Canonical.UserDomain
{
    [Key("{0}", @"^.+$", nameof(LdapAlias))]
    [IsRoot()]
    public class User : AttributedCmmsTrackedEntity
    {
        public const string Version = "1.0";

        public string Email { get; set; }

        public string UPN { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string GinNumber { get; set; }

        private string _ldapAlias;

        public string LdapAlias { get => _ldapAlias; set => _ldapAlias = !string.IsNullOrEmpty(value) ? value.ToLowerInvariant() : null; }

        public bool Status { get; set; }
    }
}