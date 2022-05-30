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
    ///     MovementComment Entity class.
    /// </summary>
    public class MovementComment
    {
        public string CommentText { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? FtlMovementHdrId { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public int MovementCommentId { get; set; }

        public int MovementCommentSiteId { get; set; }

        public int MovementHdrId { get; set; }

        public string ParentShipmentNo { get; set; }
    }
}