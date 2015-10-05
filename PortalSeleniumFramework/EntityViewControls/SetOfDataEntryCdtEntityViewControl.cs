using System;
using PortalSeleniumFramework.Helpers;
using PortalSeleniumFramework.Pages;

namespace PortalSeleniumFramework.EntityViewControls
{
	public class SetOfDataEntryCdtEntityViewControl : SetOfCdtEntities
	{
		public SetOfDataEntryCdtEntityViewControl(String attribute) : base(attribute) { }

		public override void Add<TAddEntityPopup>(Action<TAddEntityPopup> action)
		{
			var popup = new TAddEntityPopup();
			Wait.Until(d => BtnAdd.Enabled);
			BtnAdd.Click();
			popup.SwitchTo();
			action(popup);
			popup.BtnOk.Click();
			popup.SwitchBackToParent(WaitForPopupToClose.Yes);
			Wait.Until(d => BtnAdd.Enabled);
		}
	}
}
