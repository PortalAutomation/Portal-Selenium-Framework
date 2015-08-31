using System;
using CCWebUIAuto.Helpers;
using CCWebUIAuto.PrimitiveElements;
using OpenQA.Selenium;

namespace CCWebUIAuto.Pages.BasePages
{
	public class LoginPage : LayoutInitialBasedPage
	{
		/// <summary>
		/// Login link on home page
		/// </summary>
		public readonly Link LnkLogin = new Link(By.LinkText("Login"));
		/// <summary>
		/// User text box
		/// </summary>
		public readonly TextBox TxtUser = new TextBox(By.Name("username"));
		/// <summary>
		/// Password text box
		/// </summary>
		public readonly TextBox TxtPassword = new TextBox(By.Name("password"));
		/// <summary>
		/// sign in button
		/// </summary>
		public readonly Button BtnSignin = new Button(By.CssSelector("input.Button"));

		/// <summary>
		/// ctr, always set the URL here
		/// </summary>
		public LoginPage()
			: base("/Rooms/DisplayPages/LayoutInitial?Container=com.webridge.entity.Entity%5BOID%5B0A7646F3B149874E902185897C144551%5D%5D")
		{
			Url = String.Format("{0}", ClickPortalUI.AutoConfig["WebServer"]);
		}

		/// <summary>
		/// Navigates to the login page and nters the credentials
		/// </summary>
		public void Login(string userName, string password)
		{
            // Should be using Store BaseURL which is loaded on ClickPortalUI.Initalize()
			Web.Navigate(Store.BaseUrl);
            // lnkLogin is technically on the "home page" versus login page
		    if (LnkLogin.Exists)
		    {
                LnkLogin.Click();
		    }
			TxtUser.Value = userName;
			TxtPassword.Value = password;
			BtnSignin.Click();
			Wait.Until(d => new Link(By.LinkText("Logoff")).Exists);
		}

		public bool ValidateLocation()
		{
			// TODO:  need a better check
			if (!LnkLogin.Exists) {
				throw new Exception("Sign In button does not exist, not on the Login page.");
			}
			return true;
		}
	}
}
