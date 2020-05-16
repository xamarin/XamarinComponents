using System;
using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Views;
using Microsoft.OfficeUIFabric;

namespace OfficeUIFabricSampleDroid.Demos
{
    [Activity]
    public class BottomSheetActivity : DemoActivity, Microsoft.OfficeUIFabric.BottomSheetItem.IOnClickListener //IOnBottomSheetItemClickListener
    {
        protected override int ContentLayoutId => Resource.Layout.activity_bottom_sheet;

        View root_view;
        Button show_with_single_line_items_button;
        Button show_with_double_line_items_button;
        Button show_bottom_sheet_dialog_button;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            root_view = FindViewById<View>(Resource.Id.root_view);
            show_with_single_line_items_button = FindViewById<Button>(Resource.Id.show_with_single_line_items_button);
            show_with_double_line_items_button = FindViewById<Button>(Resource.Id.show_with_double_line_items_button);
            show_bottom_sheet_dialog_button = FindViewById<Button>(Resource.Id.show_bottom_sheet_dialog_button);

            show_with_single_line_items_button.Click += delegate
            {
                var bottomSheet = BottomSheet.NewInstance(
                    new List<BottomSheetItem> {
                        new BottomSheetItem(
                            Resource.Id.bottom_sheet_item_flag,
                            Resource.Drawable.ic_flag,
                            Resources.GetString(Resource.String.bottom_sheet_item_flag_title)
                        ),
                        new BottomSheetItem(
                            Resource.Id.bottom_sheet_item_reply,
                            Resource.Drawable.ic_reply,
                            Resources.GetString(Resource.String.bottom_sheet_item_reply_title)
                        ),
                        new BottomSheetItem(
                            Resource.Id.bottom_sheet_item_forward,
                            Resource.Drawable.ic_forward,
                            Resources.GetString(Resource.String.bottom_sheet_item_forward_title)
                        ),
                        new BottomSheetItem(
                            Resource.Id.bottom_sheet_item_delete,
                            Resource.Drawable.ic_trash_can,
                            Resources.GetString(Resource.String.bottom_sheet_item_delete_title)
                        )
                    });
                bottomSheet.Show(SupportFragmentManager, null);
            };

            show_with_double_line_items_button.Click += delegate {
                var bottomSheet = BottomSheet.NewInstance(
                    new List<BottomSheetItem> {
                        new BottomSheetItem(
                            Resource.Id.bottom_sheet_item_camera,
                            Resource.Drawable.ic_camera,
                            Resources.GetString(Resource.String.bottom_sheet_item_camera_title),
                            Resources.GetString(Resource.String.bottom_sheet_item_camera_subtitle)
                        ),
                        new BottomSheetItem(
                            Resource.Id.bottom_sheet_item_gallery,
                            Resource.Drawable.ic_gallery,
                            Resources.GetString(Resource.String.bottom_sheet_item_gallery_title),
                            Resources.GetString(Resource.String.bottom_sheet_item_gallery_subtitle)
                        ),
                        new BottomSheetItem(
                            Resource.Id.bottom_sheet_item_videos,
                            Resource.Drawable.ic_videos,
                            Resources.GetString(Resource.String.bottom_sheet_item_videos_title),
                            Resources.GetString(Resource.String.bottom_sheet_item_videos_subtitle)
                        ),
                        new BottomSheetItem(
                            Resource.Id.bottom_sheet_item_manage,
                            Resource.Drawable.ic_wrench,
                            Resources.GetString(Resource.String.bottom_sheet_item_manage_title),
                            Resources.GetString(Resource.String.bottom_sheet_item_manage_subtitle)
                        )
                    });
                bottomSheet.Show(SupportFragmentManager, null);
            };

            show_bottom_sheet_dialog_button.Click += delegate {
                var bottomSheetDialog = new BottomSheetDialog(
                    this,
                    new List<BottomSheetItem> {
                        new BottomSheetItem(
                            Resource.Id.bottom_sheet_item_clock,
                            Resource.Drawable.ic_clock,
                            Resources.GetString(Resource.String.bottom_sheet_item_clock_title)
                        ),
                        new BottomSheetItem(
                            Resource.Id.bottom_sheet_item_alarm,
                            Resource.Drawable.ic_alarm,
                            Resources.GetString(Resource.String.bottom_sheet_item_alarm_title)
                        ),
                        new BottomSheetItem(
                            Resource.Id.bottom_sheet_item_stop_watch,
                            Resource.Drawable.ic_stop_watch,
                            Resources.GetString(Resource.String.bottom_sheet_item_stop_watch_title)
                        ),
                        new BottomSheetItem(
                            Resource.Id.bottom_sheet_item_time_zone,
                            Resource.Drawable.ic_time_zone,
                            Resources.GetString(Resource.String.bottom_sheet_item_time_zone_title)
                        )
                    });
                bottomSheetDialog.OnItemClickListener = this;
                bottomSheetDialog.Show();
            };
        }
        public void OnBottomSheetItemClick(int id)
        {
            switch (id) {
                // single line items
                case Resource.Id.bottom_sheet_item_flag:
                    ShowSnackbar(Resources.GetString(Resource.String.bottom_sheet_item_flag_toast));
                    break;
                case Resource.Id.bottom_sheet_item_reply:
                    ShowSnackbar(Resources.GetString(Resource.String.bottom_sheet_item_reply_toast));
                    break;
                case Resource.Id.bottom_sheet_item_forward:
                    ShowSnackbar(Resources.GetString(Resource.String.bottom_sheet_item_forward_toast));
                    break;
                case Resource.Id.bottom_sheet_item_delete:
                    ShowSnackbar(Resources.GetString(Resource.String.bottom_sheet_item_delete_toast));
                    break;

                // double line items
                case Resource.Id.bottom_sheet_item_camera:
                    ShowSnackbar(Resources.GetString(Resource.String.bottom_sheet_item_camera_toast));
                    break;
                case Resource.Id.bottom_sheet_item_gallery:
                    ShowSnackbar(Resources.GetString(Resource.String.bottom_sheet_item_gallery_toast));
                    break;
                case Resource.Id.bottom_sheet_item_videos:
                    ShowSnackbar(Resources.GetString(Resource.String.bottom_sheet_item_videos_toast));
                    break;
                case Resource.Id.bottom_sheet_item_manage:
                    ShowSnackbar(Resources.GetString(Resource.String.bottom_sheet_item_manage_toast));
                    break;

                // dialog
                case Resource.Id.bottom_sheet_item_clock:
                    ShowSnackbar(Resources.GetString(Resource.String.bottom_sheet_item_clock_toast));
                    break;
                case Resource.Id.bottom_sheet_item_alarm:
                    ShowSnackbar(Resources.GetString(Resource.String.bottom_sheet_item_alarm_toast));
                    break;
                case Resource.Id.bottom_sheet_item_stop_watch:
                    ShowSnackbar(Resources.GetString(Resource.String.bottom_sheet_item_stop_watch_toast));
                    break;
                case Resource.Id.bottom_sheet_item_time_zone:
                    ShowSnackbar(Resources.GetString(Resource.String.bottom_sheet_item_time_zone_toast));
                    break;
            }
        }

        void ShowSnackbar(string message)
            => Snackbar.Companion.Make(root_view, message, Snackbar.LengthLong, Snackbar.Style.Regular).Show();

        public void OnBottomSheetItemClick(BottomSheetItem item)
        {
            throw new NotImplementedException();
        }
    }
}