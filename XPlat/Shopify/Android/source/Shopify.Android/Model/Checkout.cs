using System;
using Square.Retrofit;
using Square.Retrofit.Client;
using Android.Runtime;

using Shopify.Buy.Model;
using System.Collections.Generic;

namespace Shopify.Buy.Model
{
	partial class Checkout
	{
		public long ReservationTime
		{
			get { return GetReservationTime().LongValue(); }
			set { SetReservationTime(value); }
		}

		public long ReservationTimeLeft
		{
			get { return GetReservationTimeLeft().LongValue(); }
		}

		public long OrderId
		{
			get { return GetOrderId().LongValue(); }
		}

		public bool RequiresShipping
		{
			get { return IsRequiresShipping().BooleanValue(); }
		}

		public bool TaxesIncluded
		{
			get { return IsTaxesIncluded().BooleanValue(); }
		}
	}
}
