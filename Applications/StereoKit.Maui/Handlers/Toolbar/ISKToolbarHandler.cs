﻿using Microsoft.Maui;
using PlatformView = StereoKit.Maui.Controls.Toolbar;

namespace StereoKit.Maui.Handlers
{
	public partial interface ISKToolbarHandler : IElementHandler
	{
		new IToolbar VirtualView { get; }
		new PlatformView PlatformView { get; }
	}
}