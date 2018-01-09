using System;
using UIKit;
using System.Collections.Generic;

namespace REFrostedViewController
{
	/// <summary>
	/// Menu Item definition
	/// </summary>
	public abstract class REMenuItem
	{
		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public String Title
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the icon.
		/// </summary>
		/// <value>The icon.</value>
		public UIImage Icon
		{
			get;
			set;
		}

	}

	/// <summary>
	/// REMenuItem with assigned view controller
	/// </summary>
	public class REMenuViewControllerItem : REMenuItem
	{
		protected UIViewController mViewController;

		/// <summary>
		/// Gets a value indicating whether this instance has view controller.
		/// </summary>
		/// <value><c>true</c> if this instance has view controller; otherwise, <c>false</c>.</value>
		internal virtual bool HasViewController
		{
			get
			{
				return (mViewController != null);
			}
		}

		/// <summary>
		/// Gets or sets the value of the menu item
		/// </summary>
		/// <value>The value.</value>
		public virtual UIViewController ViewController 
		{
			get {return mViewController;} 
			set
			{
				mViewController = value;
			}
		}
	}

	/// <summary>
	/// REMenuItem with assigned view controller
	/// </summary>
	public class REMenuViewControllerItem<T> : REMenuViewControllerItem 
		where T : UIViewController, new()
	{
		/// <summary>
		/// Always create a new view controller
		/// </summary>
		/// <value><c>true</c> if always new; otherwise, <c>false</c>.</value>
		public bool AlwaysNew
		{
			get;
			set;
		}

		/// <summary>
		/// Gets a value indicating whether this instance has view controller.
		/// </summary>
		/// <value>true</value>
		/// <c>false</c>
		internal override bool HasViewController
		{
			get
			{
				if (AlwaysNew)
					return true;

				return base.HasViewController;
			}
		}

		/// <summary>
		/// Gets or sets the value of the menu item
		/// </summary>
		/// <value>The value.</value>
		public override UIViewController ViewController 
		{
			get
			{
				if (AlwaysNew)
					return new T();

				return mViewController;
			}
			set
			{
				if (!AlwaysNew)
				{
					mViewController =  value;
				}
			}
		}
	}

	/// <summary>
	/// REMenuItem with assigned view controller
	/// </summary>
	public class REMenuActionItem : REMenuItem
	{
		/// <summary>
		/// Gets or sets the value of the menu item
		/// </summary>
		/// <value>The value.</value>
		public Action Command {get; set;}
	}

	/// <summary>
	/// Section containing REMenuItem's and a title
	/// </summary>
	public class REMenuItemSection
	{
		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public String Title {get; set;}

		/// <summary>
		/// Gets or sets the items.
		/// </summary>
		/// <value>The items.</value>
		public List<REMenuItem> Items {get; set;}

		/// <summary>
		/// Initializes a new instance of the <see cref="REFrostedViewController.REMenuItemSection"/> class.
		/// </summary>
		public REMenuItemSection()
		{
			Items = new List<REMenuItem>();
		}
	}
}

