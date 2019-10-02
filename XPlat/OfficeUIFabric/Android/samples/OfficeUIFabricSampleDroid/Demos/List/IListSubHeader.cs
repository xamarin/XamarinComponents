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
using Microsoft.OfficeUIFabric;

namespace OfficeUIFabricSampleDroid.Demos.List
{
    public interface IListSubHeader : IBaseListItem
    {
        ListSubHeaderView.TitleColor TitleColor { get; set; }
        View CustomAccessoryView { get; set; }
    }

    public class ListSubHeader : IListSubHeader
    {
        public ListSubHeaderView.TitleColor TitleColor { get; set; } = ListSubHeaderView.TitleColor.Primary;
        public View CustomAccessoryView { get; set; } = null;
        public string Title { get; } = string.Empty;
    }
}