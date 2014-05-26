using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Lisa.Zuma.BlueJay.IOS.Models;

namespace Lisa.Zuma.BlueJay.IOS.Views
{
	public partial class loginViewController : UIViewController
	{
			 
		public loginViewController () : base ("loginViewController", null)
		{
			dataHelper = new DataHelper ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			btnSignIn.TouchUpInside += SignIn;

		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			this.NavigationController.SetNavigationBarHidden (true, true);
		}



		private void SignIn(Object sender, EventArgs args)
		{
			loadingOverlay = new LoadingOverlay (UIScreen.MainScreen.Bounds);
			View.Add (loadingOverlay);

			dataHelper.SignIn (txtUsername.Text, txtPassword.Text, () =>{
				InvokeOnMainThread (delegate { 
					NavigationController.PushViewController (new SummaryViewController(), true);
					loadingOverlay.Hide();
				});
			}, () => {
				InvokeOnMainThread (delegate { 
					loadingOverlay.Hide();
					new UIAlertView("Verkeerde gegevens", "De ingevoerde gegevens zijn onjuist"
						, null, "probeer opniew...", null).Show();
				});
			});

			this.txtPassword.ShouldReturn += (textField) => { 
				textField.ResignFirstResponder();
				return true; 
			};
		}

		private DataHelper dataHelper;
		private LoadingOverlay loadingOverlay;
	}
}

