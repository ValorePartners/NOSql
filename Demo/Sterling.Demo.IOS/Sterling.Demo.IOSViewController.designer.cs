// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Sterling.Demo.IOS
{
	[Register ("Sterling_Demo_IOSViewController")]
	partial class Sterling_Demo_IOSViewController
	{
		[Outlet]
		UIKit.UIButton btnStart { get; set; }

		[Outlet]
		UIKit.UILabel lblCount { get; set; }

		[Outlet]
		UIKit.UILabel lblResult { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnStart != null) {
				btnStart.Dispose ();
				btnStart = null;
			}

			if (lblCount != null) {
				lblCount.Dispose ();
				lblCount = null;
			}

			if (lblResult != null) {
				lblResult.Dispose ();
				lblResult = null;
			}
		}
	}
}
