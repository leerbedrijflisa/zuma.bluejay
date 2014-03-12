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
	[Register ("ProfileViewController")]
	partial class ProfileViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnBack { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnNewItem { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView tblProfile { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnNewItem != null) {
				btnNewItem.Dispose ();
				btnNewItem = null;
			}

			if (tblProfile != null) {
				tblProfile.Dispose ();
				tblProfile = null;
			}

			if (btnBack != null) {
				btnBack.Dispose ();
				btnBack = null;
			}
		}
	}
}
