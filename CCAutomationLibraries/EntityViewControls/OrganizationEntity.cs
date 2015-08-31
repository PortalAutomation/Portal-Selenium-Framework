using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CCWebUIAuto.PrimitiveElements;
using OpenQA.Selenium;

namespace CCWebUIAuto.EntityViewControls
{
	public class OrganizationEntity : SelectionCdtEntity
	{
		public readonly Link LnkAddOrganization;

		public OrganizationEntity(string attribute) : base(attribute)
		{
			//This link shows up when typing the name in the text box, of the organization that is not in the store
			LnkAddOrganization = new Link(By.LinkText("Add Organization"));
		}
	}
}
