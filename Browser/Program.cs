using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System.Collections.ObjectModel;
using System.Threading;

namespace Browser
{
    public class BrowserDriver : IDisposable
    {
        internal IWebDriver MyDriver = null;
        
        /// <summary>
        ///  constructor takes browser type
        /// </summary>
        /// <param name="thebrowsertype"></param>
        public BrowserDriver(BrowserType thebrowsertype)
        {
            try
            {


                // Browsertype = thebrowsertype;
                switch (thebrowsertype)
                {
                    case BrowserType.FireFox:
                        MyDriver = new OpenQA.Selenium.Firefox.FirefoxDriver();
                        break;
                    case BrowserType.IE:

                        MyDriver = new OpenQA.Selenium.IE.InternetExplorerDriver();

                        break;
                    case BrowserType.Chrome:
                        MyDriver = new OpenQA.Selenium.Chrome.ChromeDriver();
                        break;
                }
                MyDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(60));
                MyDriver.Manage().Window.Maximize();
            }
             
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        /// <summary>
        /// <summary>
        ///  Navigate
        /// </summary>
        /// <param name="theurl"></param>
        public void Navigate(string theurl)
        {
            Console.WriteLine("Navigating to " + theurl);
            MyDriver.Navigate().GoToUrl(theurl);

        }

        public enum BrowserType
        {
            FireFox = 1,
            IE = 2,
            Chrome = 3
        }

        /// <summary>
        ///  Menu link
        /// </summary>
        /// <param name="theMenuLink"></param>
        /// <returns></returns>
        public IWebElement GetNavigationLink(string theMenuLink)
        {
            IWebElement aElement = MyDriver.FindElement(By.ClassName("navigation"));
            ReadOnlyCollection<IWebElement> aCollectionElements = aElement.FindElements(By.TagName("a"));
            foreach (IWebElement aEle in aCollectionElements)
            {
                if (aEle.Text.ToLower() == theMenuLink.ToLower())
                    return aEle;
            }

            throw new Exception("Navigation link is not found." + theMenuLink);
        }

        /// <summary>
        ///  Selecting a dropdown value based on value. 
        /// </summary>
        /// <param name="theValue"></param>
        public IWebElement DropDown(string theValue)
        {
            IWebElement aElement = MyDriver.FindElement(By.Id("selectContainerProduct"));
            aElement.Click();
            //   aElement = aElement.FindElement(By.TagName("select"));
            ReadOnlyCollection<IWebElement> aCollectionWebElements = aElement.FindElements(By.ClassName("active-result"));
            foreach (IWebElement aEle in aCollectionWebElements)
                if (aEle.Text.ToLower() == theValue.ToLower())
                    return aEle;

            throw new Exception("Dropdown Item not found." + theValue);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetUrl()
        {
            return MyDriver.Url.ToLower();
        }

        /// <summary>
        ///  Dispose driver object
        /// </summary>
        public void Dispose()
        {
            MyDriver.Quit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theSubMenuName"></param>
        public IWebElement SubNavaigationLink(string theSubMenuLink)
        {

            Thread.Sleep(1000);
            IWebElement aElement = (MyDriver.FindElement(By.Id("subNav")));
            ReadOnlyCollection<IWebElement> aCollectionElements = aElement.FindElements(By.TagName("a"));
            foreach (IWebElement aEle in aCollectionElements)
            {
                if (aEle.Text.ToLower() == theSubMenuLink.ToLower())
                    return aEle;
            }

            throw new Exception("Sub Menu Navigation link is not found." + theSubMenuLink);

        }

        /// <summary>
        ///  Get Executive MEember link.
        /// </summary>
        /// <param name="theExecutiveName"></param>
        /// <returns></returns>
        public IWebElement GetExecutiveMemberLink(string theExecutiveName)
        {
            IWebElement aElement;
            string aHerfValue;
            ReadOnlyCollection<IWebElement> aCollectionElements =
                MyDriver.FindElements(By.ClassName("SecondaryCallToAction"));
            foreach (IWebElement aEle in aCollectionElements)
            {
                aElement = aEle.FindElement(By.TagName("a"));

                if (aElement.GetAttribute("href").ToLower().Contains(theExecutiveName.ToLower()))
                    return aElement;
            }

            throw new Exception("Executive link is not found." + theExecutiveName);

        }

        /// <summary>
        ///  Get ModalDialog source text
        /// </summary>
        /// <param name="theiframecount"></param>
        /// <returns></returns>
        public string GetModalDialogText(int theiframecount = 0)
        {
            Thread.Sleep(100);
           return MyDriver.SwitchTo().Frame(theiframecount).PageSource;
            throw new Exception("Modal Dialog is missing.");
        }
    }
}
