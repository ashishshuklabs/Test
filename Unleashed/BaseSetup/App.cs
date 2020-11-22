using Unleashed.Pages;

namespace Unleashed.BaseSetup {
    public class App {
        public Login LoginPage => new Login();
        public AddProduct AddProductPage => new AddProduct();
        public Dashboard Dashboard => new Dashboard();
        public AddSalesOrder AddSalesOrderPage => new AddSalesOrder();
        public StockOnHoldEnquiry StockOnHoldEnquiryPage => new StockOnHoldEnquiry();
        public void Navigate() {
            Driver.Instance.Navigate().GoToUrl("https://au.unleashedsoftware.com/v2/Account/LogOn");
        }
    }
}
