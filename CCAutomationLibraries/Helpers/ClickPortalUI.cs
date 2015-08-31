using System;
using System.Diagnostics;
using System.Linq;
using CommonUtilities;
using OpenQA.Selenium.Support.UI;

namespace CCWebUIAuto.Helpers
{
	public static class ClickPortalUI
	{
		// Declare static page containers
		public static Config AutoConfig = new Config();
		public static WebDriverWait Wait;

		public static void Initialize()
		{
			// Read the buildInfo.xml file
			// This is the path we will find it at on the automation client machine.
			String pathname = @"..\..\autoConfig.xml";
			try {
				AutoConfig.read(pathname);
			} catch {
				//Fall back to this path for local dev debugging
				pathname = @"..\..\bin\debug\autoConfig.xml";
				AutoConfig.read(pathname);
			}

			// Initialize the log listener
			if (AutoConfig.ContainsKey("DebugLogLocation")) {
				if (!Trace.Listeners.OfType<DebugLogListener>().Any()) {
					DebugLogListener listener = new DebugLogListener(AutoConfig["DebugLogLocation"], "SeleniumDebugLog", false);
					Trace.Listeners.Add(listener);
				}
			}

			// Initialize the browser specified in autoconfig

			if (AutoConfig.ContainsKey("Browser")) {

				Web.Initialize(AutoConfig["Browser"]);
			} else {
				Web.Initialize();
			}

			// Set up default Store settings
			Store.BaseUrl = String.Format("{0}", AutoConfig["WebServer"]);
			Store.CurrentUser = null;

			Wait = new WebDriverWait(Web.Driver, TimeSpan.FromSeconds(7));
		}
	}
}
