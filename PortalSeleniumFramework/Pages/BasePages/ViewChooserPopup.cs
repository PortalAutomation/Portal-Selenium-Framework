using System;
using OpenQA.Selenium;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.Pages.BasePages
{
	public class ViewChooserPopup
	{
		public static Button
			BtnOk = new Button(By.CssSelector("input[value='OK']")),
			BtnCancel = new Button(By.CssSelector("input[value='Cancel']"));

		public static void SelectView(String viewName)
		{
			var rdoView = new Radio(By.XPath("//*[@id='_webrRSV_DIV_0']/table/tbody/tr/td[text()='" + viewName + "']/../td[1]/input[@type='radio']"));
			rdoView.Click();
		}
	}
}
