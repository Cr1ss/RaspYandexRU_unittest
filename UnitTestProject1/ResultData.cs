using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    public class ResultData
    {
        public string title;
        public TimeSpan fromTime;
        public TimeSpan toTime;
        public string arrivalTime;
        public string fromName;
        public string toName;
        public int price;
        public double priceUSD;
        public string company;
        public double rate;

        public ResultData()
        {
            this.price = 0;
            this.priceUSD = 0;
            this.toName = "";
            this.fromName = "";
            this.title = "";
            this.fromTime = new TimeSpan();
            this.toTime = new TimeSpan();
            this.arrivalTime = "";
            this.company = "";
            this.rate = 56.8892934;
        }

        internal double ConvertToUSD(int priceRUB)
        {
            return priceRUB / this.rate;
        }

        public static ResultData Parse(String t)
        {
            ResultData rd = new ResultData();
            int i = 0;
            string[] arr = t.Split(System.Environment.NewLine.ToCharArray());
            rd.title = arr[i];
            i += 2;
            rd.company = arr[i];
            i += 2;
            while (1 == 1)
            {
                try
                {
                    rd.fromTime = TimeSpan.ParseExact(arr[i], "hh':'mm", null);
                    break;
                }
                catch (Exception e)
                {
                    i += 2;
                }

            }
            i += 2;
            rd.arrivalTime = arr[i];
            i += 2;
            while (1 == 1)
            {
                try
                {
                    rd.toTime = TimeSpan.ParseExact(arr[i], "hh':'mm", null);
                    break;
                }
                catch (Exception e)
                {
                    i += 2;
                }

            }
            i += 4;
            while (arr[i].Substring(arr[i].Length - 2, 2) != " Р")
            {
                i += 2;
            }
            rd.toName = arr[i - 2];
            rd.fromName = arr[i - 4];
            int j;
            for (j = arr[i].Length - 3; Char.IsDigit(arr[i][j]) && j > 0; j--)
            {
                rd.price = rd.price * 10 + int.Parse(arr[i][j].ToString());
            }
            if (j == 0 && Char.IsDigit(arr[i][j]))
                rd.price = rd.price * 10 + int.Parse(arr[i][j].ToString());
            rd.price = Reverse(rd.price);
            rd.priceUSD = rd.ConvertToUSD(rd.price);
            return rd;
        }

        public static int Reverse(int price)
        {
            int q = price;
            int res = 0;
            while (q > 0)
            {
                res = res * 10 + q % 10;
                q /= 10;
            }
            return res;
        }

    }
}
