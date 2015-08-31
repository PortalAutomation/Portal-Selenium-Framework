using System;
using CCWebUIAuto.PrimitiveElements;
using OpenQA.Selenium;

namespace CCWebUIAuto.Pages.Components
{
	public class CustomSearchComponent : RoomComponent, IDynamicResultSetView
	{
		public Button BtnPrevPage { get; set; }
		public Button BtnNextPage { get; set; }
		public TextBox TxtPage { get; set; }
		public TextBox TxtRowsPerPage { get; set; }
		public Link LnkAdvanced { get; set; }
		public Button BtnGo { get; set; }
		public Button BtnClear { get; set; }

		public readonly Button BtnExport = new Button(By.XPath("//input[@value='Export']"));
		public readonly Button BtnEmail = new Button(By.XPath("//input[@value='Email']"));

		public CustomSearchComponent() { }

		public CustomSearchComponent(String displayName)
			: base(displayName)
		{
			this.InitializeDrsv();
		}
	}

	public class CustomSearchComponentPropertiesPopup : RoomComponentPropertiesPopup, IPopup
	{
		public readonly TextBox
			TxtSingleSearchName = new TextBox(By.Name("SavedSearchView.savedSearch.name")),
			TxtResultsPerPage = new TextBox(By.Name("SavedSearchView.pageSize"));

		public readonly Button
			BtnConditionalDisplayScript = new Button(By.XPath("//img[contains(@src, 'script')]")),
			BtnSelectSingleSearch = new Button(By.Name("btnSelectSavedSearch")),
			BtnNewSingleSearch = new Button(By.Name("btnNewSavedSearch")),
			BtnEditSingleSearch = new Button(By.Name("btnEditSavedSearch")),
			BtnAddSearchToList = new Button(By.Name("btnAddSavedSearchForList")),
			BtnRemoveSearchFromList = new Button(By.Name("btnRemoveSavedSearchForList")),
			BtnNewSavedSearchForList = new Button(By.Name("btnNewSavedSearchForList")),
			BtnEditSavedSearchInList = new Button(By.Name("btnEditSavedSearchForList")),
			BtnNewSearchWithTag = new Button(By.Name("btn_NewSearchWithTag"));

		public readonly Radio
			RdoSingleSearch = new Radio(By.XPath("//input[@name='radioSearchView' and @value='single']")),
			RdoListOfSearches = new Radio(By.XPath("//input[@name='radioSearchView' and @value='list']")),
			RdoSearchWithTag = new Radio(By.XPath("//input[@name='radioSearchView' and @value='tag']"));

		public readonly Select
			SelCustomSearchList = new Select(By.Name("selectedSearches")),
			SelTag = new Select(By.Name("displaySearchesTag")),
			SelPagingMode = new Select(By.Name("SavedSearchView.pagingMode"));

		public readonly Checkbox
			ChkOpenLinksInNewWindow = new Checkbox(By.Name("SavedSearchView.openLinksInNewWindow")),
			ChkShowExportButton = new Checkbox(By.Name("SavedSearchView.showExportButton")),
			ChkShowEmailButton = new Checkbox(By.Name("SavedSearchView.showEmailLink")),
			ChkShowFilterBar = new Checkbox(By.Name("SavedSearchView.showFilterBar")),
			ChkShowDetailsLink = new Checkbox(By.Name("SavedSearchView.showViewDetailsLink")),
			ChkShowInlineDetailsLink = new Checkbox(By.Name("SavedSearchView.showInlineDetailsLinks")),
			ChkAlwaysShowPagingBar = new Checkbox(By.Name("SavedSearchView.showPagingBar")),
			ChkSearchMode = new Checkbox(By.Name("SavedSearchView.DRSV_searchMode")),
			ChkShowImage = new Checkbox(By.Name("SavedSearchView.showIcon")),
			ChkShowChangeParametersButton = new Checkbox(By.Name("SavedSearchView.showChangeParametersButton")),
			ChkListOptionsRunButton = new Checkbox(By.Name("SavedSearchView.searchListShowRunOption")),
			ChkListOptionsDescription = new Checkbox(By.Name("SavedSearchView.searchListShowDescription")),
			ChkListOptionsFilterBar = new Checkbox(By.Name("SavedSearchView.searchListShowFilterBar")),
			ChkListOptionsResultType = new Checkbox(By.Name("SavedSearchView.searchListShowResultType"));

		public new string Title { get { return "Custom Search Results Properties"; } }
	}

	public class CustomSearchResultsPopup : IPopup, IDynamicResultSetView
	{
		public String Title { get { return "Search Results"; } }

		public Button BtnPrevPage { get; set; }
		public Button BtnNextPage { get; set; }
		public TextBox TxtPage { get; set; }
		public TextBox TxtRowsPerPage { get; set; }
		public Link LnkAdvanced { get; set; }
		public Button BtnGo { get; set; }
		public Button BtnClear { get; set; }

		public readonly Button BtnExport = new Button(By.XPath("//input[@value='Export']"));
		public readonly Button BtnEmail = new Button(By.XPath("//input[@value='Email']"));
		public readonly Button BtnChangeParameters = new Button(By.XPath("//input[@value='Change Parameters']"));
	}

	public class CustomSearchExportPopup : IPopup
	{
		public String Title { get { return "Export Status"; } }
		public readonly Link LnkDownload = new Link(By.LinkText("Click here to download"));
	}
}
