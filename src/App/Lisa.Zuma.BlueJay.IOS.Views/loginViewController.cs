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

			btnMoeder.TouchUpInside += (object sender, EventArgs e) => {
				dataHelper.SignIn(1);
				PushToSummaryController();
			};

			btnBegeleider.TouchUpInside += (object sender, EventArgs e) => {
				dataHelper.SignIn(2);
				PushToSummaryController();
			};
		}

		private void PushToSummaryController()
		{
			NavigationController.PushViewController (new SummaryViewController(), true);
		}

		private DataHelper dataHelper;
	}
}

