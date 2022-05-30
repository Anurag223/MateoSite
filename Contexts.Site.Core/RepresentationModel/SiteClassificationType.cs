#region Header

// Schlumberger Private
// Copyright 2018 Schlumberger.  All rights reserved in Schlumberger
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

namespace Tlm.Fed.Contexts.Site.Core.RepresentationModel
{
    /// <summary>
    /// Site classification enum
    /// </summary>
    public enum SiteClassificationType
    {

        /// <summary>The group</summary>
        Group,

        /// <summary>
        ///   The product line
        /// </summary>
        ProductLine,

        /// <summary>The area</summary>
        Area,

        /// <summary>
        /// The Sub Geo Market
        /// </summary>
        SubGeoMarket,

        /// <summary>The geo market</summary>
        GeoMarket,                       
        /// <summary>The geo unit = geo market</summary>
        Geounit,

        /// <summary>The segment</summary>
        Segment,     

        /// <summary>The sub segment</summary>
        SubSegment,              
            
        /// <summary>The Sub Busuness Line</summary>
        SubBusinessLine
    }
}