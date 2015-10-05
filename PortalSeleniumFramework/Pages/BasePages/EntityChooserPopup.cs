using System;
using System.Diagnostics;
using OpenQA.Selenium;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.Pages.BasePages
{
	public class EntityChooserPopup : IPopup
	{
		public string Title { get { return "Chooser Editor"; } }

		public readonly TextBox
			TxtDisplayName = new TextBox(By.CssSelector("input[name='EntityChooser.name']"));

		public readonly Button
			BtnOk = new Button(By.CssSelector("input[name='btnOK']")),
			BtnCancel = new Button(By.CssSelector("input[name='btnCancel']")),
			BtnNewDisplayField = new Button(By.XPath("//*[@id='webrRSV__BBDIV_0']/input[1]")),
			BtnNewFilterField = new Button(By.XPath("//*[@id='webrRSV__BBDIV_1']/input[1]"));

		/// <summary>
		/// Set the the chooser name
		/// </summary>
		public void SetChooserName(string name)
		{
			Trace.WriteLine(String.Format("Setting the choose display name:  {0}", name));
			TxtDisplayName.Value = name;
		}

		/// <summary>
		/// Create a new display field -- required param is propertyname, rest are optional.
		/// </summary>
		public void CreateNewDisplayField(string propertyName, string displayText = "", string order = "1", bool sorting = false, bool filtering = false)
		{
			Trace.WriteLine(String.Format("Creating a new display field with property '{0}'", propertyName));
			var parentTitle = Title;
			BtnNewDisplayField.Click();
			var popup = new EntityChooserFieldPopup();
			PopUpWindow.SwitchTo(popup.Title);
			popup.SelectProperty(propertyName);
			popup.TxtOrder.Value = order;
			popup.ChkSorting.Checked = sorting;
			popup.ChkFiltering.Checked = filtering;
			popup.BtnOk.Click();
			PopUpWindow.SwitchTo(parentTitle);
		}

		/// <summary>
		/// Create a new filter field -- required param is propertyname, rest are optional.
		/// </summary>
		public void CreateNewFilterOnlyField(string propertyName, string displayText = "", string order = "1")
		{
			Trace.WriteLine(String.Format("Creating a new display field with property '{0}'", propertyName));
			var parentTitle = Title;
			BtnNewFilterField.Click();
			var popup = new EntityChooserFieldPopup();
			PopUpWindow.SwitchTo(popup.Title);
			popup.SelectProperty(propertyName);
			popup.TxtOrder.Value = order;
			popup.BtnOk.Click();
			PopUpWindow.SwitchTo(parentTitle);
		}
	}
}
