using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.Pages.BasePages
{
	public class RulesAdminPagecs : IPopup
	{
		public String Title
		{
			get
			{
				return "Conditional Display Rules Administration";
			}
		}

		public readonly Button
            BtnNew = new Button(By.Id("btn_NewRule")),
			BtnOk = new Button(By.XPath("//input[@value='OK']"));
	}
}
