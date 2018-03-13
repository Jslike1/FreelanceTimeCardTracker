using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreelanceTimeCardTracker.Models
{
    public class TimeCard
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Project { get; set; }
        public DateTime? Start_DateTime { get; set; }
        public DateTime? End_DateTime { get; set; }
        public string Notes { get; set; }


        public TimeCard()
        {
            Project = "";
            Notes = "";
        }

    }
}