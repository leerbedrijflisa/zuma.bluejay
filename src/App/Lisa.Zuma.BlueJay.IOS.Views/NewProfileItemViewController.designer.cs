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
	[Register ("NewProfileItemViewController")]
	partial class NewProfileItemViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnAddProfileItem { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblTitle { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIPickerView pckPicker { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtContent { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtTitle { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextView txtTitle2 { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnAddProfileItem != null) {
				btnAddProfileItem.Dispose ();
				btnAddProfileItem = null;
			}

			if (lblTitle != null) {
				lblTitle.Dispose ();
				lblTitle = null;
			}

			if (pckPicker != null) {
				pckPicker.Dispose ();
				pckPicker = null;
			}

			if (txtContent != null) {
				txtContent.Dispose ();
				txtContent = null;
			}

			if (txtTitle != null) {
				txtTitle.Dispose ();
				txtTitle = null;
			}

			if (txtTitle2 != null) {
				txtTitle2.Dispose ();
				txtTitle2 = null;
			}
		}
	}
}
