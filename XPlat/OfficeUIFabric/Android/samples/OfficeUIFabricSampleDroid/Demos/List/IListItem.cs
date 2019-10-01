using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using Microsoft.OfficeUIFabric;

namespace OfficeUIFabricSampleDroid.Demos.List
{
    public interface IListItem : IBaseListItem
    {
        string Subtitle { get; set; }
        string Footer { get; set; }

        int TitleMaxLines { get; set; }

        int SubtitleMaxLines { get; set; }
        int FooterMaxLines { get; set; }

        Android.Text.TextUtils.TruncateAt TitleTruncateAt { get; set; }
        Android.Text.TextUtils.TruncateAt SubtitleTruncateAt { get; set; }
        Android.Text.TextUtils.TruncateAt FooterTruncateAt { get; set; }

        View CustomView { get; set; }
        ListItemView.CustomViewSize CustomViewSize { get; set; }
        View CustomAccessoryView { get; set; }

        ListItemView.LayoutDensity LayoutDensity { get; set; }
    }

    public class ListItem : IListItem
    {
        public string Subtitle { get; set; } = string.Empty;
        public string Footer { get; set; } = string.Empty;
        public int TitleMaxLines { get; set; } = ListItemView.DefaultMaxLines;
        public int SubtitleMaxLines { get; set; } = ListItemView.DefaultMaxLines;
        public int FooterMaxLines { get; set; } = ListItemView.DefaultMaxLines;
        public TextUtils.TruncateAt TitleTruncateAt { get; set; } = TextUtils.TruncateAt.End;
        public TextUtils.TruncateAt SubtitleTruncateAt { get; set; } = TextUtils.TruncateAt.End;
        public TextUtils.TruncateAt FooterTruncateAt { get; set; } = TextUtils.TruncateAt.End;
        public View CustomView { get; set; } = null;
        public ListItemView.CustomViewSize CustomViewSize { get; set; } = ListItemView.CustomViewSize.Medium;
        public View CustomAccessoryView { get; set; } = null;
        public ListItemView.LayoutDensity LayoutDensity { get; set; } = ListItemView.LayoutDensity.Regular;
        public string Title { get; } = string.Empty;
    }
}