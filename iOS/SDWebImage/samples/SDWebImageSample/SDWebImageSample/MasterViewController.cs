
using System;
using System.Drawing;
using System.Collections.Generic;

#if __UNIFIED__
using Foundation;
using UIKit;
using MapKit;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.MapKit;
using nint = System.Int32;
#endif

using SDWebImage;

namespace SDWebImageSample
{
	public partial class MasterViewController : UITableViewController
	{
		public DetailViewController DetailViewController { get; set; }

		public List<string> objects;

		public MasterViewController () : base ("MasterViewController", null)
		{
			InitListOfImages ();
			Title = "SDWebImage";
			NavigationItem.RightBarButtonItem = new UIBarButtonItem ("Clear Cache", UIBarButtonItemStyle.Plain, ClearCache);
			SDWebImageManager.SharedManager.ImageDownloader.SetHttpHeaderValue ("SDWebImage Demo", "AppName");
			SDWebImageManager.SharedManager.ImageDownloader.ExecutionOrder = SDWebImageDownloaderExecutionOrder.LastInFirstOut;

			TableView.Source = new MyTableViewSource (this);
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		void ClearCache (object sender, EventArgs e)
		{
			SDWebImageManager.SharedManager.ImageCache.ClearMemory ();
			SDWebImageManager.SharedManager.ImageCache.ClearDisk ();
		}

		public class MyTableViewSource : UITableViewSource
		{
			MasterViewController ctrl;

			public MyTableViewSource (MasterViewController ctrl)
			{
				this.ctrl = ctrl;
			}

			public override nint NumberOfSections (UITableView tableView)
			{
				return 1;
			}

			public override nint RowsInSection (UITableView tableview, nint section)
			{
				return ctrl.objects.Count;
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				string CellIdentifier = @"Cell";
				
				UITableViewCell cell = tableView.DequeueReusableCell (CellIdentifier);
				if (cell == null)
				{
					cell = new UITableViewCell (UITableViewCellStyle.Default, CellIdentifier);
				}

				cell.TextLabel.Text = string.Format ("Image #{0}", indexPath.Row);
				cell.ImageView.SetImage (new NSUrl (ctrl.objects [indexPath.Row]), UIImage.FromBundle ("placeholder"));

				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				ctrl.DetailViewController = null;
				ctrl.DetailViewController = new DetailViewController ();
	
				string largeImageURL = ctrl.objects [indexPath.Row].Replace ("small", "source");
				ctrl.DetailViewController.ImageUrl = new NSUrl (largeImageURL);

				ctrl.NavigationController.PushViewController (ctrl.DetailViewController, true);
			}
		}

		void InitListOfImages ()
		{
			objects = new List<string> ()
			{
				// Light images
				"http://simpozia.com/pages/images/stories/windows-icon.png",
				"http://radiotray.sourceforge.net/radio.png",
				"http://www.bandwidthblog.com/wp-content/uploads/2011/11/twitter-logo.png",
				"http://weloveicons.s3.amazonaws.com/icons/100907_itunes1.png",
				"http://weloveicons.s3.amazonaws.com/icons/100929_applications.png",
				"http://www.idyllicmusic.com/index_files/get_apple-iphone.png",
				"http://www.frenchrevolutionfood.com/wp-content/uploads/2009/04/Twitter-Bird.png",
				"http://3.bp.blogspot.com/-ka5MiRGJ_S4/TdD9OoF6bmI/AAAAAAAAE8k/7ydKtptUtSg/s1600/Google_Sky%2BMaps_Android.png",
				"http://www.desiredsoft.com/images/icon_webhosting.png",
				"http://goodereader.com/apps/wp-content/uploads/downloads/thumbnails/2012/01/hi-256-0-99dda8c730196ab93c67f0659d5b8489abdeb977.png",
				"http://1.bp.blogspot.com/-mlaJ4p_3rBU/TdD9OWxN8II/AAAAAAAAE8U/xyynWwr3_4Q/s1600/antivitus_free.png",
				"http://cdn3.iconfinder.com/data/icons/transformers/computer.png",
				"http://cdn.geekwire.com/wp-content/uploads/2011/04/firefox.png?7794fe",
				"https://ssl.gstatic.com/android/market/com.rovio.angrybirdsseasons/hi-256-9-347dae230614238a639d21508ae492302340b2ba",
				"http://androidblaze.com/wp-content/uploads/2011/12/tablet-pc-256x256.jpg",
				"http://www.theblaze.com/wp-content/uploads/2011/08/Apple.png",
				"http://1.bp.blogspot.com/-y-HQwQ4Kuu0/TdD9_iKIY7I/AAAAAAAAE88/3G4xiclDZD0/s1600/Twitter_Android.png",
				"http://3.bp.blogspot.com/-nAf4IMJGpc8/TdD9OGNUHHI/AAAAAAAAE8E/VM9yU_lIgZ4/s1600/Adobe%2BReader_Android.png",
				"http://cdn.geekwire.com/wp-content/uploads/2011/05/oovoo-android.png?7794fe",
				"http://icons.iconarchive.com/icons/kocco/ndroid/128/android-market-2-icon.png",
				"http://thecustomizewindows.com/wp-content/uploads/2011/11/Nicest-Android-Live-Wallpapers.png",
				"http://c.wrzuta.pl/wm16596/a32f1a47002ab3a949afeb4f",
				"http://macprovid.vo.llnwd.net/o43/hub/media/1090/6882/01_headline_Muse.jpg",

				// Heavy images
				"https://lh6.googleusercontent.com/-55osAWw3x0Q/URquUtcFr5I/AAAAAAAAAbs/rWlj1RUKrYI/s1024/A%252520Photographer.jpg",
				"https://lh4.googleusercontent.com/--dq8niRp7W4/URquVgmXvgI/AAAAAAAAAbs/-gnuLQfNnBA/s1024/A%252520Song%252520of%252520Ice%252520and%252520Fire.jpg",
				"https://lh5.googleusercontent.com/-7qZeDtRKFKc/URquWZT1gOI/AAAAAAAAAbs/hqWgteyNXsg/s1024/Another%252520Rockaway%252520Sunset.jpg",
				"https://lh3.googleusercontent.com/--L0Km39l5J8/URquXHGcdNI/AAAAAAAAAbs/3ZrSJNrSomQ/s1024/Antelope%252520Butte.jpg",
				"https://lh6.googleusercontent.com/-8HO-4vIFnlw/URquZnsFgtI/AAAAAAAAAbs/WT8jViTF7vw/s1024/Antelope%252520Hallway.jpg",
				"https://lh4.googleusercontent.com/-WIuWgVcU3Qw/URqubRVcj4I/AAAAAAAAAbs/YvbwgGjwdIQ/s1024/Antelope%252520Walls.jpg",
				"https://lh6.googleusercontent.com/-UBmLbPELvoQ/URqucCdv0kI/AAAAAAAAAbs/IdNhr2VQoQs/s1024/Apre%2525CC%252580s%252520la%252520Pluie.jpg",
				"https://lh3.googleusercontent.com/-s-AFpvgSeew/URquc6dF-JI/AAAAAAAAAbs/Mt3xNGRUd68/s1024/Backlit%252520Cloud.jpg",
				"https://lh5.googleusercontent.com/-bvmif9a9YOQ/URquea3heHI/AAAAAAAAAbs/rcr6wyeQtAo/s1024/Bee%252520and%252520Flower.jpg",
				"https://lh5.googleusercontent.com/-n7mdm7I7FGs/URqueT_BT-I/AAAAAAAAAbs/9MYmXlmpSAo/s1024/Bonzai%252520Rock%252520Sunset.jpg",
				"https://lh6.googleusercontent.com/-4CN4X4t0M1k/URqufPozWzI/AAAAAAAAAbs/8wK41lg1KPs/s1024/Caterpillar.jpg",
				"https://lh3.googleusercontent.com/-rrFnVC8xQEg/URqufdrLBaI/AAAAAAAAAbs/s69WYy_fl1E/s1024/Chess.jpg",
				"https://lh5.googleusercontent.com/-WVpRptWH8Yw/URqugh-QmDI/AAAAAAAAAbs/E-MgBgtlUWU/s1024/Chihuly.jpg",
				"https://lh5.googleusercontent.com/-0BDXkYmckbo/URquhKFW84I/AAAAAAAAAbs/ogQtHCTk2JQ/s1024/Closed%252520Door.jpg",
				"https://lh3.googleusercontent.com/-PyggXXZRykM/URquh-kVvoI/AAAAAAAAAbs/hFtDwhtrHHQ/s1024/Colorado%252520River%252520Sunset.jpg",
				"https://lh3.googleusercontent.com/-ZAs4dNZtALc/URquikvOCWI/AAAAAAAAAbs/DXz4h3dll1Y/s1024/Colors%252520of%252520Autumn.jpg",
				"https://lh4.googleusercontent.com/-GztnWEIiMz8/URqukVCU7bI/AAAAAAAAAbs/jo2Hjv6MZ6M/s1024/Countryside.jpg",
				"https://lh4.googleusercontent.com/-bEg9EZ9QoiM/URquklz3FGI/AAAAAAAAAbs/UUuv8Ac2BaE/s1024/Death%252520Valley%252520-%252520Dunes.jpg",
				"https://lh6.googleusercontent.com/-ijQJ8W68tEE/URqulGkvFEI/AAAAAAAAAbs/zPXvIwi_rFw/s1024/Delicate%252520Arch.jpg",
				"https://lh5.googleusercontent.com/-Oh8mMy2ieng/URqullDwehI/AAAAAAAAAbs/TbdeEfsaIZY/s1024/Despair.jpg",
				"https://lh5.googleusercontent.com/-gl0y4UiAOlk/URqumC_KjBI/AAAAAAAAAbs/PM1eT7dn4oo/s1024/Eagle%252520Fall%252520Sunrise.jpg",
				"https://lh3.googleusercontent.com/-hYYHd2_vXPQ/URqumtJa9eI/AAAAAAAAAbs/wAalXVkbSh0/s1024/Electric%252520Storm.jpg",
				"https://lh5.googleusercontent.com/-PyY_yiyjPTo/URqunUOhHFI/AAAAAAAAAbs/azZoULNuJXc/s1024/False%252520Kiva.jpg",
				"https://lh6.googleusercontent.com/-PYvLVdvXywk/URqunwd8hfI/AAAAAAAAAbs/qiMwgkFvf6I/s1024/Fitzgerald%252520Streaks.jpg",
				"https://lh4.googleusercontent.com/-KIR_UobIIqY/URquoCZ9SlI/AAAAAAAAAbs/Y4d4q8sXu4c/s1024/Foggy%252520Sunset.jpg",
				"https://lh6.googleusercontent.com/-9lzOk_OWZH0/URquoo4xYoI/AAAAAAAAAbs/AwgzHtNVCwU/s1024/Frantic.jpg",
				"https://lh3.googleusercontent.com/-0X3JNaKaz48/URqupH78wpI/AAAAAAAAAbs/lHXxu_zbH8s/s1024/Golden%252520Gate%252520Afternoon.jpg",
				"https://lh6.googleusercontent.com/-95sb5ag7ABc/URqupl95RDI/AAAAAAAAAbs/g73R20iVTRA/s1024/Golden%252520Gate%252520Fog.jpg",
				"https://lh3.googleusercontent.com/-JB9v6rtgHhk/URqup21F-zI/AAAAAAAAAbs/64Fb8qMZWXk/s1024/Golden%252520Grass.jpg",
				"https://lh4.googleusercontent.com/-EIBGfnuLtII/URquqVHwaRI/AAAAAAAAAbs/FA4McV2u8VE/s1024/Grand%252520Teton.jpg",
				"https://lh4.googleusercontent.com/-WoMxZvmN9nY/URquq1v2AoI/AAAAAAAAAbs/grj5uMhL6NA/s1024/Grass%252520Closeup.jpg",
				"https://lh3.googleusercontent.com/-6hZiEHXx64Q/URqurxvNdqI/AAAAAAAAAbs/kWMXM3o5OVI/s1024/Green%252520Grass.jpg",
				"https://lh5.googleusercontent.com/-6LVb9OXtQ60/URquteBFuKI/AAAAAAAAAbs/4F4kRgecwFs/s1024/Hanging%252520Leaf.jpg",
				"https://lh4.googleusercontent.com/-zAvf__52ONk/URqutT_IuxI/AAAAAAAAAbs/D_bcuc0thoU/s1024/Highway%2525201.jpg",
				"https://lh6.googleusercontent.com/-H4SrUg615rA/URquuL27fXI/AAAAAAAAAbs/4aEqJfiMsOU/s1024/Horseshoe%252520Bend%252520Sunset.jpg",
				"https://lh4.googleusercontent.com/-JhFi4fb_Pqw/URquuX-QXbI/AAAAAAAAAbs/IXpYUxuweYM/s1024/Horseshoe%252520Bend.jpg",
				"https://lh5.googleusercontent.com/-UGgssvFRJ7g/URquueyJzGI/AAAAAAAAAbs/yYIBlLT0toM/s1024/Into%252520the%252520Blue.jpg",
				"https://lh3.googleusercontent.com/-CH7KoupI7uI/URquu0FF__I/AAAAAAAAAbs/R7GDmI7v_G0/s1024/Jelly%252520Fish%2525202.jpg",
				"https://lh4.googleusercontent.com/-pwuuw6yhg8U/URquvPxR3FI/AAAAAAAAAbs/VNGk6f-tsGE/s1024/Jelly%252520Fish%2525203.jpg",
				"https://lh5.googleusercontent.com/-GoUQVw1fnFw/URquv6xbC0I/AAAAAAAAAbs/zEUVTQQ43Zc/s1024/Kauai.jpg",
				"https://lh6.googleusercontent.com/-8QdYYQEpYjw/URquwvdh88I/AAAAAAAAAbs/cktDy-ysfHo/s1024/Kyoto%252520Sunset.jpg",
				"https://lh4.googleusercontent.com/-vPeekyDjOE0/URquwzJ28qI/AAAAAAAAAbs/qxcyXULsZrg/s1024/Lake%252520Tahoe%252520Colors.jpg",
				"https://lh4.googleusercontent.com/-xBPxWpD4yxU/URquxWHk8AI/AAAAAAAAAbs/ARDPeDYPiMY/s1024/Lava%252520from%252520the%252520Sky.jpg",
				"https://lh3.googleusercontent.com/-897VXrJB6RE/URquxxxd-5I/AAAAAAAAAbs/j-Cz4T4YvIw/s1024/Leica%25252050mm%252520Summilux.jpg",
				"https://lh5.googleusercontent.com/-qSJ4D4iXzGo/URquyDWiJ1I/AAAAAAAAAbs/k2pBXeWehOA/s1024/Leica%25252050mm%252520Summilux.jpg",
				"https://lh6.googleusercontent.com/-dwlPg83vzLg/URquylTVuFI/AAAAAAAAAbs/G6SyQ8b4YsI/s1024/Leica%252520M8%252520%252528Front%252529.jpg",
				"https://lh3.googleusercontent.com/-R3_EYAyJvfk/URquzQBv8eI/AAAAAAAAAbs/b9xhpUM3pEI/s1024/Light%252520to%252520Sand.jpg",
				"https://lh3.googleusercontent.com/-fHY5h67QPi0/URqu0Cp4J1I/AAAAAAAAAbs/0lG6m94Z6vM/s1024/Little%252520Bit%252520of%252520Paradise.jpg",
				"https://lh5.googleusercontent.com/-TzF_LwrCnRM/URqu0RddPOI/AAAAAAAAAbs/gaj2dLiuX0s/s1024/Lone%252520Pine%252520Sunset.jpg",
				"https://lh3.googleusercontent.com/-4HdpJ4_DXU4/URqu046dJ9I/AAAAAAAAAbs/eBOodtk2_uk/s1024/Lonely%252520Rock.jpg",
				"https://lh6.googleusercontent.com/-erbF--z-W4s/URqu1ajSLkI/AAAAAAAAAbs/xjDCDO1INzM/s1024/Longue%252520Vue.jpg",
				"https://lh6.googleusercontent.com/-0CXJRdJaqvc/URqu1opNZNI/AAAAAAAAAbs/PFB2oPUU7Lk/s1024/Look%252520Me%252520in%252520the%252520Eye.jpg",
				"https://lh3.googleusercontent.com/-D_5lNxnDN6g/URqu2Tk7HVI/AAAAAAAAAbs/p0ddca9W__Y/s1024/Lost%252520in%252520a%252520Field.jpg",
				"https://lh6.googleusercontent.com/-flsqwMrIk2Q/URqu24PcmjI/AAAAAAAAAbs/5ocIH85XofM/s1024/Marshall%252520Beach%252520Sunset.jpg",
				"https://lh4.googleusercontent.com/-Y4lgryEVTmU/URqu28kG3gI/AAAAAAAAAbs/OjXpekqtbJ4/s1024/Mono%252520Lake%252520Blue.jpg",
				"https://lh4.googleusercontent.com/-AaHAJPmcGYA/URqu3PIldHI/AAAAAAAAAbs/lcTqk1SIcRs/s1024/Monument%252520Valley%252520Overlook.jpg",
				"https://lh4.googleusercontent.com/-vKxfdQ83dQA/URqu31Yq_BI/AAAAAAAAAbs/OUoGk_2AyfM/s1024/Moving%252520Rock.jpg",
				"https://lh5.googleusercontent.com/-CG62QiPpWXg/URqu4ia4vRI/AAAAAAAAAbs/0YOdqLAlcAc/s1024/Napali%252520Coast.jpg",
				"https://lh6.googleusercontent.com/-wdGrP5PMmJQ/URqu5PZvn7I/AAAAAAAAAbs/m0abEcdPXe4/s1024/One%252520Wheel.jpg",
				"https://lh6.googleusercontent.com/-6WS5DoCGuOA/URqu5qx1UgI/AAAAAAAAAbs/giMw2ixPvrY/s1024/Open%252520Sky.jpg",
				"https://lh6.googleusercontent.com/-u8EHKj8G8GQ/URqu55sM6yI/AAAAAAAAAbs/lIXX_GlTdmI/s1024/Orange%252520Sunset.jpg",
				"https://lh6.googleusercontent.com/-74Z5qj4bTDE/URqu6LSrJrI/AAAAAAAAAbs/XzmVkw90szQ/s1024/Orchid.jpg",
				"https://lh6.googleusercontent.com/-lEQE4h6TePE/URqu6t_lSkI/AAAAAAAAAbs/zvGYKOea_qY/s1024/Over%252520there.jpg",
				"https://lh5.googleusercontent.com/-cauH-53JH2M/URqu66v_USI/AAAAAAAAAbs/EucwwqclfKQ/s1024/Plumes.jpg",
				"https://lh3.googleusercontent.com/-eDLT2jHDoy4/URqu7axzkAI/AAAAAAAAAbs/iVZE-xJ7lZs/s1024/Rainbokeh.jpg",
				"https://lh5.googleusercontent.com/-j1NLqEFIyco/URqu8L1CGcI/AAAAAAAAAbs/aqZkgX66zlI/s1024/Rainbow.jpg",
				"https://lh5.googleusercontent.com/-DRnqmK0t4VU/URqu8XYN9yI/AAAAAAAAAbs/LgvF_592WLU/s1024/Rice%252520Fields.jpg",
				"https://lh3.googleusercontent.com/-hwh1v3EOGcQ/URqu8qOaKwI/AAAAAAAAAbs/IljRJRnbJGw/s1024/Rockaway%252520Fire%252520Sky.jpg",
				"https://lh5.googleusercontent.com/-wjV6FQk7tlk/URqu9jCQ8sI/AAAAAAAAAbs/RyYUpdo-c9o/s1024/Rockaway%252520Flow.jpg",
				"https://lh6.googleusercontent.com/-6cAXNfo7D20/URqu-BdzgPI/AAAAAAAAAbs/OmsYllzJqwo/s1024/Rockaway%252520Sunset%252520Sky.jpg",
				"https://lh3.googleusercontent.com/-sl8fpGPS-RE/URqu_BOkfgI/AAAAAAAAAbs/Dg2Fv-JxOeg/s1024/Russian%252520Ridge%252520Sunset.jpg",
				"https://lh6.googleusercontent.com/-gVtY36mMBIg/URqu_q91lkI/AAAAAAAAAbs/3CiFMBcy5MA/s1024/Rust%252520Knot.jpg",
				"https://lh6.googleusercontent.com/-GHeImuHqJBE/URqu_FKfVLI/AAAAAAAAAbs/axuEJeqam7Q/s1024/Sailing%252520Stones.jpg",
				"https://lh3.googleusercontent.com/-hBbYZjTOwGc/URqu_ycpIrI/AAAAAAAAAbs/nAdJUXnGJYE/s1024/Seahorse.jpg",
				"https://lh3.googleusercontent.com/-Iwi6-i6IexY/URqvAYZHsVI/AAAAAAAAAbs/5ETWl4qXsFE/s1024/Shinjuku%252520Street.jpg",
				"https://lh6.googleusercontent.com/-amhnySTM_MY/URqvAlb5KoI/AAAAAAAAAbs/pFCFgzlKsn0/s1024/Sierra%252520Heavens.jpg",
				"https://lh5.googleusercontent.com/-dJgjepFrYSo/URqvBVJZrAI/AAAAAAAAAbs/v-F5QWpYO6s/s1024/Sierra%252520Sunset.jpg",
				"https://lh4.googleusercontent.com/-Z4zGiC5nWdc/URqvBdEwivI/AAAAAAAAAbs/ZRZR1VJ84QA/s1024/Sin%252520Lights.jpg",
				"https://lh4.googleusercontent.com/-_0cYiWW8ccY/URqvBz3iM4I/AAAAAAAAAbs/9N_Wq8MhLTY/s1024/Starry%252520Lake.jpg",
				"https://lh3.googleusercontent.com/-A9LMoRyuQUA/URqvCYx_JoI/AAAAAAAAAbs/s7sde1Bz9cI/s1024/Starry%252520Night.jpg",
				"https://lh3.googleusercontent.com/-KtLJ3k858eY/URqvC_2h_bI/AAAAAAAAAbs/zzEBImwDA_g/s1024/Stream.jpg",
				"https://lh5.googleusercontent.com/-dFB7Lad6RcA/URqvDUftwWI/AAAAAAAAAbs/BrhoUtXTN7o/s1024/Strip%252520Sunset.jpg",
				"https://lh5.googleusercontent.com/-at6apgFiN20/URqvDyffUZI/AAAAAAAAAbs/clABCx171bE/s1024/Sunset%252520Hills.jpg",
				"https://lh4.googleusercontent.com/-7-EHhtQthII/URqvEYTk4vI/AAAAAAAAAbs/QSJZoB3YjVg/s1024/Tenaya%252520Lake%2525202.jpg",
				"https://lh6.googleusercontent.com/-8MrjV_a-Pok/URqvFC5repI/AAAAAAAAAbs/9inKTg9fbCE/s1024/Tenaya%252520Lake.jpg",
				"https://lh5.googleusercontent.com/-B1HW-z4zwao/URqvFWYRwUI/AAAAAAAAAbs/8Peli53Bs8I/s1024/The%252520Cave%252520BW.jpg",
				"https://lh3.googleusercontent.com/-PO4E-xZKAnQ/URqvGRqjYkI/AAAAAAAAAbs/42nyADFsXag/s1024/The%252520Fisherman.jpg",
				"https://lh4.googleusercontent.com/-iLyZlzfdy7s/URqvG0YScdI/AAAAAAAAAbs/1J9eDKmkXtk/s1024/The%252520Night%252520is%252520Coming.jpg",
				"https://lh6.googleusercontent.com/-G-k7YkkUco0/URqvHhah6fI/AAAAAAAAAbs/_taQQG7t0vo/s1024/The%252520Road.jpg",
				"https://lh6.googleusercontent.com/-h-ALJt7kSus/URqvIThqYfI/AAAAAAAAAbs/ejiv35olWS8/s1024/Tokyo%252520Heights.jpg",
				"https://lh5.googleusercontent.com/-Hy9k-TbS7xg/URqvIjQMOxI/AAAAAAAAAbs/RSpmmOATSkg/s1024/Tokyo%252520Highway.jpg",
				"https://lh6.googleusercontent.com/-83oOvMb4OZs/URqvJL0T7lI/AAAAAAAAAbs/c5TECZ6RONM/s1024/Tokyo%252520Smog.jpg",
				"https://lh3.googleusercontent.com/-FB-jfgREEfI/URqvJI3EXAI/AAAAAAAAAbs/XfyweiRF4v8/s1024/Tufa%252520at%252520Night.jpg",
				"https://lh4.googleusercontent.com/-vngKD5Z1U8w/URqvJUCEgPI/AAAAAAAAAbs/ulxCMVcU6EU/s1024/Valley%252520Sunset.jpg",
				"https://lh6.googleusercontent.com/-DOz5I2E2oMQ/URqvKMND1kI/AAAAAAAAAbs/Iqf0IsInleo/s1024/Windmill%252520Sunrise.jpg",
				"https://lh5.googleusercontent.com/-biyiyWcJ9MU/URqvKculiAI/AAAAAAAAAbs/jyPsCplJOpE/s1024/Windmill.jpg",
				"https://lh4.googleusercontent.com/-PDT167_xRdA/URqvK36mLcI/AAAAAAAAAbs/oi2ik9QseMI/s1024/Windmills.jpg",
				"https://lh5.googleusercontent.com/-kI_QdYx7VlU/URqvLXCB6gI/AAAAAAAAAbs/N31vlZ6u89o/s1024/Yet%252520Another%252520Rockaway%252520Sunset.jpg",
				"https://lh4.googleusercontent.com/-e9NHZ5k5MSs/URqvMIBZjtI/AAAAAAAAAbs/1fV810rDNfQ/s1024/Yosemite%252520Tree.jpg",
				
				// Cat images
				"http://catoverflow.com/coF87Nih",
				"http://catoverflow.com/c5B6M4DM",
				"http://catoverflow.com/cdjGqi35",
				"http://catoverflow.com/cKEL3YCW",
				"http://catoverflow.com/cpe94pwr",
				"http://catoverflow.com/c2Tj9xYu",
				"http://catoverflow.com/cA0CRAQB",
				"http://catoverflow.com/cnKI9SGV",
				"http://catoverflow.com/cWuBDFwi",
				"http://catoverflow.com/cdq5MFkt",
				"http://catoverflow.com/chZ1yyMQ",
				"http://catoverflow.com/cuZHHxHT",
				"http://catoverflow.com/caPnaISr",
				"http://catoverflow.com/c7Rk8bnd",
				"http://catoverflow.com/coIuNk8v",
				"http://catoverflow.com/cUoAry5f",
				"http://catoverflow.com/cZbNK6PH",
				"http://catoverflow.com/cO6bJBbx",
				"http://catoverflow.com/cDm1X4OZ",
				"http://catoverflow.com/cdySHIeS",
				"http://catoverflow.com/cQ1N6Byx",
				"http://catoverflow.com/cqmKYyKA",
				"http://catoverflow.com/cMN0VEDh",
				"http://catoverflow.com/cTaH5AIp",
				"http://catoverflow.com/cmIRbtAt",
				"http://catoverflow.com/c4fFPNUg",
				"http://catoverflow.com/cCH1L5MF",
				"http://catoverflow.com/cM5QeSKa",
				"http://catoverflow.com/c78BYNqk",
				"http://catoverflow.com/cYgb0kfO",
				"http://catoverflow.com/cpmVGKsr",
				"http://catoverflow.com/c9lxaku0",
				"http://catoverflow.com/cMX1rLQE",
				"http://catoverflow.com/c3rK370K",
				"http://catoverflow.com/c3SSPfKG",
				"http://catoverflow.com/c0wd4lae",
				"http://catoverflow.com/cl5G75l4",
				"http://catoverflow.com/cKAljIp8",
				"http://catoverflow.com/cY5EjfYK",
				"http://catoverflow.com/c410R3qQ",
				"http://catoverflow.com/cIpBXRFM",
				"http://catoverflow.com/crjcctDf",
				"http://catoverflow.com/cKBLRPdb",
				"http://catoverflow.com/cuztyKQM",
				"http://catoverflow.com/cyrRs2Qd",
				"http://catoverflow.com/cuIICLsW",
				"http://catoverflow.com/c8nLb0xw",
				"http://catoverflow.com/cxyE2xEV",
				"http://catoverflow.com/csDeLInd",
				"http://catoverflow.com/cWyHUoSq",
				"http://catoverflow.com/cYbaJLEH",
				"http://catoverflow.com/cmWipSiP",
				"http://catoverflow.com/cUehl97u",
				"http://catoverflow.com/c9QghChE",
				"http://catoverflow.com/cgLqwTTf",
				"http://catoverflow.com/cce2LeHG",
				"http://catoverflow.com/cqq8hj9c",
				"http://catoverflow.com/cknuigmd",
				"http://catoverflow.com/cuw5fTaM",
				"http://catoverflow.com/cjaZCFK3",
				"http://catoverflow.com/chTL0W4d",
				"http://catoverflow.com/c9UfGAze",
				"http://catoverflow.com/cIFcfIMK",
				"http://catoverflow.com/czL00ohB",
				"http://catoverflow.com/cPzw1fco",
				"http://catoverflow.com/cUTxu49c",
				"http://catoverflow.com/cMauo7M8",
				"http://catoverflow.com/cMyj2zDC",
				"http://catoverflow.com/coBcKXoj",
				"http://catoverflow.com/cp5uyvSY",
				"http://catoverflow.com/c1ecA6DI",
				"http://catoverflow.com/ciLp8ppH",
				"http://catoverflow.com/csjQ0XNs",
				"http://catoverflow.com/c8DT3Stc",
				"http://catoverflow.com/citaBR6B",
				"http://catoverflow.com/cL8Vvs3h",
				"http://catoverflow.com/cUOQEFy7",
				"http://catoverflow.com/cDq8TJ5i",
				"http://catoverflow.com/cqVWBwpf",
				"http://catoverflow.com/cBRRrfbe",
				"http://catoverflow.com/clT9F69M",
				"http://catoverflow.com/cKyDz3mB",
				"http://catoverflow.com/ci9ezfTa",
				"http://catoverflow.com/cwayClrA",
				"http://catoverflow.com/cRT6Zwbl",
				"http://catoverflow.com/cDREn2rx",
				"http://catoverflow.com/cLpfZ6AQ",
				"http://catoverflow.com/cz43iSXC",
				"http://catoverflow.com/cXCnnnng",
				"http://catoverflow.com/cCJFP1tA",
				"http://catoverflow.com/c7scR1dD",
				"http://catoverflow.com/cx4aHqJH",
				"http://catoverflow.com/c3sPq0gi",
				"http://catoverflow.com/cxt5BbG7",
				"http://catoverflow.com/c5V1ycdo",
				"http://catoverflow.com/coneAjPm",
				"http://catoverflow.com/cLLBTXdo",
				"http://catoverflow.com/cllRRJvr",
				"http://catoverflow.com/cr3YV1Bg",
				"http://catoverflow.com/cnq7a1ph",
				"http://catoverflow.com/cCvJRAc9",
				"http://catoverflow.com/ckGlL7EM",
				"http://catoverflow.com/cmRu7ISd",
				"http://catoverflow.com/csf9LCOi",
				"http://catoverflow.com/ciCnWt9c",
				"http://catoverflow.com/cyA7JIDm",
				"http://catoverflow.com/cyQlEIc4",
				"http://catoverflow.com/cjOjeLSr",
				"http://catoverflow.com/cdDR8Tng",
				"http://catoverflow.com/cuenqwHY",
				"http://catoverflow.com/cuKZovDI",
				"http://catoverflow.com/c0Rp9shE",
				"http://catoverflow.com/cv9N6fEi",
				"http://catoverflow.com/cr4cIt4z",
				"http://catoverflow.com/cr4SDXok",
				"http://catoverflow.com/c8DGU6OJ",
				"http://catoverflow.com/cDT2NrpQ",
				"http://catoverflow.com/cfsDEYpy",
				"http://catoverflow.com/cmdJNwqR",
				"http://catoverflow.com/cMG5CCEJ",
				"http://catoverflow.com/cBHk7vQ8",
				"http://catoverflow.com/cXU3AWFI",
				"http://catoverflow.com/cRFjgSHv",
				"http://catoverflow.com/cRi3vykt",
				"http://catoverflow.com/cGpHRkB1",
				"http://catoverflow.com/cvZ8jcsp",
				"http://catoverflow.com/cO3m4wI8",
				"http://catoverflow.com/cJUBFggc",
				"http://catoverflow.com/coHUNGAl",
				"http://catoverflow.com/c9ACYTeZ",
				"http://catoverflow.com/cvYaajWY",
				"http://catoverflow.com/cfchfjEo",
				"http://catoverflow.com/cg1llHMT",
				"http://catoverflow.com/cMRepVls",
				"http://catoverflow.com/cxcjZuuL",
				"http://catoverflow.com/cuwJUpJY",
				"http://catoverflow.com/cxTCMZKO",
				"http://catoverflow.com/cr5bzap4",
				"http://catoverflow.com/cEiDZSqg",
				"http://catoverflow.com/cZTPKhAI",
				"http://catoverflow.com/crc6kFNs",
				"http://catoverflow.com/cesh5NI1",
				"http://catoverflow.com/cnNU2K72",
				"http://catoverflow.com/cbz6e6ZR",
				"http://catoverflow.com/clt8J1gT",
				"http://catoverflow.com/cOHpEPw7",
				"http://catoverflow.com/c0IxrjMu",
				"http://catoverflow.com/cOSFFyOK",
				"http://catoverflow.com/cQMO3III",
				"http://catoverflow.com/ciY0bXdY",
				"http://catoverflow.com/cY705Ucm",
				"http://catoverflow.com/c47O1Hse",
				"http://catoverflow.com/csP9YRXh",
				"http://catoverflow.com/ckHCMbYj",
				"http://catoverflow.com/cONp4zqB",
				"http://catoverflow.com/cREzr8ry",
				"http://catoverflow.com/c2MrWw5j",
				"http://catoverflow.com/cs6FqnZj",
				"http://catoverflow.com/cYhGtTll",
				"http://catoverflow.com/cskkrucR",
				"http://catoverflow.com/c6DoJ6X8",
				"http://catoverflow.com/cRQseZ2K",
				"http://catoverflow.com/cgSWDdC1",
				"http://catoverflow.com/caM185BO",
				"http://catoverflow.com/cyZCZOfx",
				"http://catoverflow.com/cmGuwVX9",
				"http://catoverflow.com/cl4yHdFC",
				"http://catoverflow.com/c5GB2uJo",
				"http://catoverflow.com/cYhwUfJv",
				"http://catoverflow.com/cif1eeAk",
				"http://catoverflow.com/c717sFHL",
				"http://catoverflow.com/chVEyl5Q",
				"http://catoverflow.com/c7XPRTE2",
				"http://catoverflow.com/cwL2qc4A",
				"http://catoverflow.com/c4zmHgDB",
				"http://catoverflow.com/cGvMYUWr",
				"http://catoverflow.com/cCw4kkwh",
				"http://catoverflow.com/c3Bdcs9K",
				"http://catoverflow.com/c9w6moqc",
				"http://catoverflow.com/czzaD3UF",
				"http://catoverflow.com/cxrHOfQR",
				"http://catoverflow.com/cppBcx5c",
				"http://catoverflow.com/cNp3MCDW",
				"http://catoverflow.com/cHILc9T5",
				"http://catoverflow.com/cLj5HseZ",
				"http://catoverflow.com/cBNO2S6q",
				"http://catoverflow.com/c6nFB9DJ",
				"http://catoverflow.com/c4rAGFNw",
				"http://catoverflow.com/cmQPjYEQ",
				"http://catoverflow.com/ctTGOUDA",
				"http://catoverflow.com/cbEYPb77",
				"http://catoverflow.com/czvZvXFN",
				"http://catoverflow.com/c0E39dTT",
				"http://catoverflow.com/cavaAzIn",
				"http://catoverflow.com/c419BDdV",
				"http://catoverflow.com/ctqxJ5r5",
				"http://catoverflow.com/ce1fANrV",
				"http://catoverflow.com/cZH25AxB",
				"http://catoverflow.com/c7GMoJNs",
				"http://catoverflow.com/cuSMEFHW",
				"http://catoverflow.com/c2yXCsYC",
				"http://catoverflow.com/cu2uqkeA",
				"http://catoverflow.com/ceVbWwxf",
				"http://catoverflow.com/cmnhcbqx",
				"http://catoverflow.com/csohBwpm",
				"http://catoverflow.com/ceJh8iyn",
				"http://catoverflow.com/cYja5ERn",
				"http://catoverflow.com/cPUZEG8e",
				"http://catoverflow.com/cvUvArpr",
				"http://catoverflow.com/cf1aZyha",
				"http://catoverflow.com/cdetbNnG",
				"http://catoverflow.com/cQoPCgNi",
				"http://catoverflow.com/cieCtWFN",
				"http://catoverflow.com/cBRJwpnY",
				"http://catoverflow.com/cwNX1DXG",
				"http://catoverflow.com/cz8r7lTc",
				"http://catoverflow.com/cxPJagya",
				"http://catoverflow.com/cYNjAyS0",
				"http://catoverflow.com/cF99qSLn",
				"http://catoverflow.com/c9v4bSeP",
				"http://catoverflow.com/cjMiySyJ",
				"http://catoverflow.com/cja3Ytmh",
				"http://catoverflow.com/coYZPIG9",
				"http://catoverflow.com/cEYlvPpx",
				"http://catoverflow.com/c3dG7jYY",
				"http://catoverflow.com/cxE9Ko1T",
				"http://catoverflow.com/cG7tkIPD",
				"http://catoverflow.com/cGC7ksCf",
				"http://catoverflow.com/ck2H9XeZ",
				"http://catoverflow.com/cqJVjabT",
				"http://catoverflow.com/cx42Pwrt",
				"http://catoverflow.com/cCRj3Ljx",
				"http://catoverflow.com/cPOk5uyn",
				"http://catoverflow.com/cEK1LnFa",
				"http://catoverflow.com/cNHPcCbW",
				"http://catoverflow.com/cV4ZYTFT",
				"http://catoverflow.com/cV1gYHbH",
				"http://catoverflow.com/c8R7TEar",
				"http://catoverflow.com/c0PnRR3V",
				"http://catoverflow.com/ciyXO9k7",
				"http://catoverflow.com/cxVWnE8V",
				"http://catoverflow.com/cJLC2ZaU",
				"http://catoverflow.com/cyFAMgXU",
				"http://catoverflow.com/ccBglvE3",
				"http://catoverflow.com/c3oQxnjV",
				"http://catoverflow.com/cuhGAfOz",
				"http://catoverflow.com/cL8cyP6m",
				"http://catoverflow.com/cWHl0xdp",
				"http://catoverflow.com/cEVHdvpE",
				"http://catoverflow.com/cLfhBY85",
				"http://catoverflow.com/cNBV979D",
				"http://catoverflow.com/cIZ6aJRE",
				"http://catoverflow.com/czfetvga",
				"http://catoverflow.com/cPwJgoTk",
				"http://catoverflow.com/cKF1BYig",
				"http://catoverflow.com/cx1AXBHf",
				"http://catoverflow.com/cVBVzYdg",
				"http://catoverflow.com/c7j580Wi",
				"http://catoverflow.com/cUdUmjvA",
				"http://catoverflow.com/cYglnLtf",
				"http://catoverflow.com/cWSkUkpF",
				"http://catoverflow.com/cE81egUc",
				"http://catoverflow.com/cnpTuajs",
				"http://catoverflow.com/cVr8M8uo",
				"http://catoverflow.com/cQHAHb1r",
				"http://catoverflow.com/cu4Tv1RB",
				"http://catoverflow.com/csqhGKRc",
				"http://catoverflow.com/cxgNWVsC",
				"http://catoverflow.com/cN6oM471",
				"http://catoverflow.com/cDGwdZQk",
				"http://catoverflow.com/c0MT7JO0",
				"http://catoverflow.com/cKweUSZZ",
				"http://catoverflow.com/cBchUM2k",
				"http://catoverflow.com/cpIHAdxx",
				"http://catoverflow.com/cE2KXEz7",
				"http://catoverflow.com/ccayZYMV",
				"http://catoverflow.com/cC2vs7Cy",
				"http://catoverflow.com/cwOfsCv8",
				"http://catoverflow.com/csX3ouBj",
				"http://catoverflow.com/c0tFQbHE",
				"http://catoverflow.com/cQGISAFr",
				"http://catoverflow.com/c6OrI7b8",
				"http://catoverflow.com/cyL8DL8m",
				"http://catoverflow.com/cZ6hhfv5",
				"http://catoverflow.com/cOBbwE0W",
				"http://catoverflow.com/cSUfaPdA",
				"http://catoverflow.com/cBt4lZjJ",
				"http://catoverflow.com/crJ6fkvX",
				"http://catoverflow.com/cdFCKVnb",
				"http://catoverflow.com/cWDHm20y",
				"http://catoverflow.com/c69JRY5j",
				"http://catoverflow.com/ctJNm7n1",
				"http://catoverflow.com/cqu0R189",
				"http://catoverflow.com/cIhNtF03",
				"http://catoverflow.com/casHQiwt",
				"http://catoverflow.com/c2dNrYAA",
				"http://catoverflow.com/cjgiko1a",
				"http://catoverflow.com/cmWcrUoY",
				"http://catoverflow.com/cbmaQmif",
				"http://catoverflow.com/cl8us2Sy",
				"http://catoverflow.com/cdbiqDPc",
				"http://catoverflow.com/c9gVMBos",
				"http://catoverflow.com/cOOh2KWJ",
				"http://catoverflow.com/cQyDbNpl",
				"http://catoverflow.com/cZfVXsnU",
				"http://catoverflow.com/ccXQDZiB",
				"http://catoverflow.com/cNoqSMqE",
				"http://catoverflow.com/cbU404o2",
				"http://catoverflow.com/cXOZjCHm",
				"http://catoverflow.com/czprWhku",
				"http://catoverflow.com/ck3YxMjw",
				"http://catoverflow.com/cEIB7sHp",
				"http://catoverflow.com/crp4d9A1",
				"http://catoverflow.com/cN6K9YjC",
				"http://catoverflow.com/ceWKi7Dr",
				"http://catoverflow.com/cSKAok7S",
				"http://catoverflow.com/c0JVW7as",
				"http://catoverflow.com/cdZ36gqc",
				"http://catoverflow.com/cvFHeJox",
				"http://catoverflow.com/chOnunJb",
				"http://catoverflow.com/cwoHkWn3",
				"http://catoverflow.com/cVLrb0ei",
				"http://catoverflow.com/cVzxsGZT",
				"http://catoverflow.com/c8TaFiu2",
				"http://catoverflow.com/cDscxYct"
			};
		
			// shuffle the list each time
			var rng = new Random ();  
			var n = objects.Count;  
			while (n > 1) {  
				n--;  
				int k = rng.Next (n + 1);  
				var value = objects [k];  
				objects [k] = objects [n];  
				objects [n] = value;  
			}
		}
	}
}

