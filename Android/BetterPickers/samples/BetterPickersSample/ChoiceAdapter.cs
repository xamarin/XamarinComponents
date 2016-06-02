
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.App;

namespace BetterPickersSample
{

    public class ChoiceItem
    {
        public ChoiceItem (string text, Action action)
        {
            Text = text;
            Action = action;
        }

        public Action Action { get; set; }
        public string Text { get; set; }
    }

    public class ChoiceAdapter : BaseAdapter <ChoiceItem>
    {
        public List<ChoiceItem> Items { get; set; }

        public ChoiceAdapter (Activity context, List<ChoiceItem> items)
        {
            Items = items;
            Context = context;
        }

        public Activity Context { get; set; }
        public override long GetItemId (int position) { return position; }
        public override int Count { get { return Items.Count; } }
        public override ChoiceItem this [int index] { get { return Items [index]; } }

        public override View GetView (int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? Context.LayoutInflater.Inflate (Android.Resource.Layout.SimpleListItem1, parent, false);

            view.FindViewById <TextView> (Android.Resource.Id.Text1).Text = Items [position].Text;

            return view;
        }
    }
}
