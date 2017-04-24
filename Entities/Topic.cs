using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum
{
    public class Topic
    {
        public int ID { get; set; }
        public DateTime CreationDate { get; set; }
        public String Name { get; set; }
        public bool IsDeleted { get; set; }
        public virtual User TopicStarter { get; set; }
        public String Roles { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
        
    }
}