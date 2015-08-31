using CCWebUIAuto.Helpers;
using CCWebUIAuto.PrimitiveElements;
using OpenQA.Selenium;

namespace CCWebUIAuto.Pages.BasePages.DataTypeCenter
{
	public class DataTypeCenter : CCPage
	{
		public readonly Button BtnDelete, BtnNew;
		public readonly Container Table;

		public DataTypeCenter()
		{
			BtnDelete = new Button(By.CssSelector("input[value='Delete']"));
			BtnNew = new Button(By.CssSelector("input[value='New']"));
			Table = new Container(By.XPath("html/body/table[4]/tbody/tr[2]/td/form/table[2]"));
		}

		public override void NavigateTo()
		{
			Web.Navigate(Store.BaseUrl + "/EntityTypeCustomization/EntityTypeCenter");
		}
	}
}
