using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Lisa.Zuma.BlueJay.IOS.Models;
using System.Collections.Generic;

namespace Lisa.Zuma.BlueJay.IOS
{
	public partial class NewProfileItemViewController : UIViewController
	{
		private TableHelper tableHelper;
		private ProfileViewController profileViewController;
		private DataHelper dataHelper;
		private string selectedCat;
		private PickerModel picker_model;
		private UIPickerView picker;
		private UIToolbar toolbar;

		public NewProfileItemViewController () : base ("NewProfileItemViewController", null)
		{
			tableHelper = new TableHelper();
			profileViewController = new ProfileViewController ();
			dataHelper = new DataHelper ();
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

			this.SetupPicker ();
			txtTitle.InputView = picker;
			txtTitle.InputAccessoryView = toolbar;
		}


		private void SetupPicker()
		{
			// Setup the picker and model
			PickerModel model = new PickerModel(dataHelper.picker());
			model.PickerChanged += (sender, e) => {
				this.selectedCat = e.SelectedValue;
			};

			picker = new UIPickerView();
			picker.ShowSelectionIndicator = true;
			picker.Model = model;

			// Setup the toolbar
			toolbar = new UIToolbar();
			toolbar.BarStyle = UIBarStyle.Black;
			toolbar.Translucent = true;
			toolbar.SizeToFit();

			// Create a 'done' button for the toolbar and add it to the toolbar
			UIBarButtonItem doneButton = new UIBarButtonItem("Done", UIBarButtonItemStyle.Done,
			                                                 (s, e) => {
				this.txtTitle.Text = selectedCat;
				this.txtTitle.ResignFirstResponder();
			});
			toolbar.SetItems(new UIBarButtonItem[]{doneButton}, true);

		}
	
	}
}

