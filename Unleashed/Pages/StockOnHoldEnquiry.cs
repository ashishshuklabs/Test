using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using Unleashed.BaseSetup;
using Unleashed.Utilities;

namespace Unleashed.Pages {
    public class StockOnHoldEnquiry {
        private IWebDriver driver;

        public StockOnHoldEnquiry() {
            this.driver = Driver.Instance;
            Driver.WaitUntil(() => this.driver.Title.Contains("Stock on hold Enquiry", StringComparison.OrdinalIgnoreCase), 10);
        }
        private IWebElement productCode => this.driver.FindElement(By.CssSelector("#ProductCode"), 10);
        private IWebElement runButton => this.driver.FindElement(By.CssSelector("#btnRun"), 10);
      
        private IWebElement availableQty => this.driver.FindElement(By.XPath("//*[@id=\"SOHList_DXDataRow0\"]/td[9]"), 10);
        public StockOnHoldEnquiry SelectProduct(string value) {
            Assert.True(this.productCode.Enabled,"Product code not enabled");
            this.productCode.SendKeys(value);
            Helper.SelectFromDropDown("#ui-id-1", value, true);
            return this;
        }
        public StockOnHoldEnquiry ClickRun() {
            this.runButton.WaitForClickability(10);
            this.runButton.Click();
            return this;
        }
        public void ValidateAvailableQuantity(string expectedValue) {
            this.driver.FindElement(By.CssSelector("table tbody #SOHList_DXDataRow0"), 10);
            Assert.AreEqual(expectedValue, availableQty.Text, "Expected and Actualy quantities don't match");
        }
    }
}
