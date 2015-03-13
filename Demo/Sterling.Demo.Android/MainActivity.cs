using Android.App;
using Android.Widget;
using Android.OS;
using Sterling.Demo.Shared;
using Android.Runtime;
using System;


namespace Sterling.Demo.Android
{
	[Application]
	public class MainApp : Application
	{
		public MainApp(IntPtr handle, JniHandleOwnership transfer)
			: base(handle, transfer)
		{

		}
		public override void OnCreate(){
			AppDb.Init();
		}


	}

	[Activity (Label = "Sterling.Demo.Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;
		MainViewModel vm;
		TextView txtDisplay;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);
			txtDisplay = FindViewById<TextView> (Resource.Id.textView1);
			Button button = FindViewById<Button> (Resource.Id.myButton);
			vm = new MainViewModel();
			vm.PropertyChanged+=(e,a)=>{
				switch(a.PropertyName){
				case "Counter":
					button.Text = vm.Counter;
					break;
				case "Message":
					txtDisplay.Text= vm.Message;
					break;
				}
			};


			
			button.Click += delegate {
				button.Text = string.Format ("{0} clicks!", count++);
				vm.LoadData();
			};
		}
	}
}


