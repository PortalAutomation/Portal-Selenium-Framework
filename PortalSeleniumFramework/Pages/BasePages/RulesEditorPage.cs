using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.Pages.BasePages
{
	public class RulesEditorPage : IPopup
	{
		public string Title
		{
			get
			{
				return "Rules Editor";
			}
		}

		public readonly TextBox
			TxtName = new TextBox(By.Name("Rules.name")),
			TxtInternalName = new TextBox(By.Name("Rules.internalName")),
			TxtDescription = new TextBox(By.Name("Rules.description"));

		public readonly Link LnkLogic = new Link(By.XPath("//a"));

		public readonly Button
			BtnOk = new Button(By.XPath("//input[@value='OK']")),
			BtnCancel = new Button(By.XPath("//input[@value='Cancel']"));
	}
}
