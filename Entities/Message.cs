using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum
{
    public class Message
    {
        public int ID {get; set;}
        public int NuminTopic { get; set; }
        public DateTime CreatingDate {get; set;}
        public String Text {get; set;}
        public bool IsDeleted { get; set; }
        public virtual Topic Topic {get; set;}
        public virtual User Author { get; set; }
    }
}