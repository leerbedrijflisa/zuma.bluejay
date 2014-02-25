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

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.NavigationItem.SetHidesBackButton (true, false);

			tblCell.Source = tableHelper.DataForList();

			tableHelper.DataForList().RowClicked += tableSource_RowClicked;
		}

		
		public void tableSource_RowClicked (object sender, RowClickedEventArgs e)
		{
			TimelineViewController timeLineViewController = new TimelineViewController ();
			this.NavigationController.PushViewController (timeLineViewController, true);
		}
	}
}

