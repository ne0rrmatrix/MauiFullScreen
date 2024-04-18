namespace HideTopBottomBars.Interfaces;

public interface ICustomControl
{
	void Show();
    void Hide();
#if ANDROID
	public void TransparentStatusBar();
	public void RegularStatusBar();
#endif
}
