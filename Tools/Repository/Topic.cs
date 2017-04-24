using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Repo
{
    public partial class Repository
    {
        public IQueryable<Topic> Topics
        {
            get
            {
                return context.Topics;
            }
        }
        public Topic Topic(int id)
        {
            return context.Topics.FirstOrDefault(t => t.ID == id);
        }
        public int CreateTopic(Topic instance)
        {
            instance.CreationDate = DateTime.Now;
            instance.TopicStarter = CurrentUser;
            instance.IsDeleted = false;
            context.Topics.Add(instance);
            context.SaveChanges();
            CreateLog(new Log() { Type = Tools.ActionType.Create, Obj = instance });
            return instance.ID;
        }
        public bool UpdateTopic(Topic instance)
        {
            Topic cache = context.Topics.FirstOrDefault(t => t.ID == instance.ID);
            if (cache != null)
            {
                cache.Name = instance.Name;
                context.SaveChanges();
                CreateLog(new Log() { Type = Tools.ActionType.Update, Obj = cache });
                return true;
            }
            return false;
        }
        public bool RemoveTopic(int id)
        {
            Topic rubbish = context.Topics.FirstOrDefault(t => t.ID == id);
            if (rubbish != null)
            {
                foreach (var m in rubbish.Messages)
                {
                    m.IsDeleted = true;
                }
                rubbish.IsDeleted = true;
                context.SaveChanges();
                CreateLog(new Log() { Type = Tools.ActionType.Remove, Obj = rubbish });
                return true;
            }
            return false;
        }
    }
}