using NUnit.Framework;
using OpenQA.Selenium;
using Unleashed.BaseSetup;
using Unleashed.Utilities;

namespace Unleashed.Pages {
    public class AddProduct {
        private IWebDriver driver;

        public AddProduct() {
            this.driver = Driver.Instance;
            Driver.WaitUntil(() => this.driver.Title.Contains("Add Product"), 10);
        }
        private IWebElement productCodeInput => this.driver.FindElement(By.CssSelector("#Product_ProductCode"), 10);
        private IWebElement productDescriptionInput => this.driver.FindElement(By.CssSelector("#Product_ProductDescription"), 10);
        private IWebElement saveButton => this.driver.FindElement(By.CssSelector("#btnSave"), 10);

        public AddProduct EnterProductCode(string productCode) {
            Assert.That(productCodeInput.Enabled, $"{nameof(productCodeInput)} not enabled.");
            productCodeInput.SendKeys(productCode);
            return this;
        }
        public AddProduct EnterProductDescription(string productDescription) {
            Assert.That(productDescriptionInput.Enabled, $"{nameof(productDescriptionInput)} not enabled.");
            productDescriptionInput.SendKeys(productDescription);
            return this;
        }
        public AddProduct ClickSave() {
            Assert.That(saveButton.Enabled, $"{nameof(saveButton)} not enabled.");
            saveButton.Click();
            return this;
        }
    }
}
