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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tlm.Fed.Adapters.Qtrac.Common.Models
{
    [Table("tblRigAttribute")]
    public class RigAttribute
    {
        public RigAttribute()
        {
            LrdeRunAttribute = new HashSet<LrdeRunAttribute>();
            LrdeRunEquipmentAttribute = new HashSet<LrdeRunEquipmentAttribute>();
            LrdeRunOutOfSpec = new HashSet<LrdeRunOutOfSpec>();
        }

        [Required]
        [StringLength(100)]
        public string Attribute { get; set; }

        [Required]
        [StringLength(255)]
        public string AttributeDescription { get; set; }

        [StringLength(255)]
        public string AttributeExportName { get; set; }

        [StringLength(100)]
        public string AttributeGroup { get; set; }

        [Key]
        [Column("AttributeID")]
        public int AttributeId { get; set; }

        [Column(TypeName = "text")]
        public string AttributeTipText { get; set; }

        public bool? CalculateLast { get; set; }

        [Column("CalculationSQL")]
        public bool? CalculationSql { get; set; }

        [StringLength(100)]
        public string Category { get; set; }

        public int? CategoryOrder { get; set; }

        public bool? CheckIfNull { get; set; }

        public bool? ClientCalculated { get; set; }

        public bool? Consolidate { get; set; }

        public bool? CopyAttribute { get; set; }

        [Column(TypeName = "numeric(18, 0)")]
        public decimal? DataType { get; set; }

        [Column("DefaultUOM")]
        [StringLength(50)]
        public string DefaultUom { get; set; }

        [StringLength(50)]
        public string DefaultValue { get; set; }

        public bool? Deleted { get; set; }

        [StringLength(50)]
        public string DomainType { get; set; }

        public bool? DoNotDisplay { get; set; }

        public bool? DoNotDowload { get; set; }

        [Column("FTLOnly")]
        public bool? Ftlonly { get; set; }

        [Column("FTLReadOnly")]
        public bool? FtlreadOnly { get; set; }

        [Column("JobTypeID")]
        public int? JobTypeId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? LastModified { get; set; }

        [StringLength(50)]
        public string LastModifiedBy { get; set; }

        [Column("LOVID")]
        public int? Lovid { get; set; }

        [Column("LRDE")]
        public bool? Lrde { get; set; }

        [Column("LRDE_Required")]
        public bool? LrdeRequired { get; set; }

        [InverseProperty("Attribute")]
        public virtual ICollection<LrdeRunAttribute> LrdeRunAttribute { get; set; }

        [InverseProperty("Attribute")]
        public virtual ICollection<LrdeRunEquipmentAttribute> LrdeRunEquipmentAttribute { get; set; }

        [InverseProperty("Attribute")]
        public virtual ICollection<LrdeRunOutOfSpec> LrdeRunOutOfSpec { get; set; }

        public bool? Mandatory { get; set; }

        [Column("MasterAttID")]
        public int? MasterAttId { get; set; }

        public bool? Obsolete { get; set; }

        public int? Order { get; set; }

        [Column("ParentAttributeID", TypeName = "numeric(18, 0)")]
        public decimal? ParentAttributeId { get; set; }

        [Column("QTrac_Mapping")]
        [StringLength(50)]
        public string QtracMapping { get; set; }

        [Column(TypeName = "numeric(18, 0)")]
        public decimal? Size { get; set; }

        [StringLength(50)]
        public string UnitType { get; set; }

        [Column("ValueChangedRecalcID")]
        public int? ValueChangedRecalcId { get; set; }

        [Column(TypeName = "numeric(18, 0)")]
        public decimal? ValueEnd { get; set; }

        [Column(TypeName = "numeric(18, 0)")]
        public decimal? ValueStart { get; set; }
    }
}