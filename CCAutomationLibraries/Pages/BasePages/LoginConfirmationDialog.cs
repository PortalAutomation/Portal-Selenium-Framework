using System;
using OpenQA.Selenium;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.Pages.BasePages
{
	public class LoginConfirmationDialog
	{
		public readonly TextBox
			TxtUserName = new TextBox(By.Name("confirmUserId")),
			TxtPassword = new TextBox(By.Name("confirmPassword"));

		public readonly Button BtnSubmit = new Button(By.Id("confirmLoginSubmitBtn"));

		public void SubmitCredentials(string username, string password)
		{
			// switch into Frame "GB_frame_confirmLoginMsg"
			Web.PortalDriver.SwitchTo().Frame(Web.PortalDriver.FindElement(By.Id("GB_frame_confirmLoginMsg")));
			TxtUserName.Value = "administrator";
			TxtPassword.Value = "1234";
			BtnSubmit.Click();
		}
	}
}
