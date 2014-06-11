
using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Lisa.Zuma.BlueJay.IOS.Views
{
	/// <summary>
	/// Choice camera option popup view controller. very simple class for a tiny feature.
	/// </summary>
	public partial class ChoiceCameraOptionViewController : UIViewController
	{
		public ChoiceCameraOptionViewController () : base ("ChoiceCameraOptionViewController", null)
		{
			camera = new Camera ();
		}

		public static event EventHandler hidePopUp;
			
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			btnPhoto.TouchUpInside += delegate {
				camera.CapturePhoto(String.Format("{0:d-M-yyyy-HH-mm-ss}", DateTime.Now), null);
				hidePopUp(this, EventArgs.Empty);
			};

			btnVideo.TouchUpInside += delegate {
				camera.CaptureVideo(String.Format("{0:d-M-yyyy-HH-mm-ss}", DateTime.Now), null);
				hidePopUp(this, EventArgs.Empty);
			};
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		private Camera camera;

	}
}

