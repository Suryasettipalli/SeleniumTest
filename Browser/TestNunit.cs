using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Browser;
using System.Threading;

namespace TestBrowser
{
    internal class TestNunit
    {
        protected BrowserDriver aBrowserDriver = null;
        private string URL = "http://www.omgeo.com/";

        [TestFixtureSetUp]
        public void TestFixSetup()
        {
            aBrowserDriver = new BrowserDriver(Browser.BrowserDriver.BrowserType.FireFox);
            aBrowserDriver.Navigate(URL);
            Assert.AreEqual(aBrowserDriver.GetUrl(), URL, "Not Navigating to " + URL);
        }

        [TestFixtureTearDown]
        public void TestFixTearDown()
        {
            aBrowserDriver.Dispose();
        }

        [Test]
        /// <summary>
        /// Go to www.omgeo.com, click on “About” tab. verify that we navigate to the “about Omgeo” page
        /// </summary>
        public void Testcase1()
        {

            aBrowserDriver.GetNavigationLink("About").Click();
            // verify navigated to about omgeo page
            Assert.IsTrue(aBrowserDriver.GetUrl().Contains("http://www.omgeo.com/aboutomgeo"),
                "not navigated to about page");
        }

        [Test]
        public void Testcase2()
        {
            string aDropdownITem = "Alert";
            Testcase1();
            // click on Product Drop down and select ALERT option 
            aBrowserDriver.DropDown(aDropdownITem).Click();
            // verify alert option is selected
            Assert.IsTrue(aBrowserDriver.GetUrl().Contains(URL + aDropdownITem.ToLower()),
                "Not navigated to the Alert page");

        }

        [Test]
        /// <summary>
        /// Navigate to the “Leadership Team” page a. click on a member of the Executive Team b. verify a portion of the text is displayed specific to member.
        /// </summary>
        public void Testcase3()
        {
            string aSubMenuItem = "Leadership Team";
            string aProfileName = "paula_Arthus";
            string aLeadershipurl = "http://www.omgeo.com/leadership_team";

            Testcase1();
            aBrowserDriver.SubNavaigationLink(aSubMenuItem).Click();
            aBrowserDriver.GetUrl().Contains(URL + aSubMenuItem);
            Assert.IsTrue(aBrowserDriver.GetUrl().ToLower().Contains(aLeadershipurl),
                "Not navigated to Leadership page.");
            aBrowserDriver.GetExecutiveMemberLink(aProfileName).Click();
            Thread.Sleep(1000);
            // verify modal is opened up and opens up a correct modal.
            string aModalTxt =aBrowserDriver.GetModalDialogText().ToLower();
            Assert.IsTrue(
                aModalTxt.Contains("bios/paula-arthus-omgeo.jpg"),
                "Not navigated to corresponding profile page.");

        }
    }
}
