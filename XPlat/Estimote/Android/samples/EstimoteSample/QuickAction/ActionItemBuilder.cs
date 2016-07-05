using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;

namespace Estimotes.Droid
{

    public class ActionItemBuilder 
    {
        QuickAction _parent;
        Context _context;
        int _itemId;
        string _itemTitle;
        Drawable _icon;

        internal ActionItemBuilder(Context context, QuickAction parent)
        {
            _context = context;
            _parent = parent;
        }

        public ActionItem AddToParent()
        {
            var actionItem = Build();
            _parent.AddActionItem(actionItem);
            return actionItem;
        }

        public ActionItem Build() 
        {
            var actionItem = new ActionItem(_itemId, _itemTitle, _icon);
            _itemId = -1;
            _itemTitle = string.Empty;
            _icon = null;
            return actionItem;
        }

        public ActionItemBuilder SetItemId(int itemId)
        {
            _itemId = itemId;
            return this;
        }

        public ActionItemBuilder SetTitle(string title)
        {
            _itemTitle = title;
            return this;
        }
        public ActionItemBuilder SetIcon(int drawableResourceId) 
        {
            _icon = _context.Resources.GetDrawable(drawableResourceId);
            return this;
        }
        public ActionItemBuilder SetIcon(Drawable icon)
        {
            _icon = icon;
            return this;
        }
    }
    
}
