using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        FirefoxDriver firefox;

        SearchData sd = new SearchData();
        ResultData rd = new ResultData();

        [TestMethod]
        public void TestMethod1()
        {

            firefox = new FirefoxDriver();
            firefox.Navigate().GoToUrl("http://yandex.ru/");
            firefox.Navigate().GoToUrl("https://rasp.yandex.ru/");
            firefox.FindElement(By.Name("fromName")).Clear();
            firefox.FindElement(By.Name("fromName")).SendKeys(sd.fromName);
            firefox.FindElement(By.Name("toName")).SendKeys(sd.toName);
            firefox.FindElement(By.ClassName("date-input_search__input")).Clear();
            firefox.FindElement(By.ClassName("date-input_search__input")).SendKeys(sd.searchDate);
            firefox.FindElement(By.Name("toName")).SendKeys(Keys.Enter);

            WebDriverWait wait = new WebDriverWait(firefox, new TimeSpan(0, 0, 5));
            var title = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("SearchTitle")));
                if (!Check(title.Text))
                    return;
  //121211
  
            ResultData rd = new ResultData();
            foreach (FirefoxWebElement t in firefox.FindElements(By.ClassName("SearchSegment")))
            {
                try
                {
                    rd = ResultData.Parse(t.Text);
                    
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                if (sd.searchTime <= rd.fromTime && rd.price <= sd.maxPrice)
                {
                    Console.WriteLine("Время отправления - {0} \nСтоимость проезда в долларах - {1} \nСтоимость проезда в долларах - {2}", rd.fromTime, rd.price, rd.priceUSD.ToString("N3"));
                    t.FindElementByTagName("Header").FindElement(By.TagName("a")).Click();
                    var r = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("b-page-title__title"))).Text.Replace(", "," ");
                    if (!r.Contains(rd.title))
                    {
                        Console.WriteLine("Заголовок таблицы не совпадает");
                    }
                    var q = firefox.FindElement(By.ClassName("b-timetable__row_position_last"));
                    if(!q.FindElement(By.ClassName("b-timetable__cell_position_last")).Text.Contains(rd.arrivalTime))
                    {
                        Console.WriteLine("Время в пути не совпадает");
                        //Console.WriteLine(q.FindElement(By.ClassName("b-timetable__cell_position_last")).Text);
                        //Console.WriteLine(rd.arrivalTime);
                    }
                    if (!q.FindElement(By.ClassName("b-timetable__time")).Text.Contains(rd.toTime.Hours.ToString() + ':' + rd.toTime.Minutes.ToString()))
                    {
                        Console.WriteLine("Время прибытия не совпадает");
                        //Console.WriteLine(q.FindElement(By.ClassName("b-timetable__time")).Text);
                        //Console.WriteLine(rd.toTime.Hours.ToString() + ':' + rd.toTime.Minutes.ToString());
                    }
                    if (!q.FindElement(By.ClassName("b-timetable__city")).Text.Contains(rd.toName))
                    {
                        Console.WriteLine("Место прибытия не совпадает");
                    }
                    q = firefox.FindElement(By.ClassName("b-timetable__row_type_start"));
                    if (!q.FindElement(By.ClassName("b-timetable__city")).Text.Contains(rd.fromName))
                    {
                        Console.WriteLine("Место отправления не совпадает");
                    }
                    if (!q.FindElement(By.ClassName("b-timetable__cell_type_departure")).Text.Contains(rd.fromTime.Hours.ToString() + ':' + rd.fromTime.Minutes.ToString()))
                    {
                        Console.WriteLine("Время отправления не совпадает");
                        //Console.WriteLine(q.FindElement(By.ClassName("b-timetable__cell_type_departure")).Text);
                        //Console.WriteLine(rd.fromTime.Hours.ToString() + ':' + rd.fromTime.Minutes.ToString());
                    }
                    break;
                }
            }



            if (rd.price == 0)
                Console.WriteLine("Нет доступных рейсов");



        }
      

        private bool Check(string text)
        {
            if (text.Contains(sd.title))
                return true;
            else return false;
        }

        [TestCleanup]
        public void TearDown()
        {
            //firefox.Quit();
        }
    }
}
