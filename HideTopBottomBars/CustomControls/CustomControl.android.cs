using Platform = Microsoft.Maui.ApplicationModel.Platform;
using HideTopBottomBars.Interfaces;
using AndroidX.Core.View;
using Android.Views;
using Android.Content.Res;
using Activity = Android.App.Activity;
using PageExtensions = HideTopBottomBars.Extensions.PageExtensions;

namespace HideTopBottomBars.CustomControls;

public class CustomControl : ICustomControl
{
	int defaultSystemUiVisibility;
	public CustomControl()
    {
	}

	public void Hide()
	{
		SetSystemBarsVisibility(true);
		TransparentStatusBar();
		PageExtensions.SetBarStatus(false);
	}

	public void Show()
	{
		SetSystemBarsVisibility(false);
		RegularStatusBar();
		PageExtensions.SetBarStatus(true);

	}

	public void TransparentStatusBar()
	{
		var activity = Platform.CurrentActivity;
		if (activity == null || activity.Window == null)
		{
			return;
		}
		activity.Window.ClearFlags(WindowManagerFlags.TranslucentStatus);
		activity.Window.ClearFlags(WindowManagerFlags.TranslucentNavigation);
		activity.Window.SetStatusBarColor(Android.Graphics.Color.Transparent);
		activity.Window.SetNavigationBarColor(Android.Graphics.Color.Transparent);
	}

	public void RegularStatusBar()
	{
		var activity = Platform.CurrentActivity;
		if (activity == null || activity.Window == null)
		{
			return;
		}
		activity.Window.SetStatusBarColor(Android.Graphics.Color.Blue);
		activity.Window.SetNavigationBarColor(Android.Graphics.Color.Blue);
	}

	void SetSystemBarsVisibility(bool setFullScreen)
	{
		var (_, currentWindow, _, _) = VerifyAndRetrieveCurrentWindowResources();

		var windowInsetsControllerCompat = WindowCompat.GetInsetsController(currentWindow, currentWindow.DecorView);

		var barTypes = WindowInsetsCompat.Type.StatusBars()
			| WindowInsetsCompat.Type.SystemBars()
			| WindowInsetsCompat.Type.DisplayCutout()
			| WindowInsetsCompat.Type.NavigationBars();

		if (setFullScreen)
		{
			if (OperatingSystem.IsAndroidVersionAtLeast(30))
			{
				WindowCompat.SetDecorFitsSystemWindows(currentWindow, false);
				if (currentWindow.Attributes is not null)
				{
					currentWindow.Attributes.LayoutInDisplayCutoutMode = LayoutInDisplayCutoutMode.ShortEdges;
				}
				var windowInsets = currentWindow.DecorView.RootWindowInsets;
				if (windowInsets is not null)
				{
					currentWindow.InsetsController?.Hide(WindowInsets.Type.SystemBars());
					currentWindow.InsetsController?.Hide(WindowInsets.Type.StatusBars());
					currentWindow.InsetsController?.Hide(WindowInsets.Type.NavigationBars());
					currentWindow.InsetsController?.Hide(WindowInsets.Type.DisplayCutout());
				}
			}
			else
			{
				defaultSystemUiVisibility = (int)currentWindow.DecorView.SystemUiFlags;

				currentWindow.DecorView.SystemUiFlags = currentWindow.DecorView.SystemUiFlags
					| SystemUiFlags.LayoutStable
					| SystemUiFlags.LayoutHideNavigation
					| SystemUiFlags.LayoutFullscreen
					| SystemUiFlags.HideNavigation
					| SystemUiFlags.Fullscreen
					| SystemUiFlags.Immersive;
				windowInsetsControllerCompat.Hide(barTypes);
			}

			windowInsetsControllerCompat.SystemBarsBehavior = WindowInsetsControllerCompat.BehaviorShowTransientBarsBySwipe;
		}
		else
		{
			if (OperatingSystem.IsAndroidVersionAtLeast(30))
			{
				WindowCompat.SetDecorFitsSystemWindows(currentWindow, true);
				if (currentWindow.Attributes is not null)
				{
					currentWindow.Attributes.LayoutInDisplayCutoutMode = LayoutInDisplayCutoutMode.ShortEdges;
				}
				currentWindow.InsetsController?.Show(WindowInsets.Type.SystemBars());
				currentWindow.InsetsController?.Show(WindowInsets.Type.StatusBars());
				currentWindow.InsetsController?.Show(WindowInsets.Type.NavigationBars());
				currentWindow.InsetsController?.Show(WindowInsets.Type.DisplayCutout());
				
			}
			else
			{
				currentWindow.DecorView.SystemUiFlags = (SystemUiFlags)defaultSystemUiVisibility;
				windowInsetsControllerCompat.Show(barTypes);
			}

			windowInsetsControllerCompat.SystemBarsBehavior = WindowInsetsControllerCompat.BehaviorDefault;
		}
	}

	static (Activity CurrentActivity, Android.Views.Window CurrentWindow, Resources CurrentWindowResources, Configuration CurrentWindowConfiguration) VerifyAndRetrieveCurrentWindowResources()
	{
		// Ensure current activity and window are available
		if (Platform.CurrentActivity is not Activity currentActivity)
		{
			throw new InvalidOperationException("CurrentActivity cannot be null when the FullScreen button is tapped");
		}
		if (currentActivity.Window is not Android.Views.Window currentWindow)
		{
			throw new InvalidOperationException("CurrentActivity Window cannot be null when the FullScreen button is tapped");
		}

		if (currentActivity.Resources is not Resources currentResources)
		{
			throw new InvalidOperationException("CurrentActivity Resources cannot be null when the FullScreen button is tapped");
		}

		if (currentResources.Configuration is not Configuration configuration)
		{
			throw new InvalidOperationException("CurrentActivity Configuration cannot be null when the FullScreen button is tapped");
		}

		return (currentActivity, currentWindow, currentResources, configuration);
	}
}
