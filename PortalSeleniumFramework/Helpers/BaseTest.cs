using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using CommonUtilities;
using NUnit.Framework;
using OpenQA.Selenium;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.Helpers
{
  public class BaseTest
    {
        
        [SetUp]
        public void Setup()
        {
            const String pathname = @"..\..\autoConfig.xml";
            try
            {
               ClickPortalUI.AutoConfig.read(pathname);
               ClickPortalUI.Initialize();
               Trace.WriteLine(String.Format("Executing test: {0}", TestContext.CurrentContext.Test.FullName));
            }
            catch (Exception e)
            {
                ExceptionHandler.HandleException(e);
            }
        }

        [TearDown]
        public void TearDown()
        {
            //bool deleteRecording = true;

            try
            {
                Trace.WriteLine("Closing browser.");
                Web.PortalDriver.Quit();
            }
            catch (Exception ex)
            {
				Web.HandleException(ex);
            }
        }

		public Button Button(By locator)
		{
			return new Button(locator);
		}

		public Link Link(By locator)
		{
			return new Link(locator);
		}

		public TextBox TextBox(By locator)
		{
			return new TextBox(locator);
		}

		public void AcceptAlert()
		{
			RetriableRunner.Run(() => Web.PortalDriver.SwitchTo().Alert().Accept());
		}

		public void AcceptAlert(String alertText)
		{
			RetriableRunner.Run(() => {
				var alert = Web.PortalDriver.SwitchTo().Alert();
				Assert.AreEqual(alertText, alert.Text);
				alert.Accept();	
			});
		}

		public void SwitchToMostRecentWindow()
		{
			var handles = Web.PortalDriver.WindowHandles;
			var handle = handles.Last();
			Web.PortalDriver.SwitchTo().Window(handle);
		}
    }
}
