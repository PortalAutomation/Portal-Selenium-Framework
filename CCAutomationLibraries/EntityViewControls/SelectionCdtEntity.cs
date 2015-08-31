using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CCWebUIAuto.PrimitiveElements;
using OpenQA.Selenium;

namespace CCWebUIAuto.EntityViewControls
{
	public class SelectionCdtEntity
	{
		public readonly Button BtnSelect;

		public readonly Button BtnClear;

		public readonly Link LnkCdtEntLink;

		public readonly TextBox TxtCdtEntInput;

		public readonly String Attribute;

		public String ContainerId { get { return String.Format("_{0}_container", Attribute); } }
		public String XpathPrefix { get { return String.Format("//*[@id='{0}']", ContainerId); } }

		public SelectionCdtEntity(string attribute)
		{
			Attribute = attribute;
			BtnSelect = new Button(By.XPath(XpathPrefix + "//input[@value='Select...']"));
			BtnClear = new Button(By.XPath(XpathPrefix + "//input[@value='Clear']"));
			LnkCdtEntLink = new Link(By.XPath(XpathPrefix + "//a"));
			TxtCdtEntInput = new TextBox(By.XPath(XpathPrefix + "//input[contains(@id, '_chooser')]"));
		}
	}
}
