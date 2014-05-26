using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Lisa.Zuma.BlueJay.IOS.Models;
using System.Linq;

namespace Lisa.Zuma.BlueJay.IOS.Views
{
	public partial class SummaryViewController : UIViewController
	{
		private TableHelper tableHelper;
		private TableSource tableSource;
		private DataHelper dataHelper;

		public SummaryViewController () : base ("SummaryViewController", null)
		{
			tableHelper = new TableHelper ();
			dataHelper = new DataHelper ();
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}

		public void RowClicked_handler (object sender, RowClickedEventArgs e)
		{
			TimelineViewController timeLineViewController = new TimelineViewController ();
			this.NavigationController.PushViewController (timeLineViewController, true);
		}
			
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			NavigationItem.SetHidesBackButton (true, false);
			var dosiers = dataHelper.GetDosierDatas();

			jumpToNextView = (dosiers.Count == 1);
			if (jumpToNextView) {

				dataHelper.insertNewCurrentDossier (dosiers.First().DossierId);
				// Return to prevent table from being filled! 
				return;
			}
			var sourceFromTablehelper = tableHelper.CreateSource(dosiers, d => d.DossierId, d => d.Name);

			sourceFromTablehelper.RowClicked += RowClicked_handler;
			tblCell.Source = sourceFromTablehelper;

			UIPopoverController myPopOver = new UIPopoverController(new LockScreenSettings()); 

//			btnLockScreenSettings.TouchUpInside += (sender, e) => {
//				myPopOver.PopoverContentSize = new SizeF (View.Frame.Width - 30f, View.Frame.Height - 10f);
//				myPopOver.PresentFromRect (btnLockScreenSettings.Frame, this.View, UIPopoverArrowDirection.Up, true);
//			};   
		}

		public void NewCombinationDelegate()
		{
			NavigationController.PushViewController (new LockScreenViewController (1), true);
		}

		public override void ViewDidAppear (bool animated)
		{
			Reachability.getStatus ();

			this.NavigationController.SetNavigationBarHidden (true, true);

			// If jumpToNextView == true, only one dossier is available so open the timeline directly.
			if (jumpToNextView) {
			
				NavigationController.PushViewController (new TimelineViewController (), false);
			}
		}

		private bool jumpToNextView = false;
	}
}

