using System;
using System.Collections.Generic;
using UIKit;

namespace JVMenuPopover
{
	/// <summary>
	/// Central shared class
	/// </summary>
	public class JVMenuPopoverConfig
	{
		#region Static Members
		private static JVMenuPopoverConfig mSharedInstance;

		private List<JVMenuItem> mMenuItems;

		#endregion

		#region Fields

		private UIColor mTintColor;
		private String mFontName = "HelveticaNeue-Light";
		private float mFontSize = 18.0f;
		private float mRowHeight = 70.0f;
		private UIImage mMenuImage;
		private UIImage mCancelImage;
		#endregion

		#region Properties

		/// <summary>
		/// Gets the shared instance.
		/// </summary>
		/// <value>The shared instance.</value>
		public static JVMenuPopoverConfig SharedInstance
		{
			get
			{
				if (mSharedInstance == null)
				{
					mSharedInstance = new JVMenuPopoverConfig();
				}

				return mSharedInstance;
			}
		}

		/// <summary>
		/// Gets or sets the menu items.
		/// </summary>
		/// <value>The menu items.</value>
		public List<JVMenuItem> MenuItems
		{
			get
			{
				if (mMenuItems == null)
				{
					mMenuItems = new List<JVMenuItem>();
				}

				return mMenuItems;
			}
			set
			{
				mMenuItems = value;
			}

		}

		/// <summary>
		/// Gets or sets the color of the tint.
		/// </summary>
		/// <value>The color of the tint.</value>
		public UIColor TintColor
		{
			get
			{
				if (mTintColor == null)
					mTintColor = UIColor.White;

				return mTintColor;
			}
			set {mTintColor = value;}
		}

		/// <summary>
		/// Gets or sets the name of the font.
		/// </summary>
		/// <value>The name of the font.</value>
		public String FontName
		{
			get {return mFontName;}
			set {mFontName = value;}
		}

		/// <summary>
		/// Gets or sets the size of the font.
		/// </summary>
		/// <value>The size of the font.</value>
		public float FontSize
		{
			get {return mFontSize;}
			set {mFontSize = value;}
		}

		/// <summary>
		/// Gets or sets the height of the row.
		/// </summary>
		/// <value>The height of the row.</value>
		public float RowHeight
		{
			get {return mRowHeight;}
			set {mRowHeight = value;}
		}

		/// <summary>
		/// Disable Image tinting on the Menu image
		/// </summary>
		/// <value><c>true</c> if disable image tinting; otherwise, <c>false</c>.</value>
		public bool DisableMenuImageTinting {
			get;
			set;
		}

		/// <summary>
		/// Disables Image tinting on the Cancel image
		/// </summary>
		/// <value><c>true</c> if disable cancel image tinting; otherwise, <c>false</c>.</value>
		public bool DisableCancelImageTinting {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the menu image.
		/// </summary>
		/// <value>The menu image.</value>
		public UIImage MenuImage
		{
			get
			{
				if (mMenuImage == null)
					mMenuImage = UIImage.FromBundle("menu-black-48");

				if (DisableMenuImageTinting)
					return mMenuImage.ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal);
				
				return mMenuImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
			}
			set {mMenuImage = value;}
		}

		/// <summary>
		/// Gets or sets a the cancel image
		/// </summary>
		/// <value><c>true</c> if this instance cancel image; otherwise, <c>false</c>.</value>
		public UIImage CancelImage
		{
			get
			{
				if (mCancelImage == null)
					mCancelImage = UIImage.FromBundle(@"cancel-filled-50");

				if (DisableCancelImageTinting)
					return mCancelImage.ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal);
				

				return mCancelImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
			}
			set {mCancelImage = value;}
		}
		#endregion

	}
}

