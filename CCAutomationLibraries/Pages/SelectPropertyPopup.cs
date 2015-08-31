using System;
using System.Linq;
using CCWebUIAuto.Helpers;
using CCWebUIAuto.PrimitiveElements;
using OpenQA.Selenium;

namespace CCWebUIAuto.Pages.BasePages
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
			Web.Driver.SwitchTo()
				.Frame(Web.Driver.FindElement(By.Id("ifrmAttributeTable")));
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
			Web.Driver.SwitchTo().DefaultContent();
		}

		public enum AllowMultiSelect { Yes, No }
	}
}
