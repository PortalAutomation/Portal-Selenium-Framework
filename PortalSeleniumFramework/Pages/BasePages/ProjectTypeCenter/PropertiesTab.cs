using OpenQA.Selenium;
using PortalSeleniumFramework.Helpers;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.Pages.BasePages.ProjectTypeCenter
{
	public class PropertiesTab : CCPage, ICustomAttributesTable
	{
		public readonly string ProjectTypeInternalName;

		public Button BtnNewCustomAttribute { get; set; }
		public Button BtnDeleteCustomAttribute { get; set; }
		public Container CustomAttributesForm { get; set; }

		public readonly Button
			BtnChooseDisplayValue = new Button(By.CssSelector("input[id='btnOpenDisplayAttributeChooser']")),
			OkButton = new Button(By.CssSelector("input[value='OK']"));

		public readonly TextBox
			DisplayName = new TextBox(By.CssSelector("input[id='EntityTypeDisplayName']")),
			TxtCustomizeInternalName = new TextBox(By.Id("EntityTypeInternalNameSuffix")),
			Description = new TextBox(By.CssSelector("input[id='EntityTypeDescription']")),
			IdPrefix = new TextBox(By.CssSelector("input[id='IDPrefix']")),
			DisplayValue = new TextBox(By.CssSelector("input[id='displayFieldName']"));

		public readonly Container
			SpanInternalName = new Container(By.Id("EntityTypeInternalName_text")),
			AttributesTable = new Container(By.XPath(".//*[@id='CustomAttributesForm']/table[3]")),
			Table = new Container(By.XPath(".//*[@id='CustomAttributesForm']/table[3]"));

		public PropertiesTab(string projTypeInternalName)
		{
			ProjectTypeInternalName = projTypeInternalName;
			this.InitializeCustomAttributesTableUiElements();
		}

		public override void NavigateTo()
		{
			WaitForPageLoad();
			Web.Navigate(Store.BaseUrl + "/ProjectCustomization/ProjectTypeCenter/ProjectTypeDetails?EntityTypeName=" + ProjectTypeInternalName + "&Tab=1");
		}
	}
}
