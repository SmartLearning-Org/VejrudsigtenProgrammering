using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VejrudsigtenOO
{
    public class Weather
    {
        public string Description { get; set; }
        public string Type { get; set; }

        public DayData[] Future { get; set; }
    }

    public class DayData
    {
        public double Temperature { get; set; }
        public string Type { get; set; }
    }
}
