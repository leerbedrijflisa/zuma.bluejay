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
	[Register ("LockScreenSettings")]
	partial class LockScreenSettings
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnSetNewLockscreen { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch swLockScreen { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (swLockScreen != null) {
				swLockScreen.Dispose ();
				swLockScreen = null;
			}

			if (btnSetNewLockscreen != null) {
				btnSetNewLockscreen.Dispose ();
				btnSetNewLockscreen = null;
			}
		}
	}
}
