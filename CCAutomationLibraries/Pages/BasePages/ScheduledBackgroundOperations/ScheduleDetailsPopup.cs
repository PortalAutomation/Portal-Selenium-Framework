using System;
using OpenQA.Selenium;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.Pages.BasePages.ScheduledBackgroundOperations
{
	public class ScheduleDetailsPopup : IPopup
	{
		public String Title { get { return "Schedule Details"; } }

		public readonly TextBox
			TxtName = new TextBox(By.Name("BGQTrigger.Name")),
			TxtDescription = new TextBox(By.Name("BGQTrigger.Description")),
			TxtUserName = new TextBox(By.Name("BGQTrigger.userID")),
			TxtStartDate = new TextBox(By.Name("BGQTrigger.StartTime"));

		public readonly Checkbox
			ChkEnabled = new Checkbox(By.Name("BGQTrigger.Enabled"));

		public readonly Button
			BtnOk = new Button(By.XPath("//img[contains(@src, 'ok.gif')]")),
			BtnCancel = new Button(By.XPath("//img[contains(@src, 'cancel.gif')]"));
	}
}
