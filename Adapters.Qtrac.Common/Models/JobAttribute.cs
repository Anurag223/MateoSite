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
    /// <summary>
    ///     Model for BORE_HOLE_ATTRIBUTES table
    /// </summary>
    public class JobAttribute
    {
        public DateTime? ActualEndDate { get; set; }

        public int? Borehole { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedUser { get; set; }

        public string DqrEngineerComment { get; set; }

        public string DqrFsmComment { get; set; }

        public bool? DqrLogPerformed { get; set; }

        public DateTime? DqrQcDate { get; set; }

        public bool DqrQcDone { get; set; }

        public bool? DqrQcNotified { get; set; }

        public string DqrQcUser { get; set; }

        public DateTime? DqrSentDate { get; set; }

        public bool? DqrSubmitNotified { get; set; }

        public DateTime? EstimatedEndDate { get; set; }

        public DateTime? EstimatedStartDate { get; set; }

        public double? HoleSize { get; set; }

        public int Id { get; set; }

        public int? JobId { get; set; }

        public double? LastCasingSize { get; set; }

        public string Name { get; set; }

        public double? PlannedEndDepth { get; set; }

        public int? PlannedNoOfBha { get; set; }

        public double? PlannedStartDepth { get; set; }

        public bool? SectionFinished { get; set; }

        public double? ShoeMd { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string UpdateUser { get; set; }
    }
}