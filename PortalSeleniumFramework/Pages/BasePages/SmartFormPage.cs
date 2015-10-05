using System;
using System.Threading;
using OpenQA.Selenium;
using PortalSeleniumFramework.Helpers;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.Pages.BasePages
{
	public class SmartFormPage : CCPage
	{
		public readonly Button
			 BtnContinue = new Button(By.Id("continue_btn_Top")),
			 BtnBack = new Button(By.Id("back_btn_Top")),
			 BtnExpandNotes = new Button(By.CssSelector("img[title='Show Reviewer Notes']")),
			 BtnCollapseNotes = new Button(By.CssSelector("img[title='Hide Reviewer Notes']")),
			 BtnAddReviwerNote = new Button(By.CssSelector("input[value='Add']")),
             //BtnHideShowErrors = new Button(By.CssSelector("a[title='Toggle the display of validation errors']")),
             BtnHideShowErrors = new Button(By.Id("lnkHideShowErrors_Top")),
			 BtnInlineReviewerNoteOk = new Button(By.CssSelector("input[value='OK']")),
			 BtnFinish = new Button(By.Id("finish_btn_Bottom")),
			 BtnInlineReviewerNoteCancel = new Button(By.CssSelector("input[value='Cancel']"));

		public readonly Link
			 LnkSave = new Link(By.Id("lnkSaveProjectEditor_Top")),
			 LnkExit = new Link(By.Id("lnkExitProjectEditor_Top"));

		public readonly Select
			 SelInlineReviewerNoteType = new Select(By.Name("ReviewerNote.type"));

		public readonly SaveChangesPromptContainer SaveChangesPrompt = new SaveChangesPromptContainer();
        public readonly ValidationErrorsContainer ValidationErrors = new ValidationErrorsContainer();

		public class SaveChangesPromptContainer
		{
			public readonly Button
				BtnSaveAndExit = new Button(By.XPath("//div[contains(@class, 'ui-dialog-buttonpane')]//span[text()='Save Changes & Exit']/..")),
				BtnIgnoreChangesAndExit = new Button(By.XPath("//div[contains(@class, 'ui-dialog-buttonpane')]//span[text()='Ignore Changes & Exit']/..")),
				BtnCancel = new Button(By.XPath("//div[contains(@class, 'ui-dialog-buttonpane')]//span[text()='Cancel']/.."));
		}

	    public class ValidationErrorsContainer
	    {
            // something screwy here...
           
            public readonly Button
                BtnRefresh = new Button(By.Id("refreshBtn"));
            
            /// <summary>
            /// Opens up the validation errors, and then closes it after verification.
            /// </summary>
            /// <param name="sf"></param>
            /// <param name="iconText"></param>
            /// <param name="messageName"></param>
            /// <param name="field"></param>
            /// <param name="jumpToLink"></param>
            /// <returns></returns>
            public bool VerifyRowValidationErrorExists(SmartFormPage sf, string iconText, string messageName, string field, string jumpToLink)
	        {
                bool returnValue = false;
                sf.BtnHideShowErrors.Click();
                Web.PortalDriver.SwitchTo().Frame("validationErrors");
                // use the field name as the anchor
                Container FieldContainer = new Container(By.XPath(".//td[text()='" + field + "']"));
                Container MessageContainer = new Container(By.XPath(".//td[text()='" + field + "']/../td[2][text()='" + messageName + "']"));
                Container IconTextContainer = new Container(By.XPath(".//td[text()='" + field + "']/../td[1]/img[@title='"+ iconText +"']"));
                Container JumpToContainer = new Container(By.XPath("//.//td[text()='" + field + "']/../td[4]/a[text()='" + jumpToLink + "']"));

                if (FieldContainer.Exists && MessageContainer.Exists && IconTextContainer.Exists && JumpToContainer.Exists)
	            {
	                returnValue = true;
	            }
                return returnValue;
	        }

            
	        public bool VerifyNoValidationErrors()
	        {
	            throw new NotImplementedException();

	        }
	    }

		public override void NavigateTo()
		{
			throw new NotImplementedException();
		}

		public void AddReviewerNote(String type, String note)
		{
			BtnAddReviwerNote.Click();
			var popup = new ReviewerNotePopup(ReviewerNotePopup.PopupMode.Add);
			popup.SwitchTo();
			popup.SelNoteType.SelectOption(type);
			popup.TxtNote.Value = note;
			popup.BtnOk.Click();
			popup.SwitchBackToParent(WaitForPopupToClose.Yes);
			Thread.Sleep(1500);
			WaitForPageLoad();
		}

		public void EditReviewerNoteWithPopup(Int32 index, String newType, String newNote)
		{
			if (BtnExpandNotes.Exists) {
				BtnExpandNotes.Click();
				Wait.Until(d => BtnCollapseNotes.Exists);
			}
			// accept index starting at 0, xpath index start at 1, and the first row is the filter, so
			// add 2 to index argument
			var popupLink = new Link(By.XPath("//*[@id='_webrRSV_DIV_0']/table/tbody/tr[" + (index + 2) + "]/td[1]/a/img"));
			popupLink.Click();
			var popup = new ReviewerNotePopup(ReviewerNotePopup.PopupMode.Edit);
			popup.SwitchTo();
			popup.SelNoteType.SelectOption(newType);
			popup.TxtNote.Value = newNote;
			popup.BtnOk.Click();
			popup.SwitchBackToParent(WaitForPopupToClose.Yes);
			Thread.Sleep(1500);
			WaitForPageLoad();
		}

		public void EditReviewerNoteInline(Int32 index, String newType, String newNote)
		{
			if (BtnExpandNotes.Exists) {
				BtnExpandNotes.Click();
				Wait.Until(d => BtnCollapseNotes.Exists);
			}
			// accept index starting at 0, xpath index start at 1, and the first row is the filter, so
			// add 2 to index argument
			var editLink = new Link(By.XPath("//*[@id='_webrRSV_DIV_0']/table/tbody/tr[" + (index + 2) + "]/td[2]/div[1]/strong/a"));
			editLink.Click();
			var inlineSelect =
				new Select(By.XPath("//*[@id='_webrRSV_DIV_0']/table/tbody/tr[" + (index + 2) + "]/td[2]/div[1]/div/select"));
			var inlineNoteText =
				new TextBox(By.XPath("//*[@id='_webrRSV_DIV_0']/table/tbody/tr[" + (index + 2) + "]/td[2]/div[3]/textarea"));
			inlineSelect.SelectOption(newType);
			inlineNoteText.Value = newNote;
			BtnInlineReviewerNoteOk.Click();
			Wait.Until(d => new Container(By.XPath("//div[text()='" + newNote + "']")).Exists);
		}

		public void RespondToNoteWithPopup(Int32 index, String responseType, String response)
		{
			if (BtnExpandNotes.Exists) {
				BtnExpandNotes.Click();
				Wait.Until(d => BtnCollapseNotes.Exists);
			}
			// accept index starting at 0, xpath index start at 1, and the first row is the filter, so
			// add 2 to index argument
			var popupLink = new Link(By.XPath("//*[@id='_webrRSV_DIV_0']/table/tbody/tr[" + (index + 2) + "]/td[1]/a/img"));
			popupLink.Click();
			var popup = new ReviewerNotePopup(ReviewerNotePopup.PopupMode.Respond);
			popup.SwitchTo();
			popup.SelResponseType.SelectOption(responseType);
			popup.TxtResponse.Value = response;
			popup.BtnOk.Click();
			popup.SwitchBackToParent(WaitForPopupToClose.Yes);
			Thread.Sleep(1500);
			WaitForPageLoad();
		}
	}
}
