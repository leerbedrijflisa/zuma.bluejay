using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using Lisa.Zuma.BlueJay.IOS.Models;

namespace Lisa.Zuma.BlueJay.IOS.Views
{
	public partial class LockScreenViewController : UIViewController
	{
//		private int State;
	
		public LockScreenViewController (int state = 0) : base ("LockScreenViewController", null)
		{
			DummyCombination = 1234;

//			State = state;
		}
			
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
				ButtonHandle ();
			NavigationController.SetNavigationBarHidden (true, false);
		}

		private void ButtonHandle()
		{
			ButtonList.AddRange (new List<UIButton>{btnNumberOne,
				btnNumberTwo,
				btnNumberThree,
				btnNumberFour,
				btnNumberFive,
				btnNumberSix,
				btnNumberSeven,
				btnNumberEight,
				btnNumberNine,
				btnNumberZero
			});

			foreach(var button in ButtonList){
				button.TouchUpInside += setNumberToList;
			}
		}

		private void setNumberToList(Object sender, EventArgs args)
		{
			var txt = sender as UIButton;

			if (txt != null) {

				Combination += txt.CurrentTitle;
				FillCombinationButton ();

				Console.WriteLine (Combination.Length);

//				if (Combination.Length == 4 && State == null) {
//					CombinationIsValid ();
//				}
//
//				if (Combination.Length == 4 && State != null) {
//					CombinationIsValid ();
//				}

			}
		}
		private void NewCombination()
		{


//			if (confirmCombination == firstCombination){
//				lblTitle.Text = "Voer nogmaals de combinatie in..";
//				dataHelper.newCombination (Combination);
//			}

//			if (firstCombination == null) {
//				firstCombination = Combination;
//			}


		}

		public void CombinationIsValid()
		{
			if (Combination == DummyCombination.ToString ()) {
				Console.WriteLine ("Yes!");
				Combination = string.Empty;
				NavigationController.PushViewController (new SummaryViewController(), true);
			} else {
				Console.WriteLine ("Nope !");
				CominationFailedAnimation ();
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

		private void CominationFailedAnimation()
		{
			SetButtonState(ButtonList, false);

			NSTimer.CreateScheduledTimer (TimeSpan.FromMilliseconds(500), delegate {
				SetButtonState(ButtonList, true);
				clearChosenCombination ();
			});

			ImageViewAnimation (new List<UIImageView>{imvInputOne, imvInputTwo, imvInputThree, imvInputFour});

		}

		private void ImageViewAnimation(List<UIImageView> imageViewList)
		{
			foreach( var imageView in imageViewList){
				imageView.AnimationImages = new UIImage[] {
					UIImage.FromBundle("number-emtpy.png"),
					UIImage.FromBundle("number-insert.png")
				};

				imageView.AnimationRepeatCount = 2;
				imageView.AnimationDuration = 0.2;
				imageView.StartAnimating ();
			}
		}

		private void SetButtonState(List<UIButton> buttonList, bool state)
		{
			foreach (var button in buttonList) {
				button.Enabled = state;
			}
		}

		private List<UIButton> ButtonList = new List<UIButton> (); 

		private string Combination;
		private int DummyCombination;
	}
}

