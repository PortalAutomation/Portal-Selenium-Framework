using System;
using CCWebUIAuto.Helpers;
using CCWebUIAuto.PrimitiveElements;
using OpenQA.Selenium;

namespace CCWebUIAuto.Pages.BasePages.ProjectTypeCenter
{
	public class TemplatesTab : CCPage
	{
		public readonly String ProjectTypeInternalName;

		public readonly Button
			BtnNew = new Button(By.CssSelector("input[value='New']")),
			BtnDelete = new Button(By.CssSelector("input[value='Delete']"));

		public readonly Container
			TableTemplates = new Container(By.XPath("//*[@id='_webrRSV_DIV_0']/table"));

		public TemplatesTab(String projectTypeInternalName)
		{
			ProjectTypeInternalName = projectTypeInternalName;
		}

		public override void NavigateTo()
		{
			Web.Navigate(Store.BaseUrl + "/ProjectCustomization/ProjectTypeCenter/ProjectTypeDetails?EntityTypeName=" + ProjectTypeInternalName + "&Tab=6");
		}

		public void CreateNewTemplate(String templateName, String layout, String descr = "", bool useAsynchTabs = false, bool isDefault = false)
		{
			BtnNew.Click();
			var popup = new ProjectTypeCenterTemplatePropertiesPopup();
			popup.SwitchTo();
			popup.TxtName.Value = templateName;
			popup.TxtDescription.Value = descr;
			popup.SelLayout.SelectOption(layout);
			popup.ChkUseAsynchTabs.Checked = useAsynchTabs;
			popup.ChkIsDefault.Checked = isDefault;
			popup.BtnOk.Click();
			popup.SwitchBackToParent(WaitForPopupToClose.Yes);
			var link = new Link(By.LinkText(templateName));
			ClickPortalUI.Wait.Until(d => link.Exists);
		}

		public void SetProperties(String templateName, String newTemplateName = null, String layout = null,
			String descr = null, bool? useAsynchTabs = null, bool? isDefault = null)
		{
			var link = new Link(By.XPath("//*[@id='_webrRSV_DIV_0']/table/tbody/tr/td/a[text()='" + templateName + "']"));
			link.Click();
			var popup = new ProjectTypeCenterTemplatePropertiesPopup();
			popup.SwitchTo();
			if (newTemplateName != null) popup.TxtName.Value = newTemplateName;
			if (descr != null) popup.TxtDescription.Value = descr;
			if (layout != null) popup.SelLayout.SelectOption(layout);
			if (useAsynchTabs != null) popup.ChkUseAsynchTabs.Checked = useAsynchTabs.Value;
			if (isDefault != null) popup.ChkIsDefault.Checked = isDefault.Value;
			popup.BtnOk.Click();
			popup.SwitchBackToParent(WaitForPopupToClose.Yes);
			var newLink = new Link(By.LinkText(newTemplateName ?? templateName));
			ClickPortalUI.Wait.Until(d => newLink.Exists);
		}

		public void OpenTemplateComponentsPopup(String templateName)
		{
			var link = new Link(By.XPath("//a[text()='" + templateName + "']/../../td[5]/nobr[1]/a"));
			link.Click();
		}

		public void OpenDefaultComponentsPopup(String templateName)
		{
			var link = new Link(By.XPath("//a[text()='" + templateName + "']/../../td[5]/nobr[2]/a"));
			link.Click();
		}

		public void DeleteTemplate(String templateName)
		{
			var checkbox = new Checkbox(By.XPath("//a[text()='" + templateName + "']/../../td[1]/input[@type='checkbox']"));
			checkbox.Checked = true;
			BtnDelete.Click();
			var alert = Web.Driver.SwitchTo().Alert();
			alert.Accept();
			var link = new Link(By.LinkText(templateName));
			ClickPortalUI.Wait.Until(d => !link.Exists);
		}
	}

	public class ProjectTypeCenterTemplatePropertiesPopup : IPopup
	{
		public String Title { get { return "Page Template Properties"; } }

		public readonly TextBox
			TxtName = new TextBox(By.Name("ContainerTemplate.name")),
			TxtDescription = new TextBox(By.Name("ContainerTemplate.description"));

		public readonly Select
			SelLayout = new Select(By.Name("ContainerTemplate.folderLayout"));

		public readonly Checkbox
			ChkUseAsynchTabs = new Checkbox(By.Name("ContainerTemplate.useAsynchTabs")),
			ChkIsDefault = new Checkbox(By.Name("ContainerTemplate.isDefault"));

		public readonly Button
			BtnOk = new Button(By.CssSelector("input[value='OK']")),
			BtnApply = new Button(By.CssSelector("input[value='Apply']")),
			BtnCancel = new Button(By.CssSelector("input[value='Cancel']"));
	}
}
