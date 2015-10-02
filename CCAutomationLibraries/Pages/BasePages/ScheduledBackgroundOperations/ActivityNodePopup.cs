using System;
using OpenQA.Selenium;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.Pages.BasePages.ScheduledBackgroundOperations
{
	public class ActivityNodePopup : IPopup
	{
		public String Title { get { return "Activity Node"; } }

		public readonly TextBox
			TxtName = new TextBox(By.Name("BgQNode.Name")),
			TxtDescription = new TextBox(By.Name("BgQNode.Description"));

		public readonly Select
			SelCustomSearch = new Select(By.Name("BgQNode.SavedSearch")),
			SelActivityType = new Select(By.Name("activityTypeName"));

		public readonly Button
			BtnNew = new Button(By.Id("btnNewSavedSearch")),
			BtnEdit = new Button(By.Id("btnEditSavedSearch")),
			BtnOk = new Button(By.XPath("//img[contains(@src, 'ok.gif')]")),
			BtnCancel = new Button(By.XPath("//img[contains(@src, 'cancel.gif')]"));
	}
}
