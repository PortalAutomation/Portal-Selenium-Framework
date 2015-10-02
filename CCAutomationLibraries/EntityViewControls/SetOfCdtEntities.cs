using System;
using OpenQA.Selenium;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.EntityViewControls
{
	public abstract class SetOfCdtEntities
	{
		public readonly String Attribute;

		public readonly Button BtnAdd;

		protected SetOfCdtEntities(String attribute)
		{
			Attribute = attribute;
			BtnAdd = new Button(By.XPath(XpathPrefix + "//input[@value='Add']"));
		}

		public String ContainerId { get { return String.Format("_{0}_container", Attribute); } }
		public String XpathPrefix { get { return String.Format("//*[@id='{0}']", ContainerId); } }

		public String GetValueAt(UInt16 column, UInt16 row)
		{
			// + 2 on column to account for the empty padding column
			var cellPath = XpathPrefix + "//table[contains(@id, '_DataTable')]/tbody/tr[" + (1 + row) + "]/td[" + (2 + column) + "]";
			var cell = new Container(By.XPath(cellPath));
			return cell.Text;
		}

		public abstract void Add<TAddEntityPopup>(Action<TAddEntityPopup> action) where TAddEntityPopup : DataEntryCdtAddDataPopup, new();
	}
}
