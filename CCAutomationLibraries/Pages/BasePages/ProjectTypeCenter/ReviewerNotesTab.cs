using System;
using CCWebUIAuto.Helpers;
using CCWebUIAuto.PrimitiveElements;
using OpenQA.Selenium;

namespace CCWebUIAuto.Pages.BasePages.ProjectTypeCenter
{
	public class ReviewerNotesTab : CCPage
	{
		public readonly String ProjectTypeInternalName;

		public static Button
			BtnEditProperties = new Button(By.CssSelector("input[value='Edit']")),
			BtnNewNoteType = new Button(By.XPath(".//*[@id='webrRSV__BBDIV_0']/input[1]")),
			BtnNewResponseType = new Button(By.XPath(".//*[@id='webrRSV__BBDIV_1']/input[1]")),
			BtnDeleteNoteType = new Button(By.XPath(".//*[@id='webrRSV__BBDIV_0']/input[2]")),
			BtnDeleteResponseType = new Button(By.XPath(".//*[@id='webrRSV__BBDIV_1']/input[2]"));

		public static Container
			DivNoteTypes = new Container(By.Id("webrRSV__ID_0")),
			DivNoteResponseTypes = new Container(By.Id("webrRSV__ID_1"));

		public ReviewerNotesTab(String projTypeInternalName)
		{
			ProjectTypeInternalName = projTypeInternalName;
		}

		public override void NavigateTo()
		{
			WaitForPageLoad();
			Web.Navigate(Store.BaseUrl + "/ProjectCustomization/ProjectTypeCenter/ProjectTypeDetails?EntityTypeName=" + ProjectTypeInternalName + "&Tab=8");
		}

		public void CreateNoteType(String name, Boolean response, String displayOrder)
		{
			BtnNewNoteType.Click();
			var popup = new NoteTypeProperties();
			popup.SwitchTo();
			popup.TxtName.Value = name;
			popup.ChkResponseRequired.Checked = response;
			popup.TxtDisplayOrder.Value = displayOrder;
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
		}

		public void ModifyNoteType(String name, String newName = null, Boolean? response = null, String displayOrder = null)
		{
			var reviewNoteLink = new Link(By.LinkText(name));
			reviewNoteLink.Click();
			var popup = new NoteTypeProperties();
			popup.SwitchTo();
			if (newName != null) popup.TxtName.Value = newName;
			if (response != null) popup.ChkResponseRequired.Checked = response.Value;
			if (displayOrder != null) popup.TxtDisplayOrder.Value = displayOrder;
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
		}

		public void DeleteNoteType(String reviewNoteName)
		{
			var checkbox = new Checkbox(By.XPath("//a[text()='" + reviewNoteName + "']/../../td[1]/input[@type='checkbox']"));
			checkbox.Checked = true;
			BtnDeleteNoteType.Click();
			Web.Driver.SwitchTo().Alert().Accept();
			Wait.Until(d => !new Container(By.LinkText(reviewNoteName)).Exists);
		}

		public void CreateResponseType(String name, String displayOrder)
		{
			BtnNewResponseType.Click();
			var popup = new ResponseTypeProperties();
			popup.SwitchTo();
			popup.TxtName.Value = name;
			popup.TxtDisplayOrder.Value = displayOrder;
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
		}

		public void ModifyResponseType(String name, String newName = null, String displayOrder = null)
		{
			var reviewNoteLink = new Link(By.LinkText(name));
			reviewNoteLink.Click();
			var popup = new ResponseTypeProperties();
			popup.SwitchTo();
			if (newName != null) popup.TxtName.Value = newName;
			if (displayOrder != null) popup.TxtDisplayOrder.Value = displayOrder;
			popup.BtnOk.Click();
			popup.SwitchBackToParent();
		}

		public void DeleteResponseType(String responseTypeName)
		{
			var checkbox = new Checkbox(By.XPath("//*[@id='webrRSV__ID_1']/tbody/tr/td/div/table/tbody/tr/td[4]/a[text()='" +
				responseTypeName + "']/../../td[1]/input[@type='checkbox']"));
			checkbox.Checked = true;
			BtnDeleteResponseType.Click();
			Web.Driver.SwitchTo().Alert().Accept();
			Wait.Until(d => !new Link(By.LinkText(responseTypeName)).Exists);
		}

		public Boolean NoteTypeExists(String noteSettingName)
		{
			return (DivNoteTypes.Text.Contains(noteSettingName));
		}

		public Boolean NoteResponseTypeExists(String name)
		{
			return (DivNoteResponseTypes.Text.Contains(name));
		}
	}

	public class NoteTypeProperties : IPopup
	{
		public readonly TextBox
			TxtName = new TextBox(By.Name("ReviewerNoteType.ID")),
			TxtDisplayOrder = new TextBox(By.Name("ReviewerNoteType.sequenceNumber"));

		public readonly Checkbox
			ChkResponseRequired = new Checkbox(By.Name("ReviewerNoteType.responseRequired"));

		public readonly Select
			SelAuthorPrivacy = new Select(By.Name("select_authorPrivacy")),
			SelPublishedType = new Select(By.Name("ReviewerNoteType.publishedNoteType"));

		public readonly Button
			BtnOk = new Button(By.CssSelector("input[value='OK']")),
			BtnCancel = new Button(By.CssSelector("input[value='Cancel']"));

		public String Title { get { return "Note Type Properties"; } }
	}

	public class ResponseTypeProperties : IPopup
	{
		public readonly TextBox
			TxtName = new TextBox(By.Name("ReviewerNoteResponseType.ID")),
			TxtDisplayOrder = new TextBox(By.Name("ReviewerNoteResponseType.sequenceNumber"));
		public readonly Button
			BtnOk = new Button(By.CssSelector("input[value='OK']")),
			BtnCancel = new Button(By.CssSelector("input[value='Cancel']"));

		public String Title { get { return "Response Type Properties"; } }
	}
}
