using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Lisa.Zuma.BlueJay.IOS.Models;

namespace Lisa.Zuma.BlueJay.IOS
{
	public partial class ProfileViewController : UIViewController
	{
		private TableHelper tableHelper;
		private DataHelper dataHelper;
		private string selectedCat;

		public ProfileViewController () : base ("ProfileViewController", null)
		{
			tableHelper = new TableHelper();
			dataHelper = new DataHelper ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			btnNewItem.TouchUpInside += (sender, e) => {
				NewProfileItemViewController newProfileItemViewController = new NewProfileItemViewController();
				this.NavigationController.PushViewController(newProfileItemViewController, true);
			};

			btnBack.TouchUpInside += (sender, e) => {
				TimelineViewController timeLineViewController = new TimelineViewController();
				this.NavigationController.PushViewController(timeLineViewController, true);
			};
		}

		public override void ViewWillAppear (bool animated)
		{
			tblProfile.Source = tableHelper.DataForProfileList();
		}
	}
}

