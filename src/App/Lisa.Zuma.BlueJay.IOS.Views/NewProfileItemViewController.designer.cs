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
	[Register ("NewProfileItemViewController")]
	partial class NewProfileItemViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnAddProfileItem { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIPickerView pckPicker { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtContent { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtTitle { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnAddProfileItem != null) {
				btnAddProfileItem.Dispose ();
				btnAddProfileItem = null;
			}

			if (txtContent != null) {
				txtContent.Dispose ();
				txtContent = null;
			}

			if (txtTitle != null) {
				txtTitle.Dispose ();
				txtTitle = null;
			}

			if (pckPicker != null) {
				pckPicker.Dispose ();
				pckPicker = null;
			}
		}
	}
}
