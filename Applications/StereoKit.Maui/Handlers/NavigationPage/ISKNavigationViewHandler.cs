﻿using Microsoft.Maui;
using PlatformView = StereoKit.Maui.Views.Frame;

namespace StereoKit.Maui.Handlers
{
	public partial interface ISKNavigationViewHandler : IViewHandler
	{
		new IStackNavigationView VirtualView { get; }
		new PlatformView PlatformView { get; }
	}
}