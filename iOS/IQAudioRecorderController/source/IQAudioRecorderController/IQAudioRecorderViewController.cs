using System;
using Foundation;
using UIKit;
using System.Threading.Tasks;

namespace IQAudioRecorderController {
    
    /// <summary>
    /// Recording completed delegate.
    /// </summary>
	public delegate void RecordingCompletedDelegate(IQAudioRecorderViewController controller, String fileName);

	/// <summary>
	/// Recording cancelled delegate.
	/// </summary>
	public delegate void RecordingCancelledDelegate(IQAudioRecorderViewController controller);

	/// <summary>
	/// IQAudioRecorderViewController
	/// </summary>
    public class IQAudioRecorderViewController : UINavigationController {
        
        #region Fields

		private IQInternalAudioRecorderController m_internalController;
        #endregion
        
        #region Properties
					
		/// <summary>
		/// Occurs when recording is cancelled
		/// </summary>
		public event RecordingCancelledDelegate OnCancel  = delegate {};

		/// <summary>
		/// Occurs when recording is complete
		/// </summary>
		public event RecordingCompletedDelegate OnRecordingCompleted = delegate { };

		/// <summary>
		/// Gets/Sets the wave colour when nothing is happenening
		/// </summary>
		/// <value>The color of the normal tint.</value>
		public UIColor NormalTintColor
		{
			get 
			{
				return InternalPlayer.NormalTintColor;
			}
			set
			{
				InternalPlayer.NormalTintColor = value;
			}
		}

		/// <summary>
		/// Gets/Sets the wave colour during recording
		/// </summary>
		/// <value>The color of the recording tint.</value>
		public UIColor RecordingTintColor
		{
			get 
			{
				return InternalPlayer.RecordingTintColor;
			}
			set
			{
				InternalPlayer.RecordingTintColor = value;
			}
		}

		/// <summary>
		/// Gets/Sets the wave colour during recording
		/// </summary>
		/// <value>The color of the recording tint.</value>
		public UIColor PlayingTintColor
		{
			get 
			{
				return InternalPlayer.PlayingTintColor;
			}
			set
			{
				InternalPlayer.PlayingTintColor = value;
			}
		}

		internal IQInternalAudioRecorderController InternalPlayer
		{
			get 
			{
				if (m_internalController == null)
					m_internalController = new IQInternalAudioRecorderController();

				return m_internalController;
					
			}
		}
        #endregion
        
		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="IQAudioRecorderController.IQAudioRecorderViewController"/> class.
		/// </summary>
		public IQAudioRecorderViewController ()
		{
			
		}

		#endregion

        #region Methods

		/// <summary>
		/// Views the did load.
		/// </summary>
        public override void ViewDidLoad() {

			base.ViewDidLoad ();

            //     
			InternalPlayer.CancelControllerAction = (sv) => 
			{
				this.BeginInvokeOnMainThread (() => 
					{

						OnCancel(sv);

					});
				

			};

			m_internalController.RecordingCompleteAction = (sv, pa) => {

				this.BeginInvokeOnMainThread (() => 
				{
					OnRecordingCompleted(sv,pa);
						
				});
				
			};
				
            //     
			this.ViewControllers = new UIViewController[]{m_internalController};

			 this.NavigationBar.TintColor = UIColor.White;
             this.NavigationBar.Translucent = true;
             this.NavigationBar.BarStyle = UIBarStyle.BlackTranslucent;
             
             this.ToolbarHidden = false;
             this.Toolbar.TintColor = this.NavigationBar.TintColor;
             this.Toolbar.Translucent = this.NavigationBar.Translucent;
             this.Toolbar.BarStyle = this.NavigationBar.BarStyle;

        }
        
        #endregion

		#region Static Method

		/// <summary>
		/// Shows the IQAudioRecorderViewController dialog (awaitable)
		/// </summary>
		/// <returns>The the path to the recorded file.  Will be empty if cancelled</returns>
		/// <param name="vc">The view controller that will present the view controller(Required).</param>
		/// <param name="colors">Colors arrays to set Normal, Recording and Playing wave tint colors(in that order)</param>
		public static Task<String> ShowDialogTask(UIViewController vc, UIColor[] colors = null)
		{
			if (vc == null)
				throw new Exception ("You must provide a ViewController to IQAudioRecorderViewController.ShowDialogAsync");
			
			var tcs = new TaskCompletionSource<String> ();

			new NSObject ().BeginInvokeOnMainThread (() => {

				var controller = new IQAudioRecorderViewController();

				if (colors != null && colors.Length > 0)
				{
					if (colors.Length > 1)
					{
						controller.NormalTintColor = colors[0];
					}

					if (colors.Length > 2)
					{
						controller.RecordingTintColor = colors[1];
					}

					if (colors.Length > 3)
					{
						controller.PlayingTintColor = colors[2];
					}

				}

				controller.OnCancel += (contr) => 
				{
					tcs.SetResult(null);
				};

				controller.OnRecordingCompleted += (contr, fileName) => 
				{
					tcs.SetResult(fileName);
				};

				vc.PresentViewController(controller,true,null);

			});

			return tcs.Task;
		}

		#endregion
    }
}
