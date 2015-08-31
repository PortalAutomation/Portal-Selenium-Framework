using System;
using System.Diagnostics;
using CCWebUIAuto.Helpers;
using CCWebUIAuto.PrimitiveElements;
using OpenQA.Selenium;

namespace CCWebUIAuto.Pages.BasePages.ProjectTypeCenter
{
	public class StatesTab : CCPage
	{
		public readonly String ProjectTypeInternalName;

		public readonly Button
			BtnEditProperties = new Button(By.CssSelector("input[value='Edit Properties']")),
			BtnNew = new Button(By.CssSelector("input[value='New']")),
			BtnDelete = new Button(By.CssSelector("input[value='Delete']"));

		public readonly Container
			StatesTable = new Container(By.XPath(".//*[@id='_webrRSV_DIV_0']/table"));

		public readonly Link
			ViewStateDefLink = new Link(By.LinkText("View State Definition"));

		public StatesTab(String projTypeInternalName)
		{
			ProjectTypeInternalName = projTypeInternalName;
		}

		public override void NavigateTo()
		{
			WaitForPageLoad();
			Web.Navigate(Store.BaseUrl + "/ProjectCustomization/ProjectTypeCenter/ProjectTypeDetails?EntityTypeName=" + ProjectTypeInternalName + "&Tab=3");
		}

		public void ViewStateDefinitions()
		{
			Trace.WriteLine("Viewing State Definitions.");
			ViewStateDefLink.Click();
		}

		public void EditProperties(String viewName)
		{
			Trace.WriteLine("Editing State Properties.");
			BtnEditProperties.Click();
			var popup = new EditStateProperties();
			popup.SwitchTo();
			popup.SelProjectStateView.SelectOption(viewName);
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
		}

		public void CreateNewState(String stateName)
		{
			Trace.WriteLine(String.Format("Creating state named: {0}.", stateName));
			BtnNew.Click();
			var popup = new ProjectStateForm();
			popup.SwitchTo();
			popup.TxtName.Value = stateName;
			popup.BtnOk.Click();
			popup.SwitchBackToParent(WaitForPopupToClose.Yes);
		}

		public void ModifyStateName(String stateToModify, String newStateName)
		{
			var stateNoteLink = new Link(By.LinkText(stateToModify));
			stateNoteLink.Click();
			var popup = new ProjectStateForm();
			popup.SwitchTo();
			popup.TxtName.Value = newStateName;
			popup.BtnOk.Click();
			popup.SwitchBackToParent(WaitForPopupToClose.Yes);
		}

		/// <summary>
		/// Verifies that a state exists.
		/// </summary>
		public bool VerifyStateExists(String name)
		{
			var returnValue = false;
			Trace.WriteLine(String.Format("Verifying if state '{0}' exists", name));
			if (StatesTable.Text.Contains(name)) {
				Trace.WriteLine(String.Format("State '{0}' exists", name));
				returnValue = true;
			} else {
				Trace.WriteLine(String.Format("State '{0}' does not exist", name));
			}
			return returnValue;
		}


		/// <summary>
		/// Delete a view by it's name
		/// </summary>
		public void DeleteState(String stateName)
		{
			Trace.WriteLine(String.Format("Deleting row {0} from states table", stateName));
			CheckViewTableRow(stateName);
			BtnDelete.Click();
			var alert = Web.Driver.SwitchTo().Alert();
			alert.Accept();
			Wait.Until(d => !new Link(By.LinkText(stateName)).Exists);
		}

		/// <summary>
		/// Checks the row in state table
		/// </summary>
		/// <param name="stateName"></param>
		public void CheckViewTableRow(String stateName)
		{
			var tdCheckBox = new Checkbox(By.XPath("//a[text()='" + stateName + "']/../../td[1]/input[@type='checkbox']"));
			tdCheckBox.Checked = true;
		}

	}

	public class ProjectStateForm : IPopup
	{
		public String Title { get { return "Project State Form"; } }

		public readonly Select
			SelProjectUpdateActivity = new Select(By.CssSelector("select[name='ProjectStatus.updateActivityType']")),
			SelProjectWorkspaceTemplate = new Select(By.CssSelector("select[name='ProjectStatus.workspaceTemplate']"));

		public readonly TextBox
			TxtName = new TextBox(By.Id("ProjectStatus.ID")),
			BtnOk = new TextBox(By.CssSelector("input[class='Button'][value='OK']"));

		public readonly Button
			BtnCancel = new Button(By.CssSelector("input[value='Cancel']")),
			BtnApply = new Button(By.CssSelector("input[value='Apply']"));
	}

	public class EditStateProperties : IPopup
	{
		public String Title { get { return "State Properties"; } }

		public readonly Select
			SelProjectStateView = new Select(By.CssSelector("select[name='projectStatusView']"));

		public readonly Button
			BtnOk = new Button(By.CssSelector("input[value='OK']"));
	}
}
