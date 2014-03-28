using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace Lisa.Zuma.BlueJay.IOS.Views
{
	public partial class LockScreenViewController : UIViewController
	{
		public LockScreenViewController () : base ("LockScreenViewController", null)
		{
			DummyCombination = 1234;
		}
			
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			ButtonHandle ();
		}

		private void ButtonHandle()
		{
			btnNumberOne.TouchUpInside += setNumberToList;
			btnNumberTwo.TouchUpInside += setNumberToList;
			btnNumberThree.TouchUpInside +=setNumberToList;
			btnNumberFour.TouchUpInside += setNumberToList;
			btnNumberFive.TouchUpInside += setNumberToList;
			btnNumberSix.TouchUpInside += setNumberToList;
			btnNumberSeven.TouchUpInside += setNumberToList;
			btnNumberEight.TouchUpInside += setNumberToList;
			btnNumberNine.TouchUpInside += setNumberToList;
			btnNumberZero.TouchUpInside += setNumberToList;
		}

		private void setNumberToList(Object sender, EventArgs args)
		{
			var txt = sender as UIButton;

			if (txt != null) {

				Combination += txt.CurrentTitle;
				FillCombinationButton ();

				Console.WriteLine (Combination.Length);

				if (Combination.Length == 4) {
//					NSThread.SleepFor (200);
					if (Combination == DummyCombination.ToString ()) {
						Console.WriteLine ("Good Job !");
						clearChosenCombination ();
					} else {
						Console.WriteLine ("Nope !");
						clearChosenCombination ();
					}
				}

			}
		}

		private void clearChosenCombination(){
			imvInputOne.Image = UIImage.FromBundle ("number-emtpy.png");
			imvInputTwo.Image = UIImage.FromBundle ("number-emtpy.png");
			imvInputThree.Image = UIImage.FromBundle ("number-emtpy.png");
			imvInputFour.Image = UIImage.FromBundle ("number-emtpy.png");
			Combination = string.Empty;
		}

		private void FillCombinationButton(){
			var value = Combination.Length;

			switch (value) {
			case 1:
				imvInputOne.Image = UIImage.FromBundle ("number-insert.png");
				break;
			case 2: 
				imvInputTwo.Image = UIImage.FromBundle ("number-insert.png");
				break;
			case 3:
				imvInputThree.Image = UIImage.FromBundle ("number-insert.png");
				break;
			case 4:
				imvInputFour.Image = UIImage.FromBundle ("number-insert.png");
				break;
			}
		}

		private string Combination;
		private int DummyCombination;
	}
}

