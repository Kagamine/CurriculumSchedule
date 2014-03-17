using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace CurriculumSchedule
{
	[Activity (Label = "齐齐哈尔大学课程表", MainLauncher = true)]
	public class MainActivity : Activity
	{
		public Button btnGet;
		public EditText txtStudentNumber, txtPassword;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView(Resource.Layout.Main);

			btnGet = FindViewById<Button> (Resource.Id.btnGet);
			txtStudentNumber = FindViewById<EditText> (Resource.Id.txtStudentNumber);
			txtPassword = FindViewById<EditText> (Resource.Id.txtPassword);

			if (File.Exists (Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Cache.html"))) 
			{
				StartActivity(typeof(TodayScheduleActivity));
			}

			btnGet.Click += (Sender, ev) => 
			{
				var result = HttpHelper.HttpGet(String.Format("http://qqhruapi.4321.io/?stuid={0}&pwd={1}", txtStudentNumber.Text, txtPassword.Text));
				var date = HttpHelper.HttpGet(String.Format("http://qqhruapi.4321.io/NewTerm.txt"));
				File.WriteAllText(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Date.txt"), date);
				File.WriteAllText(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Cache.html"), result);
				StartActivity(typeof(TodayScheduleActivity));
			};
		}
	}
}


