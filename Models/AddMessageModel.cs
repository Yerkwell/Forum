using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public class AddMessageModel
    {
        public int TopicID { get; set; }
        public String Text { get; set; }
    }
}