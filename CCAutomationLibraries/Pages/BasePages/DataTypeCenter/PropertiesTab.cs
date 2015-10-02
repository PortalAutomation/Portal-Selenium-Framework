using System;
using System.Diagnostics;
using OpenQA.Selenium;
using PortalSeleniumFramework.Helpers;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.Pages.BasePages.DataTypeCenter
{
	public class PropertiesTab : CCPage, ICustomAttributesTable
	{
		public readonly string DataTypeInternalName;

		public Button BtnNewCustomAttribute { get; set; }
		public Button BtnDeleteCustomAttribute { get; set; }
		public Container CustomAttributesForm { get; set; }

		public readonly Button
			BtnDisplayId = new Button(By.CssSelector("input[value='...']")),
			BtnOk = new Button(By.CssSelector("input[value='OK']"));

		public readonly TextBox
			TxtDisplayName = new TextBox(By.CssSelector("input[id='EntityTypeDisplayName']")),
			TxtCustomizeInternalName = new TextBox(By.Id("EntityTypeInternalNameSuffix")),
			TxtDescription = new TextBox(By.CssSelector("input[id='EntityTypeDescription']")),
			TxtDisplayValue = new TextBox(By.Id("EntityTypeDisplayFieldName"));

		public readonly Container
			SpanInternalName = new Container(By.Id("EntityTypeInternalName_text"));

		public readonly Radio
			RdoDataEntry = new Radio(By.Id("EntityTypeUsage")),
			RdoSelection = new Radio(By.XPath(".//*[@id='CustomAttributesForm']/table[1]/tbody/tr[5]/td[3]/input[3]"));

		public PropertiesTab(string dataTypeInternalName)
		{
			DataTypeInternalName = dataTypeInternalName;
			this.InitializeCustomAttributesTableUiElements();
		}

		public void SetDisplayValue(string propertyName)
		{
			Trace.WriteLine(String.Format("Setting display attribute ID to '{0}'", propertyName));
			var popup = new SelectPropertyModalPopup();
			popup.SelectProperty(propertyName, BtnDisplayId);
			BtnOk.Click();
		}

		public bool VerifyDisplayValue(string displayValue)
		{
			var returnValue = false;
			Trace.WriteLine(String.Format("Verifying display value '{0}'", displayValue));
			if (TxtDisplayValue.Value == displayValue) {
				Trace.WriteLine(String.Format("Display value '{0}' is displayed", displayValue));
				returnValue = true;
			}
			return returnValue;
		}

		public override void NavigateTo()
		{
			Web.Navigate(Store.BaseUrl + "/EntityTypeCustomization/EntityTypeDetails?EntityTypeName=" + DataTypeInternalName + "&Tab=1");
		}
	}
}
