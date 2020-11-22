using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics;
using Unleashed.Utilities;

namespace Unleashed.BaseSetup {
    public class Driver {
        [ThreadStatic] private static IWebDriver driver = new ChromeDriver();
        public static IWebDriver Instance {
            get {
                if(driver == null) {
                    driver = new ChromeDriver(TestContext.CurrentContext.TestDirectory);
                }
                driver.Manage().Window.Maximize();
                return driver;
            }
        }
        public static bool WaitUntil(Func<bool> condition, int timeoutInSeconds = 10) {

            Stopwatch watch = new Stopwatch();
            watch.Restart();
            do {
                try {
                    if(condition()) {
                        return true;
                    }
                } catch(Exception) {
                    DriverExtensions.WaitForSeconds(0.5);
                } finally {
                    DriverExtensions.WaitForSeconds(0.5);

                }
            } while(watch.Elapsed.Seconds < timeoutInSeconds);
            return false;
        }
        public static void Dispose() {
            try {
                if(driver != null) {
                    driver.Quit();
                    //driver.Dispose();

                }
            } catch(Exception e) {
                throw new Exception("Cannot quit driver!!", e);
            }
        }
    }
}
