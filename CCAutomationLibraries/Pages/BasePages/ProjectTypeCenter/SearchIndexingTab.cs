using System;
using CCWebUIAuto.Helpers;
using CCWebUIAuto.PrimitiveElements;
using OpenQA.Selenium;

namespace CCWebUIAuto.Pages.BasePages.ProjectTypeCenter
{
	public class SearchIndexingTab : CCPage
	{
		public readonly string ProjectTypeInternalName;

		public readonly Checkbox
			ChkEnableSearchIndexing = new Checkbox(By.Id("indexingEnabled"));

		public readonly Select
			SelIndexedAttributes = new Select(By.Id("attributeList")),
			SelIncludeStringFromMethod = new Select(By.Id("indexedMethod")),
			SelViewForSearchResultsDescription = new Select(By.Id("DescriptionView"));

		public readonly Button
			BtnAddAttribute = new Button(By.Id("addBtn")),
			BtnRemoveAttribute = new Button(By.Id("rmvBtn")),
			BtnMoveAttributeUp = new Button(By.Id("moveUpBtn")),
			BtnMoveAttributeDown = new Button(By.Id("moveDownBtn")),
			BtnViewSearchProxyDoc = new Button(By.Id("btnViewSearchProxyDoc")),
			BtnUpdateSearchProxyDoc = new Button(By.Id("btnUpdateSearchProxyDoc")),
			BtnUpdateAll = new Button(By.Id("UpdateAll")),
			BtnOk = new Button(By.Id("okBtn"));

		public readonly TextBox
			TxtSearchProxyFor = new TextBox(By.Id("txtViewSearchProxyDocId"));

		public SearchIndexingTab(string projTypeInternalName)
		{
			ProjectTypeInternalName = projTypeInternalName;
		}

		public override void NavigateTo()
		{
			Web.Navigate(Store.BaseUrl + "/ProjectCustomization/ProjectTypeCenter/ProjectTypeDetails?EntityTypeName=" + ProjectTypeInternalName + "&Tab=10");
			Wait.Until(d => BtnOk.Enabled);
		}

		public Checkbox GetCheckboxForState(String stateName)
		{
			return new Checkbox(By.XPath(String.Format("//span[text()='{0}']/../../td[1]/input", stateName)));
		}

		public void AddProperty(String propertyName)
		{
			var popup = new SelectPropertyPopup(SelectPropertyPopup.AllowMultiSelect.Yes);
			BtnAddAttribute.Click();
			popup.SwitchTo();
			popup.SelectProperty(propertyName);
			popup.OkButton.Click();
			popup.SwitchBackToParent(WaitForPopupToClose.Yes);
		}
	}
}
