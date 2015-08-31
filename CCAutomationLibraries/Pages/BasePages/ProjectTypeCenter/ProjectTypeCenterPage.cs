using CCWebUIAuto.Helpers;
using CCWebUIAuto.PrimitiveElements;
using OpenQA.Selenium;

namespace CCWebUIAuto.Pages.BasePages.ProjectTypeCenter
{
	public class ProjectTypeCenterPage : CCPage
	{
		public readonly Button
			BtnDelete = new Button(By.CssSelector("input[value='Delete']")),
			BtnNew = new Button(By.CssSelector("input[value='New']"));

		public override void NavigateTo()
		{
			Web.Navigate(Store.BaseUrl + "/ProjectCustomization/ProjectTypeCenter/ProjectTypeCenter");
		}

		public void CreateNewProjectType(string displayName, string idPrefix)
		{
			BtnNew.Click();
			var propertiesPage = new PropertiesTab("_" + displayName);
			Wait.Until(d => propertiesPage.DisplayName.Exists);
			propertiesPage.DisplayName.Value = displayName;
			propertiesPage.IdPrefix.Value = idPrefix;
			propertiesPage.OkButton.Click();
			NavigateTo();
		}

		public void DeleteProjectType()
		{
			BtnDelete.Click();
		}

		public void OpenProject(string name)
		{
			// Verify element exists on page before
			var tableLink = Web.Driver.FindElement(By.LinkText(name));
			tableLink.Click();
		}

		public bool ProjectTypeExists(string projName)
		{
			var ele = new Link(By.PartialLinkText(projName));
			return ele.Exists;
		}
	}
}
