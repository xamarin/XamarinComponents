# Getting Started with TimesSquare

> Standalone Android widget for picking a single date from a calendar view.

## Usage

We can include the `CalendarPickerView` element in out layout resource:

    <com.squareup.timessquare.CalendarPickerView
        android:id="@+id/calendar"
        android:layout_width="match_parent"
        android:layout_height="match_parent" />

This is a fairly large control so it is wise to give it ample space in our 
layout. On small devices it is recommended to use a dialog, full-screen 
fragment, or dedicated activity. On larger devices like tablets, displaying 
full-screen is not recommended. A fragment occupying part of the layout or 
a dialog is a better choice.

In the `OnCreate` of our activity/dialog or the `OnCreateView` of our fragment, 
we initialize the view with a range of valid dates as well as the currently 
selected date:

    CalendarPickerView calendar = ...
	
	var today = DateTime.Now;
    var nextYear = today.AddYears(1);

    calendar
	    .Init(today, nextYear)
		.WithSelectedDate(today);

The default mode of the view is to have one selectable date. 
If we want the user to be able to select multiple dates or a date range, we
use the `InMode` method:

    CalendarPickerView calendar = ...
	
	var today = DateTime.Now;
    var nextYear = today.AddYears(1);

    calendar
	    .Init(today, nextYear)
		.InMode(CalendarPickerView.SelectionMode.Range)
		.WithSelectedDate(today);
