using System;
using OpenQA.Selenium;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.Pages.Components
{
	public class SiteSearchComponent : RoomComponent
	{
		public readonly TextBox 
			TxtSearch = new TextBox(By.Id("ContentSearchView_txtSearch"));
		
		public readonly Button 
			BtnGo = new Button(By.Id("btnGo"));
		
		public readonly Select
			SelScope = new Select(By.Id("SelectScope"));
		
		public SiteSearchComponent() { }

		public SiteSearchComponent(String displayName) : base(displayName) { }
	}

	public class SiteSearchComponentPropertiesPopup : RoomComponentPropertiesPopup, IPopup
	{
		public new string Title { get { return "Site Search Properties"; } }

		public readonly TextBox
			TxtLabel = new TextBox(By.Id("ContentSearchView.labelText")),
			TxtPlaceholder = new TextBox(By.Id("ContentSearchView.placeholderText")),
			TxtMaxResults = new TextBox(By.Id("ContentSearchView.maxResults")),
			TxtProjectsSectionName = new TextBox(By.Id("ContentSearchView.projectsSectionName")),
			TxtDocumentsSectionName = new TextBox(By.Id("ContentSearchView.documentsSectionName")),
			TxtPagesSectionName = new TextBox(By.Id("ContentSearchView.pagesSectionName")),
			TxtScopePageName = new TextBox(By.Id("scopePageName"));

		public readonly Select
			SelUiStyle = new Select(By.Id("ContentSearchView.uiStyle")),
			SelResultsStyle = new Select(By.Id("ContentSearchView.resultsStyle")),
			SelDataSource = new Select(By.Id("ContentSearchView.dataSource"));

		public readonly Button
			BtnEditAdvancedFilterScript = new Button(By.Id("scriptImage")),
			BtnPageScopeParentChooser = new Button(By.Id("btnPageChooser")),
			BtnClearPageScopeParent = new Button(By.Id("btnClearPageFilter"));

		public readonly Checkbox
			ChkProjectsSectionEnabled = new Checkbox(By.Id("ContentSearchView.projectsSectionEnabled")),
			ChkDocumentsSectionEnabled = new Checkbox(By.Id("ContentSearchView.documentsSectionEnabled")),
			ChkPagesSectionEnabled = new Checkbox(By.Id("ContentSearchView.pagesSectionEnabled"));
	}
}
