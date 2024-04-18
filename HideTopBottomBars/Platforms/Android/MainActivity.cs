using Android.App;
using Android.Content.PM;
using Android.OS;
using HideTopBottomBars.CustomControls;
namespace HideTopBottomBars;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density | ConfigChanges.ColorMode)]
public class MainActivity : MauiAppCompatActivity
{
	protected override void OnCreate(Bundle? savedInstanceState)
	{
		base.OnCreate(savedInstanceState);
		if (Platform.AppContext is not null)
		{
			var customControl = new CustomControl(this, null, 0, 0);
			customControl.RegularStatusBar();
		}
	}
}
