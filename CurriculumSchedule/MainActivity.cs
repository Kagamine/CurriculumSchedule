using System;
using System.Collections.Generic;
using System.IO;
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
		Button btnGet;
		EditText txtStudentNumber, txtPassword;
		protected override void OnCreate (Bundle bundle)
		{
			SetContentView(Resource.Layout.Main);
			if (File.Exists (System.Environment.CurrentDirectory + "\\Curriculums.xml")) 
			{
				StartActivity(typeof(TodayScheduleActivity));
			}
			btnGet = FindViewById<Button> (Resource.Id.btnGet);
			txtStudentNumber = FindViewById<EditText> (Resource.Id.txtStudentNumber);
			txtPassword = FindViewById<EditText> (Resource.Id.txtPassword);
			btnGet.Click += (sender, e) => 
			{
				var args = new List<Argument>();
				args.Add(new Argument()
					{
						Key = "stuid",
						Value = txtStudentNumber.Text
					});
				args.Add(new Argument()
					{
						Key = "pwd",
						Value = txtPassword.Text
					});
				var result = HttpHelper.HttpPost("http://jw.qqhru.edu.cn:7778/zhxt_bks/xk_login.html", args);
				new AlertDialog.Builder(this).SetTitle("Result").SetMessage(result).Show();
				StartActivity(typeof(TodayScheduleActivity));
			};
		}
	}
}


