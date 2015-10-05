using System;
using OpenQA.Selenium;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.Pages.Components
{
	public class ProjectListingComponent : RoomComponent, IDynamicResultSetView
	{
		public Button BtnPrevPage { get; set; }
		public Button BtnNextPage { get; set; }
		public TextBox TxtPage { get; set; }
		public TextBox TxtRowsPerPage { get; set; }
		public Link LnkAdvanced { get; set; }
		public Button BtnGo { get; set; }
		public Button BtnClear { get; set; }

		public ProjectListingComponent() { }

		public ProjectListingComponent(String displayName)
			: base(displayName)
		{
			this.InitializeDrsv();
		}
	}

	public class ProjectListingComponentPropertiesPopup : RoomComponentPropertiesPopup, IPopup
	{
		public new String Title { get { return "Project Listing Properties"; } }

		public readonly Link
			LnkDisplayOptions = new Link(By.LinkText("Display Options")),
			LnkProjectFilteringOptions = new Link(By.LinkText("Project Filtering Options"));

		public readonly DisplayOptionsTab DisplayOptions = new DisplayOptionsTab();
		public readonly ProjectFilteringOptionsTab ProjectFilteringOptions = new ProjectFilteringOptionsTab();
		public readonly CustomColumnEditor CustomColumnEditorDialog = new CustomColumnEditor();

		public class DisplayOptionsTab
		{
			public readonly Button
				BtnAddColumn = new Button(By.Id("AddCustomColumn")),
				BtnEditColumn = new Button(By.Id("EditCustomColumn")),
				BtnRemoveColumn = new Button(By.Id("RemoveCustomColumn"));
				
			public readonly Checkbox
				ChkEnableFilterBar = new Checkbox(By.Name("ProjectView.enableFilterBar")),
				ChkSearchMode = new Checkbox(By.Name("ProjectView.searchMode")),
				ChkShowName = new Checkbox(By.Name("ProjectView.showName")),
				ChkShowIcon = new Checkbox(By.Name("ProjectView.showImage")),
				ChkShowId = new Checkbox(By.Name("ProjectView.showID")),
				ChkShowProjectType = new Checkbox(By.Name("ProjectView.showProjectType")),
				ChkShowOwner = new Checkbox(By.Name("ProjectView.showOwner")),
				ChkShowDateCreated = new Checkbox(By.Name("ProjectView.showDateCreated")),
				ChkShowDateModified = new Checkbox(By.Name("ProjectView.showDateModified")),
				ChkShowProjectStatus = new Checkbox(By.Name("ProjectView.showProjectStatus")),
				ChkShowDateLastStateChange = new Checkbox(By.Name("ProjectView.showDateEnteredState")),
				ChkShowSmartFormLink = new Checkbox(By.Name("ProjectView.showSmartFormLink")),
				ChkShowActivitiesLink = new Checkbox(By.Name("ProjectView.showActivityCreator")),
				ChkRefreshWholePageAfterActiviy = new Checkbox(By.Name("ProjectView.activitiesRefreshWholePage")),
				ChkAlwaysShowPagingBar = new Checkbox(By.Name("ProjectView.showPagingBar")),
				ChkSortAscending = new Checkbox(By.Name("ProjectView.sortAscending"));

			public readonly Select
				SelSmartFormLinkType = new Select(By.Name("ProjectView.smartFormLinkDisplayMode")),
				SelSmartFormExitRedirect = new Select(By.Name("ProjectView.smartFormExitRedirect")),
				SelActivitiesLinkType = new Select(By.Name("ProjectView.activitiesLinkDisplayMode")),
				SelCustomColumns = new Select(By.Name("CustomColumns")),
				SelPagingMode = new Select(By.Name("ProjectView.pagingMode")),
				SelProjectsPerPage = new Select(By.Name("ProjectView.itemsPerPage"));

			public readonly TextBox
				TxtSmartFormLinkText = new TextBox(By.Name("ProjectView.smartFormLinkText")),
				TxtActivitiesLinkText = new TextBox(By.Name("ProjectView.activitiesLinkText"));
		}

		public class ProjectFilteringOptionsTab
		{
			public readonly Checkbox
				ChkFilterUsingComponentPermissions = new Checkbox(By.Name("ProjectView.useComponentUsePermissionsAsFilter"));

			public readonly Select
				SelProjectTypes = new Select(By.Name("ProjectView.projectType")),
				SelContext = new Select(By.Name("ProjectView.dataScope")),
				SelAssignedStates = new Select(By.Name("assignedStates")),
				SelUnassignedStates = new Select(By.Name("unassignedStates"));

			public readonly Button
				BtnAddState = new Button(By.XPath("//input[@value='< Add']")),
				BtnRemoveState = new Button(By.XPath("//input[@value='Remove >']"));

			// two versions exist on the page, but only one is shown at a time
			// this reference should always resolve to the active one
			public Button BtnEditScript {
				get {
					var btnExistingScript = new Button(By.Id("scriptImage"));
					var btnNoScript = new Button(By.Id("emptyScriptImage"));
					return btnExistingScript.Displayed
						? btnExistingScript
						: btnNoScript;
				} 
			}


		}

		public class CustomColumnEditor
		{
			public readonly TextBox
				TxtHeader = new TextBox(By.Id("newCustomColumnHeader")),
				TxtDataPath = new TextBox(By.Id("newCustomColumnPath")),
				TxtSortByDataPath = new TextBox(By.Id("newCustomColumnSortBy")),
				TxtUrlDataPath = new TextBox(By.Id("newCustomColumnUrl"));

			public readonly Button
				BtnOk = new Button(By.XPath("//div[@class='ui-dialog-buttonset']/button"));
		}
	}

}
