using System;
using System.Diagnostics;
using OpenQA.Selenium;
using PortalSeleniumFramework.Helpers;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.Pages.BasePages.DataTypeCenter
{
	public class ViewsTab : CCPage
	{
		public readonly string DataTypeInternalName;

		public readonly Button
			BtnNew = new Button(By.CssSelector("input[value='New']")),
			BtnEditStandardViewProperties = new Button(By.CssSelector("input[value='Edit Standard View Properties']")),
			BtnDeleteButton = new Button(By.CssSelector("input[value='Delete']"));

		public readonly TextBox
			TxtFilterByValue = new TextBox(By.Id("_webrRSV_FilterValue_0_0"));

		public readonly Container
			ViewsTable = new Container(By.XPath(".//*[@id='_webrRSV_DIV_0']/table"));

		//public readonly Link LnkConditionalDisplay = new Link(By.XPath("//a[@title='Edit Conditional Display Rules']"));

		public ViewsTab(string dataTypeInternalName)
		{
			DataTypeInternalName = dataTypeInternalName;
		}

		/// <summary>
		/// Verifies whether a view exists in the views table
		/// </summary>
		public bool ViewExists(string name)
		{
			return new Link(By.LinkText(name)).Exists;
		}

		public void CreateNewView(string name)
		{
			Trace.WriteLine(String.Format("Creating a new entity view called {0}", name));
			BtnNew.Click();
			var popup = new EntityViewEditorPopup();
			popup.SwitchTo();
			popup.TxtDisplayName.Value = name;
			popup.BtnOk.Click();
			popup.SwitchBackToParent(WaitForPopupToClose.Yes);
		}

		/// <summary>
		/// Delete a view by it's name
		/// </summary>
		public void DeleteView(string viewName)
		{
			Trace.WriteLine(String.Format("Deleting row {0} from view table", viewName));
			CheckViewTableRow(viewName);
			BtnDeleteButton.Click();
			var alert = Web.PortalDriver.SwitchTo().Alert();
			alert.Accept();
			Wait.Until(d => !ViewExists(viewName));
		}

		/// <summary>
		/// Modify a view's name
		/// </summary>
		public void ModifyViewName(string viewName, string newViewName)
		{
			Trace.WriteLine(String.Format("Modifying entity view viewName to {0}", viewName));
			OpenView(viewName);
			var popup = new EntityViewEditorPopup();
			popup.SwitchTo();
			popup.TxtDisplayName.Value = newViewName;
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
		}

		/// <summary>
		/// Checks the checkbox for a view in the test table
		/// </summary>
		public void CheckViewTableRow(string viewName)
		{
			var tdCheckBox = new Checkbox(By.XPath("//a[text()='" + viewName + "']/../../td[1]/input[@type='checkbox']"));
			tdCheckBox.Checked = true;
		}

		public void OpenView(string viewName)
		{
			var link = new Link(By.LinkText(viewName));
			link.Click();
		}

		public override void NavigateTo()
		{
			Web.Navigate(Store.BaseUrl + "/EntityTypeCustomization/EntityTypeDetails?EntityTypeName=" + DataTypeInternalName + "&Tab=2");
		}
	}
}
