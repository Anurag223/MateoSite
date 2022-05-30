﻿#region Header

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

using System.Threading.Tasks;
using Tlm.Sdk.AspNetCore;

namespace Tlm.Fed.Contexts.Site.API
{
	public class Program : ProgramForMicroservice<StartupForSiteApi>
	{
		public static async Task Main(string[] args) => await BaseMainAsync(args);
	}
}
