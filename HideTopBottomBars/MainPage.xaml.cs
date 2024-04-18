using System.Runtime.InteropServices;
using HideTopBottomBars.CustomControls;
using Microsoft.Win32.SafeHandles;

namespace HideTopBottomBars;

public partial class MainPage : ContentPage, IDisposable
{
	bool showBars = false;
	bool disposedValue;
	SafeHandle? safeHandle = new SafeFileHandle(IntPtr.Zero, true);
#if ANDROID
	
	readonly CustomControl customControls = new(Platform.AppContext, null, 0, 0);
#else
	readonly CustomControl customControls = new();
#endif

    public MainPage()
    {
        InitializeComponent();
    }

	void OnHideBarsClicked(object sender, EventArgs e)
	{
		if(showBars)
		{
			customControls.Show();
			showBars = false;
			System.Diagnostics.Debug.WriteLine("showBars: " + showBars);
		}
		else
		{
			customControls.Hide();
			showBars = true;
			System.Diagnostics.Debug.WriteLine("showBars: " + showBars);
		}
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (!disposedValue)
		{
			if (disposing)
			{
				safeHandle?.Dispose();
				safeHandle = null;
			}

			disposedValue = true;
		}
	}
}
