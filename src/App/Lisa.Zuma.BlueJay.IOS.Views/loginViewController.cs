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

		public loginViewController () : base ("loginViewController", null)
		{
			DataHelper = new DataHelper ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			btnMoeder.TouchUpInside += (object sender, EventArgs e) => {
				DataHelper.SignIn(1);
			};

			btnBegeleider.TouchUpInside += (object sender, EventArgs e) => {
				DataHelper.SignIn(2);
			};
		}
	}
}

