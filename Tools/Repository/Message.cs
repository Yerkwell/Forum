using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Forum.Tools;

namespace Forum.Repo
{
    public partial class Repository
    {
        public IQueryable<Message> Messages
        {
            get
            {
                return context.Messages;
            }
        }
        public Message Message(int id)
        {
            return context.Messages.FirstOrDefault(m => m.ID == id);
        }
        public int CreateMessage(Message instance)
        {
            instance.CreatingDate = DateTime.Now;
            instance.Author = CurrentUser;
            instance.NuminTopic = instance.Topic.Messages != null ? instance.Topic.Messages.Count() + 1 : 1;
            instance.IsDeleted = false;
            context.Messages.Add(instance);

            instance.Author.MessageCount++;
            context.SaveChanges();
            CreateLog(new Log() { Type = ActionType.Create, Description = "Создание сообщения", Obj = instance });
            return instance.ID;
        }
        public bool UpdateMessage(Message instance)
        {
            Message cache = context.Messages.FirstOrDefault(m => m.ID == instance.ID);
            if (cache!=null)
            {
                cache.NuminTopic = instance.NuminTopic;
                cache.Text = instance.Text;
                cache.Topic = instance.Topic;
                context.SaveChanges();
                CreateLog(new Log() { Type = ActionType.Update, Description = "Обновление сообщения", Obj = cache });
                return true;
            }
            return false;
        }
        public bool RemoveMessage(int id)
        {
            Message rubbish = context.Messages.FirstOrDefault(m => m.ID == id);
            if (rubbish != null)
            {
                rubbish.IsDeleted = true;
                context.SaveChanges();
                CreateLog(new Log() { Type = ActionType.Remove, Description = "Удаление сообщения", Obj = rubbish });
                return true;
            }
            return false;
        }
    }
}