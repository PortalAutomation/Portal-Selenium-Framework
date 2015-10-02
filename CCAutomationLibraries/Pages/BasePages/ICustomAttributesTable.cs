using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using OpenQA.Selenium;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.Pages.BasePages
{
	public interface ICustomAttributesTable
	{
		Button BtnNewCustomAttribute { get; set; }
		Button BtnDeleteCustomAttribute { get; set; }
		Container CustomAttributesForm { get; set; }
	}

	public enum CustomAttributeDataType
	{
		Boolean,
		Currency,
		Date,
		DocumentContent,
		Double,
		EntityOfType,
		Float,
		Integer,
		SetOfType,
		String,
		Enumeration
	}

	public static class CustomAttributesTableMethods
	{
		public static void CreateNewCustomAttribute(this ICustomAttributesTable page, CustomAttributeDataType dataType, string display,
			string internalName, string description = "", string defaultValue = "", string currencyType = "US Dollar/US [1033] ($)",
			string displayFormat = "Short Y2K Date", string readOnlyString = "", string entityType = "", string setOfType = "",
			string booleanDisplay = "Yes/No", bool multiLineString = false, IEnumerable<string> enumValues = null)
		{
			// Opening a modal dialog causes the calling javascript to hang, and Selenium times out.
			// Manually do what the button does to work around this
			var parentTitle = ((CCPage) page).GetTitle();
			page.BtnNewCustomAttribute.AsyncClick();
			var popup = new CustomAttributeDefinitionPopup();
			PopUpWindow.SwitchTo(popup.Title);
			switch (dataType) {
			case CustomAttributeDataType.Boolean: {
					popup.SelDataType.SelectOption("Boolean");
					popup.TxtDisplayName.Value = display;
					popup.TxtInternalName.Value = internalName;
					popup.TxtDescription.Value = description;
					popup.SelBooleanDisplayFormat.SelectOption(booleanDisplay);
					break;
				}
			case CustomAttributeDataType.Currency: {
					popup.SelDataType.SelectOption("Currency...");
					popup.SelCurrencyType.SelectOption(currencyType);
					popup.TxtDisplayName.Value = display;
					popup.TxtInternalName.Value = internalName;
					popup.TxtDescription.Value = description;
					popup.TxtDefaultValue.Value = defaultValue;
					break;
				}
			case CustomAttributeDataType.Date: {
					popup.SelDataType.SelectOption("Date");
					popup.TxtDisplayName.Value = display;
					popup.TxtInternalName.Value = internalName;
					popup.TxtDescription.Value = description;
					popup.TxtDefaultValue.Value = defaultValue;
					popup.SelDisplayFormat.SelectOption(displayFormat);
					break;
				}
			case CustomAttributeDataType.DocumentContent: {
					popup.SelDataType.SelectOption("Document Content");
					popup.TxtDisplayName.Value = display;
					popup.TxtInternalName.Value = internalName;
					popup.TxtDescription.Value = description;
					popup.TxtReadOnlyString.Value = readOnlyString;
					break;
				}
			case CustomAttributeDataType.Double: {
					popup.SelDataType.SelectOption("Double");
					popup.TxtDisplayName.Value = display;
					popup.TxtInternalName.Value = internalName;
					popup.TxtDescription.Value = description;
					popup.TxtDefaultValue.Value = defaultValue;
					break;
				}
			case CustomAttributeDataType.EntityOfType: {
					popup.SelDataType.SelectOption("Entity of Type...");
					popup.SelEntityOfType.SelectOption(entityType);
					popup.TxtDisplayName.Value = display;
					popup.TxtInternalName.Value = internalName;
					popup.TxtDescription.Value = description;
					// TODO this requires selecting a value using browse button
					//this.AttributePopup.DefaultValue.Value = defaultValue;
					popup.TxtReadOnlyString.Value = readOnlyString;
					break;
				}
			case CustomAttributeDataType.Float: {
					popup.SelDataType.SelectOption("Float");
					popup.TxtDisplayName.Value = display;
					popup.TxtInternalName.Value = internalName;
					popup.TxtDescription.Value = description;
					popup.TxtDefaultValue.Value = defaultValue;
					break;
				}
			case CustomAttributeDataType.Integer: {
					popup.SelDataType.SelectOption("Integer");
					popup.TxtDisplayName.Value = display;
					popup.TxtInternalName.Value = internalName;
					popup.TxtDescription.Value = description;
					popup.TxtDefaultValue.Value = defaultValue;
					break;
				}
			case CustomAttributeDataType.SetOfType: {
					popup.SelDataType.SelectOption("Set of Type...");
					popup.TxtDisplayName.Value = display;
					popup.TxtInternalName.Value = internalName;
					popup.TxtDescription.Value = description;
					popup.SelSetOfType.SelectOption(setOfType);
					break;
				}
			case CustomAttributeDataType.String: {
					popup.SelDataType.SelectOption("String");
					popup.TxtDisplayName.Value = display;
					popup.TxtInternalName.Value = internalName;
					popup.TxtDescription.Value = description;
					popup.TxtDefaultValue.Value = defaultValue;
					if (multiLineString) { popup.ChkMultiLineString.Checked = true; }
					break;
				}
			case CustomAttributeDataType.Enumeration: {
					popup.RdoListOption.Selected = true;
					popup.TxtDisplayName.Value = display;
					popup.TxtInternalName.Value = internalName;
					popup.TxtDescription.Value = description;

					if (enumValues == null) break;
					foreach (var i in enumValues) {
						popup.TxtNewListValue.Value = i;
						popup.BtnAddValue.Click();
					}
					break;
				}

			}
			popup.BtnOk.Click();
			PopUpWindow.SwitchTo(parentTitle);
		}

		public static void EditCustomAttribute(this ICustomAttributesTable page, CustomAttributeDataType dataType, string display,
			string internalName, string description = "", string defaultValue = "", string currencyType = "US Dollar/US [1033] ($)",
			string displayFormat = "Short Y2K Date", string readOnlyString = "", string entityType = "", string setOfType = "",
			string booleanDisplay = "Yes/No", bool multiLineString = false)
		{
			// Opening a modal dialog causes the calling javascript to hang, and Selenium times out.
			// Manually do what the button does to work around this
			var parentTitle = ((CCPage) page).GetTitle();
			page.GetCustomAttributePopupLink(internalName).AsyncClick();
			var popup = new CustomAttributeDefinitionPopup();
			PopUpWindow.SwitchTo(popup.Title);
			switch (dataType) {
			case CustomAttributeDataType.Boolean: {
					if (display != null) popup.TxtDisplayName.Value = display;
					if (description != null) popup.TxtDescription.Value = description;
					if (booleanDisplay != null) popup.SelBooleanDisplayFormat.SelectOption(booleanDisplay);
					break;
				}
			case CustomAttributeDataType.Currency: {
					if (currencyType != null) popup.SelCurrencyType.SelectOption(currencyType);
					if (display != null) popup.TxtDisplayName.Value = display;
					if (description != null) popup.TxtDescription.Value = description;
					if (defaultValue != null) popup.TxtDefaultValue.Value = defaultValue;
					break;
				}
			case CustomAttributeDataType.Date: {
					if (display != null) popup.TxtDisplayName.Value = display;
					if (description != null) popup.TxtDescription.Value = description;
					if (defaultValue != null) popup.TxtDefaultValue.Value = defaultValue;
					if (displayFormat != null) popup.SelDisplayFormat.SelectOption(displayFormat);
					break;
				}
			case CustomAttributeDataType.DocumentContent: {
					if (display != null) popup.TxtDisplayName.Value = display;
					if (description != null) popup.TxtDescription.Value = description;
					if (readOnlyString != null) popup.TxtReadOnlyString.Value = readOnlyString;
					break;
				}
			case CustomAttributeDataType.Double: {
					if (display != null) popup.TxtDisplayName.Value = display;
					if (description != null) popup.TxtDescription.Value = description;
					if (defaultValue != null) popup.TxtDefaultValue.Value = defaultValue;
					break;
				}
			case CustomAttributeDataType.EntityOfType: {
					if (display != null) popup.TxtDisplayName.Value = display;
					if (description != null) popup.TxtDescription.Value = description;
					// TODO this requires selecting a value using browse button
					if (readOnlyString != null) popup.TxtReadOnlyString.Value = readOnlyString;
					break;
				}
			case CustomAttributeDataType.Float: {
					if (display != null) popup.TxtDisplayName.Value = display;
					if (description != null) popup.TxtDescription.Value = description;
					if (defaultValue != null) popup.TxtDefaultValue.Value = defaultValue;
					break;
				}
			case CustomAttributeDataType.Integer: {
					if (display != null) popup.TxtDisplayName.Value = display;
					if (description != null) popup.TxtDescription.Value = description;
					if (defaultValue != null) popup.TxtDefaultValue.Value = defaultValue;
					break;
				}
			case CustomAttributeDataType.SetOfType: {
					if (display != null) popup.TxtDisplayName.Value = display;
					if (description != null) popup.TxtDescription.Value = description;
					break;
				}
			case CustomAttributeDataType.String: {
					if (display != null) popup.TxtDisplayName.Value = display;
					if (description != null) popup.TxtDescription.Value = description;
					if (defaultValue != null) popup.TxtDefaultValue.Value = defaultValue;
					break;
				}
			/*case CustomAttributeDataType.StringList:
			{
				this.AttributePopup.ListOption.Selected = true;
				this.AttributePopup.DisplayName.Value = display;
				this.AttributePopup.InternalName.Value = internalName;
				this.AttributePopup.Description.Value = description;
				this.AttributePopup.DefaultValue.Value = defaultValue;
				break;
			}*/

			}
			popup.BtnOk.Click();
			PopUpWindow.SwitchTo(parentTitle);
		}

		public static void CheckRow(this ICustomAttributesTable page, string internalName)
		{
			var attributeRowCheckboxes = page.CustomAttributesForm.GetDescendants(".//tbody/tr/td[1]/input");
			var link = attributeRowCheckboxes.FirstOrDefault(n => n.GetAttributeValue("value") == internalName);
			if (link == null) return;
			Trace.WriteLine(String.Format("Selecting checkbox with value '{0}'", internalName));
			link.Selected = true;
		}

		public static void DeleteCustomAttribute(this ICustomAttributesTable page, string innerValue)
		{
			page.CheckRow(innerValue);
			Trace.WriteLine("Clicking delete button for attribute");
			page.BtnDeleteCustomAttribute.Click();
		}

		public static bool CustomAttributeExists(this ICustomAttributesTable page, string internalName)
		{
			var link = page.GetCustomAttributePopupLink(internalName);
			return link != null && link.Exists;
		}

		public static Link GetCustomAttributePopupLink(this ICustomAttributesTable page, string internalName)
		{
			var xpath = ".//*[@id='CustomAttributesForm']/table/tbody/tr/td[3][text()='" + internalName + "']/../td[2]/a";
			return new Link(By.XPath(xpath));
		}

		public static void InitializeCustomAttributesTableUiElements(this ICustomAttributesTable page)
		{
			page.BtnNewCustomAttribute = new Button(By.CssSelector("input[value='New']"));
			page.BtnDeleteCustomAttribute = new Button(By.CssSelector("input[value='Delete']"));
			page.CustomAttributesForm = new Container(By.XPath(".//*[@id='CustomAttributesForm']"));
		}
	}
}
