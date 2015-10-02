using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace PortalSeleniumFramework.PrimitiveElements
{
	public abstract class PageElement
	{
		public CCElement BaseElement;

		internal List <String> ValidTags = null;

		public Boolean Exists
		{
			get { return BaseElement.Exists;  }
		}

		public Boolean Enabled
		{
			get { return BaseElement.Enabled; }
		}

		public Boolean Displayed
		{
			get { return BaseElement.Displayed; }
		}

		public Boolean IsValidTag
		{
			get { return ValidTags.Contains(BaseElement.TagName); }
		}

		protected PageElement(By byLocator)
		{
			BaseElement = new CCElement(byLocator);
			if (ValidTags != null && !ValidTags.Contains(BaseElement.TagName.ToLower())) {
				throw new Exception("Tag '" + BaseElement.TagName + "' is not valid");
			}
		}

		protected PageElement(CCElement baseElement)
		{
			BaseElement = baseElement;
		}

		public PageElement()
		{
			
		}

		public String TagName
		{
			get { return BaseElement.TagName; }
		}

		public void Click()
		{
			BaseElement.Click();
		}

		public void AsyncClick()
		{
			BaseElement.AsyncClick();
		}
	}


}
