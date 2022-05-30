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
using System.ComponentModel.DataAnnotations.Schema;

namespace Tlm.Fed.Adapters.Qtrac.Common.Models
{
    /// <summary>
    ///     The Job Class
    ///     Represents the Database model for the Job table
    /// </summary>
    [Table("JOB")]
    public class Job
    {
        public Job()
        {
            LrdeRun = new HashSet<LrdeRun>();
        }

        public bool? BlockRigPackets { get; set; }

        public string Comments { get; set; }

        public Company Company { get; set; }

        [ForeignKey(nameof(Company))]
        public string CompanyCrmId { get; set; }

        public int? CompanySourceId { get; set; }

        public int? Cor { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedUser { get; set; }

        public string CrewEmail { get; set; }

        public string CrewOtherPhone { get; set; }

        public string CrewSatellitePhone { get; set; }

        public string CurrentActivity { get; set; }

        public string Directions { get; set; }

        public string DistrictAlias { get; set; }

        public int? DistrictId { get; set; }

        public MapJobSite DmLocation { get; set; }

        public int DmLocationId { get; set; }

        public PersonnelMaster DrillingEngineer { get; set; }

        [ForeignKey(nameof(DrillingEngineer))]
        public int? DrillingEngineerGin { get; set; }

        public DateTime? EndDate { get; set; }

        public int? FieldId { get; set; }

        public DateTime? FinalCompositeLogDate { get; set; }

        public PersonnelMaster FsmDd { get; set; }

        [ForeignKey(nameof(FsmDd))]
        public int? FsmDdGin { get; set; }

        public PersonnelMaster FsmMotors { get; set; }

        [ForeignKey(nameof(FsmMotors))]
        public int? FsmMotorsGin { get; set; }

        public PersonnelMaster FsmMwd { get; set; }

        [ForeignKey(nameof(FsmMwd))]
        public int? FsmMwdGin { get; set; }

        public string Guid { get; set; }

        public bool? Hpj { get; set; }

        public bool? HpjArea { get; set; }

        public int Id { get; set; }

        public bool? IsCasedHole { get; set; }

        public bool? IsCommandCenterJob { get; set; }

        public bool? IsNt5fieldTest { get; set; }

        public bool? IsRental { get; set; }

        public bool? IsRetainer { get; set; }

        public bool? JobFinished { get; set; }

        public string JobNumber { get; set; }

        public string JobState { get; set; }

        public string JobType { get; set; }

        public string JobTypeCode { get; set; }

        public decimal? Jri { get; set; }

        public string JricreatedBy { get; set; }

        public DateTime? JricreatedDate { get; set; }

        public string JriModifiedBy { get; set; }

        public DateTime? JrimodifiedDate { get; set; }

        public string JriQuestTicket { get; set; }

        public decimal? Jriscore { get; set; }

        public string Jristatus { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public virtual ICollection<Loadout> Loadouts { get; set; }

        public virtual ICollection<LrdeRun> LrdeRun { get; set; }

        public bool? Operational { get; set; }

        public bool? Opportunity { get; set; }

        public string OscComments { get; set; }

        public double? PacketLatitude { get; set; }

        public double? PacketLongitude { get; set; }

        public DateTime? PacketSpudDate { get; set; }

        public string PacketStd21Data { get; set; }

        public double? Probability { get; set; }

        public DateTime? QtracLastModifiedDate { get; set; }

        public string QuestTicketNumber { get; set; }

        public bool? ReentryWell { get; set; }

        public string ReentryWellTypeCode { get; set; }

        public Rig Rig { get; set; }

        [ForeignKey(nameof(Rig))]
        public int? RigId { get; set; }

        public string RigType { get; set; }

        public PersonnelMaster SalesEngineer { get; set; }

        [ForeignKey(nameof(SalesEngineer))]
        public int? SalesEngineerGin { get; set; }

        public string SapJobNumber { get; set; }

        public Sdmcell Sdmcell { get; set; }

        public int? SdmcellId { get; set; }

        public string SegmentName { get; set; }

        public string ServiceLineName { get; set; }

        public string SourceId { get; set; }

        public int? SourceServiceLineId { get; set; }

        public string SourceSystem { get; set; }

        public DateTime? StartDate { get; set; }

        public string Std21DataClassification { get; set; }

        public string TrackingNumber { get; set; }

        public int? Ttc { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string UpdateUser { get; set; }

        public Well Well { get; set; }

        [ForeignKey(nameof(Well))]
        public int? WellId { get; set; }

        public bool? IsFDPJob { get; set; }
    }
}