using System;
using CCWebUIAuto.Helpers;
using CCWebUIAuto.Pages.Components;
using CCWebUIAuto.PrimitiveElements;
using OpenQA.Selenium;

namespace CCWebUIAuto.Pages.BasePages
{
	public class EditPageLayoutPopup : IPopup
	{
		public String Title { get { return "Edit Page Layout"; } }

		public readonly Button
			BtnOk = new Button(By.CssSelector("input[value='OK']"));

		public void AddComponent(ComponentType componentType, Int32 area, Int32? tab = null)
		{
			if (tab == null) {
				var selComponent = new Select(By.XPath("//select[@name='Column" + area + "']"));
				var btnAdd = new Button(By.XPath("//select[@name='Column" + area + "']/../../td[2]/a"));
				selComponent.SelectOption(componentType.Name);
				btnAdd.Click();
			}
		}

		public void RemoveComponent(Int32 componentIdx, Int32 area, Int32? tab = null)
		{
			if (tab == null) {
				var btnEdit = new Button(By.XPath("//select[@name='Column" + area + "']/../../../../../table[" + (componentIdx + 4) + "]/tbody/tr[1]/td[2]/a"));
				var lnkDelete = new Link(By.XPath("//select[@name='Column" + area + "']/../../../../../table[" + (componentIdx + 4) + "]/tbody/tr[1]/td[2]//a[text()='Delete']"));
				btnEdit.Click();
				Wait.Until(d => lnkDelete.Exists);
				lnkDelete.Click();
				Wait.Until(d => !lnkDelete.Exists); // popup should refresh, removing the edit menu contents
			}
		}

		public void RunOnPropertiesPopup<TPropertiesPopupType>(Int32 componentIdx, Int32 area, Int32? tab, Action<RoomComponentPropertiesPopup> action)
			where TPropertiesPopupType : RoomComponentPropertiesPopup, new()
		{
			var editXpath = tab == null
				? "//select[@name='Column" + area + "']/../../../../../table[" + (componentIdx + 4) + "]/tbody/tr[1]/td[2]/a"
				: "//select[@name='Column" + (area + 1) + "0" + tab + "']/../../../../../table[" + (componentIdx + 3) + "]/tbody/tr[1]/td[2]/a";
			var propertiesXpath = tab == null
				? "//select[@name='Column" + area + "']/../../../../../table[" + (componentIdx + 4) + "]/tbody/tr[1]/td[2]//td[text()='Properties...']"
				: "//select[@name='Column" + (area + 1) + "0" + tab + "']/../../../../../table[" + (componentIdx + 3) + "]/tbody/tr[1]/td[2]//td[text()='Properties...']";
			var btnEdit = new Button(By.XPath(editXpath));
			var lnkProperties = new Link(By.XPath(propertiesXpath));
			btnEdit.Click();
			Wait.Until(d => lnkProperties.Exists);
			lnkProperties.Click();
			var properties = new TPropertiesPopupType();
			properties.SwitchTo();
			action(properties);
			properties.BtnOk.Click();
			properties.SwitchBackToParent(WaitForPopupToClose.Yes);
		}
	}

	public enum ComponentTemplateType
	{
		StandAlone, Template, Default
	}

	public class ComponentType
	{
		public readonly String Name;

		public ComponentType(String name)
		{
			Name = name;
		}

		public static readonly ComponentType
			Activities = new ComponentType("Activities"),
			AdvancedSearch = new ComponentType("Advanced Search"),
			BasicPageInformation = new ComponentType("Basic Page Information"),
			Calendar = new ComponentType("Calendar"),
			ContentSearch = new ComponentType("Site Search"),
			CustomSearch = new ComponentType("Custom Search"),
			DocumentQuery = new ComponentType("Document Query"),
			EditCdt = new ComponentType("Edit CDT"),
			Image = new ComponentType("Image"),
			JumpToPage = new ComponentType("Jump To Page"),
			Login = new ComponentType("Login"),
			PageNavigator = new ComponentType("Page Navigator"),
			ProjectContacts = new ComponentType("Project Contacts"),
			ProjectCreator = new ComponentType("Project Creator"),
			ProjectEditor = new ComponentType("Project Editor"),
			ProjectInfo = new ComponentType("Project Info"),
			ProjectListing = new ComponentType("Project Listing"),
			ProjectLog = new ComponentType("Project Log"),
			RssViewer = new ComponentType("RSS Viewer"),
			ReviewerNotes = new ComponentType("Reviewer Notes"),
			Search = new ComponentType("Search"),
			SelfRegistration = new ComponentType("Self Registration"),
			SmartFormProgress = new ComponentType("SmartForm Progress"),
			TextBlock = new ComponentType("Text Block"),
			WebPageLinks = new ComponentType("Web Page Links"),
			Wrapper = new ComponentType("Wrapper");
	}
}