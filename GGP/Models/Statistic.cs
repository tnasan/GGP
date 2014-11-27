using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GGP.Models
{
    public class Statistic
    {
        public string GroupName { get; set; }
        public IEnumerable<StatisticItem> ReportItems { get; set; }
    }
}