using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum
{
    public class User
    {
        public int ID { get; set; }
        public String Login { get; set; }
        public String Name { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastVisit { get; set; }
        public int MessageCount { get; set; }
    }
}