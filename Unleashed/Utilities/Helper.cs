using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using Unleashed.BaseSetup;

namespace Unleashed.Utilities {
    public class Helper {
        public static void SelectFromDropDown(string rootSelector, string value, bool exactMatch = false) {
            IWebDriver driver = Driver.Instance;
            IWebElement root = driver.FindElement(By.CssSelector(rootSelector));
            List<IWebElement> listElements = root.FindElements(By.CssSelector("li"), 10);
            IWebElement foundElement = null;
            foreach(var element in listElements) {
                if(exactMatch) {
                    if(element.Text.Equals(value, System.StringComparison.OrdinalIgnoreCase)) {
                        foundElement = element;
                    }
                } else {
                    if(element.Text.Contains(value, System.StringComparison.OrdinalIgnoreCase)) {
                        foundElement = element;
                    }
                }
            }
            foundElement.Click();
        }
    }
}
