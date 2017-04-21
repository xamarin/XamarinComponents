using Xamarin.Forms;

namespace DataConverterSample
{
	public class App : Application
	{
		public App ()
		{
			MainPage = new NavigationPage (new DataConverterPage ());
		}
	}
}
