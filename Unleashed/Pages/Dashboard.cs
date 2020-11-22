using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using Unleashed.BaseSetup;
using Unleashed.Utilities;

namespace Unleashed.Pages {
    public class Dashboard {
        private IWebDriver driver;

        public Dashboard() {

            this.driver = Driver.Instance;
            Driver.WaitUntil(() => this.driver.Title.Contains("Dashboard"), 10);
            Popup.CloseIfFound(10);
        }
        public Menu Menu => new Menu();
        
    }
}
