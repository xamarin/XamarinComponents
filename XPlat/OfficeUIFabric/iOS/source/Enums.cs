using System;
using ObjCRuntime;

namespace Microsoft.OfficeUIFabric {

	[Native]
	public enum MSActivityIndicatorViewSize : long {
		XSmall = 0,
		Small = 1,
		Medium = 2,
		Large = 3,
		XLarge = 4,
	}

	[Native]
	public enum MSAvatarSize : long {
		XSmall = 0,
		Small = 1,
		Medium = 2,
		Large = 3,
		XLarge = 4,
		XxLarge = 5,
	}

	[Native]
	public enum MSAvatarStyle : long {
		Circle = 0,
		Square = 1,
	}

	[Native]
	public enum MSBadgeViewStyle : long {
		Default = 0,
		Warning = 1,
		Error = 2,
	}

	[Native]
	public enum MSButtonStyle : long {
		PrimaryFilled = 0,
		PrimaryOutline = 1,
		SecondaryOutline = 2,
		Borderless = 3,
	}

	[Native]
	public enum MSDateStringCompactness : long {
		LongDaynameDayMonth = 1,
		LongDaynameDayMonthYear = 2,
		ShortDayname = 3,
		ShortDaynameShortMonthnameDay = 4,
		ShortDaynameShortMonthnameDayFullYear = 5,
		PartialDaynameShortDayMonth = 6,
		LongDaynameDayMonthHoursColumnsMinutes = 7,
		ShortDaynameShortMonthnameHoursColumnsMinutes = 8,
		PartialDaynameShortDayMonthHoursColumsMinutes = 9,
		PartialMonthnameDaynameFullYear = 10,
		PartialMonthnameDaynameHoursColumnsMinutes = 11,
		PartialMonthnameDayname = 12,
		LongMonthNameFullYear = 13,
		ShortDaynameHoursColumnMinutes = 14,
		ShortDayMonth = 15,
		LongDayMonthYearTime = 16,
		ShortDaynameDayShortMonthYear = 17,
	}

	[Native]
	public enum MSDateTimePickerMode : long {
		Date = 0,
		Time = 1,
		Range = 2,
		TimeRange = 3,
	}

	[Native]
	public enum MSDrawerPresentationDirection : long {
		Down = 0,
		Up = 1,
	}

	[Native]
	public enum MSDrawerResizingBehavior : long {
		None = 0,
		Dismiss = 1,
		DismissOrExpand = 2,
	}

	[Native]
	public enum MSTextColorStyle : long {
		Regular = 0,
		Secondary = 1,
		White = 2,
		Primary = 3,
		Error = 4,
		Warning = 5,
	}

	[Native]
	public enum MSTextStyle : long {
		Title1 = 0,
		Title2 = 1,
		Headline = 2,
		Body = 3,
		Subhead = 4,
		Footnote = 5,
		Caption1 = 6,
		Caption2 = 7,
	}

	[Native]
	public enum MSCustomViewSize : long {
		Default = 0,
		Zero = 1,
		Small = 2,
		Medium = 3,
	}

	[Native]
	public enum MSTableViewCellAccessoryType : long {
		None = 0,
		DisclosureIndicator = 1,
		DetailButton = 2,
	}

	[Native]
	public enum MSPersonaListViewSelectionDirection : long {
		Next = 1,
		Prev = -1,
	}

	[Native]
	public enum MSSeparatorOrientation : long {
		Horizontal = 0,
		Vertical = 1,
	}

	[Native]
	public enum MSSeparatorStyle : long {
		Default = 0,
		Shadow = 1,
	}

	[Native]
	public enum MSTimeStringCompactness : long {
		ColumnsMinutes = 1,
		MSTimeStringCompactnessHours = 2,
	}

	[Native]
	public enum MSTwoLinesTitleStyle : long {
		Light = 0,
		Dark = 1,
	}

	[Native]
	public enum MSTwoLinesTitleViewButtonStyle : long {
		Disclosure = 0,
		DownArrow = 1,
	}

	[Native]
	public enum MSObscureStyle : long {
		Blur = 0,
		Dim = 1,
	}


}
