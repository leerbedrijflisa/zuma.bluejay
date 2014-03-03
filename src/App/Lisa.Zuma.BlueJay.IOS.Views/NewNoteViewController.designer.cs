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
	[Register ("NewNoteViewController")]
	partial class NewNoteViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnCamera { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnSave { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnCamera != null) {
				btnCamera.Dispose ();
				btnCamera = null;
			}

			if (btnSave != null) {
				btnSave.Dispose ();
				btnSave = null;
			}
		}
	}
}
