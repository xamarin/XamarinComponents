using System;
using System.Collections.Generic;

using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using JavaObject = Java.Lang.Object;

namespace Estimotes.Droid
{
    public delegate void OnActionItemClickEventHandler(object sender, ActionItemClickEventArgs e);

    public delegate void OnActionItemDismissedEventHandler(object sender, EventArgs e);

    public class QuickAction : JavaObject, PopupWindow.IOnDismissListener
    {
        readonly List<ActionItem> _actionItems = new List<ActionItem>();
        readonly Context _context;
        readonly LayoutInflater _inflater;
        readonly QuickActionLayout _orientation;
        readonly PopupWindow _window;
        readonly IWindowManager _windowManager;
        ImageView _arrowDown;
        ImageView _arrowUp;
        int _childPos;
        bool _didAction;
        int _insertPos;
        View _rootView;
        int _rootWidth;
        ScrollView _scroller;
        ViewGroup _track;

        public QuickAction(Context context) : this(context, QuickActionLayout.Vertical)
        {
        }

        public QuickAction(Context context, QuickActionLayout orientation)
        {
            _context = context;
            _window = new PopupWindow(context);
            _childPos = 0;

            _window.TouchIntercepted += HandleTouchIntercepted;
            _windowManager = context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();

            _orientation = orientation;
            _inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);

            SetRootViewId(orientation == QuickActionLayout.Horizontal ? Resource.Layout.popup_horizontal : Resource.Layout.popup_vertical);
        }

        public Drawable Background { get; set; }



        void PopupWindow.IOnDismissListener.OnDismiss()
        {
        }

        public event OnActionItemDismissedEventHandler ActionItemDismissed;
        public event OnActionItemClickEventHandler ActionItemClicked;

        public void SetContentView(View root)
        {
            _rootView = root;
            _window.ContentView = root;
        }

        public void SetContentView(int layoutResourceId)
        {
            var inflater = (LayoutInflater)_context.GetSystemService(Context.LayoutInflaterService);
            SetContentView(inflater.Inflate(layoutResourceId, null));
        }

        public ActionItem GetActionItem(int index)
        {
            return _actionItems[index];
        }

        public void SetRootViewId(int id)
        {
            _rootView = _inflater.Inflate(id, null);
            _track = _rootView.FindViewById<ViewGroup>(Resource.Id.tracks);
            _arrowUp = _rootView.FindViewById<ImageView>(Resource.Id.arrow_up);
            _arrowDown = _rootView.FindViewById<ImageView>(Resource.Id.arrow_down);
            _scroller = _rootView.FindViewById<ScrollView>(Resource.Id.scroller);

            _rootView.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);

            SetContentView(_rootView);
        }

        public ActionItemBuilder GetBuilder()
        {
            return new ActionItemBuilder(_context, this);
        }

        public void AddActionItem(ActionItem item)
        {
            _actionItems.Add(item);

            var container = CreateActionItemContainer();
            DisplayIcon(container, item);
            DisplayTitle(container, item);

            if (_orientation == QuickActionLayout.Horizontal && _childPos != 0)
            {
                var separator = _inflater.Inflate(Resource.Layout.horiz_separator, null);
                var parms = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
                separator.LayoutParameters = parms;
                separator.SetPadding(5, 0, 5, 0);

                _track.AddView(separator, _insertPos);

                _insertPos++;
            }

            _track.AddView(container, _insertPos);

            _childPos++;
            _insertPos++;
        }

        View CreateActionItemContainer()
        {
            View container;
            if (_orientation == QuickActionLayout.Horizontal)
            {
                container = _inflater.Inflate(Resource.Layout.action_item_horizontal, null);
            }
            else
            {
                container = _inflater.Inflate(Resource.Layout.action_item_vertical, null);
            }
            container.Focusable = true;
            container.Clickable = true;
            var pos = _childPos;
            container.Click += (sender, e) =>{
                if (ActionItemClicked != null)
                {
                    var arg = new ActionItemClickEventArgs(this, pos);
                    ActionItemClicked(container, arg);
                }

                if (GetActionItem(pos).IsSticky)
                {
                    return;
                }
                _didAction = true;
                Dismiss();
            };


            return container;
        }

        void DisplayIcon(View container, ActionItem item)
        {
            var img = container.FindViewById<ImageView>(Resource.Id.iv_icon);
            if (item.Icon == null)
            {
                img.Visibility = ViewStates.Gone;
            }
            else
            {
                img.SetImageDrawable(item.Icon);
            }
        }
        void DisplayTitle(View container, ActionItem item)
        {
            var text = container.FindViewById<TextView>(Resource.Id.tv_title);
            if (string.IsNullOrWhiteSpace(item.Title))
            {
                text.Visibility = ViewStates.Gone;
            }
            else
            {
                text.Text = item.Title;
            }
        }

        Rect GetAnchorRectangle(View view)
        {
            var location = new int[2];
            view.GetLocationOnScreen(location);
            var anchorRect = new Rect(location[0], location[1], location[0] + view.Width, location[1] + view.Height);
            return anchorRect;
        }

        public void Show(View anchor, QuickActionAnimationStyle animationStyle = QuickActionAnimationStyle.Auto)
        {
            PreShow();
            var windowLocation = new Point();
            int arrowPos;
            _didAction = false;

            var anchorRect = GetAnchorRectangle(anchor);
            _rootView.Measure(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);

            var rootHeight = _rootView.MeasuredHeight;
            if (_rootWidth == 0)
            {
                _rootWidth = _rootView.MeasuredWidth;
            }

            var screenWidth = _windowManager.DefaultDisplay.Width;
            var screenHeight = _windowManager.DefaultDisplay.Height;

            // Get x coord of top left popup
            if ((anchorRect.Left + _rootWidth) > screenWidth)
            {
                windowLocation.X = anchorRect.Left - (_rootWidth - anchor.Width);
                windowLocation.X = (windowLocation.X < 0) ? 0 : windowLocation.X;
                arrowPos = anchorRect.CenterX() - windowLocation.X;
            }
            else
            {
                if (anchor.Width > _rootWidth)
                {
                    windowLocation.X = anchorRect.CenterX() - (_rootWidth / 2);
                }
                else
                {
                    windowLocation.X = anchorRect.Left;
                }
                arrowPos = anchorRect.CenterX() - windowLocation.X;
            }

            var dyTop = anchorRect.Top;
            var dyBottom = screenHeight - anchorRect.Bottom;

            var onTop = (dyTop > dyBottom);

            if (onTop)
            {
                if (rootHeight > dyTop)
                {
                    windowLocation.Y = 15;
                }
                else
                {
                    windowLocation.Y = anchorRect.Top - rootHeight;
                }
            }
            else
            {
                windowLocation.Y = anchorRect.Bottom;
            }

            ShowArrow(onTop ? Resource.Id.arrow_down : Resource.Id.arrow_up, arrowPos);
            SetAnimationStyle(animationStyle, screenWidth, anchorRect.CenterX(), onTop);

            _window.ShowAtLocation(anchor, GravityFlags.NoGravity, windowLocation.X, windowLocation.Y);
        }

        void SetAnimationStyle(QuickActionAnimationStyle animationStyle, int screenWidth, int requestedX, bool onTop)
        {
            var arrowPos = requestedX - _arrowUp.MeasuredWidth / 2;

            switch (animationStyle)
            {
                case QuickActionAnimationStyle.GrowLeft:
                    _window.AnimationStyle = onTop ? Resource.Style.Animations_PopUpMenu_Left : Resource.Style.Animations_PopDownMenu_Left;
                    break;

                case QuickActionAnimationStyle.GrowRight:
                    _window.AnimationStyle = onTop ? Resource.Style.Animations_PopUpMenu_Right : Resource.Style.Animations_PopDownMenu_Right;
                    break;

                case QuickActionAnimationStyle.GrowFromCenter:
                    _window.AnimationStyle = onTop ? Resource.Style.Animations_PopUpMenu_Center : Resource.Style.Animations_PopDownMenu_Center;
                    break;

                case QuickActionAnimationStyle.Reflect:
                    _window.AnimationStyle = onTop ? Resource.Style.Animations_PopUpMenu_Reflect : Resource.Style.Animations_PopDownMenu_Reflect;
                    break;

                case QuickActionAnimationStyle.Auto:
                    if (arrowPos <= screenWidth / 4)
                    {
                        _window.AnimationStyle = onTop ? Resource.Style.Animations_PopUpMenu_Left : Resource.Style.Animations_PopDownMenu_Left;
                    }
                    else if (arrowPos > screenWidth / 4 && arrowPos < 3 * (screenWidth / 4))
                    {
                        _window.AnimationStyle = onTop ? Resource.Style.Animations_PopUpMenu_Center : Resource.Style.Animations_PopDownMenu_Center;
                    }
                    else
                    {
                        _window.AnimationStyle = onTop ? Resource.Style.Animations_PopUpMenu_Right : Resource.Style.Animations_PopDownMenu_Right;
                    }
                    break;
            }
        }

        /// <summary>
        ///   Show the arrow.
        /// </summary>
        /// <param name="whichArrow">arrow type resource id</param>
        /// <param name="requestedX">distance from left screen</param>
        void ShowArrow(int whichArrow, int requestedX)
        {
            var showArrow = (whichArrow == Resource.Id.arrow_up) ? _arrowUp : _arrowDown;
            var hideArrow = (whichArrow == Resource.Id.arrow_up) ? _arrowDown : _arrowUp;

            var arrowWidth = _arrowUp.MeasuredWidth;
            showArrow.Visibility = ViewStates.Visible;

            var param = (ViewGroup.MarginLayoutParams)showArrow.LayoutParameters;
            param.LeftMargin = requestedX - arrowWidth / 2;
            hideArrow.Visibility = ViewStates.Invisible;
        }

        void OnDismiss()
        {
            if (!_didAction)
            {
                return;
            }

            if (ActionItemDismissed != null)
            {
                ActionItemDismissed(this, new EventArgs());
            }
        }

        void HandleTouchIntercepted(object sender, View.TouchEventArgs e)
        {
            if (e.Event.Action == MotionEventActions.Outside)
            {
                _window.Dismiss();
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        public virtual void Dismiss()
        {
            _window.Dismiss();
        }

        void PreShow()
        {
            if (_rootView == null)
            {
                throw new NullReferenceException("SetContentView was not called with a view to display.");
            }

            _window.SetBackgroundDrawable(Background ?? new BitmapDrawable());

            // ReSharper disable AccessToStaticMemberViaDerivedType
            _window.Width = WindowManagerLayoutParams.WrapContent;
            _window.Height = WindowManagerLayoutParams.WrapContent;
            // ReSharper restore AccessToStaticMemberViaDerivedType
            _window.Touchable = true;
            _window.Focusable = true;
            _window.OutsideTouchable = true;
            _window.ContentView = _rootView;
        }
    }
}
