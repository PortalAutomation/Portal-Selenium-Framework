using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CCWebUIAuto.PrimitiveElements;
using OpenQA.Selenium;

namespace CCWebUIAuto.Pages.BasePages
{
	public class EditStandardViewProperties : IPopup
	{
		public readonly Select
			SelCreationView			= new Select(By.Name("creationView")),
			SelSummaryView			= new Select(By.Name("summaryView")),
			SelQuickCreationView	= new Select(By.Name("quickCreationView")),
			SelPrintHeaderView		= new Select(By.Name("printHeaderView"));

		public readonly Button
			BtnOk		= new Button(By.XPath("//input[@value='OK']")),
			BtnCancel	= new Button(By.XPath("//input[@value='Cancel']")),
			BtnApply	= new Button(By.XPath("//input[@value='Apply']"));

		public string Title
		{
			get
			{
				return "Edit View Properties";
			}
		}
	}
}
