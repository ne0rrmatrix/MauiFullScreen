namespace HideTopBottomBars.Interfaces;

public interface ICustomControl
{
	/// <summary>
	/// Show the top and bottom bars
	/// </summary>
	void Show();

    /// <summary>
	/// Hide the top and bottom bars
	/// </summary>
    void Hide();
#if ANDROID

	/// <summary>
	/// Make the status bar transparent
	/// </summary>
	public void TransparentStatusBar();

	/// <summary>
	/// Make the status bar regular
	/// </summary>
	public void RegularStatusBar();
#endif
}
