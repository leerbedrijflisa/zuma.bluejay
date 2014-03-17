using System;
using MonoTouch.UIKit;

namespace Lisa.Zuma.BlueJay.IOS
{
	public class EventHandlers
	{
		public EventHandlers(UIViewController fromView)
		{
			parentView = fromView;
		}

		public EventHandler Create(Action action)
		{
			return (object sender, EventArgs args) => {
				action();
			};
		}

		public EventHandler CreatePush<T>() where T: UIViewController, new()
		{
			return (object sender, EventArgs e) => {
				parentView.NavigationController.PushViewController (new T(), true);
			};
		}

		private UIViewController parentView;
	}
}

