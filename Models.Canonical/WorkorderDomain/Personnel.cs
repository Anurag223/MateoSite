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

namespace Tlm.Fed.Models.Canonical.WorkorderDomain
{
    public class Personnel
    {
        public const string Technician1 = "Technician1";
        public const string Technician2 = "Technician2";
        public const string Technician3 = "Technician3";

        public string Alias { get; set; }

        public string Gin { get; set; }

        public string AssignmentRole { get; set; }
    }
}