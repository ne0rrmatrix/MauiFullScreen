using HideTopBottomBars.Extensions;
using HideTopBottomBars.Interfaces;

namespace HideTopBottomBars.CustomControls;

class CustomControl : ICustomControl
{
	/// <inheritdoc/>
	public void Hide()
	{
		PageExtensions.SetBarStatus(true);
	}

	/// <inheritdoc/>
	public void Show()
	{
		PageExtensions.SetBarStatus(false);
	}
}
