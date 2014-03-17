using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
		ListView lstCurriculum;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView(Resource.Layout.TodaySchedule);
			lstCurriculum = FindViewById<ListView> (Resource.Id.lstCurriculum);
			List<Curriculum> Curriculums = new List<Curriculum>();
			var result = File.ReadAllText (Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Cache.html"));

			var tmp = result.Split('/');
			foreach(var str_class in tmp)
			{
				var lines = str_class.TrimStart('\n').Split('\n');
				Curriculum c = new Curriculum();
				c.Title = lines[0];
				c.ClassRoom = lines[5];
				c.SectionOfDay = lines[6][2] - '0';
				c.DayOfWeek = StringHelper.IntToDayOfWeek(lines[6][0] - '0');
				lines[7] = lines[7].Replace("周上", "");
				c.WeekOfCurriculum = new List<int>();
				var weeks = lines[7].Split(',');
				foreach(var week in weeks)
				{
					if(week.IndexOf("-") < 0)
					{
						c.WeekOfCurriculum.Add(Convert.ToInt32(week));
					}
					else
					{
						var week_from = Convert.ToInt32(week.Split('-')[0]);
						var week_to = Convert.ToInt32(week.Split('-')[1]);
						for(int i = week_from; i <= week_to; i++)
						{
							c.WeekOfCurriculum.Add(Convert.ToInt32(i));
						}
					}
				}
				Curriculums.Add(c);
				//new AlertDialog.Builder(this).SetTitle("Result").SetMessage(String.Format("课程名：{0}\n教室：{1}\n上课时间：{2} 第{3}大节", c.Title, c.ClassRoom, c.DayOfWeek, c.SectionOfDay.ToString())).Show();
			}
			var StartDate = Convert.ToDateTime(File.ReadAllText (Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Date.txt")));
			var Weeks = Math.Ceiling ((DateTime.Now.Date - StartDate).TotalDays / 7);
			TextView tvWeek = FindViewById<TextView> (Resource.Id.tvWeek);
			tvWeek.Text = String.Format("本学期第 {0} 周", Weeks);
			var CurrentCurriculums = (from c in Curriculums
			                          where c.DayOfWeek == DateTime.Now.DayOfWeek
			                              && c.WeekOfCurriculum.Any (x => x == Weeks)
			                          orderby c.SectionOfDay ascending
			                          select c).ToList ();
			lstCurriculum.Adapter = new CurriculumAdapter (this, CurrentCurriculums);
		}
	}
}

