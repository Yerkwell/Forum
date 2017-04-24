using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Repo
{
    public partial class Repository
    {
        public IQueryable<Log> Logs
        {
            get
            {
                return context.Logs;
            }
        }
        public Log Log(int id)
        {
            return context.Logs.FirstOrDefault(l => l.ID == id);
        }
        public int CreateLog(Log instance)
        {
            instance.Time = DateTime.Now;
            instance.User = CurrentUser;
            instance.IP = HttpContext.Current.Request.UserHostAddress;
            context.Logs.Add(instance);
            context.SaveChanges();
            return instance.ID;
        }
        public bool RemoveLog(int id)
        {
            Log rubbish = Log(id);
            if (rubbish != null)
            {
                context.Logs.Remove(rubbish);
                return true;
            }
            return false;
        }
    }
}