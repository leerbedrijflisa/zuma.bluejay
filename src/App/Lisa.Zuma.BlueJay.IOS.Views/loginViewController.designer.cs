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
	[Register ("loginViewController")]
	partial class loginViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnBegeleider { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnMoeder { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnMoeder != null) {
				btnMoeder.Dispose ();
				btnMoeder = null;
			}

			if (btnBegeleider != null) {
				btnBegeleider.Dispose ();
				btnBegeleider = null;
			}
		}
	}
}
