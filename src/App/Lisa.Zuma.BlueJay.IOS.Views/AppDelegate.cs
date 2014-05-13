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

			var RootNavigationController = new UINavigationController ();
			RootNavigationController.PushViewController (new loginViewController(), true);

			this.window.RootViewController = RootNavigationController;
			this.window.MakeKeyAndVisible ();

			return true;
		}

		public override void DidEnterBackground (UIApplication application)
		{
			var RootNavigationController = new UINavigationController ();

			RootNavigationController.PushViewController (new LockScreenViewController(), true);

			this.window.RootViewController = RootNavigationController;

		}

		public override UIViewController GetViewController (UIApplication application, string[] restorationIdentifierComponents, NSCoder coder)
		{
			// NOTE: Don't call the base implementation on a Model class
			// see http://docs.xamarin.com/guides/ios/application_fundamentals/delegates,_protocols,_and_events
			throw new NotImplementedException ();
		}
	}
}

