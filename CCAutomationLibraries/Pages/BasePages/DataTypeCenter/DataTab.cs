using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using PortalSeleniumFramework.Helpers;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.Pages.BasePages.DataTypeCenter
{
	public class DataTab : CCPage
	{
		public readonly string DataTypeInternalName;

		public readonly Button
			NewButton = new Button(By.CssSelector("input[value='New']")),
			DeSelectAllButton = new Button(By.CssSelector("input[value='Deselect All']")),
			DeleteButton = new Button(By.CssSelector("input[value='Delete']"));

		public readonly Container
			DataTableDiv = new Container(By.Id("webrRSV__ID_0"));

		// All other page objects contained and associated with
		public EntityDataPopup EntityDataPopup = new EntityDataPopup();

		public DataTab(string dataTypeInternalName)
		{
			DataTypeInternalName = dataTypeInternalName;
		}

		public override void NavigateTo()
		{
			WaitForPageLoad();
			Web.Navigate(Store.BaseUrl + "/EntityTypeCustomization/EntityTypeDetails?EntityTypeName=" +
							   DataTypeInternalName + "&Tab=3");
		}

		public void CreateEntityData(string name)
		{
			Trace.WriteLine(String.Format("Creating a new data entity for CDT called '{0}'", name));
			var dataTypeName = "";
			var dataTypeCenterWindow = CurrentWindowTitle;
			var match = Regex.Match(dataTypeCenterWindow, @"Data Type \((.*)\)");
			if (match.Success) {
				dataTypeName = match.Groups[1].Value;
			}
			NewButton.Click();
			PopUpWindow.SwitchTo("Add " + dataTypeName);
			EntityDataPopup.SetDisplayString(name);
			EntityDataPopup.BtnOk.Click();
			PopUpWindow.SwitchTo(dataTypeCenterWindow);
		}

		public void ModifyEntityName(string entityName, string newName)
		{
			Trace.WriteLine(String.Format("Modifying data entity for CDT called '{0}'", entityName));
			var dataTypeCenterWindow = CurrentWindowTitle;
			var match = Regex.Match(dataTypeCenterWindow, @"Data Type \((.*)\)");
			if (!match.Success) throw new Exception("Unable to determine data type");
			var dataTypeName = match.Groups[1].Value;
			var targetImage = new Button(By.XPath("//td[text()='" + entityName + "']/../td[2]/a"));
			targetImage.Click();
			PopUpWindow.SwitchTo("Edit " + dataTypeName);
			EntityDataPopup.SetDisplayString(newName);
			EntityDataPopup.BtnOk.Click();
			PopUpWindow.SwitchTo(dataTypeCenterWindow);
		}

		public void DeleteEntity(string name)
		{
			Trace.WriteLine(String.Format("Deleting entity data '{0}'", name));
			SelectEntityRow(name);
			DeleteButton.Click();
			var alert = Web.PortalDriver.SwitchTo().Alert();
			alert.Accept();
			Wait.Until(d => !DataTableDiv.Text.Contains(name));
		}

		public void SelectEntityRow(string name)
		{
			var tdCheckBox = new Checkbox(By.XPath("//td[text()='" + name + "']/../td[1]/input[@type='checkbox']"));
			tdCheckBox.Checked = true;
		}

		public bool VerifyEntityDataExists(string name)
		{
			var returnValue = false;
			Trace.WriteLine(String.Format("Verifying if view '{0}' exists", name));
			if (DataTableDiv.Text.Contains(name)) {
				Trace.WriteLine(String.Format("View '{0}' exists", name));
				returnValue = true;
			} else {
				Trace.WriteLine(String.Format("View '{0}' does not exist", name));
			}
			return returnValue;
		}
	}
}
