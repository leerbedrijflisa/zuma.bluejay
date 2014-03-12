using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Lisa.Zuma.BlueJay.IOS.Views
{

	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{

		UIWindow window;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			this.window = new UIWindow (UIScreen.MainScreen.Bounds);

			loginViewController LoginViewController = new loginViewController ();

			var RootNavigationController = new UINavigationController ();
			RootNavigationController.PushViewController (LoginViewController, true);

			this.window.RootViewController = RootNavigationController;
			this.window.MakeKeyAndVisible ();

			return true;
		}
	}
}

