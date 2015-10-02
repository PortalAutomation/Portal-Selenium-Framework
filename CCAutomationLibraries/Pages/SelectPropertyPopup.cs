using System;
using System.Linq;
using OpenQA.Selenium;
using PortalSeleniumFramework.Helpers;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.Pages.BasePages
{
	public class SelectPropertyPopup : IPopup
	{
		public string Title
		{
			get
			{
				return _multiSelect == AllowMultiSelect.Yes
					? "Select One or More Properties"
					: "Select Property";
			}
		}

		public readonly Button OkButton = new Button(By.Id("btnOk"));

		public Container PropertyDiv = new Container(By.Id("divAttributeNav"));

		private readonly AllowMultiSelect _multiSelect = AllowMultiSelect.No;

		public SelectPropertyPopup(AllowMultiSelect allowMultiSelect = AllowMultiSelect.No)
		{
			_multiSelect = allowMultiSelect;
		}

		public void SelectProperty(string name)
		{
			// Switch to the frame within this popup dialog
			Web.PortalDriver.SwitchTo()
				.Frame(Web.PortalDriver.FindElement(By.Id("ifrmAttributeTable")));
			Wait.Until(d => new Container(By.Id("spanAttributeName")).Exists);

			var parsedName = name.Split('.');
			var path = new String[parsedName.Length - 1];
			Array.Copy(parsedName, path, parsedName.Length - 1);
			//var propertyName = parsedName.Last();

			foreach (var expander in path.Select(attr => new Button(By.XPath(String.Format("//*[@id='spanAttributeName' and text()='{0}']/../../td[1]/a", attr))))) {
				expander.Click();
			}

			var property = new Container(By.XPath(String.Format("//*[@id='spanQualifiedAttributeDisplayName' and text()='{0}']/../span", name)));
			property.Click();
			Web.PortalDriver.SwitchTo().DefaultContent();
		}

		public enum AllowMultiSelect { Yes, No }
	}
}
