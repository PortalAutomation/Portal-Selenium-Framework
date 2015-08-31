using System;
using CCWebUIAuto.PrimitiveElements;
using OpenQA.Selenium;

namespace CCWebUIAuto.Pages.BasePages.ScheduledBackgroundOperations
{
	public class Details : CCPage
	{
		public readonly Button
			BtnScheduling = new Button(By.Name("buttonscheduling")),
			BtnDelete = new Button(By.Name("buttonDelete")),
			BtnAdd = new Button(By.Name("buttonadd")),
			BtnOk = new Button(By.XPath("//input[@value='OK']"));

		public readonly Select
			SelActivityFromThisType = new Select(By.Name("project"));

		public override void NavigateTo()
		{
			throw new NotImplementedException();
		}
	}
}
