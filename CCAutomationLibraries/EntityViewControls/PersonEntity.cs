using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CCWebUIAuto.PrimitiveElements;
using OpenQA.Selenium;

namespace CCWebUIAuto.EntityViewControls
{
	public class PersonEntity : SelectionCdtEntity
	{
		public readonly Link LnkAddUser;

		public PersonEntity(string attribute) : base(attribute)
		{
			//This link shows up when typing the name in the text box, of the person that is not in the store
			LnkAddUser = new Link(By.LinkText("Add User"));
		}
	}
}
