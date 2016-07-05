using Android.Graphics;
using Android.Graphics.Drawables;

namespace Estimotes.Droid
{
    /// <summary>
    ///   Action item, displayed as menu with icon and text.
    /// </summary>
    public class ActionItem
    {
        public ActionItem() : this(-1, null, null)
        {
        }

        public ActionItem(int actionId, string title) : this(actionId, title, null)
        {
        }

        public ActionItem(Drawable icon) : this(-1, null, icon)
        {
        }

        public ActionItem(int actionId, string title, Drawable icon)
        {
            Title = title;
            Icon = icon;
            ActionId = actionId;
        }

        public int ActionId { get; set; }

        public Bitmap Thumb { get; set; }

        public string Title { get; set; }

        public Drawable Icon { get; set; }

        public bool IsSticky { get; set; }

        public bool Selected { get; set; }
    }
}
