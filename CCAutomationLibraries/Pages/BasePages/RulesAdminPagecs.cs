using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CCWebUIAuto.PrimitiveElements;
using OpenQA.Selenium;

namespace CCWebUIAuto.Pages.BasePages
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
