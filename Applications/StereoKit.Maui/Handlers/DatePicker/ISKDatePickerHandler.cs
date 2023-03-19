﻿using Microsoft.Maui;
using PlatformView = StereoKit.Maui.Controls.DatePicker;

namespace StereoKit.Maui.Handlers
{
	public partial interface ISKDatePickerHandler : IViewHandler
	{
		new IDatePicker VirtualView { get; }
		new PlatformView PlatformView { get; }
	}
}