using CCWebUIAuto.Pages.BasePages;
using CCWebUIAuto.PrimitiveElements;
using OpenQA.Selenium;

namespace CCWebUIAuto.Helpers
{
	public class Store
	{
		public static string BaseUrl;
		public static UserAccount CurrentUser;

		public static string CurrentPageSource { get { return Web.Driver.PageSource; } }

		// This does two things
		// -logs us in and out of the store
		// -tracks who is currently logged in
		public static void LoginAsUser(UserAccount user)
		{
			var loginPage = new LoginPage();
			if (CurrentUser == user) return;

			Web.Navigate(BaseUrl);
			if (CurrentUser != null) {
				new Link(By.LinkText("Logoff")).Click();
				CurrentUser = null;
				Wait.Until(d => new Button(By.CssSelector("input[value='Login']")).Exists);
			}
			if (user != null) {
				loginPage.Login(user.UserName, user.Password);
			}
			CurrentUser = user;
		}

		public static void Logout()
		{
			Web.Navigate(BaseUrl);
			new Link(By.LinkText("Logoff")).Click();
			CurrentUser = null;
			Wait.Until(d => new Button(By.CssSelector("input[value='Login']")).Exists);
		}
	}
}
