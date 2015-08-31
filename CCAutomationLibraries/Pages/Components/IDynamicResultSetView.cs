using System;
using System.Linq;
using System.Net;
using CCWebUIAuto.Helpers;
using CCWebUIAuto.PrimitiveElements;
using OpenQA.Selenium;

namespace CCWebUIAuto.Pages.Components
{
	public interface IDynamicResultSetView
	{
		Button BtnPrevPage { get; set; }
		Button BtnNextPage { get; set; }
		TextBox TxtPage { get; set; }
		TextBox TxtRowsPerPage { get; set; }
		Link LnkAdvanced { get; set; }
		Button BtnGo { get; set; }
		Button BtnClear { get; set; }
	}

	public static class DynamicResultSetViewExtensionMethods
	{
		public static void SetCriteria(this IDynamicResultSetView component, String attribute, String criteria, Int16 row = 0)
		{
			var optionElement = new Select(By.XPath("//select[contains(@name, 'queryField" + (row + 1) + "')]/option[text() = '" + attribute + "']"));
			var displayName = ((RoomComponent) component).DisplayName;
			var prefix = "//span[text()='" + displayName + "']";
            var selAttribute = new Select(By.XPath(prefix + "/../../../../../table[2]//table[contains(@id,'_filterTable')]//select[contains(@id,'_queryField" + (row + 1) + "')]"));
            var txtCriteria = new TextBox(By.XPath(prefix + "/../../../../../table[2]//table[contains(@id,'_filterTable')]//input[contains(@id,'_queryCriteria" + (row + 1) + "')]"));
			Wait.Until(d => selAttribute.Enabled && optionElement.Exists && txtCriteria.Enabled);
			selAttribute.SelectOption(attribute);
			txtCriteria.Value = criteria;
			component.BtnGo.Click();
			Wait.Until(d => component.BtnGo.Enabled);
		}

		public static void InitializeDrsv(this IDynamicResultSetView component)
		{
			var displayName = ((RoomComponent) component).DisplayName;
			var prefix = "//span[text()='" + WebUtility.HtmlEncode(displayName) + "']";
            component.BtnPrevPage = new Button(By.XPath(prefix + "/../../../../../table[2]//img[contains(@id,'_goPrev')]"));
            component.BtnNextPage = new Button(By.XPath(prefix + "/../../../../../table[2]//img[contains(@id,'_goNext')]"));
            component.TxtPage = new TextBox(By.XPath(prefix + "/../../../../../table[2]//input[contains(@id,'_pageInput')]"));
            component.TxtRowsPerPage = new TextBox(By.XPath(prefix + "/../../../../../table[2]//input[contains(@id,'_pagesize')]"));
            component.LnkAdvanced = new Link(By.XPath(prefix + "/../../../../../table[2]//a[text()='Advanced']"));
            component.BtnGo = new Button(By.XPath(prefix + "/../../../../../table[2]//input[contains(@id,'_requery')]"));
            component.BtnClear = new Button(By.XPath(prefix + "/../../../../../table[2]//input[contains(@id,'_clearquery')]"));
		}

		public static void SetPageSize(this IDynamicResultSetView component, String size)
		{
			component.WaitUntilReady();
			component.TxtRowsPerPage.Value = size;
			// send tab key to complete
			component.TxtRowsPerPage.SendKeys(Keys.Tab);
		}

		/// <summary>
		/// Column, then row
		/// </summary>
		public static String GetValueAt(this IDynamicResultSetView component, UInt16 column, UInt16 row)
		{
			var displayName = ((RoomComponent) component).DisplayName;
			//var prefix = "//div[@data-display-name= '" + displayName + "']";
            var prefix = "//span[text()='" + WebUtility.HtmlEncode(displayName) + "']";
            var cellPath = prefix + "/../../../../../table[2]//div[contains(@id, 'component') and not(contains(@id, '_paging'))]/table/tbody/tr[" + (2 + row) + "]/td[" + (1 + column) + "]";
			var cell = new Container(By.XPath(cellPath));
			return cell.Text;
		}

		public static void WaitUntilReady(this IDynamicResultSetView component)
		{
			var displayName = ((RoomComponent) component).DisplayName;
            var prefix = "//span[text()='" + WebUtility.HtmlEncode(displayName) + "']";
			//var prefix = "//div[@data-display-name= '" + displayName + "']";
            var dataSpan = new Container(By.XPath(prefix + "//../../../../../table[2]//span[@data-is-ready='true']"));
			Wait.Until(d => dataSpan.Exists);
		}

		public static Int32 GetNumberOfRowsDisplayed(this IDynamicResultSetView component)
		{
			component.WaitUntilReady();
			var componentDiv = ((RoomComponent) component).DivComponentArea;
            var rows = componentDiv.GetDescendants("/../../../../../table[2]//div[contains(@id, 'component') and not(contains(@id, '_paging'))]/table/tbody/tr");
			return rows.Count() - 1; // don't count the header row
		}

		public static void Clear(this IDynamicResultSetView component)
		{
			Wait.Until(d => component.BtnGo.Enabled);
			if (component.BtnClear.Enabled) {
				component.BtnClear.Click();
			}
			Wait.Until(d => component.BtnGo.Enabled);
		}

		// the sort indicators make it slightly harder to find the header links, so
		// we have to grab them in a round-about way
		public static void SortColumn(this IDynamicResultSetView component, UInt16 columnIdx)
		{
			var displayName = ((RoomComponent) component).DisplayName;
			//var prefix = "//div[@data-display-name= '" + displayName + "']";
            var prefix = "//span[text()='" + WebUtility.HtmlEncode(displayName) + "']";
            //var path = prefix + "/../../../../../table[2]//table[contains(@id,'_filterTable')]//div[contains(@id, 'component') and not(contains(@id, '_paging'))]/table/tbody/tr[1]/td[" + (columnIdx + 1) + "]/a";
            var path = prefix + "/../../../../../table[2]//div[contains(@id, 'component')]/table/tbody/tr[1]/td[" + (columnIdx + 1) + "]/a";
            var link = new Link(By.XPath(path));
			link.Click();
			Wait.Until(d => component.TxtPage.Enabled);
		}
	}
}
