using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions =  SeleniumExtras.WaitHelpers.ExpectedConditions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Unleashed.BaseSetup;

namespace Unleashed.Utilities {
    public static class DriverExtensions {

        public static IWebElement FindElement(this IWebDriver driver, By by, int timeout,
            bool throwIfNotFound = true) {
            return FindElements(driver, by, timeout, throwIfNotFound).First();

        }
        public static List<IWebElement> FindElements(this IWebDriver driver, By by, int timeout,
            bool throwIfNotFound = true) {
            if(by == null) {
                throw new ArgumentNullException($"Parameter {nameof(by)} cannot be null or empty");
            }

            IReadOnlyCollection<IWebElement> result = null;

            Stopwatch watch = new Stopwatch();
            watch.Restart();
            do {
                try {
                    result = driver.FindElements(by);
                    if(result.Count > 0) {
                        return new List<IWebElement>(result);
                    }
                    WaitForSeconds(0.5);
                } catch(Exception) {
                    WaitForSeconds(0.5);
                }
            } while(watch.Elapsed.Seconds < timeout);

            if(throwIfNotFound) {
                throw new NotFoundException($"Element(s) with selector [{by}] not found within [{timeout}] seconds.");
            }

            return null;
        }

        public static void WaitForSeconds(double seconds) {
            System.Threading.Thread.Sleep(Convert.ToInt32(seconds * 1000));
        }
        public static void WaitForClickability(this IWebElement element, int timeout = 10) {
            var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(timeout));
            wait.Until(ExpectedConditions.ElementToBeClickable(element));
        }

        public static List<IWebElement> FindElements(this IWebElement element, By by, int timeoutInSeconds = 10, bool throwIfNotFound = true) {
            if(by == null) {
                throw new ArgumentNullException($"Parameter {nameof(by)} cannot be null or empty");
            }

            IReadOnlyCollection<IWebElement> result = null;

            Stopwatch watch = new Stopwatch();
            watch.Restart();
            do {
                try {
                    result = element.FindElements(by);
                    if(result.Count > 0) {
                        return new List<IWebElement>(result);
                    }
                    WaitForSeconds(0.5);
                } catch(Exception) {
                    WaitForSeconds(0.5);
                }
            } while(watch.Elapsed.Seconds < timeoutInSeconds);

            if(throwIfNotFound) {
                throw new NotFoundException($"Element(s) with selector [{by}] not found within [{timeoutInSeconds}] seconds.");
            }

            return null;

        }
    }
}
