using OpenQA.Selenium;
using PortalSeleniumFramework.Helpers;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.Pages.BasePages
{
	public class CommandWindow : CCPage
	{
		private const string LayoutPath = "/debugconsole/commandwindow/command";
		private const string ScriptName = "AutoTest";
		public readonly Button BtnNew, BtnRun, BtnSave, BtnDelete;
		public readonly TextBox TxtScriptName, TxtScript, TxtResult;
		public readonly Select SelScript;

		public CommandWindow()
		{
			SelScript = new Select(By.CssSelector("#selectScript"));
			TxtScriptName = new TextBox(By.CssSelector("#CommandScript\\2e name"));
			TxtScript = new TextBox(By.CssSelector("#CommandScript\\2e script"));
			BtnNew = new Button(By.CssSelector("#New"));
			BtnRun = new Button(By.CssSelector("#Run"));
			BtnSave = new Button(By.CssSelector("#Save"));
			BtnDelete = new Button(By.CssSelector("#Delete"));
			TxtResult = new TextBox(By.CssSelector("#OutputScript"));
		}

		public override void NavigateTo()
		{
			WaitForPageLoad();
			Web.Navigate(Store.BaseUrl + LayoutPath);
		}

		public string Run(string script)
		{
			NavigateTo();
			if (SelScript.SelectedOption.Contains(ScriptName)) {
				SelScript.SelectOption(ScriptName);
				WaitForPageLoad();
			} else {
				BtnNew.Click();
				TxtScriptName.Value = ScriptName;
				BtnSave.Click();
			}
			TxtScript.Value = script;
			BtnRun.Click();
			WaitForPageLoad();
			return TxtResult.Value;
		}
	}
}
