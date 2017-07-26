using System;
using Android.Content;
using Android.Support.V4.App;

using FloatingSearchViews;

namespace FloatingSearchViewSample
{
	public abstract class BaseExampleFragment : Fragment
	{
		private IBaseExampleFragmentCallbacks callbacks;

		public override void OnAttach(Context context)
		{
			base.OnAttach(context);

			if (context is IBaseExampleFragmentCallbacks)
			{
				callbacks = (IBaseExampleFragmentCallbacks)context;
			}
			else
			{
				throw new Exception(context.GetType().FullName + " must implement IBaseExampleFragmentCallbacks.");
			}
		}

		public override void OnDetach()
		{
			base.OnDetach();

			callbacks = null;
		}

		protected void AttachSearchViewActivityDrawer(FloatingSearchView searchView)
		{
			if (callbacks != null)
			{
				callbacks.OnAttachSearchViewToDrawer(searchView);
			}
		}

		public abstract bool OnActivityBackPress();

		public interface IBaseExampleFragmentCallbacks
		{
			void OnAttachSearchViewToDrawer(FloatingSearchView searchView);
		}
	}
}
