using System;
using Ordersystem.Functions;
using UIKit;

namespace Ordersystem.iOS
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            var cm = new CommunicationManager();
            btn_Log_In.TouchDown += delegate
            {
                if (cm.ValidSocialSecurityNumber(TxtBox_CPRTextField.Text))
                {
                    var board = UIStoryboard.FromName("MainWindow", null);
                    var ctrl = board.InstantiateViewController("MainWindow");
                    ctrl.ModalTransitionStyle = UIModalTransitionStyle.FlipHorizontal;
                    //this.PresentedViewController (ctrl, true, null);
                }
            };
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}