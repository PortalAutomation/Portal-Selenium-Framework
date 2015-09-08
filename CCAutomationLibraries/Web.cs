using System;
using System.Diagnostics;
using System.IO;
using CCWebUIAuto.Helpers;
using ClickPortal.PortalDriver;
using CommonUtilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace CCWebUIAuto
{

	/// <summary>
	/// Wrapper class for selenium webdriver class
	/// </summary>
	public static class Web
	{
		public static Browsers CurrentBrowser = Browsers.Default;
		public static PortalDriver Driver;

		public static void Maximize()
		{
			Trace.WriteLine(String.Format("Maximizing browser window."));
			Driver.Manage().Window.Maximize();
		}

		/// <summary>
		/// Navigates to a url
		/// </summary>
		/// <param name="url"></param>
		public static void Navigate(string url)
		{
			Trace.WriteLine(String.Format("Navigating to URL '{0}'", url));
			Driver.Navigate().GoToUrl(url);
		}

		/// <summary>
		/// Navigate forward, backward or refresh.
		/// </summary>
		/// <param name="navType">Forward, Backward, Refresh</param>
		public static void Navigate(NavigateType navType)
		{
			Trace.WriteLine(String.Format("Navigating: '{0}'", navType.ToString()));
			switch (navType) {
			case NavigateType.Back:
				Driver.Navigate().Back();
				break;
			case NavigateType.Forward:
				Driver.Navigate().Forward();
				break;
			case NavigateType.Refresh:
				Driver.Navigate().Refresh();
				break;
			}
		}

		/// <summary>
		/// Wait for page load.  Use if Selenium blocking API is not sufficient.
		/// </summary>
		public static void WaitForPageLoad(int msTimeout)
		{
			RetriableRunner.Run(() => {
				var isPageLoaded = false;
				var endTime = DateTime.Now.AddMilliseconds(msTimeout);

				while (isPageLoaded == false && DateTime.Now < endTime) {
					isPageLoaded = JavascriptExecutor.Execute<string>("return document.readyState").Equals("complete");
					System.Threading.Thread.Sleep(100);
				}

				if (isPageLoaded == false) {
					throw new Exception("Timeout period of " + msTimeout + "ms expired for page load to finish.");
				}

				return string.Empty;
			});
		}

		/// <summary>
		/// Takes a screenshot in jpeg format and saves it as the specified filename
		/// </summary>
		/// <param name="fileName">Filename of the saved screenshot.</param>
		public static void TakeScreenShot(string fileName)
		{
			var screen = ((ITakesScreenshot) Driver).GetScreenshot();
			// I'm not sure what resharper is going on about here, not sure where the assignment is happening?
			// ReSharper disable AssignNullToNotNullAttribute
			if (!Directory.Exists(Path.GetDirectoryName(fileName))) {
				Directory.CreateDirectory(Path.GetDirectoryName(fileName));
			}
			// ReSharper restore AssignNullToNotNullAttribute
			screen.SaveAsFile(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
		}

		public static void Close()
		{
			Driver.Close();
		}

		/// <summary>
		/// Creates an instance of the IWebDriver and spawns a new browser window.  Uses browser from config.
		/// </summary>
		public static void Initialize()
		{
			Initialize(Browsers.Default);
		}

		/// <summary>
		/// Creates an instance of the IWebDriver and spawns a new browser window.  Uses browser from config.
		/// </summary>
        public static void Initialize(string Browser)
        {
            var browser = (Browsers) Enum.Parse(typeof(Browsers), Browser);
            Initialize(browser);
        }

		/// <summary>
		/// Initialize browser
		/// </summary>
        public static void Initialize(Browsers browser)
        {
            Trace.WriteLine(String.Format("Browser selection is '{0}'", browser));

            if (browser == Browsers.Default)
            {
                if (Enum.TryParse(Environment.GetEnvironmentVariable("UITest.Browser", EnvironmentVariableTarget.User), true, out browser))
                {
                    Trace.WriteLine(String.Format("Browser from environment variable UITest.Browser is '{0}'", browser));
                }
                else
                {
                    browser = Browsers.Firefox; //If it is set to default and there is no browser environment variable, fall back to firefox.
                }
            }

            Trace.WriteLine(String.Format("Spawning a '{0}' browser window.", browser));
            CurrentBrowser = browser;
            switch (browser)
            {
                case Browsers.Chrome:
                    IWebDriver chromeDriver = new ChromeDriver();
                    Driver = new PortalDriver(null, chromeDriver);
                    break;
                case Browsers.IE:
                    var options = new InternetExplorerOptions
                    {
                        EnableNativeEvents = false,
                        EnablePersistentHover = true,
                        ForceCreateProcessApi = true
                    };
                    //options.BrowserCommandLineArguments = "-private";
                    IWebDriver ieDriver = new InternetExplorerDriver(options);
                    Driver = new PortalDriver(null, ieDriver);
                    break;
                case Browsers.Firefox:
                case Browsers.Default:
                    var profile = new FirefoxProfile { AcceptUntrustedCertificates = true };
                    IWebDriver ffDriver = new FirefoxDriver(new FirefoxBinary(), profile, TimeSpan.FromMinutes(3));
                    Driver = new PortalDriver(null,ffDriver);
                    break;
                default:
                    Debug.Assert(false, "new browser enum value added... need to add a case here");
                    //Trace.WriteLine(String.Format("Browser '{0}' not recognized.  Spawning default Firefox browser.", browser));
                    //IWebDriver firefoxDriver = new FirefoxDriver(new FirefoxBinary(), profile, TimeSpan.FromMinutes(3));//Driver = new FirefoxDriver();
                    break;
            }
            
            Maximize();
        }
        
		public static void HandleException(Exception ex)
		{
			ExceptionHandler.HandleException(ex, true);

		}
	}

	public enum NavigateType
	{
		Back,
		Forward,
		Refresh
	}


	/// <summary>
	/// Browsers, default will get the browser from environment variable.
	/// </summary>
	public enum Browsers
	{
		Firefox,
		Chrome,
		IE,
		Default
	}
}
