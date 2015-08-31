using System;
using System.Diagnostics;
using CCWebUIAuto.PrimitiveElements;
using OpenQA.Selenium;

namespace CCWebUIAuto.Pages.BasePages
{
	public class EntityDataPopup : IPopup
	{
		public string Title { get { return "???"; } }

		public readonly Button
			BtnOk = new Button(By.CssSelector("input[value='OK']"));

		public TextBox
			DisplayString = new TextBox(By.CssSelector("input[name='_TestSelection.customAttributes.string']"));

		public void SetDisplayString(string name)
		{
			Trace.WriteLine(String.Format("Setting entity data name as '{0}'", name));
			DisplayString.Value = name;
		}
	}
}
