using OpenQA.Selenium;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.Pages.BasePages
{
	public class EntityChooserFieldPopup : IPopup
	{
		public string Title { get { return "Chooser Field"; } }

		public readonly TextBox
			TxtPropertyName = new TextBox(By.CssSelector("input[class='inputControl']")),
			TxtOrder = new TextBox(By.XPath(".//*[@id='frmEntityChooserField']/table/tbody/tr[2]/td/table/tbody/tr[4]/td[3]/input")),
			TxtDisplay = new TextBox(By.XPath(".//*[@id='frmEntityChooserField']/table/tbody/tr[2]/td/table/tbody/tr[3]/td[3]"));

		public readonly Button
			BtnOk = new Button(By.CssSelector("input[name='btnOK']")),
			BtnCancel = new Button(By.CssSelector("input[name='btnCancel']")),
			BtnSelectProperty = new Button(By.Name("btnBrowseProperties"));

		public readonly Checkbox
			ChkSorting = new Checkbox(By.XPath(".//*[@id='frmEntityChooserField']/table/tbody/tr[2]/td/table/tbody/tr[6]/td[3]/input[1]")),
			ChkFiltering = new Checkbox(By.XPath(".//*[@id='frmEntityChooserField']/table/tbody/tr[2]/td/table/tbody/tr[7]/td[3]/input[1]"));

		public void SelectProperty(string name)
		{
			var popup = new SelectPropertyModalPopup();
			popup.SelectProperty(name, BtnSelectProperty);
		}
	}
}
