using System;

namespace DatePickerSample
{
	public static class Helper
	{
		public static string FormatDayHelper (DateTime selectedDate)
		{
			var daysRemaining = (selectedDate - DateTime.Now.Date).Days;
			var formattedDateString = selectedDate.ToString ("D");
			return string.Format ("{0} {1} left until {2}", daysRemaining, daysRemaining > 1 ? "days" : "day", formattedDateString);
		}
	}
}

