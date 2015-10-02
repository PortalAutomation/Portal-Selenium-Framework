using OpenQA.Selenium;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.Pages.BasePages
{
	public class ActivityDetails
	{
		public readonly Link
			LnkReturnToWorkspace = new Link(By.LinkText("<< Return to Workspace")),
			LnkPrev = new Link(By.LinkText("< Prev")),
			LnkNext = new Link(By.LinkText("Next >")),
			LnkActivityFormTab = new Link(By.LinkText("Activity Form")),
			LnkPropertyChangesTab = new Link(By.LinkText("Property Changes")),
			LnkDocumentsTab = new Link(By.LinkText("Documents")),
			LnkNotificationsTab = new Link(By.LinkText("Notifications"));
	}
}
