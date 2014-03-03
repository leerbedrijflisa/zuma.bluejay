using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Lisa.Zuma.BlueJay.IOS.Models;

namespace Lisa.Zuma.BlueJay.IOS
{
	public partial class NewProfileItemViewController : UIViewController
	{
		private TableHelper tableHelper;
		private ProfileViewController profileViewController;
		private DataHelper dataHelper;
		private string selectedCat;

		public NewProfileItemViewController () : base ("NewProfileItemViewController", null)
		{
			tableHelper = new TableHelper();
			profileViewController = new ProfileViewController ();
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			UITextView textView = new UITextView ();

			textView.Frame = new RectangleF(txtContent.Frame.Y, txtContent.Frame.X, txtContent.Frame.Width, 200);
			textView.BackgroundColor = UIColor.FromRGB(242, 242, 242);

			btnAddProfileItem.TouchUpInside += (sender, e) => {
				tableHelper.InsertProfileItem(txtTitle.Text, txtContent.Text);
				this.NavigationController.PushViewController(profileViewController, true);
			};

			PickerModel model = new PickerModel(dataHelper);
			model.PickerChanged += (sender, e) => {
				this.selectedCat = e.SelectedValue;
			};

			UIPickerView picker = new UIPickerView();
			picker.ShowSelectionIndicator = true;
			picker.Model = model;	

		}
	
	}
}

