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
	public class CurriculumAdapter: BaseAdapter<Curriculum> 
	{
		public List<Curriculum> items;
		Activity context;
		public CurriculumAdapter(Activity context, List<Curriculum> items)
			: base()
		{
			this.context = context;
			this.items = items;
		}
		public override long GetItemId(int position)
		{
			return position;
		}
		public override Curriculum this[int position]
		{
			get { return items[position]; }
		}
		public override int Count
		{
			get { return items.Count; }
		}
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var item = items[position];
			View view = convertView;
			if (view == null) // no view to re-use, create new
				view = context.LayoutInflater.Inflate(Resource.Layout.CurriculumItem, null);
			view.FindViewById<TextView>(Resource.Id.tvTitle).Text = item.Title;
			view.FindViewById<TextView> (Resource.Id.tvDetail).Text = String.Format ("第{0}大节 {1}", item.SectionOfDay, item.ClassRoom);
			return view;
		}
	}
}

