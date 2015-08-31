using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using CCWebUIAuto.Helpers;
using CCWebUIAuto.PrimitiveElements;
using OpenQA.Selenium;

namespace CCWebUIAuto.Pages.BasePages.ProjectTypeCenter
{
	public class SmartFormTabManageSteps : CCPage
	{
		public readonly String ProjectTypeInternalName;
		public static Button
			BtnNew = new Button(By.CssSelector("input[value='New']")),
			BtnDelete = new Button(By.CssSelector("input[value='Delete']")),
			BtnEditDisplayOrder = new Button(By.CssSelector("input[value='Edit Display Order']"));

		public SmartFormTabManageSteps(String newProjectTypeInternalName)
		{
			ProjectTypeInternalName = newProjectTypeInternalName;
		}

		public override void NavigateTo()
		{
			WaitForPageLoad();
			Web.Navigate(Store.BaseUrl + "/ProjectCustomization/ProjectTypeCenter/ProjectTypeDetails?EntityTypeName=" + ProjectTypeInternalName + "&Tab=7&SubTab=1");
		}

		public void CreateStep(String name, String viewName, String defaultNextStep = null,
			Double? displayOrder = null, Boolean? showInJumpTo = null, Boolean? showInPrinting = null)
		{
			var parentWindow = CurrentWindowTitle;
			BtnNew.AsyncClick();
			var newStepPopup = new NewStepPopup();
			PopUpWindow.SwitchTo(newStepPopup.Title);

			newStepPopup.TxtName.Value = name;
			if (viewName != null) {
				newStepPopup.BtnSelectView.Click();
				PopUpWindow.SwitchTo("Select View");
				ViewChooserPopup.SelectView(viewName);
				ViewChooserPopup.BtnOk.Click();
				PopUpWindow.SwitchTo("SmartForm Step");
			}
			if (defaultNextStep != null) newStepPopup.SelDefaultNextStep.SelectOption(defaultNextStep);
			if (displayOrder != null) newStepPopup.TxtDisplayOrder.Value = Convert.ToString(displayOrder, CultureInfo.InvariantCulture);
			if (showInJumpTo != null) newStepPopup.ChkShowInJumpTo.Checked = showInJumpTo.Value;
			if (showInPrinting != null) newStepPopup.ChkShowInPrinting.Checked = showInPrinting.Value;
			newStepPopup.BtnOk.Click();
			PopUpWindow.SwitchTo(parentWindow);
			Wait.Until(d => new Link(By.LinkText(name)).Exists);
		}

		public void EditStep(String name, String newName = null, String viewName = null, String defaultNextStep = null,
			Double? displayOrder = null, Boolean? showInJumpTo = null, Boolean? showInPrinting = null)
		{
			var parentWindow = CurrentWindowTitle;
			var lnkStep = new Link(By.LinkText(name));
			lnkStep.Click();
			var stepPopup = new NewStepPopup();
			PopUpWindow.SwitchTo("SmartForm Step");
			if (newName != null) stepPopup.TxtName.Value = newName;
			if (viewName != null) {
				stepPopup.BtnSelectView.Click();
				PopUpWindow.SwitchTo("Select View");
				ViewChooserPopup.SelectView(viewName);
				ViewChooserPopup.BtnOk.Click();
				PopUpWindow.SwitchTo("SmartForm Step");
			}
			if (defaultNextStep != null) stepPopup.SelDefaultNextStep.SelectOption(defaultNextStep);
			if (displayOrder != null) stepPopup.TxtDisplayOrder.Value = Convert.ToString(displayOrder, CultureInfo.InvariantCulture);
			if (showInJumpTo != null) stepPopup.ChkShowInJumpTo.Checked = showInJumpTo.Value;
			if (showInPrinting != null) stepPopup.ChkShowInPrinting.Checked = showInPrinting.Value;
			stepPopup.BtnOk.Click();
			PopUpWindow.SwitchTo(parentWindow);
			Wait.Until(d => new Link(By.LinkText(newName)).Exists);
		}

		public void DeleteStep(String name)
		{
			var chkStep = new Checkbox(
				By.XPath("(//*[@id='_webrRSV_DIV_0']/table/tbody/tr/td[3]/a[text()='" + name +
						 "'])[1]/../../td[1]/input[@type='checkbox']"));
			chkStep.Checked = true;
			BtnDelete.Click();
			Web.Driver.SwitchTo().Alert().Accept();
			Wait.Until(d => !new Link(By.LinkText(name)).Exists);
		}

		public void AddBranch(String stepName, String targetStepName)
		{
			var btnAddBranch = new Button(
				By.XPath("(//*[@id='_webrRSV_DIV_0']/table/tbody/tr/td[3]/a[text()='" + stepName +
						 "'])[1]/../../td[6]/input[@value='Add Branch']"));
			var parentWindow = CurrentWindowTitle;
			btnAddBranch.Click();
			PopUpWindow.SwitchTo("Edit SmartForm Branch");
			var selNextStep = new Select(By.Name("WizardBranch.nextPage"));
			selNextStep.SelectOption(targetStepName);
			var btnOk = new Button(By.CssSelector("input[value='OK']"));
			btnOk.Click();
			PopUpWindow.SwitchTo(parentWindow);
			var spanExpected = new Container(
				By.XPath("(//*[@id='_webrRSV_DIV_0']/table/tbody/tr/td[3]/a[text()='" + stepName + "" +
						 "'])[1]/../../td[4]/table/tbody/tr/td/table/tbody/tr/td[2]/span[text()='" + targetStepName + "']"));
			Wait.Until(d => spanExpected.Exists);
		}

		public void AddBranchCriteria(String stepName, string targetStepName, string propertyName, string criteriaValue)
		{
			var lnkBranchCriteria = new Link(
				By.XPath("(//*[@id='_webrRSV_DIV_0']/table/tbody/tr/td[3]/a[text()='" + stepName + "" +
						 "'])[1]/../../td[4]/table/tbody/tr/td/table/tbody/tr/td[2]/span[text()='" + targetStepName +
						 "']/../../td[1]/a"));
			var parentWindow = CurrentWindowTitle;
			var popup = new BranchCriteriaPopup();
			lnkBranchCriteria.Click();
			PopUpWindow.SwitchTo(popup.Title);
			popup.AddBranchCriterion(propertyName, criteriaValue);
			PopUpWindow.SwitchTo(parentWindow);
			Wait.Until(d => !PopUpWindow.IsOpen(popup.Title));
			Thread.Sleep(1500);
			WaitForPageLoad();
		}

		public IEnumerable<String> GetBranchCriteria(String stepName, string targetStepName)
		{
			var lnkBranchCriteria = new Link(
				By.XPath("(//*[@id='_webrRSV_DIV_0']/table/tbody/tr/td[3]/a[text()='" + stepName + "" +
						 "'])[1]/../../td[4]/table/tbody/tr/td/table/tbody/tr/td[2]/span[text()='" + targetStepName +
						 "']/../../td[1]/a"));
			var parentWindow = CurrentWindowTitle;
			var popup = new BranchCriteriaPopup();
			lnkBranchCriteria.Click();
			PopUpWindow.SwitchTo(popup.Title);
			Thread.Sleep(1500);
			var result = popup.SelCriteriaBox.GetOptionsText();
			PopUpWindow.SwitchTo(parentWindow);
			return result;
		}

		public void CopyBranching(String sourceStepName, string targetStepName, String copyType = "all", Boolean eraseAll = false)
		{
			var btnAddBranch = new Button(
				By.XPath("(//*[@id='_webrRSV_DIV_0']/table/tbody/tr/td[3]/a[text()='" + sourceStepName +
						 "'])[1]/../../td[6]/input[@value='Copy Branches']"));
			btnAddBranch.Click();
			var popup = new CopyBranchesPopup();
			popup.SwitchTo();
			var chkTarget = new Checkbox(By.XPath("//span[text()='" + targetStepName + "']/../../td[1]/input[@type='checkbox']"));
			chkTarget.Click();
			var rdoCopyType = copyType == "smart" ? popup.RdoSmartCopy : popup.RdoAllBranches;
			rdoCopyType.Click();
			popup.ChkEraseExisting.Checked = eraseAll;
			popup.BtnOk.Click();
			popup.SwitchBackToParent(WaitForPopupToClose.Yes);
			Thread.Sleep(1500);
			WaitForPageLoad(5000);
		}
	}

	public class NewStepPopup : IPopup
	{
		public String Title { get { return "SmartForm Step"; } }

		public readonly TextBox
			TxtName = new TextBox(By.Name("WizardPage.name")),
			TxtView = new TextBox(By.Name("viewName")),
			TxtDisplayOrder = new TextBox(By.Name("WizardPage.sequence"));

		public readonly Button
			BtnSelectView = new Button(By.CssSelector("input[value='Select View']")),
			BtnOk = new Button(By.CssSelector("input[value='OK']")),
			BtnCancel = new Button(By.CssSelector("input[value='Cancel']"));

		public readonly Select
			SelDefaultNextStep = new Select(By.Name("WizardPage.defaultNextPage")),
			SelSection = new Select(By.Name("WizardPage.section"));

		public readonly Checkbox
			ChkShowInJumpTo = new Checkbox(By.Name("WizardPage.show")),
			ChkShowInPrinting = new Checkbox(By.Name("WizardPage.showInPrint"));
	}

	public class BranchCriteriaPopup : SavedSearchEditorBase
	{
		public Button BtnSave { get { return InternalSaveButton; } }
		public Button BtnAddCriteria { get { return InternalBtnAddCriteria; } }
		public Button BtnAddCriterion { get { return InternalBtnAddCriterion; } }
		public TextBox TxtValueField { get { return InternalValueField; } }
		public Container SpanTopPropertyTree { get { return InternalTopPropertyTree; } }
		public Select SelCriteriaBox { get { return InternalSelCriteriaBox; } }

		public override String Title { get { return "Branch Criteria"; } }

		public void AddBranchCriterion(string propertyName, string criteriaValue)
		{
			AddCriterion(propertyName, criteriaValue);
		}
	}

	public class CopyBranchesPopup : IPopup
	{
		public String Title { get { return "Copy SmartForm Branches"; } }

		public readonly Radio
			RdoAllBranches = new Radio(By.CssSelector("input[value='all']")),
			RdoSmartCopy = new Radio(By.CssSelector("input[value='smart']"));

		public readonly Checkbox
			ChkEraseExisting = new Checkbox(By.Name("EraseAll"));

		public readonly Button
			BtnOk = new Button(By.CssSelector("input[value='OK']")),
			BtnCancel = new Button(By.CssSelector("input[value='Cancel']"));
	}

	public class SmartFormTabManageSections : CCPage
	{
		public readonly String ProjectTypeInternalName;
		public static Button
			BtnNew = new Button(By.CssSelector("input[value='New']")),
			BtnDelete = new Button(By.CssSelector("input[value='Delete']")),
			BtnEditDisplayOrder = new Button(By.CssSelector("input[value='Edit Display Order']")),
			BtnEditProgressLabels = new Button(By.CssSelector("input[value='Edit Progress Labels']"));

		public SmartFormTabManageSections(String newProjectTypeInternalName)
		{
			ProjectTypeInternalName = newProjectTypeInternalName;
		}

		public override void NavigateTo()
		{
			Web.Navigate(Store.BaseUrl + "/ProjectCustomization/ProjectTypeCenter/ProjectTypeDetails?EntityTypeName=" + ProjectTypeInternalName + "&Tab=7&SubTab=2");
		}

		public void CreateSection(String sectionName, String description = null, Boolean showInJumpTo = false,
			IEnumerable<String> stepNames = null)
		{
			BtnNew.Click();
			var popup = new SmartFormSectionPropertiesPopup();
			popup.SwitchTo();
			popup.TxtName.Value = sectionName;
			if (!String.IsNullOrWhiteSpace(description))
				popup.TxtDescription.Value = description;
			popup.ChkShowInJumpTo.Checked = showInJumpTo;
			if (stepNames != null) {
				foreach (var step in stepNames) {
					var chkBox = new Checkbox(By.XPath("//span[text()='" + step + "']/../../td[1]/input[@type='checkbox']"));
					chkBox.Checked = true;
				}
			}
			popup.BtnOk.Click();
			popup.SwitchBackToParent(WaitForPopupToClose.Yes);
			Thread.Sleep(1500);
			WaitForPageLoad(5000);
		}

		public void EditSection(String sectionName, String newSectionName, String description = null,
			Boolean? showInJumpTo = null, IEnumerable<String> stepNames = null)
		{
			var sectionLink = new Link(By.LinkText(sectionName));
			sectionLink.Click();
			var popup = new SmartFormSectionPropertiesPopup();
			popup.SwitchTo();
			if (newSectionName != null) popup.TxtName.Value = newSectionName;
			if (!String.IsNullOrWhiteSpace(description)) popup.TxtDescription.Value = description;
			if (showInJumpTo != null) popup.ChkShowInJumpTo.Checked = showInJumpTo.Value;
			if (stepNames != null) {
				// clear existing list of steps
				var form = new Container(By.Id("_webrRSV_DIV_0"));
				var checkboxes = form.GetDescendants(".//table/tbody/tr/td[1]/input[@type='checkbox']");
				foreach (var checkbox in checkboxes) {
					checkbox.SetCheckBox(false);
				}

				// setup new list of steps
				foreach (var chkBox in stepNames.Select(step => new Checkbox(By.XPath("//span[text()='" + step + "']/../../td[1]/input[@type='checkbox']")))) {
					chkBox.Checked = true;
				}
			}
			popup.BtnOk.Click();
			popup.SwitchBackToParent(WaitForPopupToClose.Yes);
			Thread.Sleep(1500);
			WaitForPageLoad(5000);
		}

		public void DeleteSection(String sectionName)
		{
			var checkbox = new Checkbox(By.XPath("//a[text()='" + sectionName + "']/../../td[1]/input[@type='checkbox']"));
			checkbox.Checked = true;
			BtnDelete.Click();
			var alert = Web.Driver.SwitchTo().Alert();
			alert.Accept();
			Wait.Until(d => !new Link(By.LinkText(sectionName)).Exists);
		}
	}

	public class SmartFormSectionPropertiesPopup : IPopup
	{
		public String Title { get { return "SmartForm Section Properties"; } }

		public readonly TextBox
			TxtName = new TextBox(By.Name("WizardSection.name")),
			TxtDescription = new TextBox(By.Name("WizardSection.description"));

		public readonly Checkbox
			ChkShowInJumpTo = new Checkbox(By.Name("WizardSection.showInJumpTo"));

		public readonly Button
			BtnOk = new Button(By.CssSelector("input[value='OK']")),
			BtnCancel = new Button(By.CssSelector("input[value='Cancel']"));
	}

	public class SmartFormTabManageSettings : CCPage
	{
		public readonly String ProjectTypeInternalName;

		public static Button
			BtnNew = new Button(By.CssSelector("input[value='New'"));

		public SmartFormTabManageSettings(String newProjectTypeInternalName)
		{
			ProjectTypeInternalName = newProjectTypeInternalName;
		}

		public override void NavigateTo()
		{
			Web.Navigate(Store.BaseUrl + "/ProjectCustomization/ProjectTypeCenter/ProjectTypeDetails?EntityTypeName=" + ProjectTypeInternalName + "&Tab=7&SubTab=3");
		}
	}
}
