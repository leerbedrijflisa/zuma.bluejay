using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Lisa.Zuma.BlueJay.IOS.Models;

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

		public void RowClicked_handler (object sender, RowClickedEventArgs e){
			TimelineViewController timeLineViewController = new TimelineViewController ();
			this.NavigationController.PushViewController (timeLineViewController, true);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.NavigationItem.SetHidesBackButton (true, false);
			var dosiers = dataHelper.GetDosierDatas();

			var sourceFromTablehelper = tableHelper.CreateSource(dosiers, d => d.ID, d => d.Name);

			sourceFromTablehelper.RowClicked += RowClicked_handler;
			tblCell.Source = sourceFromTablehelper;

		}


	}
}

