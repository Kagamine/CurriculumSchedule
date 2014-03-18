using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CurriculumSchedule
{
	public static class StringHelper
	{
		public static string GetMiddleText(string Source, string Begin, string End)
		{
			var begin = Source.IndexOf (Begin) + Begin.Length;
			var end = Source.IndexOf (End);
			return Source.Substring (begin, end - begin);
		}
		public static int DayOfWeekToInt(DayOfWeek d)
		{
			switch (d) 
			{
			case DayOfWeek.Monday:
				return 1;
			case DayOfWeek.Tuesday:
				return 2;
			case DayOfWeek.Wednesday:
				return 3;
			case DayOfWeek.Thursday:
				return 4;
			case DayOfWeek.Friday:
				return 5;
			case DayOfWeek.Saturday:
				return 6;
			case DayOfWeek.Sunday:
				return 7;
			default:
				return 0;
			}
		}
		public static DayOfWeek IntToDayOfWeek(int d)
		{
			switch(d)
			{
			case 1: return DayOfWeek.Monday;
			case 2: return DayOfWeek.Tuesday;
			case 3: return DayOfWeek.Wednesday;
			case 4: return DayOfWeek.Thursday;
			case 5: return DayOfWeek.Friday;
			case 6: return DayOfWeek.Saturday;
			case 7: return DayOfWeek.Sunday;
			default: return DayOfWeek.Monday;
			}
		}
		public static string DayOfWeekToCHN(DayOfWeek d)
		{
			switch (d) 
			{
			case DayOfWeek.Monday:
				return "一";
			case DayOfWeek.Tuesday:
				return "二";
			case DayOfWeek.Wednesday:
				return "三";
			case DayOfWeek.Thursday:
				return "四";
			case DayOfWeek.Friday:
				return "五";
			case DayOfWeek.Saturday:
				return "六";
			case DayOfWeek.Sunday:
				return "日";
			default:
				return "";
			}
		}
	}
}

