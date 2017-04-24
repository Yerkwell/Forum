using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Forum.Context;
using Forum.Models;

namespace Forum.Repo
{
    public partial class Repository
    {
        private ForumContext context = new ForumContext();
        public User CurrentUser
        {
            get
            {
                return context.Users.FirstOrDefault(u => u.Login == HttpContext.Current.User.Identity.Name);
            }
        }
    }
}