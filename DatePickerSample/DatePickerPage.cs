using System;
using Xamarin.Forms;
using Microsoft.WindowsAzure.MobileServices;

namespace DatePickerSample
{
	public class DatePickerPage : ContentPage
	{
		public static MobileServiceClient MobileService = new MobileServiceClient(
			"https://howmanydaysuntil.azure-mobile.net/",
			"qZkDObDIDfwmNjbCKhqgoAhurTGjiH56"
		);

		public DatePickerPage ()
		{
			//Header label
			Label header = new Label
			{
				Text = "How Many Days Until",
				FontSize = 30,
				HorizontalOptions = LayoutOptions.Center
			};

			//Result label
			Label result = new Label
			{
				Text = string.Empty,
				FontSize = 18,
				HorizontalOptions = LayoutOptions.CenterAndExpand
			};
					
			//Date Picker
			DatePicker datePicker = new DatePicker
			{
				Format = "D",
				MinimumDate = DateTime.Now.AddDays(1),
			};

			//When a date is selected from the date picker
			datePicker.DateSelected += (s, e) => 
			{
				var selectedDate = (s as DatePicker).Date;
				result.Text = Helper.FormatDayHelper(selectedDate);
			};

			// Accomodate iPhone status bar. We don't have a mac here but if we did.....
			this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

			//Button to load dates
			Button loadButton = new Button {
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				FontSize = 20,
				Text = "Load Data From Azure"
			};
					
			//List view to hold the dates
			var listView = new ListView
			{
				RowHeight = 50
					
			};
					
			//Load the important dates from Azure
			loadButton.Clicked += async (s, e) => 
			{
				var importantDates = (from impDate in MobileService.GetTable<ImportantDate>()
					orderby impDate.ImpDate
					select new { 
						ImpDate = impDate.ImpDate.ToString ("d"), 
						impDate.Description
					});
						
				var list = (await importantDates.ToListAsync());


				//Set up List View Template
				var template = new DataTemplate (typeof (TextCell));
				template.SetBinding (TextCell.TextProperty, "ImpDate");
				template.SetBinding (TextCell.DetailProperty, "Description");
				listView.ItemTemplate = template;

				//Bind the dates to the list view
				listView.ItemsSource = list;
			};




			// Build the page.
			this.Content = new StackLayout {
				Spacing = 25,
				Children = {
					header,
					datePicker,
					result,
					loadButton,
					listView
				}
			};
		}
	}
}


