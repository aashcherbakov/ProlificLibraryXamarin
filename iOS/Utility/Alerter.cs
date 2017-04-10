using UIKit;
using System;

namespace ProlificLibrary.iOS
{
	public class Alerter
	{
		public Alerter() { }

		#region Static Methods
		public static void PresentOKAlert(
            string title, 
            string description,
            UIViewController controller, 
            Action<UIAlertAction> okAction = null)
		{
			var alert = UIAlertController.Create(title, description, UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, okAction));
            controller.PresentViewController(alert, true, null);
		}
		#endregion	
	}
}
