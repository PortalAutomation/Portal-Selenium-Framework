using System;
using System.Collections.Generic;
using CCWebUIAuto.Helpers;

namespace CCWebUIAuto.Pages
{
	public interface IPopup
	{
		String Title { get; }
	}

	public static class IPopupExtensionMethods
	{
		private static readonly Dictionary<IPopup, String> ParentWindowTitles = new Dictionary<IPopup, string>();

		public static void SwitchTo(this IPopup popup, bool partialMatch = false)
		{
			ParentWindowTitles[popup] = Web.Driver.Title;
            if (partialMatch)
		    {
		        PopUpWindow.SwitchTo(popup.Title, true);
		    }
		    else
		    {
                PopUpWindow.SwitchTo(popup.Title);
		    }
		}

		public static void SwitchBackToParent(this IPopup popup, WaitForPopupToClose waitForClose = WaitForPopupToClose.No)
		{
			PopUpWindow.SwitchTo(ParentWindowTitles[popup]);
			if (waitForClose == WaitForPopupToClose.Yes) {
				Wait.Until(d => !popup.IsDisplayed());
			}
		}

		public static Boolean IsDisplayed(this IPopup popup)
		{
			return PopUpWindow.IsOpen(popup.Title);
		}

		public static void Close(this IPopup popup)
		{
			Web.Close();
		}
	}

	public enum WaitForPopupToClose { Yes, No }
}
