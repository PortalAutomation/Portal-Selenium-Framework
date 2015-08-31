using CCWebUIAuto.PrimitiveElements;
using OpenQA.Selenium;

namespace CCWebUIAuto.Pages.BasePages
{
	public class CustomAttributeDefinitionPopup : IPopup
	{
		public string Title { get { return "Field Definition"; } }

		public readonly Button
			BtnOk = new Button(By.XPath(".//*[@id='allowSaveTable']/tbody/tr/td/input[1]")),
			BtnAddValue = new Button(By.Id("addValueBtn"));

		public readonly TextBox
			TxtDisplayName = new TextBox(By.CssSelector("input[id='caption']")),
			TxtInternalName = new TextBox(By.CssSelector("input[id='name']")),
			TxtDescription = new TextBox(By.CssSelector("input[id='description']")),
			TxtDefaultValue = new TextBox(By.CssSelector("input[id='defaultValue']")),
			TxtReadOnlyString = new TextBox(By.Id("nullDisplayString")),
			TxtNewListValue = new TextBox(By.Id("newValue"));

		public readonly Select
			Select = new Select(By.XPath(".//*[@id='dataType']")),
			SelDataType = new Select(By.XPath(".//*[@id='dataType']")),
			SelDisplayFormat = new Select(By.Id("displayFormat_date")),
			SelEntityOfType = new Select(By.Id("entityOfType")),
			SelSetOfType = new Select(By.Id("setOfType")),
			SelBooleanDisplayFormat = new Select(By.Id("displayFormat_boolean")),
			SelCurrencyType = new Select(By.Id("currencyType"));

		public readonly Radio
			RdoListOption = new Radio(By.Id("enumeratedFieldBtn"));

		public readonly Checkbox
			ChkMultiLineString = new Checkbox(By.Id("multilineString"));

		public CustomAttributeDefinitionPopup() { }
	}
}
