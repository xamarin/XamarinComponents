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

namespace OfficeUIFabricSampleDroid.Demos.Views
{
    public enum CellOrientation
    {
        Horizontal,
        Vertical
    }

    public class Cell : TemplateView
    {
        public Cell(Context context, Android.Util.IAttributeSet attrs, int defStyleAttr) : base (context, attrs, defStyleAttr)
        {
            if (attrs != null)
            {
                var styledAttrs = context.ObtainStyledAttributes(attrs, Resource.Styleable.Cell);
                title = styledAttrs.GetString(Resource.Styleable.Cell_title);
                description = styledAttrs.GetString(Resource.Styleable.Cell_description);
                var orientationOrdinal = styledAttrs.GetInt(Resource.Styleable.Cell_orientation, (int)DefaultOrientation);
                orientation = (CellOrientation)orientationOrdinal;
                styledAttrs.Recycle();
            }
        }

        public static CellOrientation DefaultOrientation = CellOrientation.Horizontal;

        CellOrientation orientation = DefaultOrientation;

        public CellOrientation Orientation {
            get => orientation;
            set {
                orientation = value;
                InvalidateTemplate();
            }
        }

        string title = string.Empty;
        public string Title
        {
            get => title;
            set
            {
                title = value;
                UpdateTemplate();
            }
        }

        string description = string.Empty;
        public string Description
        {
            get => description;
            set
            {
                description = value;
                UpdateTemplate();
            }
        }

        protected override int TemplateId 
            => orientation == CellOrientation.Horizontal 
                ? Resource.Layout.template_cell_horizontal
                : Resource.Layout.template_cell_vertical;


        TextView titleView = null;
        TextView descriptionView = null;


        void UpdateTemplate()
        {
            if (titleView != null)
                titleView.Text = title;
            if (descriptionView != null)
                descriptionView.Text = description;
        }

        protected override void OnTemplateLoaded()
        {
            base.OnTemplateLoaded();

            titleView = FindViewInTemplateById(Resource.Id.cell_title) as TextView;
            descriptionView = FindViewInTemplateById(Resource.Id.cell_description) as TextView;
            UpdateTemplate();
        }
    }
}