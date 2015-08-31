using System;
using System.Diagnostics;
using CCWebUIAuto.Helpers;
using OpenQA.Selenium;

namespace CCWebUIAuto.Pages
{
	/// <summary>
	/// Container class for all elements
	/// </summary>
	public abstract class CCPage
	{

		public string Url;

		/// <summary>
		/// Navigates to the Url specified by navType
		/// </summary>
		/// <param name="navType">Where to navigate to</param>
		public void Navigate(NavigateType navType)
		{
			Web.Navigate(navType);
			Trace.WriteLine(String.Format("Successfully navigated to '{0}', validating location.", Url));
		}

		public abstract void NavigateTo();

		public static void NavigateBack()
		{
			Web.Navigate(NavigateType.Back);
		}

		public static void NavigateForward()
		{
			Web.Navigate(NavigateType.Forward);
		}

		public static void Refresh()
		{
			Web.Navigate(NavigateType.Refresh);
		}

		/// <summary>
		/// Allows you to execute any script you want
		/// </summary>
		/// <param name="script">The script to execute. </param>
		/// <param name="args">Values to replace in the script, using syntax [0]</param>
		/// <returns></returns>
		public object ExecuteScript(string script, params object[] args)
		{
			Trace.WriteLine(String.Format("Executing javascript: '{0}'.", script));
			return JavascriptExecutor.Execute<object>(script, args);
		}

		/// <summary>
		/// Returns the title of the current page.
		/// </summary>
		/// <returns></returns>
		public string GetTitle()
		{
			return Web.Driver.Title;
		}

		public static string CurrentWindowTitle
		{
			get
			{
				return Web.Driver.Title;
			}
		}

		/// <summary>
		/// Wait for all AJAX calls made via jQuery to end
		/// </summary>
		public void waitForJQueryToFinish(int msTimeout)
		{
			Trace.WriteLine("Waiting for Ajax loading to finish");
			DateTime startTime = DateTime.Now;
			bool isAjaxFinished = false;
			DateTime endTime = DateTime.Now.AddMilliseconds(msTimeout);
			try {
				while (isAjaxFinished == false && DateTime.Now < endTime) {
					isAjaxFinished = JavascriptExecutor.Execute<bool>("return jQuery.active == 0");
					System.Threading.Thread.Sleep(100);
				}
			} catch (WebDriverException ex) {
				// Sometimes we'll hit the exception Unexpected error. ReferenceError: jQuery is not defined
				// We may want to change this to retry since it might take time for jQuery to be defined?
				Trace.WriteLine("Recieved the following exception: " + ex.Message);
				return;
			}
			if (isAjaxFinished == false) {
				throw new Exception("Timeout period of " + msTimeout + "ms expired for JQuery to finish.");
			}
			Trace.WriteLine("Ajax finished executing in " + (DateTime.Now - startTime).TotalMilliseconds + "ms");
		}

		/// <summary>
		/// Wait for page load.  Use if Selenium blocking API is not sufficient.
		/// </summary>
		public void WaitForPageLoad(int msTimeout = 5000)
		{
			Trace.WriteLine("Waiting for page load.");
			DateTime startTime = DateTime.Now;
			Boolean isPageLoaded = false;
			DateTime endTime = DateTime.Now.AddMilliseconds(msTimeout);
			try {
				while (isPageLoaded == false && DateTime.Now < endTime) {
					isPageLoaded = JavascriptExecutor.Execute<string>("return document.readyState").Equals("complete");
					System.Threading.Thread.Sleep(100);
				}
			} catch (WebDriverException ex) {
				Trace.WriteLine("Recieved the following exception: " + ex.Message);
				return;
			}
			if (isPageLoaded == false) {
				throw new Exception("Timeout period of " + msTimeout + "ms expired for page load to finish.");
			}
			Trace.WriteLine("Page load finished executing in " + (DateTime.Now - startTime).TotalMilliseconds + "ms");
		}
	}
}
