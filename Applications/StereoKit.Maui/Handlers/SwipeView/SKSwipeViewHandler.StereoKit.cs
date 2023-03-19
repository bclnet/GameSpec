using Microsoft.Maui;
using System;

namespace StereoKit.Maui.Handlers
{
	public partial class SKSwipeViewHandler : SKViewHandler<ISwipeView, object>
	{
		protected override object CreatePlatformView() => throw new NotImplementedException();

		public static void MapContent(ISKSwipeViewHandler handler, ISwipeView view) { }

		public static void MapSwipeTransitionMode(ISKSwipeViewHandler handler, ISwipeView swipeView) { }

		public static void MapRequestOpen(ISKSwipeViewHandler handler, ISwipeView swipeView, object? args)
		{
			if (args is not SwipeViewOpenRequest request)
				return;
		}

		public static void MapRequestClose(ISKSwipeViewHandler handler, ISwipeView swipeView, object? args)
		{
			if (args is not SwipeViewCloseRequest request)
				return;
		}

		public static void MapLeftItems(ISKSwipeViewHandler handler, ISwipeView view) { }
		public static void MapTopItems(ISKSwipeViewHandler handler, ISwipeView view) { }
		public static void MapRightItems(ISKSwipeViewHandler handler, ISwipeView view) { }
		public static void MapBottomItems(ISKSwipeViewHandler handler, ISwipeView view) { }
	}
}