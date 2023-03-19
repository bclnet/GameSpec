﻿using Microsoft.Maui;
using StereoKit.Maui.Controls;
using System;
using System.Globalization;

namespace StereoKit.Maui.Platform
{
    public static class TimePickerExtensions
	{
		static readonly string s_defaultFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern;

		public static void UpdateFormat(this Entry platformTimePicker, ITimePicker timePicker)
			=> UpdateTime(platformTimePicker, timePicker);

		public static void UpdateTime(this Entry platformTimePicker, ITimePicker timePicker)
			// Xamarin using DateTime formatting (https://developer.xamarin.com/api/property/Xamarin.Forms.TimePicker.Format/)
			=> platformTimePicker.Text = new DateTime(timePicker.Time.Ticks).ToString(timePicker.Format ?? s_defaultFormat);
	}
}