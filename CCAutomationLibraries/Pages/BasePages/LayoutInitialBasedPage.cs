using CCWebUIAuto.Helpers;
using CCWebUIAuto.PrimitiveElements;
using OpenQA.Selenium;

namespace CCWebUIAuto.Pages.BasePages
{
	public class LayoutInitialBasedPage : CCPage
	{
		public readonly Button
			BtnPageComponents = new Button(By.XPath("//img[contains(@src, 'components.png')]/.."));

		public LayoutInitialBasedPage(string newUrl)
		{
			Url = newUrl;
		}

		public override void NavigateTo()
		{
			Web.Navigate(Store.BaseUrl + Url);
		}
	}
}
