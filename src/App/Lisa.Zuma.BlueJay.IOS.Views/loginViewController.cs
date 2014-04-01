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

		private void SignIn(Object sender, EventArgs args)
		{
			loadingOverlay = new LoadingOverlay (UIScreen.MainScreen.Bounds);
			View.Add (loadingOverlay);

			dataHelper.SignIn (txtUsername.Text, txtPassword.Text, () =>{
				InvokeOnMainThread (delegate { 
					NavigationController.PushViewController (new SummaryViewController(), true);
					loadingOverlay.Hide();
				});
			});
		}

		private DataHelper dataHelper;
		private LoadingOverlay loadingOverlay;
	}
}

