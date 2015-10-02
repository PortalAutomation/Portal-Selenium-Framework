using System;
using System.Diagnostics;
using OpenQA.Selenium;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.Pages.BasePages
{
	// There are several different popups that are based on the custom search editor, so the
	// popup classes for each should inherit from SavedSearchEditorBase, then expose or add
	// the functionality unique to that popup

	// TODO: This is actually the same layout as the custom search editor, so build on this for custom search
	public abstract class SavedSearchEditorBase : IPopup
	{
		public abstract String Title { get; }

		// these elements are defined as protected, because not every element is visible in each popup
		// subtypes should use properties to expose only the elements actually exposed on that popup

		protected readonly Button
			InternalSaveButton = new Button(By.CssSelector("input[value='Save']")),
			InternalBtnAddCriteria = new Button(By.Id("btnAddCriteria")),
			InternalBtnAddCriterion = new Button(By.Id("btnAddCriterion")),
			InternalBtnAttributeCriterion = new Button(By.Id("btnAttribute"));

		protected readonly TextBox
			InternalValueField = new TextBox(By.Id("txtValue"));

		protected readonly Container
			// This top tree element seems to be consistent with id value.
			InternalTopPropertyTree = new Container(By.Id("ID100S"));

		protected readonly Select
			InternalSelCriteriaBox = new Select(By.Id("selCriteria"));

		// TODO: This will need an operator parameter eventually
		// The appropriate name to expose this method with will vary depending on the type of popup
		protected void AddCriterion(string propertyName, string criteriaValue)
		{
			Trace.WriteLine(String.Format("Adding property: {0} with criteria value: {1}", propertyName, criteriaValue));
			InternalTopPropertyTree.Click();
			//var targetLink = new Link(By.XPath(".//span[contains(.,'" + propertyName + "')]"));
			//var spanTarget = targetLink.GetDescendants<Container>(".//span");
			var spanTarget = new Container(By.XPath(".//span[contains(.,'" + propertyName + "')]/span"));
			spanTarget.Click();
			InternalBtnAddCriteria.Click();
			InternalValueField.Value = criteriaValue;
			InternalBtnAddCriterion.Click();
			InternalSaveButton.Click();
		}
	}

	public class ChangeProperties : SavedSearchEditorBase
	{
		public Button BtnSave { get { return InternalSaveButton; } }
		public Button BtnAddCriteria { get { return InternalBtnAddCriteria; } }
		public Button BtnAddCriterion { get { return InternalBtnAddCriterion; } }
		public TextBox TxtValueField { get { return InternalValueField; } }
		public Container TopPropertyTree { get { return InternalTopPropertyTree; } }
		public Select SelCriteriaBox { get { return InternalSelCriteriaBox; } }

		public override string Title { get { return "Change Properties"; } }

		public void AddChangeProperties(string propertyName, string criteriaValue)
		{
			AddCriterion(propertyName, criteriaValue);
		}
	}

	public class EvaluateProperties : SavedSearchEditorBase
	{
		public Button BtnSave { get { return InternalSaveButton; } }
		public Button BtnAddCriteria { get { return InternalBtnAddCriteria; } }
		public Button BtnAddCriterion { get { return InternalBtnAddCriterion; } }
		public TextBox TxtValueField { get { return InternalValueField; } }
		public Container TopPropertyTree { get { return InternalTopPropertyTree; } }
		public Select SelCriteriaBox { get { return InternalSelCriteriaBox; } }

		public override string Title { get { return "Evaluate Properties"; } }

		public void AddTransitionCriterion(string propertyName, string criteriaValue)
		{
			AddCriterion(propertyName, criteriaValue);
		}
	}

	public class SavedSearchEditorPopup : SavedSearchEditorBase
	{
		public override string Title { get { return "Custom Search"; } }

		public Button BtnSave { get { return InternalSaveButton; } }
		public Button BtnAddCriteria { get { return InternalBtnAddCriteria; } }
		public Button BtnAddCriterion { get { return InternalBtnAddCriterion; } }
		public Button BtnAttributeCriterion { get { return InternalBtnAttributeCriterion; } }
		public TextBox TxtValueField { get { return InternalValueField; } }
		public Container TopPropertyTree { get { return InternalTopPropertyTree; } }
		public Select SelCriteriaBox { get { return InternalSelCriteriaBox; } }


	}
}
