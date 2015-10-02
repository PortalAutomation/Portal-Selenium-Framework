using System;
using OpenQA.Selenium;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.Pages.BasePages
{
	public class WorkflowScriptEditor : IPopup
	{
		public String Title { get { return "Workflow Script Editor"; } }

		public readonly Button
			BtnClear = new Button(By.Id("clearBtn")),
			BtnOk = new Button(By.CssSelector("input[value='OK']")),
			BtnCancel = new Button(By.CssSelector("input[value='Cancel']")),
			BtnApply = new Button(By.CssSelector("input[value='Apply']"));

		public readonly TextBox
			TxtScript = new TextBox(By.Id("script"));
	}
}
