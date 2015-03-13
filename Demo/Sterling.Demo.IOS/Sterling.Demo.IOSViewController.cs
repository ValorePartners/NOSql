using System;
using UIKit;
using Sterling.Demo.Shared;

namespace Sterling.Demo.IOS
{
	public partial class Sterling_Demo_IOSViewController : UIViewController
	{
		MainViewModel vm;

		public Sterling_Demo_IOSViewController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}



		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			vm = new MainViewModel();
			vm.PropertyChanged+=(e,a)=>{
				switch(a.PropertyName){
				case "Counter":
					lblCount.Text = vm.Counter;
					break;
				case "Message":
					lblResult.Text= vm.Message;
					break;
				}
			};
			btnStart.TouchUpInside+=(e,a)=>{
				vm.LoadData();
			};
		}

	
	}
}

