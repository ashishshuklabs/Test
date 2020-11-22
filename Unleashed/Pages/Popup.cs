using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using Unleashed.BaseSetup;
using Unleashed.Utilities;

namespace Unleashed.Pages {
    public class Popup {
        private IWebDriver driver;
        private IWebElement dialog;
        private IWebElement endTourButton => this.dialog.FindElement(By.CssSelector(".popover-navigation a[data-role=\"end\"]"));
        public Popup(By selector, int timeout = 5) {
            this.driver = Driver.Instance;
            this.dialog = this.driver.FindElement(selector, timeout, false);

        }
        public static void  CloseIfFound(int timeout) {
            new Popup(By.CssSelector("#step-0"), timeout).endTourButton.Click();
            DriverExtensions.WaitForSeconds(1.5);
        }
    }
}
