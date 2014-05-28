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
	[Register ("SummaryViewController")]
	partial class SummaryViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnLockScreenSettings { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView tblCell { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (tblCell != null) {
				tblCell.Dispose ();
				tblCell = null;
			}

			if (btnLockScreenSettings != null) {
				btnLockScreenSettings.Dispose ();
				btnLockScreenSettings = null;
			}
		}
	}
}
