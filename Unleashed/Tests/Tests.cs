using NUnit.Framework;
using RestSharp;
using Unleashed.BaseSetup;
using Unleashed.Utilities;

namespace Unleashed.Tests {
    [TestFixture]
    public class Tests {
        protected App UnleashedApp {get; private set;}
        [SetUp]
        public void Setup() {
            UnleashedApp = new App();

        }
        [TearDown]
        public void Teardown() {
            Driver.Dispose();
        }

        [Test]
        [Category("Unleashed")]
        public void AddNewProduct() {
            UnleashedApp.Navigate();
            UnleashedApp.LoginPage
                .EnterEmail("qa+Shukla@unl.sh")
                .EnterPassword("Unleashed123")
                .ClickLogin();
            UnleashedApp.Dashboard.Menu
                .Open()
                .ClickAddProduct();
            UnleashedApp.AddProductPage
                .EnterProductCode("123")
                .EnterProductDescription("Some random prodcut")
                .ClickSave();
            DriverExtensions.WaitForSeconds(5);
        }
        [Test]
        [Category("Unleashed")]
        public void AddAndVerifySalesOrder() {
            UnleashedApp.Navigate();
            UnleashedApp.LoginPage
                .EnterEmail("qa+Shukla@unl.sh")
                .EnterPassword("Unleashed123")
                .ClickLogin();
            UnleashedApp.Dashboard.Menu
                .Open()
                .ClickSalesOrder();
            UnleashedApp.AddSalesOrderPage
                .SelectCustomerCode("MATSUP")
                .SelectProductCode("CHAIR")
                .EnterQuantity("1")
                .ClickAdd()
                .ClickComplete();
            UnleashedApp.AddSalesOrderPage.Menu
                .Open()
                .ClickStockOnHandEnquiry();
            UnleashedApp.StockOnHoldEnquiryPage
                .SelectProduct("CHAIR")
                .ClickRun()
                .ValidateAvailableQuantity("14.00");
            DriverExtensions.WaitForSeconds(5);
        }
        
    }
}
