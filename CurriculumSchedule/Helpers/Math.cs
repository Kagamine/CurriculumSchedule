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
	public static class Math
	{
		public static int Ceiling(double num)
		{
			if(Convert.ToInt32(num) == num)
				return Convert.ToInt32(num);
			else return Convert.ToInt32(num) + 1;
		}
	}
}

