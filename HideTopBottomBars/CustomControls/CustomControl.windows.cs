using HideTopBottomBars.Extensions;
using HideTopBottomBars.Interfaces;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.UI.WindowManagement;
using WinRT.Interop;
using AppWindow = Microsoft.UI.Windowing.AppWindow;

namespace HideTopBottomBars.CustomControls;

class CustomControl : ICustomControl
{
	static readonly AppWindow appWindow = GetAppWindowForCurrentWindow();

	/// <summary>
	/// Gets the presented page.
	/// </summary>
	protected static Page CurrentPage =>
		PageExtensions.GetCurrentPage(Application.Current?.MainPage ?? throw new InvalidOperationException($"{nameof(Application.Current.MainPage)} cannot be null."));

	/// <inheritdoc/>
	public void Hide()
	{
		PageExtensions.SetBarStatus(false);

		appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
	}

	/// <inheritdoc/>
	public void Show()
	{
		PageExtensions.SetBarStatus(true);

		appWindow.SetPresenter(AppWindowPresenterKind.Default);
	}
	static AppWindow GetAppWindowForCurrentWindow()
	{
		// let's cache the CurrentPage here, since the user can navigate or background the app
		// while this method is running
		var currentPage = CurrentPage;

		if (currentPage?.GetParentWindow().Handler.PlatformView is not MauiWinUIWindow window)
		{
			throw new InvalidOperationException($"{nameof(window)} cannot be null.");
		}

		var handle = WindowNative.GetWindowHandle(window);
		var id = Win32Interop.GetWindowIdFromWindow(handle);

		return AppWindow.GetFromWindowId(id);
	}
}
