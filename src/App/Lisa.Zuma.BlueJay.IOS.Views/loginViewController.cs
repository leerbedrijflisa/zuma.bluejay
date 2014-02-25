using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Lisa.Zuma.BlueJay.IOS.Models;

namespace Lisa.Zuma.BlueJay.IOS
{
	public partial class loginViewController : UIViewController
	{
		private DataHelper DataHelper;
		private SummaryViewController summaryViewController;
			 
		public loginViewController () : base ("loginViewController", null)
		{
			DataHelper = new DataHelper ();
			summaryViewController = new SummaryViewController ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			btnMoeder.TouchUpInside += (object sender, EventArgs e) => {
				DataHelper.SignIn(1);
				this.NavigationController.PushViewController(summaryViewController, true);
			};

			btnBegeleider.TouchUpInside += (object sender, EventArgs e) => {
				DataHelper.SignIn(2);
				this.NavigationController.PushViewController(summaryViewController, true);
			};
		}
	}
}

