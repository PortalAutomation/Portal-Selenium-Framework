using System;
using System.Diagnostics;
using System.Linq;
using CCWebUIAuto.Helpers;
using CCWebUIAuto.PrimitiveElements;
using OpenQA.Selenium;

namespace CCWebUIAuto.Pages.BasePages.DataTypeCenter
{
	public class ChoosersTab : CCPage
	{
		public readonly string DataTypeInternalName;

		public readonly Button
			NewButton = new Button(By.Id("btnNewChooser")),
			SetDefaultButton = new Button(By.Id("btnSetDefaultChooser")),
			DeleteButton = new Button(By.Id("btnDeleteChooser")),
			DeSelectAllButton = new Button(By.CssSelector("input[value='Deselect All']"));

		public readonly Container
			ChoosersTableDiv = new Container(By.Id("_webrRSV_DIV_0"));

		public ChoosersTab(string dataTypeInternalName)
		{
			DataTypeInternalName = dataTypeInternalName;
		}

		public override void NavigateTo()
		{
			WaitForPageLoad();
			Web.Navigate(Store.BaseUrl + "/EntityTypeCustomization/EntityTypeDetails?EntityTypeName=" +
							   DataTypeInternalName + "&Tab=4");
		}

		/// <summary>
		/// Clicks new chooser button, and switches window focus
		/// </summary>
		public void CreateChooser(string chooserName, string displayFieldType, string filterOnlyFieldType)
		{
			// Grab a reference to this dynamic window title for data type center
			NewButton.Click();
			var popup = new EntityChooserPopup();
			popup.SwitchTo();
			popup.SetChooserName(chooserName);
			popup.CreateNewDisplayField(displayFieldType);
			popup.CreateNewFilterOnlyField(filterOnlyFieldType);
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
			Wait.Until(d => new Link(By.LinkText(chooserName)).Exists);
		}

		/// <summary>
		/// Checks the checkbox associated with chooser row
		/// </summary>
		public void SelectChooser(string chooserName)
		{
			var tdCheckBox = new Checkbox(By.XPath("//a[text()='" + chooserName + "']/../../td[1]/input[@type='checkbox']"));
			tdCheckBox.Checked = true;
		}

		public void DeleteChooser(string chooserName)
		{
			Trace.WriteLine(String.Format("Deleting chooser '{0}'", chooserName));
			SelectChooser(chooserName);
			DeleteButton.Click();
			var alert = Web.Driver.SwitchTo().Alert();
			alert.Accept();
			Wait.Until(d => !new Link(By.LinkText(chooserName)).Exists);
		}

		public void SetChooserAsDefault(string chooserName)
		{
			SelectChooser(chooserName);
			Trace.WriteLine(String.Format("Setting '{0}' as the default chooser.", chooserName));
			SetDefaultButton.Click();
		}

		/// <summary>
		/// Verify whether a chooser exists by it's display name
		/// </summary>
		public bool VerifyChooserExists(string chooserName)
		{
			var returnValue = false;
			Trace.WriteLine(String.Format("Verifying if chooser '{0}' exists", chooserName));
			var chooserRowLinks = ChoosersTableDiv.GetDescendants(".//table/tbody/tr/td[4]/a");
			var desiredRow = chooserRowLinks.FirstOrDefault(n => n.Text == chooserName);
			if (desiredRow != null) {
				Trace.WriteLine(String.Format("Chooser '{0}' exists", chooserName));
				returnValue = true;
			} else {
				Trace.WriteLine(String.Format("Chooser '{0}' does not exist", chooserName));
			}
			return returnValue;
		}

		/// <summary>
		/// Verify whether a chooser is set as default
		/// </summary>
		public bool VerifyChooserIsDefault(string chooserName)
		{
			var indicator = new Image(By.XPath("//a[text()='" + chooserName + "']/../../td[6]/img"));
			return indicator.Exists;
		}
	}
}
