using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using PortalSeleniumFramework.Helpers;

namespace PortalSeleniumFramework.PrimitiveElements
{
    /// <summary>
    /// This class holds extensions for webdriver element types (i.e. textboxes, links, containers, etc) as well
    /// as extension methods of element locators
    /// </summary>
    public class CCElement
    {
        // specify this elsewhere in global config
        //int maxRetries = 3;
        //int waitBetweenRetries = 1000;

        /// <summary>
        /// Reference to current selenium element
        /// </summary>
        internal IWebElement webElement;

        /// <summary>
        /// By Locator used to find web element
        /// </summary>
        public By CCByLocator;

        /// <summary>
        /// An identifier for this element that will be used in logging.  
        /// </summary>
        public string ElementIdentifier = "";
        
        /// <summary>
        /// The global value of number of retries to attempt before giving up on finding an element.
        /// </summary>
        public static int GlobalMaxRetries = 3;

        /// <summary>
        /// The global value of time to wait in milliseconds in between each retry attempt to find an element.
        /// </summary>
        public static int GlobalWaitBetweenRetries = 5000;

        /// <summary>
        /// The value of number of retries to attempt before giving up on finding this element.
        /// </summary>
        public int? MaxRetries = null;

        /// <summary>
        /// The value of time to wait in milliseconds in between each retry attempt to find this element.
        /// </summary>
        public int? WaitBetweenRetries = null;

		public String TagName
		{
			get { return webElement.TagName; }
		}

        /// <summary>
        /// Element Constructor
        /// </summary>
        public CCElement()
        {
            CCByLocator = null;
        }

        /// <summary>
        /// Element Constructor using By locator
        /// </summary>
        public CCElement(By byLocator)
        {
            CCByLocator = byLocator;            
        }

        /// <summary>
        /// Clicks the element
        /// </summary>
        public virtual void Click(bool ensureVisible = true)
        {
            Initialize();
            if (ensureVisible)
            {
                EnsureVisible();
            }
            Trace.WriteLine(String.Format("Attempting to click element '{0}'", ElementIdentifier));
			webElement.Click();
        }

		
		/// <summary>
        /// Clicks the element
        /// </summary>
        public virtual void AsyncClick(bool ensureVisible = true)
        {
            Initialize();
            if (ensureVisible)
            {
                EnsureVisible();
            }
            Trace.WriteLine(String.Format("Attempting to async click element '{0}'", ElementIdentifier));
			Task.Factory.StartNew(() => webElement.Click());
        }

        /// <summary>
        /// Pass a value or multiple values from selenium's Keys class.
        /// </summary>
        /// <param name="keys">A value from Selenium's Keys class.</param>
        public virtual void SendKeys(string keys)
        {
            Initialize();
            Trace.WriteLine(String.Format("Sending key '{0}' to element '{1}'", keys, ElementIdentifier));
            webElement.SendKeys(keys);
        }

        /// <summary>
        /// Get or set the text
        /// </summary>
        public string Text
        {
            get
            {
                Initialize();
				var returnText = webElement.Text;
				Trace.WriteLine(String.Format("Getting text of element '{0}', '{1}'", ElementIdentifier, returnText));
                return returnText;
            }
            set
            {
                Initialize();
                webElement.Clear();
                Trace.WriteLine(String.Format("Setting text of element '{0}' to '{1}'", ElementIdentifier, value));
                webElement.Click();                
                webElement.SendKeys(value);
            }
        }

        public void SetAttribute(string attrName, string value)
        {
            Initialize();
            JavascriptExecutor.Execute("arguments[0].setAttribute('" + attrName + "', '" + value + "' )", webElement);
        }

        //TODO: Look up selenium documentation to see exactly what this does for other element types if anything.  Maybe move this out to just RadioButton and Checkbox classes.
        /// <summary>
        /// Gets or sets whether Checkboxes and RadioButtons are selected.
        /// </summary>
        public bool Selected
        {
            get
            {
                Initialize();
                Trace.WriteLine(String.Format("Getting selected value '{0}' from '{1}'", webElement.Selected, ElementIdentifier));
                return webElement.Selected;
            }
            set
            {
                Initialize();
                EnsureVisible();
                bool isSelected = webElement.Selected;
                Trace.WriteLine(String.Format("Setting selected value '{0}' from '{1}'", value, ElementIdentifier));
                if (isSelected != value)
                {
                    webElement.Click();
                }
            }
        }

        /// <summary>
        /// Checks a checkbox element depending on bool value supplied.
        /// </summary>
        /// <param name="checkBoxValue"></param>
        public void SetCheckBox(bool checkBoxValue)
        {
            Initialize();
            Trace.WriteLine(String.Format("Setting checkbox '{0}' to value '{1}'", ElementIdentifier, checkBoxValue));
			if (webElement.Selected != checkBoxValue) {
				webElement.Click();
			}
        }

        /// <summary>
        /// Returns whether the element exists or not
        /// </summary>
        public bool Exists
        {
            get
            {
                try
                {
                    RetriableRunner.Run(() =>
                    {
						Web.PortalDriver.FindElement(CCByLocator);
                    }, 3);
                }
                catch (Exception)
                {
                    Trace.WriteLine(String.Format("element '{0}' not found", ElementIdentifier));
                    return false;
                }
                Trace.WriteLine(String.Format("element '{0}' found", ElementIdentifier));
                return true;
            }
        }

        /// <summary>
        /// Gets whether the element is enabled or disabled.
        /// </summary>
        public bool Enabled
        {
            get
            {
                Initialize();
                Trace.WriteLine(String.Format("Getting whether '{0}' element is enabled.", ElementIdentifier));
                return webElement.Enabled;
            }
        }

        /// <summary>
        /// Gets whether the element is displayed or not.
        /// </summary>
        public bool Displayed
        {
            get
            {
				Trace.WriteLine(String.Format("Getting whether '{0}' element is displayed.", ElementIdentifier));
				try {
					Initialize();
				} catch {
					return false;
				}
                return webElement.Displayed;
            }
        }

        /// <summary>
        /// Verifies whether the specified text matches the element's text value.
        /// </summary>
        /// <param name="textToVerify">Expected text in the element's text value.</param>
        public bool VerifyText(string textToVerify)
        {
            return VerifyText(textToVerify, true);
        }

        /// <summary>
        /// Verifies whether the specified text matches the element's text value.
        /// </summary>
        /// <param name="textToVerify">Expected text in the element's text value.</param>
        /// <param name="exactMatch">Whether to match exactly or do a partial case insensitive match.</param>
        public bool VerifyText(string textToVerify, bool exactMatch)
        {
            Initialize();
            bool returnValue = false;
            Trace.WriteLine(String.Format("Verifying text '{0}' matches actual text '{1}' for element '{2}'", textToVerify, Text, ElementIdentifier));
            if (exactMatch)
            {
                if (textToVerify == Text)
                {
                    returnValue = true;
                    //throw new Exception("Text did not match expected value.");
                }
            }
            else
            {
                if (Text.ToLowerInvariant().Contains(textToVerify.ToLowerInvariant()))
                {
                    returnValue = true;
                    //throw new Exception("Text did not match expected value.");
                }
            }
            return returnValue;
        }

        internal void EnsureVisible()
        {
            int maxRetries = MaxRetries == null ? GlobalMaxRetries : (int)MaxRetries;
            int waitBetweenRetries = WaitBetweenRetries == null ? GlobalWaitBetweenRetries : (int)WaitBetweenRetries;

            // Check to make sure the element is visible and displayed and wait if it is not.
            for (int i = 0; i <= maxRetries; i++)
            {
                if (i == maxRetries - 1)
                {
                    throw new Exception(String.Format("Tried '{0}' times and the element '{1}' is still not displayed or enabled.", maxRetries + 1, ElementIdentifier));
                }
                if (!webElement.Displayed || !webElement.Enabled)
                {
                    Trace.WriteLine(String.Format("The element '{0}' is either not displayed or not enabled.", ElementIdentifier));
                    Thread.Sleep(waitBetweenRetries);
                    continue;
                }

                break;
            }
        }

        /// <summary>
        /// Selects an option item from the ComboBox
        /// </summary>
        /// <param name="selectOptionText">The text of the option to select.</param>
        public void SelectByInnerText(string selectOptionText)
        {
            Initialize();
            EnsureVisible();
            Trace.WriteLine(String.Format("Selecting option text '{0}' from ComboBox '{1}'", selectOptionText, ElementIdentifier));
            ReadOnlyCollection<IWebElement> options = webElement.FindElements(By.TagName("option"));
            IWebElement matchingOption = options.FirstOrDefault(opt => opt.Text == selectOptionText);
            if (matchingOption == null)
            {
                throw new Exception(String.Format("The option '{0}' was not found in the ComboBox '{1}'", selectOptionText, ElementIdentifier));
            }
            matchingOption.Click();
        }

        /// <summary>
        /// Selects an option item from the ComboBox
        /// </summary>
        /// <param name="attributeName">The name of the attribute to match on</param>
        /// <param name="attributeValue">The value of the attribute to match on</param>
        public void SelectByAttribute(string attributeName, string attributeValue)
        {
            Initialize();
            EnsureVisible();
            Trace.WriteLine(String.Format("Selecting option with '{0}' attribute value '{1}' from ComboBox '{2}'", attributeName, attributeValue, ElementIdentifier));
            ReadOnlyCollection<IWebElement> options = webElement.FindElements(By.TagName("option"));
            IWebElement matchingOption = options.FirstOrDefault(opt => opt.GetAttribute(attributeName) == attributeValue);
            if (matchingOption == null)
            {
                throw new Exception(String.Format("The option '{0}' attribute '{1}' was not found in the ComboBox '{2}'", attributeName, attributeValue, ElementIdentifier));
            }
            matchingOption.Click();
        }

        /// <summary>
        /// Selects an option item from the ComboBox
        /// </summary>
        /// <param name="selectOptionValue">The text of the option to select.</param>
        public void SelectByValue(string selectOptionValue)
        {
            SelectByAttribute("value", selectOptionValue);
        }

        /// <summary>
        /// Selects an option item from the ComboBox
        /// </summary>
        /// <param name="index">The index of the option to select.</param>
        public void SelectByIndex(int index)
        {
            Initialize();
            EnsureVisible();
            Trace.WriteLine(String.Format("Selecting index '{0}' from ComboBox '{1}'", index.ToString(), ElementIdentifier));
            ReadOnlyCollection<IWebElement> options = webElement.FindElements(By.TagName("option"));
            if (options != null)
            {
				options[index].Click();
			}
        }

		public String Value
		{
			get
			{
				Initialize();
				Trace.WriteLine(String.Format("Obtaining attribute value for 'value'"));
				return webElement.GetAttribute("value");
			}
		}

        public string GetAttributeValue(string attribute)
        {
            Initialize();
            Trace.WriteLine(String.Format("Obtaining attribute value for '{0}'", attribute));
            return webElement.GetAttribute(attribute);
        }

        /// <summary>
        /// Gets List of descendants matching the XPath
        /// </summary>
        /// <param name="xPath">The XPath to match the descendant elements. Must start with '.' to specify current element.</param>
        /// <returns>The found descendant elements</returns>
        public List<CCElement> GetDescendants(string xPath)
        {
            Initialize();
            Trace.WriteLine(String.Format("Attempting to find descendants of '{0}'.", ElementIdentifier));
            List<CCElement> elements = new List<CCElement>();
			foreach (IWebElement iWebElement in webElement.FindElements(By.XPath(xPath)))
            {
                CCElement element = new CCElement();
                element.webElement = iWebElement;
                elements.Add(element);
            }

            return elements;
        }

        public CCElement GetDescendant(string xPath)
        {
            Initialize();
            Trace.WriteLine(String.Format("Attempting to find descendant of '{0}.", ElementIdentifier));
            IWebElement iwebElement = webElement.FindElement(By.XPath(xPath));
            CCElement element = new CCElement();
            element.webElement = iwebElement;

            if (webElement != null)
            {
                return element;
            }
            else
            {
                throw new Exception(String.Format("There was no descendant found for '{0}'", ElementIdentifier));
            }
        }

        /// <summary>
        /// Gets List of all descendants
        /// </summary>
        /// <returns>The found descendant elements</returns>
        public List<CCElement> GetDescendants()
        {
            // Gets All Descendants
            Trace.WriteLine(String.Format("Attempting to find all descendants of '{0}'.", ElementIdentifier));
            return GetDescendants(".//*");
        }

        public CCElement GetParent()
        {
            Initialize();
            Trace.WriteLine(String.Format("Attempting to find parent of '{0}.", ElementIdentifier));
            IWebElement iwebElement = webElement.FindElement(By.XPath(".."));
            CCElement element = new CCElement();
            element.webElement = iwebElement;

            if (webElement != null)
            {
                return element;
            }
            else
            {
                throw new Exception(String.Format("There was no parent of '{0}'", ElementIdentifier));
            }

        }

        /// <summary>
        /// Called every time to check the existence of an element, sets the elementIdentifier used for logging purposes.
        /// </summary>
        /// <returns></returns>
        public bool Initialize()
        {
            if (CCByLocator != null)
            {
                Trace.WriteLine(String.Format("Finding element with By identifier '{0}'", CCByLocator));
                return RetriableRunner.Run(() =>
                {
                    Trace.WriteLine("... " + CCByLocator);
					webElement = Web.PortalDriver.FindElement(CCByLocator);
                    ElementIdentifier = webElement.TagName;
                    return webElement != null;
                });
            }

            // this path assumes element has already been found (used by getDescendents)           
            return webElement != null;
        }
    }

}
