using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CCWebUIAuto.Helpers;
using CCWebUIAuto.PrimitiveElements;
using OpenQA.Selenium;

namespace CCWebUIAuto.Pages.BasePages.ProjectTypeCenter
{
	public class StateTransitionsTab : CCPage
	{
		public readonly string ProjectTypeInternalName;

		public readonly Button
			BtnNew = new Button(By.CssSelector("input[value='New']")),
			BtnDelete = new Button(By.CssSelector("input[value='Delete']")),
			BtnDeselectAll = new Button(By.CssSelector("input[value='Deselect All']")),
			BtnEditPriority = new Button(By.CssSelector("input[value='Edit Priority']")),
			BtnGlobalPostProcessingScript = new Button(By.CssSelector("input[value='Global Post-Processing Script']"));

		public StateTransitionsTab(string projectTypeInternalName)
		{
			ProjectTypeInternalName = projectTypeInternalName;
		}

		public override void NavigateTo()
		{
			Web.Navigate(Store.BaseUrl + "/ProjectCustomization/ProjectTypeCenter/ProjectTypeDetails?EntityTypeName=" + ProjectTypeInternalName + "&Tab=5");
		}

		public void Create(string currentState, string nextState,
			StateTransitionVersionIncrementAction versionIncrementAction = StateTransitionVersionIncrementAction.None,
			string description = "")
		{
			BtnNew.Click();
			var popup = new ProjectStateTransitionPropertiesPopup();
			popup.SwitchTo();
			popup.FillPropertiesPopup(currentState, nextState, versionIncrementAction, description);
			popup.SwitchBackToParent();
		}

		public void Edit(string currentState, string nextState,
			StateTransitionVersionIncrementAction versionIncrementAction = StateTransitionVersionIncrementAction.None,
			string description = "", int rowIndex = -1, bool getLastRow = false)
		{
			var row = new StateTransitionTableRowValues(rowIndex, getLastRow);
			row.LnkCurrentState.Click();
			var popup = new ProjectStateTransitionPropertiesPopup();
			popup.SwitchTo();
			popup.FillPropertiesPopup(currentState, nextState, versionIncrementAction, description);
			popup.SwitchBackToParent();
		}

		public int GetNumTransitions()
		{
			var table = new Container(By.XPath("//*[@id='_webrRSV_DIV_0']"));
			var rows = table.GetDescendants(".//table/tbody/tr"); // returns all transition rows plus the header row
			return rows.Count() - 1;
		}

		public void Delete(int rowIndex = -1, bool getLastRow = false)
		{
			var row = GetRow(getLastRow: true);
			row.ChkBox.Click();
			BtnDelete.Click();
			var alert = Web.Driver.SwitchTo().Alert();
			alert.Accept();
			ClickPortalUI.Wait.Until(d => new Container(By.XPath("//td[contains(@class, 'SuccessArea')]")).Exists);
		}

		public void SetActivityCriteria(IEnumerable<string> activities, int rowIndex = -1, bool getLastRow = false)
		{
			var row = new StateTransitionTableRowValues(rowIndex, getLastRow);
			row.LnkActivityCriteria.Click();
			var popup = new StateTransitionActivityCriteriaPopup();
			popup.SwitchTo();
			var checkboxes = activities
				.Select(activity => new Checkbox(By.XPath("//span[text()='" + activity + "']/../../td[1]/input[@type='checkbox']")))
				.Where(checkbox => !checkbox.Checked);
			foreach (var checkbox in checkboxes) {
				checkbox.Click();
			}
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
		}

		public void ClearActivityCriteria(IEnumerable<string> activities, int rowIndex = -1, bool getLastRow = false)
		{
			var row = new StateTransitionTableRowValues(rowIndex, getLastRow);
			row.LnkActivityCriteria.Click();
			var popup = new StateTransitionActivityCriteriaPopup();
			popup.SwitchTo();
			var checkboxes = activities
				.Select(activity => new Checkbox(By.XPath("//span[text()='" + activity + "']/../../td[1]/input[@type='checkbox']")))
				.Where(checkbox => checkbox.Checked);
			foreach (var checkbox in checkboxes) {
				checkbox.Click();
			}
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
		}

		public bool AreActivitiesSelectedAsCriteria(IEnumerable<string> activities, int rowIndex = -1, bool getLastRow = false)
		{
			var row = new StateTransitionTableRowValues(rowIndex, getLastRow);
			row.LnkActivityCriteria.Click();
			var popup = new StateTransitionActivityCriteriaPopup();
			popup.SwitchTo();
			var returnValue = activities
				.Select(activity => new Checkbox(By.XPath("//span[text()='" + activity + "']/../../td[1]/input[@type='checkbox']")))
				.All(checkbox => checkbox.Checked);
			popup.BtnCancel.Click();
			popup.SwitchBackToParent();
			return returnValue;
		}

		public void AddPropertyCriteria(string property, string value, int rowIndex = -1, bool getLastRow = false)
		{
			var row = new StateTransitionTableRowValues(rowIndex, getLastRow);
			row.LnkPropertiesCriteria.Click();
			var popup = new EvaluateProperties();
			popup.SwitchTo();
			popup.AddTransitionCriterion(property, value);
			popup.SwitchBackToParent();
		}

		public IEnumerable<String> GetPropertyCriteria(int rowIndex = -1, bool getLastRow = false)
		{
			var row = new StateTransitionTableRowValues(rowIndex, getLastRow);
			row.LnkPropertiesCriteria.Click();
			var popup = new EvaluateProperties();
			popup.SwitchTo();
			var returnValue = popup.SelCriteriaBox.GetOptionsText();
			popup.BtnSave.Click();
			popup.SwitchBackToParent();
			return returnValue;
		}

		public void SetScriptCriteria(string script, int rowIndex = -1, bool getLastRow = false)
		{
			var row = new StateTransitionTableRowValues(rowIndex, getLastRow);
			row.LnkScriptCriteria.Click();
			var popup = new WorkflowScriptEditor();
			popup.SwitchTo();
			popup.TxtScript.Value = script;
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
		}

		public string GetScriptCriteria(int rowIndex = -1, bool getLastRow = false)
		{
			var row = new StateTransitionTableRowValues(rowIndex, getLastRow);
			row.LnkScriptCriteria.Click();
			var popup = new WorkflowScriptEditor();
			popup.SwitchTo();
			var returnValue = popup.TxtScript.Value;
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
			return returnValue;
		}

		public void SetPreProcessingScript(string script, int rowIndex = -1, bool getLastRow = false)
		{
			var row = new StateTransitionTableRowValues(rowIndex, getLastRow);
			row.LnkPreProcessingScript.Click();
			var popup = new WorkflowScriptEditor();
			popup.SwitchTo();
			popup.TxtScript.Value = script;
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
		}

		public string GetPreProcessingScript(int rowIndex = -1, bool getLastRow = false)
		{
			var row = new StateTransitionTableRowValues(rowIndex, getLastRow);
			row.LnkPreProcessingScript.Click();
			var popup = new WorkflowScriptEditor();
			popup.SwitchTo();
			var returnValue = popup.TxtScript.Value;
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
			return returnValue;
		}

		public void AddChangeProperty(string property, string value, int rowIndex = -1, bool getLastRow = false)
		{
			var row = new StateTransitionTableRowValues(rowIndex, getLastRow);
			row.LnkChangeProperties.Click();
			var popup = new ChangeProperties();
			popup.SwitchTo();
			popup.AddChangeProperties(property, value);
			popup.SwitchBackToParent();
			Thread.Sleep(1500);
			WaitForPageLoad();
		}

		public IEnumerable<String> GetChangeProperty(int rowIndex = -1, bool getLastRow = false)
		{
			var row = new StateTransitionTableRowValues(rowIndex, getLastRow);
			row.LnkChangeProperties.Click();
			var popup = new ChangeProperties();
			popup.SwitchTo();
			var returnValue = popup.SelCriteriaBox.GetOptionsText();
			popup.BtnSave.Click();
			popup.SwitchBackToParent();
			return returnValue;
		}

		public void SetPostProcessingScript(string script, int rowIndex = -1, bool getLastRow = false)
		{
			var row = new StateTransitionTableRowValues(rowIndex, getLastRow);
			row.LnkPostProcessingScript.Click();
			var popup = new WorkflowScriptEditor();
			popup.SwitchTo();
			popup.TxtScript.Value = script;
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
		}

		public string GetPostProcessingScript(int rowIndex = -1, bool getLastRow = false)
		{
			var row = new StateTransitionTableRowValues(rowIndex, getLastRow);
			row.LnkPostProcessingScript.Click();
			var popup = new WorkflowScriptEditor();
			popup.SwitchTo();
			var returnValue = popup.TxtScript.Value;
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
			return returnValue;
		}

		public void SetGlobalPostProcessingScript(string script)
		{
			BtnGlobalPostProcessingScript.Click();
			var popup = new WorkflowScriptEditor();
			popup.SwitchTo();
			popup.TxtScript.Value = script;
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
		}

		public string GetGlobalPostProcessingScript()
		{
			BtnGlobalPostProcessingScript.Click();
			var popup = new WorkflowScriptEditor();
			popup.SwitchTo();
			var returnValue = popup.TxtScript.Value;
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
			return returnValue;
		}

		public StateTransitionTableRowValues GetRow(int rowIndex = -1, bool getLastRow = false)
		{
			return new StateTransitionTableRowValues(rowIndex, getLastRow);
		}
	}

	public class ProjectStateTransitionPropertiesPopup : IPopup
	{
		public String Title { get { return "Project State Transition"; } }

		public readonly Select
			SelCurrentState = new Select(By.CssSelector("select[name='ProjectStateTransition.currentStatus']")),
			SelNextState = new Select(By.CssSelector("select[name='ProjectStateTransition.nextStatus']"));

		public readonly Radio
			RdoNoVersionIncrement = new Radio(By.CssSelector("input[value='None']")),
			RdoMinorVersionIncrement = new Radio(By.CssSelector("input[value='Minor']")),
			RdoMajorVersionIncrement = new Radio(By.CssSelector("input[value='Major']"));
		public readonly TextBox
			TxtVersionDescription = new TextBox(By.CssSelector("input[name='ProjectStateTransition.versionDescription']"));

		public readonly Button
			BtnOk = new Button(By.CssSelector("input[value='OK']")),
			BtnCancel = new Button(By.CssSelector("input[value='Cancel']")),
			BtnApply = new Button(By.CssSelector("input[value='Apply']"));

		public void FillPropertiesPopup(string currentState, string nextState,
			StateTransitionVersionIncrementAction versionIncrementAction = StateTransitionVersionIncrementAction.None,
			string description = "")
		{
			SelCurrentState.SelectOption(currentState);
			SelNextState.SelectOption(nextState);
			switch (versionIncrementAction) {
			case StateTransitionVersionIncrementAction.None:
				RdoNoVersionIncrement.Click();
				break;
			case StateTransitionVersionIncrementAction.Minor:
				RdoMinorVersionIncrement.Click();
				break;
			case StateTransitionVersionIncrementAction.Major:
				RdoMajorVersionIncrement.Click();
				break;
			default:
				throw new Exception();
			}
			TxtVersionDescription.Value = description;
			BtnOk.Click();
		}
	}

	public class StateTransitionTableRowValues
	{
		public readonly Checkbox
			ChkBox;

		public readonly Link
			LnkCurrentState,
			LnkNextState,
			LnkActivityCriteria,
			LnkPropertiesCriteria,
			LnkScriptCriteria,
			LnkPreProcessingScript,
			LnkChangeProperties,
			LnkNotifications,
			LnkPostProcessingScript;

		public readonly String
			NextState,
			CurrentState;

		public readonly StateTransitionVersionIncrementAction VersionIncrementAction;
		public readonly double Priority;

		/// <summary>
		/// Get a data structure holding page elements for the row, indexing starts at 1
		/// </summary>
		/// <param name="rowIndex">Starts at 1</param>
		/// <param name="getLastRow">Set to true to get the bottom row</param>
		public StateTransitionTableRowValues(int rowIndex = -1, bool getLastRow = false)
		{
			var xpath = rowIndex > 0 && !getLastRow
				? "//*[@id='_webrRSV_DIV_0']/table/tbody/tr[" + rowIndex + "]"
				: "//*[@id='_webrRSV_DIV_0']/table/tbody/tr[last()]";

			ChkBox = new Checkbox(By.XPath(xpath + "/td[1]/input[@type='checkbox']"));
			LnkCurrentState = new Link(By.XPath(xpath + "/td[4]/a"));
			LnkNextState = new Link(By.XPath(xpath + "/td[5]/a"));
			LnkActivityCriteria = new Link(By.XPath(xpath + "/td[6]/a[1]"));
			LnkPropertiesCriteria = new Link(By.XPath(xpath + "/td[6]/a[2]"));
			LnkScriptCriteria = new Link(By.XPath(xpath + "/td[6]/a[3]"));
			LnkPreProcessingScript = new Link(By.XPath(xpath + "/td[8]/a[1]"));
			LnkChangeProperties = new Link(By.XPath(xpath + "/td[8]/a[2]"));
			LnkNotifications = new Link(By.XPath(xpath + "/td[8]/a[3]"));
			LnkPostProcessingScript = new Link(By.XPath(xpath + "/td[8]/a[4]"));

			CurrentState = LnkCurrentState.Text;
			NextState = LnkNextState.Text;

			var incrementActionText = new Container(By.XPath(xpath + "/td[9]/span")).Text;
			if (incrementActionText == "None") VersionIncrementAction = StateTransitionVersionIncrementAction.None;
			if (incrementActionText == "Minor") VersionIncrementAction = StateTransitionVersionIncrementAction.Minor;
			if (incrementActionText == "Major") VersionIncrementAction = StateTransitionVersionIncrementAction.Major;

			var priorityText = new Container(By.XPath(xpath + "/td[10]/span")).Text;
			Priority = Convert.ToDouble(priorityText);
		}

	}

	public class StateTransitionActivityCriteriaPopup : IPopup
	{
		public String Title { get { return "Verify Activity"; } }

		public readonly Button
			BtnOk = new Button(By.CssSelector("input[value='OK']")),
			BtnCancel = new Button(By.CssSelector("input[value='Cancel']"));
	}

	public enum StateTransitionVersionIncrementAction
	{
		None,
		Major,
		Minor
	}
}
