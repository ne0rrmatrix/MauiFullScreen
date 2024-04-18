// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
#if ANDROID
using Android.Views;
#endif

namespace HideTopBottomBars.Extensions;
// Since MediaElement can't access .NET MAUI internals we have to copy this code here
// https://github.com/dotnet/maui/blob/main/src/Controls/src/Core/Platform/PageExtensions.cs
static class PageExtensions
{
    static bool navBarIsVisible = false;
    static bool tabBarIsVisible = false;
    static string pageTitle = string.Empty;
    static Page CurrentPage => GetCurrentPage(Application.Current?.MainPage ?? throw new InvalidOperationException($"{nameof(Application.Current.MainPage)} cannot be null."));

	public static void SetBarStatus(bool ShowBars)
    {
#if IOS || MACCATALYST
#pragma warning disable CA1422 // Validate platform compatibility
        UIKit.UIApplication.SharedApplication.SetStatusBarHidden(ShowBars, UIKit.UIStatusBarAnimation.Fade);
#pragma warning restore CA1422 // Validate platform compatibility
#endif
        // let's cache the CurrentPage here, since the user can navigate or background the app
        // while this method is running
        var currentPage = CurrentPage;

        if (!ShowBars)
        {
            navBarIsVisible = Shell.GetNavBarIsVisible(currentPage);
            tabBarIsVisible = Shell.GetTabBarIsVisible(currentPage);
            pageTitle = currentPage.Title;
            currentPage.Title = string.Empty;
            Shell.SetNavBarIsVisible(currentPage, false);
            Shell.SetTabBarIsVisible(currentPage, false);
        }
        else
        {
			Shell.SetNavBarIsVisible(currentPage, navBarIsVisible);
			Shell.SetTabBarIsVisible(currentPage, tabBarIsVisible);
            currentPage.Title = pageTitle;
        }
    }
    internal static Page GetCurrentPage(this Page currentPage)
    {
#pragma warning disable CA1826 // Do not use Enumerable methods on indexable collections
        if (currentPage.NavigationProxy.ModalStack.LastOrDefault() is Page modal)
        {
            return modal;
        }
#pragma warning restore CA1826 // Do not use Enumerable methods on indexable collections

        if (currentPage is FlyoutPage flyoutPage)
        {
            return GetCurrentPage(flyoutPage.Detail);
        }

        if (currentPage is Shell shell && shell.CurrentItem?.CurrentItem is IShellSectionController shellSectionController)
        {
            return shellSectionController.PresentedPage;
        }

        if (currentPage is IPageContainer<Page> paigeContainer)
        {
            return GetCurrentPage(paigeContainer.CurrentPage);
        }

        return currentPage;
    }
}
