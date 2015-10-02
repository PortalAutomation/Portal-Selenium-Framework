using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace PortalSeleniumFramework.Helpers
{
	public static class Wait
	{
		private readonly static WebDriverWait Pause = new WebDriverWait(Web.PortalDriver, TimeSpan.FromSeconds(7));

		public static void Until(Func<IWebDriver, bool> func)
		{
			Thread.Sleep(500);
			RetriableRunner.Run(() => Pause.Until(func));
		}
	}
}
