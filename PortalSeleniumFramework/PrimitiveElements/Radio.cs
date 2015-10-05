using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace PortalSeleniumFramework.PrimitiveElements
{
	public class Radio : PageElement
	{
		public Radio(By byLocator) : base(byLocator)
		{
			ValidTags = new List<string> { "input" };
		}

		public Radio() { }

		public Boolean Selected
		{
			get { return BaseElement.Selected; }
			set { BaseElement.Selected = value; }
		}
	}
}