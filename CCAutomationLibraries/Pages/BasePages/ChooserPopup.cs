using System;
using OpenQA.Selenium;
using PortalSeleniumFramework.Helpers;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.Pages.BasePages
{
	public class ChooserPopup : IPopup
	{
		public String Title
		{
			get
			{
				return _allowMultiSelect
					? String.Format("Select One or More {0}", _dataType)
					: String.Format("Select {0}", _dataType);
			}
		}

		public readonly Select
			SelFilterField = new Select(By.Name("_webrRSV_FilterField_0_0"));

		public readonly TextBox
			TxtFilterCriteria = new TextBox(By.Name("_webrRSV_FilterValue_0_0"));

		public readonly Button
			BtnGo = new Button(By.Name("webrRSV__FilterGo_0")),
			BtnOk = new Button(By.Name("btnOk")),
			BtnAddUser = new Button(By.Id("createUserSelection")), //Appears in the Person chooser
			BtnAddOrganization = new Button(By.Id("createOrgSelection")); //Appears in the Organization chooser
		

		private readonly String _dataType;
		private readonly Boolean _allowMultiSelect;

		/// <summary>
		/// If using multi-select mode, the dataTypeDisplayName may need to be in plural form,
		/// such as 'Custom Searches' vs 'Custom Search'
		/// </summary>
		public ChooserPopup(String dataTypeDisplayName, Boolean allowMultiSelect = false)
		{
			_dataType = dataTypeDisplayName;
			_allowMultiSelect = allowMultiSelect;
		}

		public void SelectValue(String value, String filterByType = null)
		{
			var selector = _allowMultiSelect
				? (PageElement) new Checkbox(By.XPath("//td[text()='" + value + "']/../td[1]/input[@type='checkbox']"))
				: new Radio(By.XPath("//td[text()='" + value + "']/../td[1]/input[@type='radio']"));

			if (filterByType != null) {
				SelFilterField.SelectOption(filterByType);
				TxtFilterCriteria.Value = value;
				BtnGo.Click();
				Wait.Until(d => selector.Exists);
			}
			selector.Click();
		}
	}
}
