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
	[Register ("NewNoteViewController")]
	partial class NewNoteViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnCamera { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnMediaSummary { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnPhotoCamera { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnPickPhoto { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnPickVideo { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnSave { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnCamera != null) {
				btnCamera.Dispose ();
				btnCamera = null;
			}

			if (btnPhotoCamera != null) {
				btnPhotoCamera.Dispose ();
				btnPhotoCamera = null;
			}

			if (btnPickPhoto != null) {
				btnPickPhoto.Dispose ();
				btnPickPhoto = null;
			}

			if (btnPickVideo != null) {
				btnPickVideo.Dispose ();
				btnPickVideo = null;
			}

			if (btnSave != null) {
				btnSave.Dispose ();
				btnSave = null;
			}

			if (btnMediaSummary != null) {
				btnMediaSummary.Dispose ();
				btnMediaSummary = null;
			}
		}
	}
}
