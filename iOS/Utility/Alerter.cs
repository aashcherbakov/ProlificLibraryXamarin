using UIKit;
using System;
namespace ProlificLibrary.iOS
{
	public class Alerter
	{
		public Alerter() { }

		#region Static Methods
		public static UIAlertController PresentOKAlert(string title, string description, UIViewController controller)
		{
			var alert = UIAlertController.Create(title, description, UIAlertControllerStyle.Alert);
			alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, (action) => { }));
			return alert;
		}
		#endregion	
	}
}
