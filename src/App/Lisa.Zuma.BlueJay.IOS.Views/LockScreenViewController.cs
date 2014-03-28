using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Lisa.Zuma.BlueJay.IOS.Views
{
	public partial class LockScreenViewController : UIViewController
	{
		public LockScreenViewController () : base ("LockScreenViewController", null)
		{
		}
			
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
		
		}

		private void InitializeUI()
		{
			btnNumberOne.TouchUpInside += WriteToConsole;
			btnNumberTwo.TouchUpInside += WriteToConsole;
			btnNumberThree.TouchUpInside +=WriteToConsole;
			btnNumberFour.TouchUpInside += WriteToConsole;
			btnNumberFive.TouchUpInside += WriteToConsole;
			btnNumberSix.TouchUpInside += WriteToConsole;
			btnNumberSeven.TouchUpInside += WriteToConsole;
			btnNumberEight.TouchUpInside += WriteToConsole;
			btnNumberNine.TouchUpInside += WriteToConsole;
			btnNumberZero.TouchUpInside += WriteToConsole;
		}

		public void WriteToConsole(Object sender, EventArgs args)
		{
			var txt = sender as UIButton;
			if (txt != null) {
				var val = txt.CurrentTitle;
				Console.WriteLine (val);
			}
		}
	}
}

