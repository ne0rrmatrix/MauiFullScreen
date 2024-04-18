using HideTopBottomBars.CustomControls;

namespace HideTopBottomBars;

public partial class MainPage : ContentPage
{
	bool showBars = false;
	readonly CustomControl customControls = new();

    public MainPage()
    {
        InitializeComponent();
    }

	void OnHideBarsClicked(object sender, EventArgs e)
	{
		System.Diagnostics.Debug.WriteLine("OnHideBarsClicked");
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
}
