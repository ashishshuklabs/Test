using NUnit.Framework;
using OpenQA.Selenium;
using Unleashed.BaseSetup;
using Unleashed.Utilities;

namespace Unleashed.Pages {
    public class Login {
        private IWebDriver driver;

        public Login() {

            this.driver = Driver.Instance;
            Driver.WaitUntil(() => this.driver.Title.Contains("Log In"), 10);
        }
        public IWebElement emailInput => this.driver.FindElement(By.CssSelector("#username"), 10);
        public IWebElement passwordInput => this.driver.FindElement(By.CssSelector("#password"), 10);
        public IWebElement loginButton => this.driver.FindElement(By.CssSelector("#btnLogOn"), 10);

        public Login EnterEmail(string email) {
            Assert.True(emailInput.Enabled,$"{nameof(emailInput)} is not enabled");
            emailInput.SendKeys(email);
            return this;
        }
        public Login EnterPassword(string password) {
            Assert.True(passwordInput.Enabled, $"{nameof(passwordInput)} is not enabled");
            passwordInput.SendKeys(password);
            return this;
        }
        public Login ClickLogin() {
            Assert.True(loginButton.Enabled, $"{nameof(loginButton)} is not enabled");
            loginButton.Click();
            return this;
        }
    }
}
