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
	[Register ("TimelineViewController")]
	partial class TimelineViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnCamera { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnEditProfile { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnLogout { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnNewNote { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnRefresh { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblTitle { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIWebView wvTimeline { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnEditProfile != null) {
				btnEditProfile.Dispose ();
				btnEditProfile = null;
			}

			if (btnLogout != null) {
				btnLogout.Dispose ();
				btnLogout = null;
			}

			if (btnNewNote != null) {
				btnNewNote.Dispose ();
				btnNewNote = null;
			}

			if (btnRefresh != null) {
				btnRefresh.Dispose ();
				btnRefresh = null;
			}

			if (lblTitle != null) {
				lblTitle.Dispose ();
				lblTitle = null;
			}

			if (wvTimeline != null) {
				wvTimeline.Dispose ();
				wvTimeline = null;
			}

			if (btnCamera != null) {
				btnCamera.Dispose ();
				btnCamera = null;
			}
		}
	}
}
