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
using OfficeUIFabricSampleDroid.Demos;

namespace OfficeUIFabricSampleDroid
{
    internal class Demo : Java.Lang.Object
    {
        const string AVATAR_VIEW = "AvatarView";
        const string BASIC_INPUTS = "Basic Inputs";
        const string BOTTOM_SHEET = "BottomSheet";
        const string CALENDAR_VIEW = "CalendarView";
        const string DATE_TIME_PICKER_DIALOG = "DateTimePickerDialog";
        const string DRAWER = "Drawer";
        const string LIST_ITEM_VIEW = "ListItemView";
        const string PEOPLE_PICKER_VIEW = "PeoplePickerView";
        const string PERSONA_CHIP_VIEW = "PersonaChipView";
        const string PERSONA_LIST_VIEW = "PersonaListView";
        const string PERSONA_VIEW = "PersonaView";
        const string PROGRESS = "Progress";
        const string SNACKBAR = "Snackbar";
        const string TEMPLATE_VIEW = "TemplateView";
        const string TOOLTIP = "Tooltip";
        const string TYPOGRAPHY = "Typography";

        internal static List<Demo> Demos = new List<Demo> {
            new Demo(AVATAR_VIEW, typeof(AvatarViewActivity)),
            new Demo(BASIC_INPUTS, typeof(BasicInputsActivity)),
            new Demo(BOTTOM_SHEET, typeof(BottomSheetActivity)),
            new Demo(CALENDAR_VIEW, typeof(CalendarViewActivity)),
            //new Demo(DATE_TIME_PICKER_DIALOG, typeof(DateTimePickerDialogActivity)),
            new Demo(DRAWER, typeof(DrawerActivity)),
            //new Demo(LIST_ITEM_VIEW, typeof(ListItemViewActivity)),
            //new Demo(PEOPLE_PICKER_VIEW, typeof(PeoplePickerViewActivity)),
            new Demo(PERSONA_CHIP_VIEW, typeof(PersonaChipViewActivity)),
            //new Demo(PERSONA_LIST_VIEW, typeof(PersonaListViewActivity)),
            new Demo(PERSONA_VIEW, typeof(PersonaViewActivity)),
            new Demo(PROGRESS, typeof(ProgressActivity)),
            new Demo(SNACKBAR, typeof(SnackbarActivity)),
            //new Demo(TEMPLATE_VIEW, typeof(TemplateViewActivity)),
            //new Demo(TOOLTIP, typeof(TooltipActivity)),
            new Demo(TYPOGRAPHY, typeof(TypographyActivity))
        };

        public Demo(string title, Type demoActivityType)
        {
            Title = title;
            ActivityType = demoActivityType;
        }
        public string Title { get; private set; }
        public Type ActivityType { get; private set; }
        public string Id { get; private set; } = Guid.NewGuid().ToString();
    }
}