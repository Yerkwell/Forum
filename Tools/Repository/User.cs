using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Repo
{
    public partial class Repository
    {
        public IQueryable<User> Users
        {
            get
            {
                return context.Users;
            }
        }
        public User User(int id)
        {
            return context.Users.FirstOrDefault(u => u.ID == id);
        }
        public int CreateUser(User instance)
        {
            instance.RegistrationDate = DateTime.Now;
            instance.LastVisit = DateTime.Now;
            instance.MessageCount = 0;
            context.Users.Add(instance);
            context.SaveChanges();
            return instance.ID;
        }
        public bool UpdateUser(User instance)
        {
            User cache = context.Users.FirstOrDefault(u => u.ID == instance.ID);
            if (cache != null)
            {
                cache.Name = instance.Name;
                cache.MessageCount = instance.MessageCount;
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool RemoveUser(int id)
        {
            User rubbish = User(id);
            if (rubbish != null)
            {
                context.Users.Remove(rubbish);
                return true;
            }
            return false;
        }
    }
}