using System;
using System.Diagnostics;
using System.Threading;
using OpenQA.Selenium;
using PortalSeleniumFramework.Helpers;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.Pages.BasePages.ProjectTypeCenter
{
	public class ActivitiesTab : CCPage
	{
		public readonly string ProjectTypeInternalName;

		public readonly Button
			BtnNew = new Button(By.CssSelector("input[value='New']")),
			BtnDelete = new Button(By.CssSelector("input[value='Delete'")),
			BtnGlobalPostProcessing = new Button(By.CssSelector("input[value='Global Post-Processing Script']")),
			BtnCustomzieDetailsPage = new Button(By.CssSelector("input[value='Customize Activity Details Page']"));

		//public readonly Link LnkConditionalDisplay = new Link(By.XPath("//a[@title='Edit Conditional Display Rules']"));

		public readonly Container
			ActivitiesTable = new Container(By.XPath(".//*[@id='_webrRSV_DIV_0']/table"));

		public ActivitiesTab(string projTypeInternalName)
		{
			ProjectTypeInternalName = projTypeInternalName;
		}

		public override void NavigateTo()
		{
			WaitForPageLoad();
			Web.Navigate(Store.BaseUrl + "/ProjectCustomization/ProjectTypeCenter/ProjectTypeDetails?EntityTypeName=" + ProjectTypeInternalName + "&Tab=4");
		}

		/// <summary>
		/// Creates an activity with name, optionally can also provide a custom internal name
		/// </summary>
		public void CreateNewActivity(string name, string internalName = null, bool acceptSanitizedInternalName = false)
		{
			Trace.WriteLine(String.Format("Creating a new activity named: {0} with internal name: {1}", name, internalName));

			BtnNew.Click();
			var page = new NewActivityPage();
			page.TxtDisplayName.Value = name;
			if (internalName != null) {
				page.TxtInternalName.Value = internalName;
			}
			page.BtnOk.Click();
			try {
				// The alert is in case a badly formed internal name
				var alert = Web.PortalDriver.SwitchTo().Alert();
				if (acceptSanitizedInternalName) {
					alert.Accept();
				} else {
					alert.Dismiss();
				}
			} catch (NoAlertPresentException ex) {
				Trace.WriteLine(ex.Message);
			}
			NavigateTo();
		}

		/// <summary>
		/// Verifies that a state exists.
		/// </summary>
		public bool VerifyActivityExists(string name)
		{
			var returnValue = false;
			Trace.WriteLine(String.Format("Verifying if activity '{0}' exists", name));
			if (ActivitiesTable.Text.Contains(name)) {
				Trace.WriteLine(String.Format("Activity '{0}' exists", name));
				returnValue = true;
			} else {
				Trace.WriteLine(String.Format("Activity '{0}' does not exist", name));
			}
			return returnValue;
		}

		public bool VerifyActivityInternalName(string name)
		{
			var returnValue = false;
			Trace.WriteLine(String.Format("Verifying if activity internal name exists '{0}' exists", name));
			if (ActivitiesTable.Text.Contains(name)) {
				Trace.WriteLine(String.Format("Activity internal name: '{0}' exists", name));
				returnValue = true;
			} else {
				Trace.WriteLine(String.Format("Activity internal name: '{0}' does not exist", name));
			}
			return returnValue;
		}


		/// <summary>
		/// Delete an activity by it's name
		/// </summary>
		public void DeleteActivity(string name)
		{
			Trace.WriteLine(String.Format("Deleting row {0} from activity table", name));
			CheckTableRow(name);
			BtnDelete.Click();
			var alert = Web.PortalDriver.SwitchTo().Alert();
			alert.Accept();
		}

		/// <summary>
		/// Checks the row in state table
		/// </summary>
		public void CheckTableRow(string name)
		{
			var tdCheckBox = new Checkbox(By.XPath("//a[text()='" + name + "']/../../td[1]/input[@type='checkbox']"));
			tdCheckBox.Checked = true;
		}

		/// <summary>
		/// Add globabl processing script.
		/// </summary>
		public void AddGlobalPostProcessingScript(string globalScript)
		{
			Trace.WriteLine("Creating a global-script for activities.");
			BtnGlobalPostProcessing.Click();
			var popup = new WorkflowScriptEditor();
			popup.SwitchTo();
			popup.TxtScript.Value = globalScript;
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
		}

		public void AddCustomizeActivityScript(string customActivityScript)
		{
			Trace.WriteLine("Customizing activity details page -- adding script.");
			var parentWindow = GetTitle();
			BtnCustomzieDetailsPage.Click();
			var popup = new WorkflowScriptEditor();
			popup.SwitchTo();
			popup.TxtScript.Value = customActivityScript;
			popup.BtnOk.Click();
			PopUpWindow.SwitchTo(parentWindow);
		}

		/// <summary>
		/// Enter a pre-script for post processing with an activity
		/// </summary>
		public void AddPreScriptPostProcessing(string activityName, string script)
		{
			Trace.WriteLine(String.Format("Creating a pre-script for post processing with '{0}'", activityName));
			var imageLink = new Button(By.XPath("//a[text()='" + activityName + "']/../../td[7]/table/tbody/tr/td/a[1]/img"));
			imageLink.Click();
			var popup = new WorkflowScriptEditor();
			popup.SwitchTo();
			popup.TxtScript.Value = script;
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
		}

		/// <summary>
		/// Enter a post-script for post processing with an activity
		/// </summary>
		public void AddPostScriptPostProcessing(string activityName, string script)
		{
			Trace.WriteLine(String.Format("Creating a post-script for post processing with '{0}'", activityName));
			var imageLink = new Button(By.XPath("//a[text()='" + activityName + "']/../../td[7]/table/tbody/tr/td/a[4]/img"));
			imageLink.Click();
			var popup = new WorkflowScriptEditor();
			popup.SwitchTo();
			popup.TxtScript.Value = script;
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
		}

		/// <summary>
		/// Simply adds a property with '=' criteria and value
		/// </summary>
		public void AddChangeProperty(string activityName, string property, string value)
		{
			Trace.WriteLine(String.Format("Adding property: {0} to activity: {1} with value: '{2}'", property, activityName, value));
			var imageLink = new Button(By.XPath("//a[text()='" + activityName + "']/../../td[7]/table/tbody/tr/td/a[2]/img"));
			imageLink.Click();
			var popup = new ChangeProperties();
			popup.SwitchTo();
			popup.AddChangeProperties(property, value);
			popup.SwitchBackToParent();
			Thread.Sleep(1500);
			WaitForPageLoad();
		}

		public void AddNotification(string activityName, string toProperty, string subjectLine)
		{
			Trace.WriteLine(String.Format("Attempting to add notification with ToProperty: {0} to activity: {1} with subjectLine: '{2}'", toProperty, activityName, subjectLine));
			var imageLink = new Button(By.XPath("//a[text()='" + activityName + "']/../../td[7]/table/tbody/tr/td/a[3]/img[1]"));
			imageLink.Click();
			var popup = new ActivityNotificationPopup();
			popup.SwitchTo();
			popup.AddNotification(toProperty, subjectLine);
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
		}

		/// <summary>
		/// Simply verifies that the property appears in selection criteria field
		/// </summary>
		public bool VerifyProperty(string activityName, string property)
		{
			var returnValue = false;
			var imageLink = new Button(By.XPath("//a[text()='" + activityName + "']/../../td[7]/table/tbody/tr/td/a[2]/img"));
			imageLink.Click();
			var popup = new ChangeProperties();
			popup.SwitchTo();
			if (popup.SelCriteriaBox.Contains(property)) {
				Trace.WriteLine(String.Format("Verified change property: {0} within activity: {1}", property,
					activityName));
				returnValue = true;
			} else {
				Trace.WriteLine(String.Format("Change property: {0} within activity: {1} could not be found.", property,
					activityName));
			}
			popup.SwitchBackToParent();
			return returnValue;
		}

		public void EditActivityViewEditorName(string activityName, string entityViewName)
		{
			Trace.WriteLine(String.Format("Attempting to edit the entity view name of activity: {0} with display name: '{1}'", activityName, entityViewName));
			var imageLink = new Button(By.XPath("//a[text()='" + activityName + "']/../../td[9]/a[1]/img"));
			imageLink.Click();
			var popup = new EntityViewEditorPopup();
			popup.SwitchTo();
			popup.TxtDisplayName.Value = entityViewName;
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
		}

		/// <summary>
		/// Validates the contents of the pre-script editor.  Assumes you are on the Activity tab.
		/// </summary>
		public bool VerifyPreScriptExists(string activityName, string validationScript)
		{
			var returnValue = false;
			Trace.WriteLine(String.Format("Validating workflow script window contains script:  '{0}'", validationScript));
			var imageLink = new Button(By.XPath("//a[text()='" + activityName + "']/../../td[7]/table/tbody/tr/td/a[1]/img"));
			imageLink.Click();
			var popup = new WorkflowScriptEditor();
			popup.SwitchTo();
			if (popup.TxtScript.Value == validationScript) {
				returnValue = true;
			}
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
			return returnValue;
		}

		/// <summary>
		/// Validates the contents of the post-script editor.  Assumes you are on the Activity tab.
		/// </summary>
		public bool VerifyPostScriptExists(string activityName, string validationScript)
		{
			var returnValue = false;
			Trace.WriteLine(String.Format("Validating activity post processing script: window contains script:  '{0}'", validationScript));
			var imageLink = new Button(By.XPath("//a[text()='" + activityName + "']/../../td[7]/table/tbody/tr/td/a[4]/img"));
			imageLink.Click();
			var popup = new WorkflowScriptEditor();
			popup.SwitchTo();
			if (popup.TxtScript.Value == validationScript) {
				Trace.WriteLine(String.Format("Workflow script window contains script:  '{0}'", validationScript));
				returnValue = true;
			}
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
			return returnValue;
		}

		public bool VerifyGlobalPostScriptExists(string validationScript)
		{
			var returnValue = false;
			Trace.WriteLine(String.Format("Validating global post processing script:  script window contains script:  '{0}'", validationScript));
			BtnGlobalPostProcessing.Click();
			var popup = new WorkflowScriptEditor();
			popup.SwitchTo();
			if (popup.TxtScript.Value == validationScript) {
				returnValue = true;
			}
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
			return returnValue;
		}

		public bool VerifyCustomizeActivityDetailsScriptExists(string validationScript)
		{
			var returnValue = false;
			Trace.WriteLine(String.Format("Validating customize activity details script:  script window contains script:  '{0}'", validationScript));
			BtnCustomzieDetailsPage.Click();
			var popup = new WorkflowScriptEditor();
			popup.SwitchTo();
			if (popup.TxtScript.Value == validationScript) {
				returnValue = true;
			}
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
			return returnValue;
		}

		public bool VerifyActivityEntityViewName(string activityName, string displayName)
		{
			Trace.WriteLine(String.Format("For activity:  '{0}', verifying the display name:  '{1}' in entity view editor", activityName, displayName));
			var parentWindow = GetTitle();
			var imageLink = new Button(By.XPath("//a[text()='" + activityName + "']/../../td[9]/a[1]/img"));
			imageLink.Click();
			var popup = new EntityViewEditorPopup();
			popup.SwitchTo();
			var returnValue = popup.TxtDisplayName.Value == displayName;
			popup.BtnOk.Click();
			PopUpWindow.SwitchTo(parentWindow);
			return returnValue;
		}
	}

	public class ActivitiesTabManageGroupsSubTab : CCPage
	{
		public readonly string ProjectTypeInternalName;

		public readonly Button
			BtnNew = new Button(By.CssSelector("input[value='New']")),
			BtnDelete = new Button(By.CssSelector("input[value='Delete'"));

		public readonly Container
			ActivitiesTable = new Container(By.XPath(".//*[@id='_webrRSV_DIV_0']/table")),
			WarningSpan = new Container(By.XPath(".//*[@id='webrRSV__BBDIV_0']//span"));

		public ActivitiesTabManageGroupsSubTab(string projTypeInternalName)
		{
			ProjectTypeInternalName = projTypeInternalName;
		}

		public override void NavigateTo()
		{
			WaitForPageLoad();
			Web.Navigate(Store.BaseUrl + "/ProjectCustomization/ProjectTypeCenter/ProjectTypeDetails?EntityTypeName=" + ProjectTypeInternalName + "&Tab=4&SubTab=2");
		}

		public bool VerifyNoActivityWarning()
		{
			if (WarningSpan.Text == "You must first create an Activity by using the Manage Activities tab.") {
				return true;
			}
			return false;
		}

		/// <summary>
		/// Creates an empty activity group
		/// </summary>
		public void CreateActivityGroup(string name)
		{
			BtnNew.Click();
			var popup = new ActivityGroupEditor();
			popup.SwitchTo();
			popup.TxtGroupName.Value = name;
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
		}

		public void CreateActivityGroup(string name, string[] activities)
		{
			BtnNew.Click();
			var popup = new ActivityGroupEditor();
			popup.SwitchTo();

			popup.TxtGroupName.Value = name;

			foreach (var activity in activities) {
				var targetCheckBox = new Checkbox(By.XPath("//span[contains(.,'" + activity + "')]/../../td[1]/input[@type='checkbox']"));
				popup.BtnClear.Click();
				popup.SelFilterAttribute.SelectOption("Activity Name");
				popup.TxtFilterValue.Value = activity;
				popup.BtnGo.Click();
				Wait.Until(d => targetCheckBox.Exists);
				targetCheckBox.Checked = true;
			}
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
		}

		public bool VerifyActivityGroupExists(string name)
		{
			return new Link(By.LinkText(name)).Exists;
		}

		public void ModifyActivityGroupName(string origName, string newName)
		{
			var targetLink = new Link(By.LinkText(origName));
			targetLink.Click();
			var popup = new ActivityGroupEditor();
			popup.SwitchTo();
			popup.TxtGroupName.Value = newName;
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
		}

		public void CheckActivityRowCheckBox(string activityName)
		{
			var targetCheckBox = new Checkbox(By.XPath("//a[text() = '" + activityName + "']/../../td[1]/input[@type='checkbox']"));
			targetCheckBox.Checked = true;
		}
	}

	public class NewActivityPage : CCPage
	{
		public readonly Button
			BtnOk = new Button(By.CssSelector("input[value='OK']"));

		public readonly TextBox
			TxtInternalName = new TextBox(By.Id("EntityTypeInternalNameSuffix")),
			TxtHiddenInternalName = new TextBox(By.Id("EntityTypeInternalName_text")),
			TxtDisplayName = new TextBox(By.Id("EntityTypeDisplayName"));

		public static string WindowTitle = "Activity";

		public override void NavigateTo()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Used for validating changes to internal / display name rules
		/// </summary>
		public bool VerifyDisplayName(string name)
		{
			var returnValue = false;
			if (TxtDisplayName.Value == name) {
				Trace.WriteLine(String.Format("Displayname : '{0}' verified", name));
				returnValue = true;
			} else {
				Trace.WriteLine(String.Format("Displayname: '{0}' does not match actual value:  {1}", name, TxtDisplayName.Value));
			}
			return returnValue;
		}

		/// <summary>
		/// Validate the customize internal name box for changes to display / internal name rules
		/// </summary>
		public bool VerifyCustomizeInternalName(string name)
		{
			var returnValue = false;

			if (TxtInternalName.Value == name) {
				Trace.WriteLine(String.Format("Activity customized internal name: '{0}' matches actual value:  '{1}'", name, TxtInternalName.Value));
				returnValue = true;
			} else {
				Trace.WriteLine(String.Format("Activity customized internal name: '{0}' does not match actual value: {1}", name, TxtInternalName.Value));
			}
			return returnValue;
		}

		/// <summary>
		/// Used for validating internal name for changes to internal / display name rules
		/// </summary>
		public bool VerifyInternalName(string name)
		{
			var returnValue = false;
			if (TxtHiddenInternalName.Value == name) {
				Trace.WriteLine(String.Format("Activity internal name: '{0}' matches '{1}'", name, TxtHiddenInternalName.Value));
				returnValue = true;
			} else {
				Trace.WriteLine(String.Format("Activity internal name: '{0}' does not match actual value: {1}", name, TxtHiddenInternalName.Value));
			}
			return returnValue;
		}
	}

	public class ActivityGroupEditor : IPopup
	{
		public readonly Button
			BtnOk = new Button(By.CssSelector("input[value='OK']")),
			BtnGo = new Button(By.CssSelector("input[value='Go']")),
			BtnClear = new Button(By.CssSelector("input[value='Clear']"));

		public readonly TextBox
			TxtGroupName = new TextBox(By.CssSelector("input[name='ActivityTypeGroup.name']")),
			TxtGroupDescription = new TextBox(By.CssSelector("input[name='ActivityTypeGroup.description']")),
			TxtCssClassName = new TextBox(By.CssSelector("input[name='ActivityTypeGroup.groupClassName']")),
			TxtFilterValue = new TextBox(By.Id("_webrRSV_FilterValue_0_0"));

		public readonly Select
			SelFilterAttribute = new Select(By.Id("_webrRSV_FilterField_0_0"));

		public readonly Container
			ActivitiesTable = new Container(By.Id("_webrRSV_DIV_0"));

		public string Title { get { return "Activity Group Editor"; } }

	}
}
