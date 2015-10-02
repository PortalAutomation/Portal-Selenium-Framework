using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortalSeleniumFramework.Helpers
{
	public class UserAccount
	{
		public readonly string UserName;
		public readonly string Password;

		public UserAccount(string user, string pwd)
		{
			UserName = user;
			Password = pwd;
		}
	}
}
