using System.Collections.Generic;
using OpenQA.Selenium;

namespace PortalSeleniumFramework.PrimitiveElements
{
	public class Button : PageElement
	{
		public Button(By byLocator) : base(byLocator) 
		{
			ValidTags = new List<string> { "img", "input", "button" };
		}

	    public void SendKeys(string value)
	    {
	        BaseElement.SendKeys(value);
	    }

		public Button() { }
	}
}
