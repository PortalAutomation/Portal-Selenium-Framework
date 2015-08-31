using System;
using System.Diagnostics;
using CCWebUIAuto.Helpers;
using CCWebUIAuto.PrimitiveElements;
using OpenQA.Selenium;

namespace CCWebUIAuto.Pages.BasePages.ProjectTypeCenter
{
	public class ProjectTypeCenterLayout : CCPage
	{
		private const string LayoutPath = "/ProjectCustomization/ProjectTypeCenter/ProjectTypeCenter";

		public readonly Button
			BtnDelete = new Button(By.CssSelector("input[value='Delete']")),
			BtnNew = new Button(By.CssSelector("input[value='New']"));

		public readonly Container
			Table = new Container(By.XPath("html/body/table[4]/tbody/tr[2]/td/form/table[2]"));

		public override void NavigateTo()
		{
			WaitForPageLoad();
			Web.Navigate(Store.BaseUrl + LayoutPath);
		}

		public void CreateNewProjectType(string name)
		{
			BtnNew.Click();
			var newProjTypePage = new ProjectTypeCenterNewFormLayout();
			newProjTypePage.TxtDisplayName.Value = name;
			newProjTypePage.BtnOk.Click();
			NavigateTo();
		}

		public bool VerifyProjectTypeExists(string name)
		{
			bool returnValue = false;
			Trace.WriteLine(String.Format("Verifying if state '{0}' exists", name));
			if (Table.Text.Contains(name)) {
				Trace.WriteLine(String.Format("State '{0}' exists", name));
				returnValue = true;
			} else {
				Trace.WriteLine(String.Format("State '{0}' does not exist", name));
			}
			return returnValue;
		}
	}

	public class ProjectTypeCenterNewFormLayout : CCPage
	{
		public readonly Container
			SpanInternalName = new Container(By.CssSelector("span[id='EntityTypeInternalName_text']"));

		public readonly TextBox
			TxtDisplayName = new TextBox(By.CssSelector("input[id='EntityTypeDisplayName']")),
			TxtCustomInternalName = new TextBox(By.CssSelector("input[id='EntityTypeInternalNameSuffix']"));

		public readonly Button
			BtnOk = new Button(By.CssSelector("input[value='OK']"));

		public override void NavigateTo()
		{
			throw new NotImplementedException();
		}
	}
}
