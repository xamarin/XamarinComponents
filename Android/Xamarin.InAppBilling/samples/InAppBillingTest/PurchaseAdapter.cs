using System;
using Xamarin.InAppBilling;
using Android.Widget;
using Android.Content;
using System.Collections.Generic;
using Android.App;

namespace InAppBilling
{
	/// <summary>
	/// Handles the data for the list of purchased items
	/// </summary>
	public class PurchaseAdapter : BaseAdapter<Purchase>
	{
		#region Private Variables
		private Activity _context;
		private IList<Purchase> _purchases;
		#endregion

		#region Computed Properties
		/// <summary>
		/// Gets the <see cref="Xamarin.InAppBilling.Product"/> with the specified position.
		/// </summary>
		/// <param name="position">Position.</param>
		public override Purchase this [int position] {
			get {
				return _purchases [position];
			}
		}

		/// <summary>
		/// Gets the list of items attached to this <see cref="InAppService.PurchaseAdapter"/> 
		/// </summary>
		/// <value>The items.</value>
		public IList<Purchase> Items {
			get {
				return _purchases;
			}
		}

		/// <summary>
		/// Gets the count purchased items.
		/// </summary>
		/// <value>The count.</value>
		public override int Count {
			get {
				return _purchases.Count;
			}
		}
		#endregion 

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="InAppService.PurchaseAdapter"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="purchases">Purchases.</param>
		public PurchaseAdapter (Activity context, IList<Purchase> purchases) : base()
		{
			_context = context;
			_purchases = purchases;
		}
		#endregion 

		#region Override Methods
		/// <summary>
		/// Gets the ID of the item at the given possition.
		/// </summary>
		/// <param name="position">Position.</param>
		public override long GetItemId (int position)
		{
			return position;
		}

		/// <Docs>The position of the item within the adapter's data set of the item whose view
		///  we want.</Docs>
		/// <summary>
		/// Gets the view.
		/// </summary>
		/// <returns>The view.</returns>
		/// <param name="position">Position.</param>
		/// <param name="convertView">Convert view.</param>
		/// <param name="parent">Parent.</param>
		public override Android.Views.View GetView (int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
		{
			var view = convertView;

			if (view == null) {
				view = _context.LayoutInflater.Inflate (Android.Resource.Layout.SimpleListItem1, null);
			}

			view.FindViewById<TextView> (Android.Resource.Id.Text1).Text = _purchases [position].ProductId;
			return view;
		}

		#endregion

	}
}

