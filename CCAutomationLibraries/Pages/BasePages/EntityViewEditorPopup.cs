using System;
using CCWebUIAuto.PrimitiveElements;
using OpenQA.Selenium;

namespace CCWebUIAuto.Pages.BasePages
{
	/// <summary>
	/// This class represents the popup window for creating new entity views
	/// </summary>
	public class EntityViewEditorPopup : IPopup
	{
		public readonly Button
			BtnOk = new Button(By.XPath(".//*[@id='frmEntityView']/table/tbody/tr[3]/td/table/tbody/tr/td[2]/input[2]"));

		public readonly TextBox
			TxtDisplayName = new TextBox(By.CssSelector("input[name='EntityView.name']"));


		public String Title { get { return "Entity View Editor"; } }
	}
}
