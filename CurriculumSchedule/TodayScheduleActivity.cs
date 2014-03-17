using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CurriculumSchedule
{
	[Activity (Label = "今日课程")]			
	public class TodayScheduleActivity : Activity
	{
		public List<Curriculum> Curriculums = new List<Curriculum> ();
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView(Resource.Layout.TodaySchedule);
			var today = DateTime.Now.DayOfWeek;
			XDocument xml = XDocument.Load (System.Environment.CurrentDirectory + "\\Curriculums.xml");
			var StartDay = Convert.ToDateTime (xml.Element ("Config").Attribute ("StartDay").Value);
			var Weeks = (DateTime.Now.Date - StartDay).TotalDays + 1;
			Curriculums = (from e in xml.Elements ("Curriculum")
				where e.Attribute ("DayOfWeek").Value == DateTime.Now.DayOfWeek.ToString ()
				&& e.Elements ("WeekOfCurriculum").Any (x => Convert.ToInt32 (x.Value) == Weeks)
				select new Curriculum()
				{
					Title = e.Attribute("Title").Value.ToString(),
					SectionOfDay = Convert.ToInt32(e.Attribute("SectionOfDay").Value),
					Teacher = e.Attribute("Teacher").Value.ToString(),
					ClassRoom = e.Attribute("ClassRoom").Value.ToString()
				}).ToList();

		}
	}
}

