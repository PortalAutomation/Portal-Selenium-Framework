using System;
using CCWebUIAuto.PrimitiveElements;
using OpenQA.Selenium;

namespace CCWebUIAuto.Pages.BasePages
{
	public class ActivityNotificationPopup : IPopup
	{
		public readonly Button
			BtnAdd = new Button(By.CssSelector("img[alt='Add']")),
			BtnOk = new Button(By.CssSelector("input[value='OK']")),
			BtnCancel = new Button(By.CssSelector("input[value='Cancel']")),
			BtnDelete = new Button(By.CssSelector("input[value='Delete']"));

		public String Title { get { return "Activity Notifications Editor"; } }

		public void AddNotification(string toProperty, string subjectLine)
		{
			BtnAdd.Click();
			var emailPopup = new EmailConfiguration();
			emailPopup.SwitchTo();
			var propertiesPopup = new SelectPropertyModalPopup(SelectPropertyPopup.AllowMultiSelect.Yes);
			propertiesPopup.SelectProperty(toProperty, emailPopup.BtnAddProperty);
			emailPopup.TxtSubject.Value = subjectLine;
			emailPopup.BtnOk.Click();
			emailPopup.SwitchBackToParent();
		}

		public bool VerifyNotificationExists(string name)
		{
			var activityLink = new Link(By.LinkText(name));
			return activityLink.Exists;
		}
	}
}
