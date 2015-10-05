using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.Pages.BasePages
{
	public class RuleLogicPage : IPopup
	{
		public String Title
		{
			get
			{
				return "Rule Logic";
			}
		}

		public readonly Image ImgPlus = new Image(By.XPath("//div[@id='IDENTIFIER' and contains(@style, 'display: block')]//img[contains(@src, 'plus.gif')]"));

		public readonly Container
			CntID = new Container(By.XPath("//td[@id='CellTreeExpr']//span[text()='ID']")),
			CntName = new Container(By.XPath("//td[@id='CellTreeExpr']//span[text()='Name']"));

		public readonly Button
			BtnAddCriteria = new Button(By.Id("btnAddCriteria")),
			BtnAddCriterion = new Button(By.Id("btnAddCriterion")),
			BtnSave = new Button(By.XPath("//input[@value='Save']")),
			BtnClose = new Button(By.XPath("//input[@value='Close']"));

		public readonly TextBox TxtFilterValue = new TextBox(By.XPath("//div[@id='Wiz_Div_Text']/input"));

		public readonly Select SelCriteria = new Select(By.Id("selCriteria"));

	}
}
