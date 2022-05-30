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
using System.Globalization;
using System.Text.RegularExpressions;

namespace Tlm.Fed.Adapters.Qtrac.Common.Helper
{
    public static class DateTimeWithZoneExtension
    {
        private static readonly Regex Regex;
        private static readonly TimeSpan MatchTimeout;

        static DateTimeWithZoneExtension()
        {
            MatchTimeout = TimeSpan.FromSeconds(10);
            Regex = new Regex(@"^(?<IntegerPart>[-0-9]*).{0,1}(?<FractionalPart>[0-9]*)?$", RegexOptions.Compiled, MatchTimeout);
        }

        public static TimeSpan ConvertToTimeSpan(this double difference)
        {
            var match = Regex.Match(difference.ToString(CultureInfo.CurrentCulture));
            if (match.Success)
            {
                var integerPart = match.Groups["IntegerPart"].Value;
                if (string.IsNullOrEmpty(integerPart))
                    throw new InvalidCastException($"Fail to convert timezone hour value {difference}");

                var fractionalPart = match.Groups["FractionalPart"].Value;
                var fraction = 0;
                if (!string.IsNullOrEmpty(fractionalPart))
                    fraction = Convert.ToInt32(fractionalPart);

                var min = 0;

                switch (fraction)
                {
                    case 5:
                    case 50:
                        min = 30;
                        break;
                    case 25:
                        min = 15;
                        break;

                    case 75:
                        min = 45;
                        break;
                    default:
                        min = (int)(fraction * 0.6);
                        break;
                }

                var hour = Convert.ToInt32(integerPart);

                // change min to negative if hour is negative
                min = hour < 0 ? -min : min;
                return new TimeSpan(hour, min, 0);
            }

            throw new InvalidCastException($"Fail to convert timezone value {difference}");
        }
    }
}