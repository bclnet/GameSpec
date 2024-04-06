﻿using StereoKit.Controls.Controls;
using StereoKit.UIX.Controls;
using System;
using MRect = Microsoft.Maui.Graphics.Rect;
using MSize = Microsoft.Maui.Graphics.Size;
using TSize = System.Drawing.SizeF;
using Microsoft.Maui;

namespace StereoKit.Maui.Platform
{
	public class LayoutViewGroup : ViewGroup, IMeasurable
	{
		IView _virtualView;
		MSize _measureCache;

		bool _needMeasureUpdate;
		internal int IsLayoutUpdating { get; set; } = 0;

		public LayoutViewGroup(IView view)
		{
			_virtualView = view;
			LayoutUpdated += OnLayoutUpdated;
		}

		public Func<double, double, MSize>? CrossPlatformMeasure { get; set; }
		public Func<MRect, MSize>? CrossPlatformArrange { get; set; }

		public void SetNeedMeasureUpdate()
		{
			_needMeasureUpdate = true;
			MarkChanged();
			if (IsLayoutUpdating == 0)
				Layout.RequestLayout();
		}

        public void ClearNeedMeasureUpdate()
			=> _needMeasureUpdate = false;

		public TSize Measure(double availableWidth, double availableHeight)
			=> InvokeCrossPlatformMeasure(availableWidth.ToScaledDP(), availableHeight.ToScaledDP()).ToPixel();

		public bool InputTransparent { get; set; } = false;

		protected override bool HitTest(object touch)
			=> !InputTransparent;

		public MSize InvokeCrossPlatformMeasure(double availableWidth, double availableHeight)
		{
			if (CrossPlatformMeasure == null)
				return Microsoft.Maui.Graphics.Size.Zero;

			var measured = CrossPlatformMeasure(availableWidth, availableHeight);
			if (measured != _measureCache && _virtualView?.Parent is IView parentView)
				parentView?.InvalidateMeasure();
			_measureCache = measured;
			ClearNeedMeasureUpdate();
			return measured;
		}

		void OnLayoutUpdated(object? sender, EventArgs e)
		{
			IsLayoutUpdating++;
			var platformGeometry = this.GetBounds().ToDP();

			if (_needMeasureUpdate || _measureCache != platformGeometry.Size)
				InvokeCrossPlatformMeasure(platformGeometry.Width, platformGeometry.Height);
			if (platformGeometry.Width > 0 && platformGeometry.Height > 0)
			{
				platformGeometry.X = 0;
				platformGeometry.Y = 0;
				CrossPlatformArrange?.Invoke(platformGeometry);
			}
			IsLayoutUpdating--;
		}
	}
}
