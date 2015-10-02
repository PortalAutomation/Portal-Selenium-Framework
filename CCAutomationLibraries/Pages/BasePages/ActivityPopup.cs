using System;
using OpenQA.Selenium;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.Pages.BasePages
{
	public class ActivityPopup : IPopup
	{
		public String Title { get { return String.Format("Execute \"{0}\" on {1}", _activityName, _projectId); } }

	    public readonly Button
	        BtnOk = new Button(By.Name("okBtn")),
	        BtnCancel = new Button(By.Name("cancelBtn")),
	        BtnSubmit = new Button(By.Id("confirmLoginSubmitBtn"));

        public readonly TextBox
            UserId = new TextBox(By.Id("confirmUserId")),
            UserPassword = new TextBox(By.Id("confirmPassword"));

		private readonly String _projectId, _activityName;

		public ActivityPopup(String projectId, String activityName)
		{
			_projectId = projectId;
			_activityName = activityName;
		}

        /// <summary>
        /// Used for Confirm Credentials embedded into activity popups
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pswd"></param>
	    public void ConfirmCredentials(string user, string pswd)
	    {
            //Web.PortalDriver.SwitchTo().Frame(Web.PortalDriver.FindElement(By.Id("GB_frame_confirmLoginMsg")));
            Web.PortalDriver.SwitchTo().Frame("GB_frame_confirmLoginMsg");
	        UserId.Value = user;
	        UserPassword.Value = pswd;
            this.BtnSubmit.Click();
	    }
        
	}
}
