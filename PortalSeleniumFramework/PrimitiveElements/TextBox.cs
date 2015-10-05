using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace PortalSeleniumFramework.PrimitiveElements
{
	public class TextBox : PageElement
	{
		public String Value
		{
			get
			{
				BaseElement.Initialize();
				return BaseElement.Value.TrimEnd();
			}
			set
			{
				BaseElement.Initialize();
				BaseElement.webElement.SendKeys(Keys.Control + "a");
				BaseElement.webElement.SendKeys(value);
			}
		}

		public TextBox(By byLocator) : base(byLocator) 
		{
			ValidTags = new List<string> { "input", "textarea" };
		}

	    public void SendKeys(string stringValue)
	    {
	        BaseElement.SendKeys(stringValue);
	    }

		public TextBox() { }
	}
}
