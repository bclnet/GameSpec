using Microsoft.Extensions.Logging;
using StereoKit.Maui;

namespace Microsoft.Maui.Handlers
{
    public partial class SKApplicationHandler
	{
		internal const string TerminateCommandKey = "Terminate";

		public static IPropertyMapper<IApplication, SKApplicationHandler> Mapper = new PropertyMapper<IApplication, SKApplicationHandler>(ElementMapper);

		public static CommandMapper<IApplication, SKApplicationHandler> CommandMapper = new(ElementCommandMapper)
		{
			[TerminateCommandKey] = MapTerminate,
#pragma warning disable CA1416 // TODO: should we propagate SupportedOSPlatform("ios13.0") here
			[nameof(IApplication.OpenWindow)] = MapOpenWindow,
			[nameof(IApplication.CloseWindow)] = MapCloseWindow,
#pragma warning restore CA1416
		};

		ILogger<SKApplicationHandler>? _logger;

		public SKApplicationHandler() : base(Mapper, CommandMapper) { }

		public SKApplicationHandler(IPropertyMapper? mapper) : base(mapper ?? Mapper, CommandMapper) { }

		public SKApplicationHandler(IPropertyMapper? mapper, CommandMapper? commandMapper) : base(mapper ?? Mapper, commandMapper ?? CommandMapper) { }

		ILogger? Logger =>
			_logger ??= MauiContext?.Services.CreateLogger<SKApplicationHandler>();
	}
}