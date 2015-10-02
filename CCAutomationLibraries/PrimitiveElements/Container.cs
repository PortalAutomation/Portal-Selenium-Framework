using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace PortalSeleniumFramework.PrimitiveElements
{
	public class Container : PageElement
	{
		public String Text
		{
			get { return BaseElement.Text; }
		}

		public Container(By byLocator) : base(byLocator)
		{
			ValidTags = new List<string> { "div", "span", "form", "table", "tr", "td" };
		}

		public Container(CCElement baseElement) : base(baseElement)
		{
			ValidTags = new List<string> { "div", "span", "form", "table", "tr", "td" };
		}

		public Container() { }

		public IEnumerable<T> GetChildren<T>(String xpath) where T : PageElement, new()
		{
			return BaseElement
				.GetDescendants(xpath)
				.Select(e => new T {BaseElement = e})
				.ToList();
		}

		public IEnumerable<CCElement> GetDescendants(String xpath)
		{
			return BaseElement.GetDescendants(xpath);
		} 
	}
}