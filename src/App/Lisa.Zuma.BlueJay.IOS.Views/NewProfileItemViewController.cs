using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Lisa.Zuma.BlueJay.IOS.Models;
using System.Collections.Generic;

namespace Lisa.Zuma.BlueJay.IOS.Views
{
	public partial class NewProfileItemViewController : UIViewController
	{
		public NewProfileItemViewController () : base ("NewProfileItemViewController", null)
		{
			dataHelper = new DataHelper ();
			eventHandlers = new EventHandlers (this);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			InitializeUI ();
			btnAddProfileItem.TouchUpInside += eventHandlers.Create(CreateNewProfileItem);
		}

		private void CreateNewProfileItem()
		{
			if(!string.IsNullOrEmpty(txtTitle.Text) && !string.IsNullOrEmpty(txtContent.Text))
			{
				dataHelper.AddDossierDetail (txtTitle.Text, txtContent.Text, InsertProfileItem);
			}
			else
			{
				new UIAlertView("Lege invoervelden", "vul alle velden in !"
					, null, "probeer opniew...", null).Show();
			}
		}

		private void InsertProfileItem()
		{
			InvokeOnMainThread(() => {
				dataHelper.InsertProfileItem(txtTitle.Text, txtContent.Text);
				NavigationController.PushViewController(new ProfileViewController(), true);
			});
		}

		private void InitializeUI()
		{
			UITextView textView = new UITextView ();

			textView.Frame = new RectangleF(txtContent.Frame.Y, txtContent.Frame.X, txtContent.Frame.Width, 200);
			textView.BackgroundColor = UIColor.FromRGB(242, 242, 242);

			SetupToolbar();
			CreateDoneButton ();
			SetupPicker ();
		}

		private void SetupToolbar()
		{
			toolbar = new UIToolbar();
			toolbar.BarStyle = UIBarStyle.Black;
			toolbar.Translucent = true;
			toolbar.SizeToFit();
			txtTitle.InputAccessoryView = toolbar;
		}

		private void CreateDoneButton()
		{
			UIBarButtonItem doneButton = new UIBarButtonItem("Done", UIBarButtonItemStyle.Done, (s, e) => {
				txtTitle.Text = selectedCat;
				txtTitle.ResignFirstResponder();
			});

			toolbar.SetItems(new UIBarButtonItem[]{doneButton}, true);
		}

		private void SetupPicker()
		{
			PickerModel model = new PickerModel(dataHelper.picker());

			picker = new UIPickerView();
			picker.ShowSelectionIndicator = true;
			picker.Model = model;

			txtTitle.InputView = picker;

			model.PickerChanged += (sender, e) => {
				selectedCat = e.SelectedValue;
			};
		}

		private DataHelper dataHelper;
		private string selectedCat;
		private UIPickerView picker;
		private UIToolbar toolbar;
		private EventHandlers eventHandlers;
	
	}
}

