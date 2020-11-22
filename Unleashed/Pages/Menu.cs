using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using Unleashed.BaseSetup;
using Unleashed.Utilities;

namespace Unleashed.Pages {
    public class Menu {
        private IWebDriver driver;

        public Menu() {
            this.driver = Driver.Instance;
        }
        private IWebElement menuIcon => this.driver.FindElement(By.CssSelector("#shortcuts .shortcut-menu-icon .fa.fa-th"), 10);
        private List<IWebElement> allMenuItems => this.driver.FindElements(By.CssSelector(".shortcut-menu.show div[ng-repeat=\"item in vm.quickLinksMenu\"]"), 10);
        private IWebElement addProductItem => allMenuItems.Find(e => e.Text.Contains("Add Product")).FindElement(By.CssSelector("a.shortcut-item"));
        private IWebElement addSalesOrderItem => allMenuItems.Find(e => e.Text.Contains("Add Sales Order")).FindElement(By.CssSelector("a.shortcut-item"));
        
        private List<IWebElement> quickListItems => this.driver.FindElements(By.CssSelector("div[ng-repeat=\"item in vm.quickLinksMenu2\"]"), 10);
        private IWebElement stockOnHandInquiryQuickMenuItem => quickListItems.Find(e => e.Text.Contains("stock on hand enquiry", StringComparison.OrdinalIgnoreCase)).FindElement(By.CssSelector("a.shortcut-item"));

        public Menu ClickStockOnHandEnquiry() {
            this.stockOnHandInquiryQuickMenuItem.WaitForClickability(10);
            this.stockOnHandInquiryQuickMenuItem.Click();
            return this;
        }
        public Menu ClickSalesOrder() {
            this.addSalesOrderItem.WaitForClickability(10);
            Assert.That(this.addSalesOrderItem.Enabled, "Add Sales Order Item is not enabled");
            this.addSalesOrderItem.Click();
            return this;
        }
        public Menu ClickAddProduct() {
            this.addProductItem.WaitForClickability(10);
            Assert.That(this.addProductItem.Enabled, "Add Product Item is not enabled");
            this.addProductItem.Click();
            return this;
        }

        public Menu Open() {
            this.menuIcon.WaitForClickability(10);
            Assert.That(this.menuIcon.Enabled, "Menu not enabled");
            this.menuIcon.Click();

            return this;
        }
    }
}
