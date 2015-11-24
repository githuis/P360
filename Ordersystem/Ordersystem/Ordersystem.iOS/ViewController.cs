using System;
using System.Linq;
using UIKit;
using Ordersystem.Functions;
namespace Ordersystem.iOS
{
	public partial class ViewController : UIViewController
	{
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			CommunicationManager cm = new CommunicationManager();
			int i;
			btn_Log_In.TouchDown += delegate {
				if(int.TryParse(TxtBox_CPRTextField.Text,out i))
				{
					if(cm.ValidSocialSecurityNumber(i)){
						UIStoryboard board = UIStoryboard.FromName ("MainWindow", null);
						UIViewController ctrl = (UIViewController)board.InstantiateViewController ("MainWindow");
						ctrl.ModalTransitionStyle = UIModalTransitionStyle.FlipHorizontal;
						//this.PresentedViewController (ctrl, true, null);
					}
				}
			};
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

