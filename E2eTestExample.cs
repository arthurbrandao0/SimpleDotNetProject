using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using Xunit;


namespace TesteSelenium.Tests
{
    public class SumFormTest : IDisposable
    {
        private IWebDriver _driver;

        public SumFormTest()
        {          
            new DriverManager().SetUpDriver(new ChromeConfig());

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--headless");
            _driver = new ChromeDriver(chromeOptions);
        }

        [Theory]
        [InlineData("5", "10", "15")]
        [InlineData("3", "7", "10")]
        [InlineData("1", "1", "2")]
        [InlineData("13", "1", "14")]
        [InlineData("999", "1", "1000")]
        public void TestSumForm(string firstNumber, string secondNumber, string expectedResult)
        {
            _driver.Navigate().GoToUrl("https://www.lambdatest.com/selenium-playground/simple-form-demo");

            // Enter first number
            _driver.FindElement(By.Id("sum1")).SendKeys(firstNumber);

            // Enter second number
            _driver.FindElement(By.Id("sum2")).SendKeys(secondNumber);

            // Click "Get Total" button
            string button_text = "Get Sum";
            _driver.FindElement(By.XPath("//button[text()='" + button_text + "']")).Click();
            
            // Check result
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var resultElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\'addmessage\']")));
            string result = resultElement.Text;
            Assert.Equal(expectedResult, result);
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
