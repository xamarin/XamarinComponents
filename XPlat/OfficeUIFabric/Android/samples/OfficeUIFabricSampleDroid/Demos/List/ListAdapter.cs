using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Microsoft.OfficeUIFabric;

namespace OfficeUIFabricSampleDroid.Demos.List
{
    class ListAdapter : RecyclerView.Adapter
    {
        public ListAdapter(Context context)
        {
            Context = context;
        }

        public Context Context { get; private set; }

        enum ViewType
        {
            SubHeader, Item
        }

        List<IBaseListItem> listItems = new List<IBaseListItem>();

        public override int ItemCount { get; }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            throw new NotImplementedException();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var lp = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);

            if (viewType == (int)ViewType.SubHeader)
            {
                var subHeaderView = new ListSubHeaderView(Context);
                subHeaderView.LayoutParameters = lp;
                return new ListSubHeaderViewHolder(Context, subHeaderView);
            } else
            {
                var listItemView = new ListItemView(Context);
                listItemView.LayoutParameters = lp;
                return new ListItemViewHolder(Context, listItemView);
            }
        }

        class ListItemViewHolder : RecyclerView.ViewHolder
        {
            ListItemView listItemView;

            public Context Context { get; private set; }

            public ListItemViewHolder(Context context, ListItemView view) : base(view)
            {
                Context = context;
                listItemView = view;
                listItemView.Click += ListItemView_Click;
            }

            void ListItemView_Click(object sender, EventArgs e)
            {
                Snackbar.Make(listItemView, Context.Resources.GetString(Resource.String.list_item_click), Snackbar.LengthShort, Snackbar.Style.Regular).Show();
            }
            
            public void SetListItem(IListItem listItem)
                => listItemView.SetListItem(listItem);

            public void ClearCustomViews()
            {
                listItemView.CustomView = null;
                listItemView.CustomAccessoryView = null;
            }
        }

        private class ListSubHeaderViewHolder : RecyclerView.ViewHolder
        {
            ListSubHeaderView listSubHeaderView;

            public Context Context { get; private set; }

            public ListSubHeaderViewHolder(Context context, ListSubHeaderView view) : base(view)
            {
                Context = context;
                listSubHeaderView = view;
            }

            public void SetListSubHeader(IListSubHeader listSubHeader)
                => listSubHeaderView.SetListSubHeader(listSubHeader);
        }
    }

    public static class ListExtensions
    {
        public static void SetListItem(this ListItemView s, IListItem listItem)
        {
            s.Title = listItem.Title;
            s.Subtitle = listItem.Subtitle;
            s.Footer = listItem.Footer;

            s.TitleMaxLines = listItem.TitleMaxLines;
            s.SubtitleMaxLines = listItem.SubtitleMaxLines;
            s.FooterMaxLines = listItem.FooterMaxLines;

            s.TitleTruncateAt = listItem.TitleTruncateAt;
            s.SubtitleTruncateAt = listItem.SubtitleTruncateAt;
            s.FooterTruncateAt = listItem.FooterTruncateAt;

            s.CustomView = listItem.CustomView;
            s.SetCustomViewSize(listItem.CustomViewSize);
            s.CustomAccessoryView = listItem.CustomAccessoryView;

            s.SetLayoutDensity(listItem.LayoutDensity);
        }

        public static void SetListSubHeader(this ListSubHeaderView s, IListSubHeader listSubHeader)
        {
            s.Title = listSubHeader.Title;
            s.SetTitleColor(listSubHeader.TitleColor);
            s.CustomAccessoryView = listSubHeader.CustomAccessoryView;
        }
    }
}