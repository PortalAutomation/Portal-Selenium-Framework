using System;
using CCWebUIAuto.Helpers;
using CCWebUIAuto.Pages.BasePages;
using CCWebUIAuto.PrimitiveElements;
using OpenQA.Selenium;

namespace CCWebUIAuto.Pages.Components
{
	public class ReviewerNotesComponent : RoomComponent, IDynamicResultSetView
	{
		public Button BtnPrevPage { get; set; }
		public Button BtnNextPage { get; set; }
		public TextBox TxtPage { get; set; }
		public TextBox TxtRowsPerPage { get; set; }
		public Link LnkAdvanced { get; set; }
		public Button BtnGo { get; set; }
		public Button BtnClear { get; set; }

		public ReviewerNotesComponent() { }

		public ReviewerNotesComponent(String displayName)
			: base(displayName)
		{
			this.InitializeDrsv();
		}

		public void EditNoteInline(Int32 index, String noteType, String newText)
		{
			var lnkTitle = new Link(By.XPath(XpathPrefix + "//tr[@data-drsv-row='" + index + "']//a[contains(@class, 'ReviewTitle')]"));
			lnkTitle.Click();
			var noteArea = new TextBox(By.XPath(XpathPrefix + "//tr[@data-drsv-row='" + index + "']//textArea"));
			var btnEditOk = new Button(By.XPath(XpathPrefix + "//tr[@data-drsv-row='" + index + "']//input[@value = 'OK']"));
			var selType = new Select(By.XPath(XpathPrefix + "//tr[@data-drsv-row='" + index + "']//select"));
			Wait.Until(d => noteArea.Displayed);
			selType.SelectOption(noteType);
			noteArea.Value = newText;
			btnEditOk.Click();
			Wait.Until(d => new Container(By.XPath("//div[contains(text(), '" + newText + "')]")).Displayed);
		}

		public void RespondInline(Int32 index, String responseType, String response)
		{
			var lnkRespond = new Link(By.XPath(XpathPrefix + "//tr[@data-drsv-row='" + index + "']//a[contains(@id, 'CreateResponse')]"));
			lnkRespond.Click();
			var txtResponse = new TextBox(By.XPath(XpathPrefix + "//tr[@data-drsv-row='" + index + "']//div[@class='ResponseNote']//textarea"));
			var btnRespondOk = new Button(By.XPath(XpathPrefix + "//tr[@data-drsv-row='" + index + "']//div[@class='ResponseNote']//input[@value = 'OK']"));
			var selType = new Select(By.XPath(XpathPrefix + "//tr[@data-drsv-row='" + index + "']//div[@class='ResponseTitle']//select"));
			Wait.Until(d => txtResponse.Displayed);
			selType.SelectOption(responseType);
			txtResponse.Value = response;
			btnRespondOk.Click();
			Wait.Until(d => new Container(By.XPath("//div[contains(text(), '" + response + "')]")).Displayed);
		}

		public void EditNoteInPopup(Int32 index, String noteType, String newText)
		{
			var btnOpenPopup = new Button(By.XPath(XpathPrefix + "//tr[@data-drsv-row='" + index + "']//img[contains(@src, 'ReviewNote.gif')]"));
			var popup = new ReviewerNotePopup(ReviewerNotePopup.PopupMode.Edit);
			btnOpenPopup.Click();
			popup.SwitchTo();
			popup.SelNoteType.SelectOption(noteType);
			popup.TxtNote.Value = newText;
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
			Wait.Until(d => new Container(By.XPath("//div[contains(text(), '" + newText + "')]")).Displayed);
		}

		public void RespondInPopup(Int32 index, String responseType, String response)
		{
			var btnOpenPopup = new Button(By.XPath(XpathPrefix + "//tr[@data-drsv-row='" + index + "']//img[contains(@src, 'ReviewNote.gif')]"));
			var popup = new ReviewerNotePopup(ReviewerNotePopup.PopupMode.Respond);
			btnOpenPopup.Click();
			popup.SwitchTo();
			popup.SelResponseType.SelectOption(responseType);
			popup.TxtResponse.Value = response;
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
			Wait.Until(d => new Container(By.XPath("//div[contains(text(), '" + response + "')]")).Displayed);
		}
	}

	public class ReviewerNotesComponentPropertiesPopup : RoomComponentPropertiesPopup, IPopup
	{
		public new string Title { get { return "Reviewer Notes Properties"; } }

		public readonly Checkbox
			ChkShowFilterBar = new Checkbox(By.Name("ReviewerNotesView.showFilterBar")),
			ChkShowReviewer = new Checkbox(By.Name("ReviewerNotesView.showReviewer")),
			ChkShowCreatedDate = new Checkbox(By.Name("ReviewerNotesView.showDateCreated")),
			ChkShowModifiedDate = new Checkbox(By.Name("ReviewerNotesView.showDateModified")),
			ChkShowProjectId = new Checkbox(By.Name("ReviewerNotesView.showProjectID")),
			ChkShowNotesNotInPath = new Checkbox(By.Name("ReviewerNotesView.showNotesNotInPath")),
			ChkReturnBackToCurrentTemplate = new Checkbox(By.Name("ReviewerNotesView.returnToTemplate")),
			ChkSortAscending = new Checkbox(By.Name("ReviewerNotesView.sortAscending"));

		public readonly Select
			SelNotesPerPage = new Select(By.Name("ReviewerNotesView.itemsPerPage")),
			SelSortType = new Select(By.Name("ReviewerNotesView.sortType"));

		public readonly TextBox
			TxtMaximumNoteLength = new TextBox(By.Name("ReviewerNotesView.notesDisplayLength"));
	}
}
