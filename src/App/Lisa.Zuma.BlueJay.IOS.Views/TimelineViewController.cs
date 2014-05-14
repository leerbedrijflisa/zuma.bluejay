using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Lisa.Zuma.BlueJay.IOS.Models;
using System.IO;
using GCDiscreetNotification;

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
			OverlayView = new UIView ();
			DossierID = dataHelper.getCurrentDossier();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			backgroundColor.TouchUpInside += eventHandlers.Create(HideNoteCreator);

			btnNewNote.TouchUpInside += eventHandlers.Create(CreateNote);
			btnEditProfile.TouchUpInside += eventHandlers.CreatePush<ProfileViewController>();
			btnRefresh.TouchUpInside += eventHandlers.Create(UpdateList);

			dataHelper.SyncNotesDataByID (DossierID, UpdateList);
		}

		public void UpdateList()
		{
			var parsedHTML = templateParser.ParseTimeLine(DossierID);
			string contentDirectoryPath = Path.Combine (NSBundle.MainBundle.BundlePath, "HTML/");

			InvokeOnMainThread (() => {
				wvTimeline.LoadHtmlString(parsedHTML, new NSUrl(contentDirectoryPath, true));
				wvTimeline.ScalesPageToFit = true;
			});
		}

		public void CreateNote()
		{
			InitializeNewNoteUI();
		}


		//This function will be temporary, depending on the UI.
		public void HideNoteCreator()
		{
			backgroundColor.Hidden = true;
			OverlayView.Hidden = true;
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			this.NavigationController.SetNavigationBarHidden (true, true);
			UpdateList ();

			lblTitle.Text = dataHelper.GetCurrentDossierDataName();

		}

		private void InitializeNewNoteUI()
		{
			backgroundColor.Hidden = false;
			OverlayView.Hidden = false;

			backgroundColor.Frame = new RectangleF (0, 0, View.Frame.Width, View.Frame.Height);
			backgroundColor.BackgroundColor = UIColor.Black;
			backgroundColor.Alpha = 0.5f;
			View.Add (backgroundColor);

			var x = View.Frame.Width / 2 -300;
			var y = View.Frame.Height / 2 - 300;

			OverlayView.Frame = new RectangleF(x, y, 400, 600);
			OverlayView.Add (newNoteViewController.View);

			View.Add (OverlayView);
		}

		private HTMLTemplates templateParser;
		private UIButton backgroundColor;
		private DataHelper dataHelper;
		private NewNoteViewController newNoteViewController;
		private EventHandlers eventHandlers;
		private UIView OverlayView;
		public int DossierID;


	}
}

