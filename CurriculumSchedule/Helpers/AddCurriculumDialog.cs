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
	public class AddCurriculumDialog : DialogFragment
	{
		public string[] Sections = { "第一大节", "第二大节", "第三大节", "第四大节" };
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			Dialog.SetTitle ("添加课程");
			var view = inflater.Inflate(Resource.Layout.AddCurriculum, container, false);
			view.FindViewById<Button>(Resource.Id.btnAddCurriculum).Click += delegate
			{
				var title = view.FindViewById<EditText>(Resource.Id.txtCurriculumTitle).Text;
				var date = view.FindViewById<DatePicker>(Resource.Id.dpDate).DateTime.Date;
				var section = view.FindViewById<SeekBar>(Resource.Id.sbSection).Progress + 1;
				var position = view.FindViewById<EditText>(Resource.Id.txtPosition).Text;
				int week = Convert.ToInt32((date - TodayScheduleActivity.StartDate).TotalDays) / 7 + 1;
				var result = File.ReadAllText (Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Cache.html"));
				if(result.IndexOf('/') >= 0)
					result += "/";
				result += String.Format("{0}\n\n0\n\n\n{1}\n{2}-{3}\n{4}周上", title, position, StringHelper.DayOfWeekToInt(date.DayOfWeek), section, week);
				File.WriteAllText(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Cache.html"), result);
				List<int> weeks = new List<int>();
				weeks.Add(week);
				TodayScheduleActivity.Curriculums.Add(new Curriculum()
					{
						Title = title,
						DayOfWeek = date.DayOfWeek,
						SectionOfDay = section,
						WeekOfCurriculum = weeks,
						ClassRoom = position
					});
				Toast.MakeText(Application.Context, "课程添加成功", ToastLength.Short).Show();
				Dismiss();
			};
			view.FindViewById<SeekBar>(Resource.Id.sbSection).ProgressChanged += (sender, e) => 
			{
				view.FindViewById<TextView>(Resource.Id.tvSection).Text = String.Format("上课时间 第{0}大节", ((SeekBar)sender).Progress + 1);
			};
			return view;
		}
	}
}

