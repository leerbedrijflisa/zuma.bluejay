// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace Lisa.Zuma.BlueJay.IOS
{
	[Register ("TimelineViewController")]
	partial class TimelineViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIWebView wvTimeline { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (wvTimeline != null) {
				wvTimeline.Dispose ();
				wvTimeline = null;
			}
		}
	}
}
