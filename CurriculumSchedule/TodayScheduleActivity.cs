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
		public static List<Curriculum> Curriculums;
		ListView lstCurriculum;
		public DateTime Target;
		public static DateTime StartDate;
		public CurriculumAdapter adapter;
		public Button btn_week_1, btn_week_2, btn_week_3, btn_week_4, btn_week_5, btn_week_6, btn_week_7;
		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			menu.Add (0, 0, 0, "切换帐号");
			menu.Add (1, 1, 1, "前一天");
			menu.Add (1, 2, 2, "后一天");
			menu.Add (2, 3, 3, "添加课程");
			return true;
		}
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView(Resource.Layout.TodaySchedule);
			adapter = new CurriculumAdapter(this, new List<Curriculum>());
			lstCurriculum = FindViewById<ListView> (Resource.Id.lstCurriculum);
			lstCurriculum.Adapter = adapter;
			btn_week_1 = FindViewById<Button> (Resource.Id.btn_week_1);
			btn_week_2 = FindViewById<Button> (Resource.Id.btn_week_2);
			btn_week_3 = FindViewById<Button> (Resource.Id.btn_week_3);
			btn_week_4 = FindViewById<Button> (Resource.Id.btn_week_4);
			btn_week_5 = FindViewById<Button> (Resource.Id.btn_week_5);
			btn_week_6 = FindViewById<Button> (Resource.Id.btn_week_6);
			btn_week_7 = FindViewById<Button> (Resource.Id.btn_week_7);
			btn_week_1.Click += (sender, e) => 
			{
				while(Target.DayOfWeek != DayOfWeek.Monday)
				{
					Target = Target.AddDays(-1);
				}
				while(Target.DayOfWeek != DayOfWeek.Monday)
				{
					Target = Target.AddDays(1);
				}
				Display();
			};
			btn_week_2.Click += (sender, e) => 
			{
				while(Target.DayOfWeek != DayOfWeek.Monday)
				{
					Target = Target.AddDays(-1);
				}
				while(Target.DayOfWeek != DayOfWeek.Tuesday)
				{
					Target = Target.AddDays(1);
				}
				Display();
			};
			btn_week_3.Click += (sender, e) => 
			{
				while(Target.DayOfWeek != DayOfWeek.Monday)
				{
					Target = Target.AddDays(-1);
				}
				while(Target.DayOfWeek != DayOfWeek.Wednesday)
				{
					Target = Target.AddDays(1);
				}
				Display();
			};
			btn_week_4.Click += (sender, e) => 
			{
				while(Target.DayOfWeek != DayOfWeek.Monday)
				{
					Target = Target.AddDays(-1);
				}
				while(Target.DayOfWeek != DayOfWeek.Thursday)
				{
					Target = Target.AddDays(1);
				}
				Display();
			};
			btn_week_5.Click += (sender, e) => 
			{
				while(Target.DayOfWeek != DayOfWeek.Monday)
				{
					Target = Target.AddDays(-1);
				}
				while(Target.DayOfWeek != DayOfWeek.Friday)
				{
					Target = Target.AddDays(1);
				}
				Display();
			};
			btn_week_6.Click += (sender, e) => 
			{
				while(Target.DayOfWeek != DayOfWeek.Monday)
				{
					Target = Target.AddDays(-1);
				}
				while(Target.DayOfWeek != DayOfWeek.Saturday)
				{
					Target = Target.AddDays(1);
				}
				Display();
			};
			btn_week_7.Click += (sender, e) => 
			{
				while(Target.DayOfWeek != DayOfWeek.Monday)
				{
					Target = Target.AddDays(-1);
				}
				while(Target.DayOfWeek != DayOfWeek.Sunday)
				{
					Target = Target.AddDays(1);
				}
				Display();
			};
			Curriculums = new List<Curriculum>();
			StartDate = Convert.ToDateTime(File.ReadAllText (Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Date.txt")));
			var result = File.ReadAllText (Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Cache.html"));
			var tmp = result.Split('/');
			try
			{
				foreach(var str_class in tmp)
				{
					var lines = str_class.Trim('\n').Split('\n');
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
				}
			}
			catch
			{
				throw;
				File.Delete (Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Cache.html"));
				StartActivity (typeof(MainActivity));
			}
			lstCurriculum.ItemLongClick+= (object sender, AdapterView.ItemLongClickEventArgs e) => 
			{
				int Days = Convert.ToInt32((Target.Date - StartDate).TotalDays);
				var Weeks = Convert.ToInt32(Days / 7 + 1);
				new AlertDialog.Builder(this).SetTitle("删除").SetMessage(String.Format("{0}\n确认删除吗？", adapter.items[e.Position].Title)).SetPositiveButton("确认", delegate {
					var index = Curriculums.FindIndex(x=>x.DayOfWeek == Target.DayOfWeek
						&& x.SectionOfDay == adapter.items[e.Position].SectionOfDay
						&& x.WeekOfCurriculum.Any(w => w == Weeks));
					Curriculums[index].WeekOfCurriculum.Remove(Weeks);
					result = "";
					foreach(var c in Curriculums)
					{
						string t = "";
						foreach(var week in c.WeekOfCurriculum)
						{
							t += week + ",";
						}
						t = t.TrimEnd(',');
						if(t == String.Empty)
							t = "65536";
						result += String.Format("/\n{0}\n\n\n\n\n{1}\n{2}-{3}\n{4}周上\n",c.Title, c.ClassRoom, StringHelper.DayOfWeekToInt(c.DayOfWeek), c.SectionOfDay, t);
					}
					result = result.TrimStart('/');
					File.WriteAllText(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Cache.html"), result);
					Display();
			}).SetNegativeButton("取消", delegate { }).Show();
			};
			Target = DateTime.Now;
			Display ();
		}

		protected void Display()
		{
			int Days = Convert.ToInt32((Target.Date - StartDate).TotalDays);
			var Weeks = Convert.ToInt32(Days / 7 + 1);
			Title = Target.ToString ("M月d日") + " 星期" + StringHelper.DayOfWeekToCHN (Target.DayOfWeek)+String.Format(" 第 {0} 周", Weeks);
			btn_week_1.SetBackgroundColor (Android.Graphics.Color.Rgb(0,0,0));
			btn_week_2.SetBackgroundColor (Android.Graphics.Color.Rgb(0,0,0));
			btn_week_3.SetBackgroundColor (Android.Graphics.Color.Rgb(0,0,0));
			btn_week_4.SetBackgroundColor (Android.Graphics.Color.Rgb(0,0,0));
			btn_week_5.SetBackgroundColor (Android.Graphics.Color.Rgb(0,0,0));
			btn_week_6.SetBackgroundColor (Android.Graphics.Color.Rgb(0,0,0));
			btn_week_7.SetBackgroundColor (Android.Graphics.Color.Rgb(0,0,0));
			switch (Target.DayOfWeek) 
			{
			case DayOfWeek.Monday:
				btn_week_1.SetBackgroundColor (Android.Graphics.Color.Rgb(41,162,220));
				break;
			case DayOfWeek.Tuesday:
				btn_week_2.SetBackgroundColor (Android.Graphics.Color.Rgb(41,162,220));
				break;
			case DayOfWeek.Wednesday:
				btn_week_3.SetBackgroundColor (Android.Graphics.Color.Rgb(41,162,220));
				break;
			case DayOfWeek.Thursday:
				btn_week_4.SetBackgroundColor (Android.Graphics.Color.Rgb(41,162,220));
				break;
			case DayOfWeek.Friday:
				btn_week_5.SetBackgroundColor (Android.Graphics.Color.Rgb(41,162,220));
				break;
			case DayOfWeek.Saturday:
				btn_week_6.SetBackgroundColor (Android.Graphics.Color.Rgb(41,162,220));
				break;
			case DayOfWeek.Sunday:
				btn_week_7.SetBackgroundColor (Android.Graphics.Color.Rgb(41,162,220));
				break;
			}
			var CurrentCurriculums = (from c in Curriculums
				where c.DayOfWeek == Target.DayOfWeek
				&& c.WeekOfCurriculum.Any (x => x == Weeks)
				orderby c.SectionOfDay ascending
				select c).ToList ();
			if (CurrentCurriculums.Count == 0) 
			{
				CurrentCurriculums.Add (new Curriculum()
					{
						Title = "没有任何课程安排"
					});
			}
			adapter.items = CurrentCurriculums;
			lstCurriculum.InvalidateViews();
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			switch (item.ItemId) 
			{
			case 0: 
				File.Delete (Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Cache.html"));
				StartActivity (typeof(MainActivity));
				break;
			case 1:
				Target = Target.AddDays (-1);
				Display ();
				break;
			case 2:
				Target = Target.AddDays (1);
				Display ();
				break;
			case 3:
				var transaction = FragmentManager.BeginTransaction ();
				var addcurriculum = new AddCurriculumDialog ();
				addcurriculum.Show (transaction, "添加课程");
				Display ();
				break;
			}
			return true;
		}
	}
}

