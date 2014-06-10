using System;
using System.Linq;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Lisa.Zuma.BlueJay.IOS.Models;
using Lisa.Zuma.BlueJay.IOS.Data;
using System.Collections.Generic;

namespace Lisa.Zuma.BlueJay.IOS.Views
{
	public partial class ProfileViewController : UIViewController
	{
		public ProfileViewController () : base ("ProfileViewController", null)
		{
			dataHelper = new DataHelper ();
			eventHandlers = new EventHandlers (this);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			InitializeUI ();
		}

		public void InitializeUI()
		{
			btnNewItem.TouchUpInside += eventHandlers.CreatePush<NewProfileItemViewController> ();
			btnBack.TouchUpInside += eventHandlers.CreatePush<TimelineViewController> ();
		}

		public override void ViewWillAppear (bool animated)
		{
			var profileItems = dataHelper.GetProfileItems ();

			var itemGroup = new List<TableItemGroup> ();
			profileItems.ToList()
				.ForEach (p => {
					var group = itemGroup.FirstOrDefault (g => g.Name == p.Title);
					if (group == null) {
						group = new TableItemGroup ();
						group.Name = p.Title;
						itemGroup.Add(group);
					}

					group.Items.Add (new TableItem {
						Heading = p.Content
					});
				});
				
			tblProfile.Source = new ProfileTableSource (itemGroup);
		}

		private DataHelper dataHelper;
		private EventHandlers eventHandlers;
	}
}

