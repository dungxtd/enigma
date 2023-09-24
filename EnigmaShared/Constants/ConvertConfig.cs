using System;
using Newtonsoft.Json;

namespace EnigmaShared.Constants
{
	public class ConvertConfig
	{
		public static readonly DateFormatHandling JSONDateFormatHandling = DateFormatHandling.IsoDateFormat;
        public static readonly DateTimeZoneHandling JSONDateTimeZoneHandling = DateTimeZoneHandling.Local;
        public static readonly string DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':ss.fffFFFFK";
        public static readonly NullValueHandling JSONNullValueHandling = NullValueHandling.Ignore;
        public static readonly ReferenceLoopHandling JSONReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    }
}

