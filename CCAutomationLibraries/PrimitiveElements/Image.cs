using System.Collections.Generic;
using OpenQA.Selenium;

namespace CCWebUIAuto.PrimitiveElements
{
	public class Image : PageElement
	{
		public Image(By byLocator)
			: base(byLocator)
		{
			ValidTags = new List<string> { "img" };
		}

		public Image() { }
	}
}