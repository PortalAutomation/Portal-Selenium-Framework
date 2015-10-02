using System;
using OpenQA.Selenium;
using PortalSeleniumFramework.Helpers;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.Pages.BasePages.ScheduledBackgroundOperations
{
	public class Main : CCPage
	{
		private const String LayoutPath = "/SiteAdministration/ScheduledOperations/BackgroundOperations";

		public readonly Link
			LnkUrlSettings = new Link(By.LinkText("URL Settings")),
			LnkScheduledBackgroundOperationCleanup = new Link(By.LinkText("Scheduled Background Operation Cleanup"));

		public readonly Select
			SelFilterByField = new Select(By.Id("_webrRSV_FilterField_0_0")),
			SelRefresh = new Select(By.Name("_autoRefreshWidget_seconds"));

		public readonly Button
			BtnNew = new Button(By.XPath("//input[@value='New']")),
			BtnDelete = new Button(By.XPath("//input[@value='Delete']")),
			BtnDeselectAll = new Button(By.XPath("//input[@value='Deselect All']"));

		public override void NavigateTo()
		{
			Web.Navigate(Store.BaseUrl + LayoutPath);
		}

		public void OpenBackgroundOperationDetails(String name)
		{
			var link = new Link(By.PartialLinkText(name));
			link.Click();
		}

		public void DeleteBackgroundOperation(String name)
		{
			var checkbox = new Checkbox(By.XPath("//a[text()='" + name + "']/../../td[1]/input"));
			checkbox.Click();
			BtnDelete.Click();
			
		}
	}
}
