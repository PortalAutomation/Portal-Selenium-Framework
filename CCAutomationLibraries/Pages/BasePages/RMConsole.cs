using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PortalSeleniumFramework.Helpers;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.Pages.BasePages
{
    public class RMConsole : CCPage
    {

        public FastFind FastFind = new FastFind();

        public override void NavigateTo()
        {
            Web.Navigate(Store.BaseUrl + "/RMConsole/MainFrame");
        }
        
    }

    public class FastFind : PageElement
    {
        public Button BtnFindNow = new Button(By.Id("btnSearch"));
        public Container FastFindCell = new Container(By.XPath(".//*[@id='divSidebar']/table[1]/tbody/tr[1]/td[3]"));
        //public Link LnkFastFind = new Link(By.XPath(".//*[@id='divSidebar']/table[1]/tbody/tr[1]/td[3]/a"));
        public Link LnkFastFind = new Link(By.LinkText("Fast Find"));
        public Link LnkFastFindId = new Link(By.Id("TabLink2"));
        public Select SelSearchType = new Select(By.Id("selSearchType"));

        public TextBox
            TxtOrganization = new TextBox(By.Id("txtCompanyName")),
            TxtContactLastName = new TextBox(By.Id("txtPersonLastName")),
            TxtContactFirstName = new TextBox(By.Id("txtPersonFirstName")),
            TxtProject = new TextBox(By.Id("txtProjectName")),
            TxtProjectID = new TextBox(By.Id("txtProjectID"));

        public enum SearchCategory {Organizations, Contacts, Projects}

        /// <summary>
        /// Does not support projects yet TODO
        /// </summary>
        /// <param name="category"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="organization"></param>
        public void Search(FastFind.SearchCategory category, string firstName = "", string lastName = "", string organization = "")
        {
            //FastFindCell.Click();
            //LnkFastFind.Click();
            this.SwitchToFrame();

            CCElement element = new CCElement(By.LinkText("Projects"));

            LnkFastFindId.Click();

            //JavascriptExecutor.Execute("document.getElementById('TabLink2').click();");

            switch (category)
            {
                case SearchCategory.Contacts:
                {
                    SelSearchType.SelectOption("Contacts");
                    TxtOrganization.Value = organization;
                    TxtContactFirstName.Value = firstName;
                    TxtContactLastName.Value = lastName;
                    BtnFindNow.Click();
                    Wait.Until(d => BtnFindNow.Enabled);
                    break;
                    }

                case SearchCategory.Organizations:
                {
                    SelSearchType.SelectOption("Organizations");
                    TxtOrganization.Value = organization;
                    TxtContactFirstName.Value = firstName;
                    TxtContactLastName.Value = lastName;
                    BtnFindNow.Click();
                    Wait.Until(d => BtnFindNow.Enabled);
                    break;
                    }

                case SearchCategory.Projects:
                    // TODO implement this
                    {
                        throw new NotImplementedException();
                        
                    }

            }
        }

        /// <summary>
        /// Use this to find if a person exists in displayed search results
        /// TODO 
        /// </summary>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public bool PersonExists(string lastName)
        {
            Web.PortalDriver.SwitchTo().Frame("ifrmResults");
            Link result = new Link(By.PartialLinkText(lastName));
            return result.Exists;
        }

        public void SwitchToFrame()
        {
            Web.PortalDriver.SwitchTo().Frame("sidebar");
        }

    }
}
