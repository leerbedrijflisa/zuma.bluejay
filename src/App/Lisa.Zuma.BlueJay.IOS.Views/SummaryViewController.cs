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
	
		public SummaryViewController () : base ("SummaryViewController", null)
		{
			tableHelper = new TableHelper ();
			dataHelper = new DataHelper ();
		}

		public void RowClicked_handler (object sender, RowClickedEventArgs e)
		{
			this.NavigationController.PushViewController (new TimelineViewController(), true);
		}
			
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			NavigationItem.SetHidesBackButton (true, false);
			var dossiers = dataHelper.GetDossierDatas();

			jumpToNextView = (dossiers.Count == 1);
			if (jumpToNextView) 
			{
				dataHelper.insertNewCurrentDossier (dossiers.First().DossierId);
				// Return to prevent table from being filled! 
				return;
			}

			//Create source for UItableview
			var sourceFromTablehelper = tableHelper.CreateSource(dossiers, d => d.DossierId, d => d.Name);

			sourceFromTablehelper.RowClicked += RowClicked_handler;
			tblCell.Source = sourceFromTablehelper;
		}
			
		public override void ViewDidAppear (bool animated)
		{
			//Check if there is connection with the internet, if not show a alert messagebox. (just a tiny feature for the future, not relevant for now.)
			Reachability.getStatus ();

			this.NavigationController.SetNavigationBarHidden (true, true);

			// If jumpToNextView equals true, only one dossier is available and open the timeline directly.
			if (jumpToNextView){
				NavigationController.PushViewController (new TimelineViewController (), false);
			}
		}

		private bool jumpToNextView = false;
		private TableHelper tableHelper;
		private DataHelper dataHelper;
	}
}

