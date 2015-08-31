using System;
using System.Linq;
using System.Threading;
using CCWebUIAuto.Helpers;
using CCWebUIAuto.PrimitiveElements;
using OpenQA.Selenium;

namespace CCWebUIAuto.Pages.BasePages
{
	public class SelectPropertyModalPopup : IPopup
	{
		public string Title
		{
			get
			{
				return _multiSelect == SelectPropertyPopup.AllowMultiSelect.Yes
					? "Select One or More Properties"
					: "Select Property";
			}
		}

		public readonly Button OkButton = new Button(By.Id("btnOk"));

		public Container PropertyDiv = new Container(By.Id("divAttributeNav"));

		private readonly SelectPropertyPopup.AllowMultiSelect _multiSelect = SelectPropertyPopup.AllowMultiSelect.No;

		public SelectPropertyModalPopup(SelectPropertyPopup.AllowMultiSelect allowMultiSelect = SelectPropertyPopup.AllowMultiSelect.No)
		{
			_multiSelect = allowMultiSelect;
		}

		public void SelectProperty(string name, Button openPopupWithThis)
		{
			var parentWindow = Web.Driver.Title;
			Wait.Until(d => openPopupWithThis.Exists);
			openPopupWithThis.AsyncClick();
			Thread.Sleep(2000);
			PopUpWindow.SwitchTo(Title);

			// Switch to the frame within this popup dialog
			Web.Driver.SwitchTo()
				.Frame(Web.Driver.FindElement(By.Id("ifrmAttributeTable")));
			Wait.Until(d => new Container(By.Id("spanAttributeName")).Exists);

			var parsedName = name.Split('.');
			var path = new String[parsedName.Length - 1];
			Array.Copy(parsedName, path, parsedName.Length - 1);
			var propertyName = parsedName.Last();

			foreach (var expander in path.Select(attr => new Button(By.XPath(String.Format("//*[@id='spanAttributeName' and text()='{0}']/../../td[1]/a", attr))))) {
				expander.Click();
			}

			var property = new Container(By.XPath(String.Format("//*[@id='spanAttributeName' and text()='{0}']", propertyName)));
			property.Click();

			// Switch back out of frame
			PopUpWindow.SwitchTo(Title);
			OkButton.Click();
			PopUpWindow.SwitchTo(parentWindow);
		}
	}
}
