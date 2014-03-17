using System;
using System.Web;
using System.Collections.Generic;
using System.Web.UI;

namespace WebHost
{
	public partial class Default : System.Web.UI.Page
	{
		protected override void OnLoad (EventArgs e)
		{
			base.OnLoad (e);
			HttpHelper HttpHelper = new HttpHelper ();
			var Stuid = Request.QueryString ["stuid"].ToString ();
			var Pwd = Request.QueryString ["Pwd"].ToString ();
			var args = new List<Argument>();
			args.Add(new Argument()
				{
					Key = "stuid",
					Value = Stuid
				});
			args.Add(new Argument()
				{
					Key = "pwd",
					Value = Pwd
				});
			var result = HttpHelper.HttpPost("http://jw.qqhru.edu.cn:7778/pls/wwwbks/bks_login2.login", args);
			if(string.IsNullOrEmpty(result))
			{
				result = "";
			}
			result = HttpHelper.HttpGet("http://jw.qqhru.edu.cn:7778/pls/wwwbks/xk.CourseView");
			result = GetMiddleText(result,"<td bgcolor=\"#EAE2F3\"><p align=\"center\"><strong>上课周次</strong></p></td>", "</table>\n</td>\n</tr>\n</table>\n</BODY></HTML>" )
				.Replace("\r\n", "\n")
				.Replace("<td bgcolor=\"#EAE2F3\"><font size=\"2\"><p align=\"center\">","")
				.Replace("</p></td>","")
				.Replace("\n</TR>", "")
				.Replace("\n<FONT COLOR=\"#FF0000\"></FONT>", "")
				.Replace("<FONT COLOR=\"#FF0000\">", "")
				.Replace("</FONT>", "")
				.Replace("<TR>", "/")
				.TrimStart('\n')
				.TrimStart('/')
				.TrimStart('\n');
			Response.Write (result);
		}
		public static string GetMiddleText(string Source, string Begin, string End)
		{
			var begin = Source.IndexOf (Begin) + Begin.Length;
			var end = Source.IndexOf (End);
			return Source.Substring (begin, end - begin);
		}
	}
}

