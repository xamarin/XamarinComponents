using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Foundation;
using UIKit;
using Newtonsoft.Json.Linq;

using DZNEmptyDataSet;
using CoreAnimation;

namespace DZNEmptyDataSetSample
{
    partial class ViewController : UITableViewController
    {
        private ColorsEmptyDataSetSource colorsEmptyDataSetSource;
        private ColorsEmptyDataSetDelegate colorsEmptyDataSetDelegate;
        private SearchEmptyDataSetSource searchEmptyDataSetSource;
        private SearchEmptyDataSetDelegate searchEmptyDataSetDelegate;

        private List<Colour> colors;
        private bool showData = true;

        public ViewController (IntPtr handle)
            : base (handle)
        {
            colorsEmptyDataSetSource = new ColorsEmptyDataSetSource ();
            colorsEmptyDataSetDelegate = new ColorsEmptyDataSetDelegate ();
            searchEmptyDataSetSource = new SearchEmptyDataSetSource ();
            searchEmptyDataSetDelegate = new SearchEmptyDataSetDelegate ();

            var json = File.ReadAllText (NSBundle.MainBundle.PathForResource ("colors", "json"));
            colors = JArray.Parse (json)
                .Select (c => new Colour (c ["name"], c ["rgb"], c ["hex"]))
                .ToList ();
        }

        public List<Colour> FilteredColors {
            get {
                if (!showData) {
                    return new List<Colour> ();
                }
                if (searchBar.IsFirstResponder && searchBar.Text.Length > 0) {
                    return colors.Where (c => c.Name.IndexOf (searchBar.Text, StringComparison.OrdinalIgnoreCase) != -1)
                        .ToList ();
                }
                return colors;
            }
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            // remove the lines for empty rows
            TableView.TableFooterView = new UIView ();
            searchDisplayController.SearchResultsTableView.TableFooterView = new UIView ();

            // the main table view
            TableView.SetEmptyDataSetSource (colorsEmptyDataSetSource);
            TableView.SetEmptyDataSetDelegate (colorsEmptyDataSetDelegate);

            // the search table view
            var searchTableView = searchDisplayController.SearchResultsTableView;
            searchTableView.SetEmptyDataSetSource (searchEmptyDataSetSource);
            searchTableView.SetEmptyDataSetDelegate (searchEmptyDataSetDelegate);

            // hide the default message
            searchDisplayController.SetValueForKey ((NSString)"", (NSString)"noResultsMessage");
        }

        public override nint NumberOfSections (UITableView tableView)
        {
            return 1;
        }

        public override nint RowsInSection (UITableView tableView, nint section)
        {
            return FilteredColors.Count;
        }

        public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
        {
            var cell = TableView.DequeueReusableCell ("Cell") ??
                       new UITableViewCell (UITableViewCellStyle.Subtitle, "Cell");

            var color = FilteredColors [indexPath.Row];

            cell.TextLabel.Text = color.Name;

            cell.DetailTextLabel.Text = color.Hex;

            cell.ImageView.Image = color.Image;

            return cell;
        }

        public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
        {
            return 70.0f;
        }


        partial void OnToggleData (UIBarButtonItem sender)
        {
            showData = sender == loadColors;
            TableView.ReloadData ();
        }


        private class ColorsEmptyDataSetSource : EmptyDataSetSource
        {
            public override NSAttributedString GetTitle (UIScrollView scrollView)
            {
                var text = "No Colors Loaded";

                var attributes = new UIStringAttributes {
                    Font = UIFont.BoldSystemFontOfSize (17.0f),
                    ForegroundColor = UIColor.FromRGB (170, 171, 179),
                    ParagraphStyle = new NSMutableParagraphStyle {
                        LineBreakMode = UILineBreakMode.WordWrap,
                        Alignment = UITextAlignment.Center        
                    }
                };
                    
                return new NSAttributedString (text, attributes);
            }

            public override NSAttributedString GetDescription (UIScrollView scrollView)
            {
                var text = "To show a list of random colors, tap on the refresh icon in the right top corner.\n\nTo clean the list, tap on the trash icon.";

                var attributes = new UIStringAttributes {
                    Font = UIFont.BoldSystemFontOfSize (15.0f),
                    ForegroundColor = UIColor.FromRGB (170, 171, 179),
                    ParagraphStyle = new NSMutableParagraphStyle {
                        LineBreakMode = UILineBreakMode.WordWrap,
                        Alignment = UITextAlignment.Center        
                    }
                };

                return new NSAttributedString (text, attributes);
            }

            public override UIImage GetImage (UIScrollView scrollView)
            {
                return UIImage.FromBundle ("empty_placeholder.png");
            }

            public override UIColor GetBackgroundColor (UIScrollView scrollView)
            {
                return UIColor.White;
            }
        }

        private class ColorsEmptyDataSetDelegate : EmptyDataSetDelegate
        {
            public override bool EmptyDataSetShouldAllowTouch (UIScrollView scrollView)
            {
                return true;
            }

            public override bool EmptyDataSetShouldAllowScroll (UIScrollView scrollView)
            {
                return false;
            }

            public override void EmptyDataSetDidTapView (UIScrollView scrollView, UIView view)
            {
                Console.WriteLine ("Did tap view: " + view);
            }
        }

        private class SearchEmptyDataSetSource : EmptyDataSetSource
        {
            public override NSAttributedString GetTitle (UIScrollView scrollView)
            {
                return new NSAttributedString ("No Color Found");
            }

            public override NSAttributedString GetDescription (UIScrollView scrollView)
            {
                var text = "Make sure that all words are spelled correctly.";
                return new NSMutableAttributedString (text, UIFont.BoldSystemFontOfSize (17));
            }

            public override NSAttributedString GetButtonTitle (UIScrollView scrollView, UIControlState state)
            {
                var text = "Add Another Color";
                var font = UIFont.SystemFontOfSize (16);
                var textColor = state == UIControlState.Normal ? UIColor.FromRGB (0, 122, 255) : UIColor.FromRGB (198, 222, 249);

                return new NSAttributedString (text, font, textColor);
            }

            public override UIColor GetBackgroundColor (UIScrollView scrollView)
            {
                return UIColor.White;
            }

            public override nfloat GetVerticalOffset (UIScrollView scrollView)
            {
                return -64.0f;
            }
        }

        private class SearchEmptyDataSetDelegate : EmptyDataSetDelegate
        {
            public override bool EmptyDataSetShouldAllowTouch (UIScrollView scrollView)
            {
                return true;
            }

            public override bool EmptyDataSetShouldAllowScroll (UIScrollView scrollView)
            {
                return true;
            }

            public override void EmptyDataSetDidTapView (UIScrollView scrollView, UIView view)
            {
                new UIAlertView("Tap View", "Not going to actually add a color...", null, "OK").Show();
            }

            public override void EmptyDataSetDidTapButton (UIScrollView scrollView, UIButton button)
            {
                new UIAlertView ("Tap Button", "Not going to actually add a color...", null, "OK").Show ();
            }
        }
    }
}
