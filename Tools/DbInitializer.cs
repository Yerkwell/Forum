using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Security;

using Forum.Context;

namespace Forum.Tools
{
    public class DbInitializer: DropCreateDatabaseAlways<ForumContext>
    {
        protected override void Seed(ForumContext context)
        {

            List<User> users = new List<User>();

            users.Add(new User() { Name="Рома", MessageCount = 0, RegistrationDate = DateTime.Now, LastVisit = DateTime.Now, Login="Рома" });
            users.Add(new User() { Name = "Алех", MessageCount = 3, RegistrationDate = DateTime.Now, LastVisit = DateTime.Now, Login = "Alej" });
   //         users.Add(new User() { Name = "Феря", MessageCount = 0, RegistrationDate = DateTime.Now, LastVisit = DateTime.Now, Login = "Фьорден" });
            users.ForEach(u => context.Users.Add(u));
            List<Topic> topics = new List<Topic>();

            topics.Add(new Topic() {Name = "Тестовая тема", CreationDate = DateTime.Now, TopicStarter = users[1]});
            topics.ForEach(t => context.Topics.Add(t));
            context.SaveChanges();
            context.Messages.Add(new Message() { Text = "Hey!", NuminTopic = 1, CreatingDate = DateTime.Now, Topic = topics[0], Author = users[1] });
            context.Messages.Add(new Message() { Text = "OK!", NuminTopic = 2, CreatingDate = DateTime.Now, Topic = topics[0], Author = users[1] });

            base.Seed(context);
        }
    }
}