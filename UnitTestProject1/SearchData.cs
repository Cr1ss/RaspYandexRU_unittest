using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    public class SearchData
    {
        public string title;
        public string searchDate;
        public TimeSpan searchTime;
        public string fromNameGenitive;
        public string fromName;
        public string toName;
        public string toNameAccusative;
        public int maxPrice;
        public SearchData()
        {
            this.fromName = "Екатеринбург";
            this.fromNameGenitive = "Екатеринбурга";
            this.toName = "Каменск-Уральский";
            this.toNameAccusative = "Каменск-Уральский";
            this.searchDate = "Вторник";
            this.searchTime = new TimeSpan(12, 0, 0);
            this.maxPrice = 200;
            this.title = "Расписание транспорта и билеты на поезд, электричку и автобус из " + this.fromNameGenitive + " в " + this.toNameAccusative;
        }
    }
}
