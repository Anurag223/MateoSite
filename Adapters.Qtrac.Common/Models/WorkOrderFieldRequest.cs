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

using System;

namespace Tlm.Fed.Adapters.Qtrac.Common.Models
{
    /// <summary>
    ///     WorkOrderFieldRequest class
    /// </summary>
    public class WorkOrderFieldRequest
    {
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string FrcaddyId { get; set; }

        public string Frnumber { get; set; }

        public string Frstatus { get; set; }

        public int Id { get; set; }

        public bool IssueOutSubmitted { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public string PolineNumber { get; set; }

        public string Ponumber { get; set; }

        public string Postatus { get; set; }

        public string TransactionId { get; set; }

        public string TransactionStatus { get; set; }

        public string TransactionType { get; set; }

        public int WorkOrderId { get; set; }
    }
}