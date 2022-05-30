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
using System.Collections.Generic;

namespace Tlm.Fed.Adapters.Qtrac.Common.Models
{
    /// <summary>
    ///     Role
    /// </summary>
    public class Role
    {
        public Role()
        {
            RoleObject = new HashSet<RoleObject>();
            UserRoles = new HashSet<UserRoles>();
        }

        public bool Active { get; set; }

        public bool AdminOnly { get; set; }

        public string Code { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public string RoleDesc { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public ICollection<RoleObject> RoleObject { get; set; }

        public int RoleSiteId { get; set; }

        public ICollection<UserRoles> UserRoles { get; set; }
    }
}