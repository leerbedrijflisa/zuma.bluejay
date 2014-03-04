using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Lisa.Zuma.BlueJay.IOS.Models;

namespace Lisa.Zuma.BlueJay.IOS
{
	public partial class SummaryViewController : UIViewController
	{
		private TableHelper tableHelper;
		private TableSource tableSource;

		public SummaryViewController () : base ("SummaryViewController", null)
		{
			tableHelper = new TableHelper ();
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}

		public void RowClicked_handler (object sender, RowClickedEventArgs e){
			TimelineViewController timeLineViewController = new TimelineViewController ();
			this.NavigationController.PushViewController (timeLineViewController, true);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.NavigationItem.SetHidesBackButton (true, false);



			var sourceFromTablehelper = tableHelper.DataForList();

			sourceFromTablehelper.RowClicked += RowClicked_handler;
			tblCell.Source = sourceFromTablehelper;

			if(!Reachability.IsHostReachable("http://google.com")) {
				new UIAlertView("Offline modus", "De iPad heeft geen verbinding met een WIFI netwerk, video en plaatjes zijn niet beschikbaar.", null, "ok", null).Show(); 
			}
		}


	}
}

