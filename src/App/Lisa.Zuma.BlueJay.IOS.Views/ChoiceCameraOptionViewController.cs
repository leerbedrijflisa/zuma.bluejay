
using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Lisa.Zuma.BlueJay.IOS.Views
{
	public partial class ChoiceCameraOptionViewController : UIViewController
	{
		public ChoiceCameraOptionViewController () : base ("ChoiceCameraOptionViewController", null)
		{
			camera = new Camera ();
		}
			

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			btnPhoto.TouchUpInside += delegate {
				camera.CaptureVideo(String.Format("{0:d-M-yyyy-HH-mm-ss}", DateTime.Now), () => {});
			};

			btnVideo.TouchUpInside += delegate {
				camera.CapturePhoto(String.Format("{0:d-M-yyyy-HH-mm-ss}", DateTime.Now), () => {});
			};
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		private Camera camera;
	}
}

