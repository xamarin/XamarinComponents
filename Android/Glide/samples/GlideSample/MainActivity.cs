using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Xml.Serialization;
using Bumptech.Glide;

namespace GlideSample
{
	[Activity(Label = "GlideSample", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : ListActivity
	{
		CatAdapter adapter;

		protected async override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			adapter = new CatAdapter { Parent = this };

			ListAdapter = adapter;

			await adapter.ReloadAsync();
		}
	}

	public class CatAdapter : BaseAdapter<Image>
	{
		public Activity Parent { get; set; }

		const string CATS_URL = "http://thecatapi.com/api/images/get?format=xml&size=small&results_per_page=100";

		static readonly HttpClient http = new HttpClient();
		Response lastResponse;

		public async Task ReloadAsync ()
		{
			var serializer = new XmlSerializer(typeof(Response));
			using (var rs = await http.GetStreamAsync(CATS_URL))
				lastResponse = (Response)serializer.Deserialize(rs);

			NotifyDataSetChanged();
		}


		public override Image this[int position] => lastResponse?.Data?.Images?.Image?[position];

		public override int Count => lastResponse?.Data?.Images?.Image?.Count ?? 0;

		public override long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var item = lastResponse?.Data?.Images?.Image?[position];

			if (item == null)
				return convertView;
			
			var view = convertView ?? LayoutInflater.FromContext(Parent).Inflate(Resource.Layout.ListItem, parent, false);

			var imageView = view.FindViewById<ImageView>(Resource.Id.imageView);

			Glide.With(Parent).Load(item.Url).Into(imageView);

			return view;
		}
	}
}

