using OpenQA.Selenium;
using PortalSeleniumFramework.Helpers;
using PortalSeleniumFramework.PrimitiveElements;

namespace PortalSeleniumFramework.Pages.BasePages
{
    public class VersionInformation : PageElement
    {
        public readonly Container PatchHistoryDiv = new Container(By.Id("_webrRSV_DIV_0"));

        public readonly Container FrameworkVersionSpan = new Container(By.XPath(".//span[text() = 'Click Portal Framework']/../following-sibling::td[1]/child::span[1]"));

        public readonly Container StoreVersionSpan = new Container(By.XPath(".//span[text() = 'Click Portal']/../following-sibling::td[1]/child::span[1]"));

        public void NavigateTo()
        {
            Web.Navigate(Store.BaseUrl + "/utilities/ShowBuildNumber");
        }

        public bool ValidatePatchId(string cpbuild)
        {
            return PatchHistoryDiv.Text.Contains("Build " + cpbuild);
        }

        public bool ValidateClickPortalFrameworkVersion(string version)
        {
            return FrameworkVersionSpan.Text == version;

        }

        public bool ValidateClickPortalStoreVersion(string version)
        {
            return StoreVersionSpan.Text == version;
        }

    }
}
