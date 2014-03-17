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
		public DatePicker dateNewTerm;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView(Resource.Layout.Main);

			btnGet = FindViewById<Button> (Resource.Id.btnGet);
			txtStudentNumber = FindViewById<EditText> (Resource.Id.txtStudentNumber);
			txtPassword = FindViewById<EditText> (Resource.Id.txtPassword);
			dateNewTerm = FindViewById<DatePicker> (Resource.Id.dateNewTerm);

			if (File.Exists (Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Cache.html"))) 
			{
				StartActivity(typeof(TodayScheduleActivity));
			}

			btnGet.Click += (Sender, ev) => 
			{

				var args = new List<Argument>();
				args.Add(new Argument()
					{
						Key = "stuid",
						Value = txtStudentNumber.Text
						//Value = "2012023045"
					});
				args.Add(new Argument()
					{
						Key = "pwd",
						Value = txtPassword.Text
						//Value = "zys1993110"
					});
				var result = HttpHelper.HttpPost("http://jw.qqhru.edu.cn:7778/pls/wwwbks/bks_login2.login", args);
				if(string.IsNullOrEmpty(result))
				{
					new AlertDialog.Builder(this).SetTitle("获取失败").SetMessage("请核对您的学号和密码是否正确。");
					return;
				}
				result = HttpHelper.HttpGet("http://jw.qqhru.edu.cn:7778/pls/wwwbks/xk.CourseView");
				result = StringHelper.GetMiddleText(result,"<td bgcolor=\"#EAE2F3\"><p align=\"center\"><strong>上课周次</strong></p></td>", "</table>\n</td>\n</tr>\n</table>\n</BODY></HTML>" )
					.Replace("\r\n", "\n")
					.Replace("<td bgcolor=\"#EAE2F3\"><font size=\"2\"><p align=\"center\">","")
					.Replace("</p></td>","")
					.Replace("\n</TR>", "")
					.Replace("\n<FONT COLOR=\"#FF0000\"></FONT>", "")
					.Replace("<FONT COLOR=\"#FF0000\">", "")
					.Replace("</FONT>", "")
					.Replace("<TR>", "/")
					.TrimStart('\n')
					.TrimStart('/');
				File.WriteAllText(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Date.txt"), dateNewTerm.DateTime.Date.ToString());
				File.WriteAllText(Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Cache.html"), result);
				StartActivity(typeof(TodayScheduleActivity));
			};
		}
	}
}


