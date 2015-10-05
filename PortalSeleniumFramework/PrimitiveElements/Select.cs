using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace PortalSeleniumFramework.PrimitiveElements
{
	public class Select : PageElement
	{
		public Select(By byLocator) : base(byLocator) 
		{
			ValidTags = new List<string> { "select" };
		}

		public Select() { }

		public String SelectedOption
		{
			get {
				BaseElement.Initialize();
				var x = new SelectElement(BaseElement.webElement);
				return x.SelectedOption.Text;
			}
		}

		public void SelectOption(String value)
		{
			BaseElement.SelectByInnerText(value);
		}

		public Boolean Contains(String value)
		{
			return BaseElement.GetDescendants(".//option[text()='" + value + "']").Any();
		}

		public IEnumerable<String> GetOptionsText()
		{
			return BaseElement.GetDescendants(".//option").Select(e => e.Text).ToList();
		}

		public void DeselectAll()
		{
			BaseElement.Initialize();
			var x = new SelectElement(BaseElement.webElement);
			x.DeselectAll();
		}
	}
}
