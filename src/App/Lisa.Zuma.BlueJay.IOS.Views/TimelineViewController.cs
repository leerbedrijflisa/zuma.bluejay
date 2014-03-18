using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Lisa.Zuma.BlueJay.IOS.Models;
using System.IO;
using GCDiscreetNotification;

namespace Lisa.Zuma.BlueJay.IOS
{
	public partial class TimelineViewController : UIViewController
	{

		public TimelineViewController () : base ("TimelineViewController", null)
		{
			templateParser = new HTMLTemplates ();
			dataHelper = new DataHelper ();
			newNoteViewController = new NewNoteViewController(this);
			eventHandlers = new EventHandlers (this);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			btnNewNote.TouchUpInside += eventHandlers.Create(CreateNote);
			btnEditProfile.TouchUpInside += eventHandlers.CreatePush<ProfileViewController>();
			btnRefresh.TouchUpInside += eventHandlers.Create(UpdateList);

			dataHelper.SyncNotesDataByID (1, UpdateList);
		}

		public void UpdateList()
		{
			var parsedHTML = templateParser.ParseTimeLine(1);
			string contentDirectoryPath = Path.Combine (NSBundle.MainBundle.BundlePath, "HTML/");

			InvokeOnMainThread (() => {
				wvTimeline.LoadHtmlString(parsedHTML, new NSUrl(contentDirectoryPath, true));
				wvTimeline.ScalesPageToFit = true;
			});
		}

		public void CreateNote()
		{
			InitializeNewNoteUI();
			NavigationController.SetNavigationBarHidden (false, true);
		}


		//This function will be temporary, depending on the UI.
		public void HideNoteCreator()
		{
			label.Hidden = true;
			this.NavigationController.SetNavigationBarHidden (false, true);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			this.NavigationController.SetNavigationBarHidden (true, true);
			UpdateList ();
		}

		private void InitializeNewNoteUI()
		{
			label = new UIButton ();
			label.Frame = new RectangleF (0, 0, View.Frame.Width, View.Frame.Height);
			label.BackgroundColor = UIColor.Black;
			label.Alpha = 0.5f;
			View.Add (label);

			var x = View.Frame.Width / 2 -300;
			var y = View.Frame.Height / 2 - 300;

			UIView OverlayView = new UIView ();
			OverlayView.Frame = new RectangleF(x, y, 400, 600);
			OverlayView.Add (newNoteViewController.View);

			View.Add (OverlayView);
		}

		private HTMLTemplates templateParser;
		private UIView label;
		private DataHelper dataHelper;
		private NewNoteViewController newNoteViewController;
		private EventHandlers eventHandlers;


	}
}

