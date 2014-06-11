using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Lisa.Zuma.BlueJay.IOS.Models;
using System.IO;

namespace Lisa.Zuma.BlueJay.IOS.Views
{
	public partial class TimelineViewController : UIViewController
	{

		public TimelineViewController () : base ("TimelineViewController", null)
		{
			templateParser = new HTMLTemplates ();
			dataHelper = new DataHelper ();
			newNoteViewController = new NewNoteViewController(this);
			eventHandlers = new EventHandlers (this);
			backgroundColor = new UIButton ();
			overlayView = new UIView ();
			dossierId = dataHelper.getCurrentDossier();
			cameraOptionsPopOver = new UIPopoverController(new ChoiceCameraOptionViewController()); 
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			backgroundColor.TouchUpInside += eventHandlers.Create(HideNoteCreator);
			btnNewNote.TouchUpInside += eventHandlers.Create(InitializeNewNoteUI);
			btnEditProfile.TouchUpInside += eventHandlers.CreatePush<ProfileViewController>();
			btnRefresh.TouchUpInside += eventHandlers.Create (RefreshButtonUpdateList);
			btnLogout.TouchUpInside += eventHandlers.CreatePush<loginViewController>();
			btnCamera.TouchUpInside += eventHandlers.Create (ShowCameraPopOver);
			lblTitle.Text = dataHelper.GetCurrentDossierDataName();
		}

		public void RefreshButtonUpdateList() 
		{
			this.ViewWillAppear (false);
		}

		public void ShowCameraPopOver()
		{
			cameraOptionsPopOver.PopoverContentSize = new SizeF (277f, 79f);
			cameraOptionsPopOver.PresentFromRect (btnCamera.Frame, this.View, UIPopoverArrowDirection.Up, true);
		}

		public void UpdateList()
		{
			var parsedHTML = templateParser.ParseTimeLine(dossierId);
			string contentDirectoryPath = Path.Combine (NSBundle.MainBundle.BundlePath, "HTML/");

			//reloading the webview is an UI event and is only allowd in the mainthread. 
			InvokeOnMainThread (() => {
				wvTimeline.LoadHtmlString(parsedHTML, new NSUrl(contentDirectoryPath, true));
				wvTimeline.ScalesPageToFit = true;
			});
		}

		//This function will be temporary, depending on the UI.
		public void HideNoteCreator()
		{
			dataHelper.DeleteAllDataElements ();
			newNoteViewController.UpdateButtonNumber ();
			backgroundColor.Hidden = true;
			overlayView.Hidden = true;
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			this.NavigationController.SetNavigationBarHidden (true, true);
			dataHelper.SyncNotesDataByID (dossierId, UpdateList);
		}
			
		private void InitializeNewNoteUI()
		{
			backgroundColor.Hidden = false;
			overlayView.Hidden = false;

			backgroundColor.Frame = new RectangleF (0, 0, View.Frame.Width, View.Frame.Height);
			backgroundColor.BackgroundColor = UIColor.Black;
			backgroundColor.Alpha = 0.5f;
			View.Add (backgroundColor);

			var x = View.Frame.Width / 2 - 210;
			var y = View.Frame.Height / 2 - 250;

			overlayView.Frame = new RectangleF(x, y, 423, 498);
			overlayView.Add (newNoteViewController.View);
			View.Add (overlayView);
		}

		private HTMLTemplates templateParser;
		private UIButton backgroundColor;
		private DataHelper dataHelper;
		private NewNoteViewController newNoteViewController;
		private EventHandlers eventHandlers;
		private UIView overlayView;
		private UIPopoverController cameraOptionsPopOver;
		public int dossierId;


	}
}

