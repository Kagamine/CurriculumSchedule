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
	public class Curriculum
	{
		public DayOfWeek DayOfWeek { get; set; }
		public int SectionOfDay { get; set; }
		public string Title { get; set; }
		public string Teacher { get; set; }
		public string ClassRoom { get; set; }
		public int[] WeekOfCurriculum { get; set; }
	} 
}

