using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace CCWebUIAuto.PrimitiveElements
{
	public class Link : PageElement
	{
		public Link(By byLocator)
			: base(byLocator)
		{
			ValidTags = new List<string> { "a" };
		}

		public Link() { }

		public String Text { get { return BaseElement.Text; } }
	}
}