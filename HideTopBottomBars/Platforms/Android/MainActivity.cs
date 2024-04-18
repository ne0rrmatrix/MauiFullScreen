using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using AndroidX.Core.View;
using Google.Android.Material.Internal;
using HideTopBottomBars.CustomControls;

namespace HideTopBottomBars;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density | ConfigChanges.ColorMode)]
public class MainActivity : MauiAppCompatActivity
{
	readonly CustomControl customControl = new();
	protected override void OnCreate(Bundle? savedInstanceState)
	{
		base.OnCreate(savedInstanceState);
		customControl.RegularStatusBar();
	}
}
