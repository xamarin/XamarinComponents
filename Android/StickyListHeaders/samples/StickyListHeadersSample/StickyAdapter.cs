using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;

using StickyListHeaders;

namespace StickyListHeadersSample
{
    public class StickyAdapter : BaseAdapter<string>, IStickyListHeadersAdapter, ISectionIndexer
    {
        private readonly Context context;
        private readonly LayoutInflater inflater;

        private string[] countries;
        private int[] sectionIndices;
        private Java.Lang.Object[] sectionLetters;

        public StickyAdapter(Context context)
        {
            this.context = context;
            inflater = LayoutInflater.From(context);

            countries = context.Resources.GetStringArray(Resource.Array.countries);
            sectionIndices = GetSectionIndices();
            sectionLetters = GetSectionLetters();
        }

        private int[] GetSectionIndices()
        {
            var sectionIndices = new List<int>();
            var lastFirstChar = countries[0][0];
            sectionIndices.Add(0);
            for (var i = 1; i < countries.Length; i++)
            {
                if (countries[i][0] != lastFirstChar)
                {
                    lastFirstChar = countries[i][0];
                    sectionIndices.Add(i);
                }
            }
            var sections = new int[sectionIndices.Count];
            for (var i = 0; i < sectionIndices.Count; i++)
            {
                sections[i] = sectionIndices[i];
            }
            return sections;
        }

        private Java.Lang.Object[] GetSectionLetters()
        {
            var letters = new Java.Lang.Object[sectionIndices.Length];
            for (var i = 0; i < sectionIndices.Length; i++)
            {
                letters[i] = countries[sectionIndices[i]][0];
            }
            return letters;
        }

        public override int Count
        {
            get { return countries.Length; }
        }

        public override string this[int position]
        {
            get { return countries[position]; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ViewHolder holder;

            if (convertView == null)
            {
                holder = new ViewHolder();
                convertView = inflater.Inflate(Resource.Layout.ListItemLayout, parent, false);
                holder.text = convertView.FindViewById<TextView>(Resource.Id.text);
                convertView.Tag = holder;
            }
            else
            {
                holder = (ViewHolder)convertView.Tag;
            }

            holder.text.Text = countries[position];

            return convertView;
        }

        public View GetHeaderView(int position, View convertView, ViewGroup parent)
        {
            ViewHolder holder;

            if (convertView == null)
            {
                holder = new ViewHolder();
                convertView = inflater.Inflate(Resource.Layout.Header, parent, false);
                holder.text = convertView.FindViewById<TextView>(Resource.Id.text1);
                convertView.Tag = holder;
            }
            else
            {
                holder = (ViewHolder)convertView.Tag;
            }

            // set header text as first char in name
            var headerChar = countries[position].Substring(0, 1);
            holder.text.Text = headerChar;

            return convertView;
        }

        public long GetHeaderId(int position)
        {
            // headers need static IDs
            return countries[position].Substring(0, 1)[0];
        }

        public int GetPositionForSection(int section)
        {
            if (sectionIndices.Length == 0)
            {
                return 0;
            }

            if (section >= sectionIndices.Length)
            {
                section = sectionIndices.Length - 1;
            }
            else if (section < 0)
            {
                section = 0;
            }
            return sectionIndices[section];
        }

        public int GetSectionForPosition(int position)
        {
            for (int i = 0; i < sectionIndices.Length; i++)
            {
                if (position < sectionIndices[i])
                {
                    return i - 1;
                }
            }
            return sectionIndices.Length - 1;
        }

        public Java.Lang.Object[] GetSections()
        {
            return sectionLetters;
        }

        public virtual void Clear()
        {
            countries = new string[0];
            sectionIndices = new int[0];
            sectionLetters = new Java.Lang.Object[0];
            NotifyDataSetChanged();
        }

        public virtual void Restore()
        {
            countries = context.Resources.GetStringArray(Resource.Array.countries);
            sectionIndices = GetSectionIndices();
            sectionLetters = GetSectionLetters();
            NotifyDataSetChanged();
        }

        private class ViewHolder : Java.Lang.Object
        {
            public TextView text;
        }
    }
}
