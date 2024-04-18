using HideTopBottomBars.Extensions;
using HideTopBottomBars.Interfaces;

namespace HideTopBottomBars.CustomControls;

class CustomControl : ICustomControl
{
	public void Hide()
	{
		PageExtensions.SetBarStatus(false);
	}

	public void Show()
	{
		PageExtensions.SetBarStatus(true);
	}
}
