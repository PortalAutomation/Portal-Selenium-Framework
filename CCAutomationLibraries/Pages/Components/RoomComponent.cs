using System;
using OpenQA.Selenium;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.Pages.Components
{
	public class RoomComponent
	{
		public readonly String DisplayName;
		public readonly Button BtnEditDropdown;
		public readonly Link LnkEditDropdwonProperties;
		public readonly Container DivComponentArea;
		public readonly String XpathPrefix;

		public RoomComponent() { }

		public RoomComponent(String displayName)
		{
            XpathPrefix = "//span[text()='" + displayName + "']";
			DisplayName = displayName;
			DivComponentArea = new Container(By.XPath(XpathPrefix));
            BtnEditDropdown = new Button(By.XPath(XpathPrefix + "//..//..//img[contains(@src,'edit_menu')]"));
			LnkEditDropdwonProperties = new Link(By.XPath(XpathPrefix + "//..//..//a[text()='Properties...']"));
		}

		public void RunOnPropertiesPopup<TPropertiesPopupType>(Action<RoomComponentPropertiesPopup> action)
			where TPropertiesPopupType : RoomComponentPropertiesPopup, new()
		{
			var properties = new TPropertiesPopupType();
			BtnEditDropdown.Click();
			LnkEditDropdwonProperties.Click();
			properties.SwitchTo();
			action(properties);
			properties.BtnOk.Click();
			properties.SwitchBackToParent(WaitForPopupToClose.Yes);
		}

		public T GetChildElementByXPath<T>(String xpath)
			where T : PageElement, new()
		{
			var baseElement = new CCElement(By.XPath(XpathPrefix + xpath));
			var pageElement = new T { BaseElement = baseElement };
			return pageElement;
		}
	}

	public class RoomComponentPropertiesPopup : IPopup
	{
		public virtual String Title { get { return ""; } }

		public readonly TextBox
			TxtTitle = new TextBox(By.Name("RoomComponent.DisplayName")),
			TxtIntroduction = new TextBox(By.Name("RoomComponent.description"));

		public readonly Button
			//BtnOk = new Button(By.XPath("//img[contains(@src, 'ok.gif')]")),
			BtnCancel = new Button(By.XPath("//img[contains(@src, 'cancel.gif')]"));

		public Button BtnOk
		{
			get
			{
				var img = new Button(By.XPath("//img[contains(@src, 'ok.gif')]"));
				var btn = new Button(By.XPath("//input[@value='OK']"));
				return img.Exists
					? img
					: btn;
			}
		}

		public readonly Select
			SelPresentationStyle = new Select(By.Name("RoomComponent.componentStyle")),
			SelSpacingStyle = new Select(By.Name("RoomComponent.spacingStyle"));

	}
}
