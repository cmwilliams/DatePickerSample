using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DatePickerSample.Droid
{
	[Activity (Label = "DatePickerSample.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		public static MobileServiceClient MobileService = new MobileServiceClient(
			"https://howmanydaysuntil.azure-mobile.net/",
			"qZkDObDIDfwmNjbCKhqgoAhurTGjiH56"
		);
		
		private static void populateAzureDatabase()
		{
			var items = new List<ImportantDate> () {
				new ImportantDate { ImpDate = new DateTime (2015, 7, 3), Description = "Independence Day" },
				new ImportantDate { ImpDate = new DateTime (2015, 9, 7), Description = "Labor Day" },
				new ImportantDate { ImpDate = new DateTime (2015, 11, 26), Description = "Thanksgiving Day" },
				new ImportantDate { ImpDate = new DateTime (2015, 11, 27), Description = "Day After Thanksgiving" },
				new ImportantDate { ImpDate = new DateTime (2015, 12, 24), Description = "Christmas Eve" },
				new ImportantDate { ImpDate = new DateTime (2015, 12, 25), Description = "Christmas Day" }
			};

			foreach (var importantDate in items) 
			{
				MobileService.GetTable<ImportantDate>().InsertAsync(importantDate);
			}

		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

			//populateAzureDatabase ();

			LoadApplication (new App ());
		}
	}
}

