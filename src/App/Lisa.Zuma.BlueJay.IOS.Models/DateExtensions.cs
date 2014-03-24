using System;
using System.Collections.Generic;
using System.Linq;

namespace Lisa.Zuma.BlueJay.IOS.Models
{
	public static class DateExtensions
	{
		public static long GetTimestamp(this DateTime dateTime)
		{
			return long.Parse(dateTime.ToString("yyyyMMddHHmmssffff"));
		}

		public static DateTime ToClientTime(this DateTime dateTime, string timeZone)
		{
			return TimeZoneInfo.ConvertTimeFromUtc(dateTime, TimeZoneInfo.FindSystemTimeZoneById(timeZone));
		}

		public static string ToFancyString(this DateTime dateTime, string timeZone = null)
		{
			DateTime now = DateTime.UtcNow;
			DateTime clientDateTime = dateTime;

			if (!string.IsNullOrEmpty(timeZone))
			{
				now = now.ToClientTime(timeZone);
				clientDateTime = dateTime.ToClientTime(timeZone);
			}

			string dateFormat = "dddd, dd MMMM yyyy, H:mm";

			if (clientDateTime.GetTimestamp() > now.GetTimestamp())
			{
				dateFormat = "'Ergens in de toekomst'";
			}
			else if (clientDateTime.Day == now.Day)
			{
				if (clientDateTime.Hour == now.Hour)
				{
					string sfx = string.Empty;
					int diff = 0;
					if (clientDateTime.Minute == now.Minute)
					{
						sfx = "secode" + (diff == 0 || diff > 1 ? "s" : "");
						diff = now.Second - clientDateTime.Second;
					}
					else
					{
						diff = now.Minute - clientDateTime.Minute;
						sfx = "minuten" + (diff == 0 || diff > 1 ? "s" : "");
					}
					dateFormat = string.Format("'{0} {1} ago'", diff, sfx);
				}
				else
				{
					dateFormat = "'vandaag,' H:mm";
				}
			}
			else if (clientDateTime.Day == now.Day - 1)
			{
				dateFormat = "'gisteren,' H:mm";
			}
			else if (clientDateTime.Year == now.Year)
			{
				if (clientDateTime.DayOfYear < now.DayOfYear && clientDateTime.DayOfYear > now.DayOfYear - 7)
				{
					dateFormat = "dddd, H:mm";
				}
				else
				{
					dateFormat = "dddd, dd MMMM, H:mm";
				}
			}

			return clientDateTime.ToString(dateFormat, new System.Globalization.CultureInfo("en-US", false));
		}
	}
}