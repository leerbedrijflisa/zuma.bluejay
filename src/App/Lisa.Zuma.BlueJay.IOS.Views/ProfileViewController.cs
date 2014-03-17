using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Lisa.Zuma.BlueJay.IOS.Models;

namespace Lisa.Zuma.BlueJay.IOS
{
	public partial class ProfileViewController : UIViewController
	{
		public ProfileViewController () : base ("ProfileViewController", null)
		{
			tableHelper = new TableHelper();
			dataHelper = new DataHelper ();
			eventHandlers = new EventHandlers (this);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			btnNewItem.TouchUpInside += eventHandlers.CreatePush<NewProfileItemViewController> ();
			btnBack.TouchUpInside += eventHandlers.CreatePush<TimelineViewController> ();
		}

		public override void ViewWillAppear (bool animated)
		{
			var profileItems = dataHelper.GetProfileItems();

			tblProfile.Source = tableHelper.CreateSource(profileItems, p => p.ID, p => p.Title);
		}

		private TableHelper tableHelper;
		private DataHelper dataHelper;
		private EventHandlers eventHandlers;
	}
}

