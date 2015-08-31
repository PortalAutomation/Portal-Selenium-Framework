using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using CCWebUIAuto.PrimitiveElements;
using CommonUtilities;
using NUnit.Framework;
using OpenQA.Selenium;

namespace CCWebUIAuto.Helpers
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
                Web.Driver.Quit();
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
			RetriableRunner.Run(() => Web.Driver.SwitchTo().Alert().Accept());
		}

		public void AcceptAlert(String alertText)
		{
			RetriableRunner.Run(() => {
				var alert = Web.Driver.SwitchTo().Alert();
				Assert.AreEqual(alertText, alert.Text);
				alert.Accept();	
			});
		}

		public void SwitchToMostRecentWindow()
		{
			var handles = Web.Driver.WindowHandles;
			var handle = handles.Last();
			Web.Driver.SwitchTo().Window(handle);
		}
    }
}
