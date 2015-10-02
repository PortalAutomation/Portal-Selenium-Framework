using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace PortalSeleniumFramework.PrimitiveElements
{
	public class Checkbox : PageElement
	{
		public Checkbox(By byLocator) : base(byLocator) 
		{
			ValidTags = new List<string> { "input" };
		}

		public Checkbox() { }

		public Boolean Checked
		{
			get
			{
				BaseElement.Initialize(); 
				return BaseElement.webElement.Selected;
			}
			set
			{
				BaseElement.SetCheckBox(value);
			}
		}
	}
}
