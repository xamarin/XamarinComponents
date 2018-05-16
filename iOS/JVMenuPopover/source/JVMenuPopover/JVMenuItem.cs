using System;
using UIKit;

namespace JVMenuPopover
{
	/// <summary>
	/// JVMenu Item
	/// </summary>
	public abstract class JVMenuItem
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
	/// JVMenuItem with assigned view controller
	/// </summary>
	public class JVMenuViewControllerItem : JVMenuItem
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
	/// JVMenuItem with assigned view controller
	/// </summary>
	public class JVMenuViewControllerItem<T> : JVMenuViewControllerItem 
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
	/// JVMenuItem with assigned view controller
	/// </summary>
	public class JVMenuActionItem : JVMenuItem
	{
		/// <summary>
		/// Gets or sets the value of the menu item
		/// </summary>
		/// <value>The value.</value>
		public Action Command {get; set;}
	}

}

