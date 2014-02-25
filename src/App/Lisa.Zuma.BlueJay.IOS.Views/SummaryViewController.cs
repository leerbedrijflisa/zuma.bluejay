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

		}

		public void tableSource_RowClicked (object sender, RowClickedEventArgs e)
		{
			Console.WriteLine (e.ClickedItem.Id);
		}
	}
}

