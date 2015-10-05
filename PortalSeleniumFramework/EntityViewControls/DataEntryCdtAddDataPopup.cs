using System;
using OpenQA.Selenium;
using PortalSeleniumFramework.Pages;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.EntityViewControls
{
	public abstract class DataEntryCdtAddDataPopup : IPopup
	{
		public virtual String Title { get { return ""; } }

		public readonly Button
			BtnOk = new Button(By.XPath("//input[@name='ok_btnName']")),
			BtnOkAndAddAnother = new Button(By.XPath("//input[@name='okAddMore_btnName']")),
			BtnCancel = new Button(By.XPath("//input[@name='cancel_btnName']"));
	}
}
