using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Forum.Tools;

namespace Forum
{
    public class Log
    {
        public int ID { get; set; }
        public DateTime Time { get; set; }
        public String IP { get; set; }
        public User User { get; set; }
        public String Description { get; set; }
        public ActionType Type { get; set; }
        public Object Obj { get; set; }
    }
}