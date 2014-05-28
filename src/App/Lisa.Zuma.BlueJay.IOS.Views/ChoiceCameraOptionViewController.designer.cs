// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace Lisa.Zuma.BlueJay.IOS.Views
{
	[Register ("ChoiceCameraOptionViewController")]
	partial class ChoiceCameraOptionViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnPhoto { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnVideo { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnPhoto != null) {
				btnPhoto.Dispose ();
				btnPhoto = null;
			}

			if (btnVideo != null) {
				btnVideo.Dispose ();
				btnVideo = null;
			}
		}
	}
}
