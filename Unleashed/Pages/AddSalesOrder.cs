using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using Unleashed.BaseSetup;
using Unleashed.Utilities;

namespace Unleashed.Pages {
    public class AddSalesOrder {
        private IWebDriver driver;

        public AddSalesOrder() {
            this.driver = Driver.Instance;
            Driver.WaitUntil(() => this.driver.FindElement(By.CssSelector("input#SelectedCustomerCode")) != null, 10);
        }
        private IWebElement productCodeInput => this.driver.FindElement(By.CssSelector("#Product_ProductCode"), 10);
        private IWebElement saveButton => this.driver.FindElement(By.CssSelector("#btnSave"), 10);
        //MATSUP- client code
        //Product Code: GLUE| List-->ul#ui-id-2 | input field-->#ProductAddLine
        private IWebElement customerCodeInput => this.driver.FindElement(By.CssSelector("input#SelectedCustomerCode"));
        private IWebElement addButton => this.driver.FindElement(By.CssSelector("#btnAddOrderLine"), 10);
        private IWebElement productCode => this.driver.FindElement(By.CssSelector("#ProductAddLine"), 10);

        public Menu Menu => new Menu();
        public AddSalesOrder SelectProductCode(string value) {
            Assert.True(productCode.Enabled,"Product Code field is not enabled.");
            productCode.SendKeys(value);
            Helper.SelectFromDropDown("ul#ui-id-2", value);
            return this;
        }

        public AddSalesOrder SelectCustomerCode(string value) {
            Assert.True(customerCodeInput.Enabled,"Customer Code field is not enabled.");
            customerCodeInput.SendKeys(value);
            Helper.SelectFromDropDown("ul#ui-id-4", value);
            Driver.WaitUntil(() => driver.FindElement(By.CssSelector("#btnComplete")) != null, 10);
            return this;
        }
        private string batchNumber;
        public AddSalesOrder UpdateBatchNumberAndSave() {
            var saveBtn = modal.FindElements(By.CssSelector("#saveButtonDiv"), 5)[0];
            //Update the number first

            saveBtn.Click();
            DriverExtensions.WaitForSeconds(1);
            return this;
        }

        public IWebElement quantityInput => this.driver.FindElement(By.CssSelector("#QtyAddLine"), 10);
        private IWebElement completeButton => this.driver.FindElement(By.CssSelector("#btnComplete"), 10);
        public AddSalesOrder EnterQuantity(string value) {
            batchNumber = value;
            Assert.True(quantityInput.Enabled, $"Quantity is not enabled");
            quantityInput.SendKeys(value);
            return this;
        }

        public void ClickComplete() {
            Assert.True(completeButton.Enabled, $"Complete is not enabled");
            completeButton.Click();
            Dialog.ClickYes();
        }
        
        public AddSalesOrder EnterProductCode(string productCode) {
            Assert.That(productCodeInput.Enabled, $"{nameof(productCodeInput)} not enabled.");
            productCodeInput.SendKeys(productCode);
            //Wait until complete button found.
            return this;
        }
        //Add line
        //#MinSaleQty table#addLineTable td a#btnAddOrderLine
        public AddSalesOrder ClickAdd() {
            Assert.That(addButton.Enabled, $"{nameof(addButton)} not enabled.");
            addButton.Click();
            this.driver.FindElement(By.CssSelector("table tbody #SalesOrderLinesList_DXDataRow0"),10);
            return this;
        }
        private IWebElement InterestingRow => this.driver.FindElement(By.CssSelector("#SalesOrderLinesList_DXMainTable #SalesOrderLinesList_DXDataRow0"), 10);
        private IWebElement batchLink => InterestingRow.FindElement(By.CssSelector("a.batch-expander"));
        private IWebElement modal => this.driver.FindElement(By.CssSelector("div#simplemodal-container"), 10);
        public AddSalesOrder ClickUpdateBatchLink() {
            batchLink.Click();
            //SalesOrderLinesList_DXMainTable
            //Row 1 --> SalesOrderLinesList_DXDataRow0
            return this;
        }
        public AddSalesOrder ClickSave() {
            Assert.That(saveButton.Enabled, $"{nameof(saveButton)} not enabled.");
            saveButton.Click();
            return this;
        }
    }
}
