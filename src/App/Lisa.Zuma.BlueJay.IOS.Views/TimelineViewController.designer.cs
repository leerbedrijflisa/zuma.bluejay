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
		MonoTouch.UIKit.UIButton btnEditProfile { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnNewNote { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnRefresh { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIWebView wvTimeline { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnEditProfile != null) {
				btnEditProfile.Dispose ();
				btnEditProfile = null;
			}

			if (btnNewNote != null) {
				btnNewNote.Dispose ();
				btnNewNote = null;
			}

			if (wvTimeline != null) {
				wvTimeline.Dispose ();
				wvTimeline = null;
			}

			if (btnRefresh != null) {
				btnRefresh.Dispose ();
				btnRefresh = null;
			}
		}
	}
}
