using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using Unleashed.BaseSetup;

namespace Unleashed.Pages {
    public class Dialog {
        private IWebDriver driver;
        private IWebElement dialog;
        private IWebElement yesButton => this.dialog.FindElement(By.CssSelector("#generic-confirm-modal-yes"));
        public Dialog() {
            this.driver = Driver.Instance;
            this.dialog = this.driver.FindElement(By.CssSelector("#generic-confirm-dialog"));
        }

        public static void ClickYes() {
            new Dialog().yesButton.Click();
        }
    }
}
