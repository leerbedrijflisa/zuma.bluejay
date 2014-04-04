
using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Lisa.Zuma.BlueJay.IOS.Models;

namespace Lisa.Zuma.BlueJay.IOS.Views
{
	public partial class MediaSummaryViewController : UIViewController
	{
		public MediaSummaryViewController () : base ("MediaSummaryViewController", null)
		{
			tableHelper = new TableHelper ();
			dataHelper = new DataHelper ();

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			UpdateList ();
		}

		public void UpdateList()
		{
			var TemporaryMediaItems = dataHelper.GetSummaryOfMediaItems();

			var sourceFromTablehelper = tableHelper.CreateSource(TemporaryMediaItems, tmi => tmi.ID, tmi => tmi.fileName, tmi => tmi.Path);

			tblView.Source = sourceFromTablehelper;
		}

		public override void ViewDidAppear (bool animated)
		{
			UpdateList ();
			tblView.ReloadData ();
		}

		private DataHelper dataHelper;
		private TableHelper tableHelper;
	}
}

