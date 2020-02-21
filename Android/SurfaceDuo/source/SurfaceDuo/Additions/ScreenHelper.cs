using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Microsoft.Device.Display
{
	public class ScreenHelper
	{
		public DisplayMask DisplayMask { get; private set; }

		public Activity Activity { get; private set; }

		public static bool IsDualScreenDevice(Context context)
			=> context.PackageManager.HasSystemFeature("com.microsoft.device.display.displaymask");

		public bool Initialize(Activity activity)
		{
			if (!IsDualScreenDevice(activity))
				return false;

			try
			{
				Activity = activity;
				DisplayMask = DisplayMask.FromResourcesRectApproximation(Activity);
				if (DisplayMask == null)
					return false;
			}
			catch (Java.Lang.NoSuchMethodError ex)
			{
				ex.PrintStackTrace();
				return false;
			}
			catch (Java.Lang.RuntimeException ex)
			{
				ex.PrintStackTrace();
				return false;
			}
			catch (Java.Lang.NoClassDefFoundError ex) {
				ex.PrintStackTrace();
				return false;
			}

			return true;
		}

		public SurfaceOrientation GetRotation()
			=> GetRotation(Activity);

		Rect GetHinge(SurfaceOrientation rotation)
		{
			// Hinge's coordinates of its 4 edges in different mode
			// Double Landscape Rect(0, 1350 - 1800, 1434)
			// Double Portrait  Rect(1350, 0 - 1434, 1800)
			var boundings = DisplayMask.GetBoundingRectsForRotation(rotation);

			if (boundings.Count <= 0)
				return new Rect();

			return boundings[0];
		}

		Rect GetWindowRect()
		{
			var windowRect = new Rect();
			Activity.WindowManager.DefaultDisplay.GetRectSize(windowRect);;
			return windowRect;
		}

		void GetScreenRects(Rect windowRect, Rect hinge, Rect screenRect1, Rect screenRect2)
		{
			// Hinge's coordinates of its 4 edges in different mode
			// Double Landscape Rect(0, 1350 - 1800, 1434)
			// Double Portrait  Rect(1350, 0 - 1434, 1800)
			if (hinge.Left > 0)
			{
				screenRect1.Left = 0;
				screenRect1.Right = hinge.Left;
				screenRect1.Top = 0;
				screenRect1.Bottom = windowRect.Bottom;
				screenRect2.Left = hinge.Right;
				screenRect2.Right = windowRect.Right;
				screenRect2.Top = 0;
				screenRect2.Bottom = windowRect.Bottom;
			}
			else
			{
				screenRect1.Left = 0;
				screenRect1.Right = windowRect.Right;
				screenRect1.Top = 0;
				screenRect1.Bottom = hinge.Top;
				screenRect2.Left = 0;
				screenRect2.Right = windowRect.Right;
				screenRect2.Top = hinge.Bottom;
				screenRect2.Bottom = windowRect.Bottom;
			}
		}

		void GetScreenRects(Rect screenRect1, Rect screenRect2, SurfaceOrientation rotation)
		{
			Rect hinge = GetHinge(rotation);
			Rect windowRect = GetWindowRect();
			GetScreenRects(windowRect, hinge, screenRect1, screenRect2);
		}

		public bool IsDualMode
		{
			get
			{
				var rotation = GetRotation();
				Rect hinge = GetHinge(rotation);
				Rect windowRect = GetWindowRect();

				if (windowRect.Width() > 0 && windowRect.Height() > 0)
				{
					// The windowRect doesn't intersect hinge
					return hinge.Intersect(windowRect);
				}

				return false;
			}
		}

		public Rect GetHingeBounds()
			=> GetHinge(GetRotation());

		public Rect GetHingeBoundsDip()
			=> RectPixelsToDip(GetHingeBounds());

		public static SurfaceOrientation GetRotation(Activity activity)
		{
			var wm = activity.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();
			var rotation = SurfaceOrientation.Rotation0;
			if (wm != null)
				rotation = wm.DefaultDisplay.Rotation;
			return rotation;
		}

		double PixelsToDip(double px)
			=> px / Activity?.Resources?.DisplayMetrics?.Density ?? 1;

		Rect RectPixelsToDip(Rect rect)
			=> new Rect((int)PixelsToDip(rect.Left), (int)PixelsToDip(rect.Top), (int)PixelsToDip(rect.Width()), (int)PixelsToDip(rect.Height()));
	}
}