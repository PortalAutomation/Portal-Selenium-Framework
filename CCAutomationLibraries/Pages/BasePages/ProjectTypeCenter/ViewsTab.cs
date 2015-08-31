using System;
using System.Collections.Generic;
using System.Diagnostics;
using CCWebUIAuto.Helpers;
using CCWebUIAuto.PrimitiveElements;
using OpenQA.Selenium;

namespace CCWebUIAuto.Pages.BasePages.ProjectTypeCenter
{
	public class ViewsTab : CCPage
	{
		public readonly string ProjectTypeInternalName;

		public readonly Button
			BtnEditStandardViewProperties = new Button(By.CssSelector("input[value='Edit Standard View Properties']")),
			BtnNew = new Button(By.XPath(".//*[@id='webrRSV__BBDIV_0']/input[1]")),
			BtnDelete = new Button(By.CssSelector("input[value='Delete']"));

		//public readonly Link LnkConditionalDisplay = new Link(By.XPath("//a[@title='Edit Conditional Display Rules']"));

		public readonly Container
			ViewsTable = new Container(By.Id("_webrRSV_DIV_0"));

		public ViewsTab(string projTypeInternalName)
		{
			ProjectTypeInternalName = projTypeInternalName;
		}

		public override void NavigateTo()
		{
			WaitForPageLoad();
			Web.Navigate(Store.BaseUrl + "/ProjectCustomization/ProjectTypeCenter/ProjectTypeDetails?EntityTypeName=" + ProjectTypeInternalName + "&Tab=2");
		}

		public void SetStandardViewProperties(string creationViewName = "", string summaryViewName = "", string printHeaderName = "")
		{
			Trace.WriteLine("Editing Standard View Properties.");
			BtnEditStandardViewProperties.Click();
			var popup = new EditViewProperties();
			popup.SwitchTo();
			popup.CreationViewDropDown.SelectOption(creationViewName);
			popup.SummaryViewDropDown.SelectOption(summaryViewName);
			popup.PrintHeaderViewDropDown.SelectOption(printHeaderName);
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
		}

		public void CreateNewView(string viewName, IEnumerable<string> attributes = null)
		{
			Trace.WriteLine(String.Format("Creating view named: {0}.", viewName));
			BtnNew.Click();
			var popup = new EntityViewEditorPopup();
			popup.SwitchTo();
			popup.TxtDisplayName.Value = viewName;
			// currently this functionality doesn't work on Firefox
			/*if (attributes != null)
			{
				foreach (var attr in attributes)
				{
					new CCElement(By.XPath("//span[@class='textControl' and text() = '" + attr + "']")).Click();
					new CCElement(By.Id("_btnAddAttribute")).Click();
				}
			}*/
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
		}

		public void ModifyViewName(string viewToModify, string newViewName)
		{
			// click on hyperlink (if null), bail on test
			var reviewNoteLink = new Link(By.LinkText(viewToModify));
			if (reviewNoteLink == null) {
				throw new Exception(String.Format("Cannot select reviewerNote {0} because it is not displayed or exists.",
						viewToModify));
			}
			reviewNoteLink.Click();
			var popup = new EntityViewEditorPopup();
			popup.SwitchTo();
			Trace.WriteLine(String.Format("Modifying View name to: {0}", newViewName));
			popup.TxtDisplayName.Value = newViewName;
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
		}

		/// <summary>
		/// Verifies that a view exists.
		/// </summary>
		public bool VerifyViewExists(string name)
		{
			var link = new Link(By.LinkText(name));
			var returnValue = link.Exists;
			Trace.WriteLine(String.Format("View '{0}' exists? {1}", name, returnValue));
			return returnValue;
		}

		/// <summary>
		/// Enter a validation script for a project type view.
		/// </summary>
		public void SetValidationScript(string viewName, string validationScript)
		{
			Trace.WriteLine(String.Format("Creating a validation script for '{0}'", viewName));
			var scriptButton = new Link(By.XPath("//a[text()='" + viewName + "']/../..//td[6]/a/img"));
			scriptButton.Click();
			var popup = new WorkflowScriptEditor();
			popup.SwitchTo();
			popup.TxtScript.Value = validationScript;
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
		}

		/// <summary>
		/// Delete a view by it's name
		/// </summary>
		public void DeleteView(string viewName)
		{
			Trace.WriteLine(String.Format("Deleting row {0} from view table", viewName));
			CheckViewTableRow(viewName);
			BtnDelete.Click();
			var alert = Web.Driver.SwitchTo().Alert();
			alert.Accept();
		}

		/// <summary>
		/// Checks the row in view table
		/// </summary>
		public void CheckViewTableRow(string viewName)
		{
			var tdCheckBox = new Checkbox(By.XPath("//a[text()='" + viewName + "']/../../td[1]/input[@type='checkbox']"));
			tdCheckBox.Checked = true;
		}


		/// <summary>
		/// Validates the contents of the validation script.  Assumes you are on the Views tab.
		/// </summary>
		public bool VerifyValidationScriptExists(string viewName, string validationScript)
		{
			Trace.WriteLine(String.Format("Validating validation script window contains script:  '{0}'", validationScript));
			var scriptButton = new Button(By.XPath("//a[text()='" + viewName + "']/../..//td[6]/a/img"));
			scriptButton.Click();
			var popup = new WorkflowScriptEditor();
			popup.SwitchTo();
			var value = popup.TxtScript.Value;
			var returnValue = value == validationScript;
			popup.BtnCancel.Click();
			popup.SwitchBackToParent();
			return returnValue;
		}
	}

	public class EditViewProperties : IPopup
	{
		public String Title { get { return "Edit View Properties"; } }

		public readonly Select
			CreationViewDropDown = new Select(By.CssSelector("select[name='creationView']")),
			SummaryViewDropDown = new Select(By.CssSelector("select[name='summaryView']")),
			PrintHeaderViewDropDown = new Select(By.CssSelector("select[name='printHeaderView']"));

		public readonly Button
			BtnOk = new Button(By.CssSelector("input[value='OK']")),
			BtnCancel = new Button(By.CssSelector("input[value='Cancel']")),
			BtnApply = new Button(By.CssSelector("input[value='Apply']"));
	}
}
