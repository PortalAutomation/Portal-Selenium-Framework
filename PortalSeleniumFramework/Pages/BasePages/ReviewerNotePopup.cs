using System;
using OpenQA.Selenium;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.Pages.BasePages
{
	public class ReviewerNotePopup : IPopup
	{
		public String Title
		{
			get
			{
				switch (_mode) {
				case PopupMode.Add:
					return "Add Reviewer Note";
				case PopupMode.Edit:
					return "Edit Reviewer Note";
				case PopupMode.Respond:
					return "Respond to Reviewer Note";
				default:
					throw new Exception("Unknown mode");
				}
			}
		}

		public readonly Select
			SelNoteType = new Select(By.Name("ReviewerNote.type")),
			SelResponseType = new Select(By.Name("ReviewerNote.responseType"));

		public readonly TextBox
			TxtNote = new TextBox(By.Name("ReviewerNote.note")),
			TxtResponse = new TextBox(By.Name("ReviewerNote.response"));

		public readonly Button
			BtnOk = new Button(By.CssSelector("input[value='OK']")),
			BtnCancel = new Button(By.CssSelector("input[value='Cancel']"));

		private readonly PopupMode _mode;

		public ReviewerNotePopup(PopupMode mode)
		{
			_mode = mode;
		}

		public enum PopupMode { Add, Edit, Respond }
	}
}
