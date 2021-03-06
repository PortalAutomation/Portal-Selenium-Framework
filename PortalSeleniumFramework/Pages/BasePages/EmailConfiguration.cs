﻿using System;
using OpenQA.Selenium;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.Pages.BasePages
{
	public class EmailConfiguration : IPopup
	{
		public String Title { get { return "Configure E-mail"; } }

		public readonly Button
			BtnOk = new Button(By.CssSelector("input[value='OK']")),
			BtnAddProperty = new Button(By.XPath("//*[@id='To']//input[@value='Add Property...']"));

		public readonly TextBox
			TxtSubject = new TextBox(By.CssSelector("input[name='EmailNotificationSetting.subject']"));
	}
}
