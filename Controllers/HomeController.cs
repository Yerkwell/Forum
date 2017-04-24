using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Forum.Repo;
using Forum.Models;

namespace Forum.Controllers
{
    public class HomeController : Controller
    {
        Repository Repository = new Repository();
        // GET: Home
        public ActionResult Index()
        {
            getUserInfo();
            String[] roles = { "Outsider" };
            if (User.Identity.IsAuthenticated)
                roles = Roles.GetRolesForUser(User.Identity.Name);
            ViewBag.Topics = Repository.Topics.Where(t => !t.IsDeleted).ToList().Where(t => roles.Any(r => t.Roles == null || t.Roles.Contains(r))).ToList();
            ViewBag.Title = "Главная";
            return View();
        }
        [HttpPost]
        public ActionResult Error(string Message)
        {
            getUserInfo();
            ViewBag.Title = "Ошибка!";
            ViewBag.Message = Message;
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult Add(int topicId)
        {
            getUserInfo();
            if (Repository.Topics.Where(t => t.ID == topicId).Count() <= 0)
            {
                ViewBag.Title = "Ошибка!";
                ViewBag.Message = "Тема не существует!";
                return View("Error");
            }
            ViewBag.TopicID = topicId;
            ViewBag.Title = "Добавление сообщения";
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult Add(AddMessageModel model)
        {
            var topic = Repository.Topic(model.TopicID);
            var m = new Message()
                {
                    Text = model.Text,
                    Topic = topic
                };
            Repository.CreateMessage(m);
            return Redirect("/Home/Topic/" + model.TopicID);
        }
        [HttpGet]
        public ActionResult Topic(int id)
        {
            getUserInfo();
            var topic = Repository.Topic(id);
            if (topic == null || !Roles.GetRolesForUser().Any(r => topic.Roles == null || topic.Roles.Contains(r)))    //Если темы нет или войти в неё низзя
                return Content("Ошибка! Тема не существует!");
            else
            {
                ViewBag.TopicID = id;
                ViewBag.Title = "Тема";
                ViewBag.Messages = Repository.Messages.Where(x => x.Topic.ID == id).ToList();
                return View();
            }
        }
        [HttpGet]
        public ActionResult UserList()
        {
            ViewBag.Title = "Список пользователей";
            getUserInfo();
            ViewBag.Users = Repository.Users.ToList();
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult AddTopic()
        {
            getUserInfo();
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult AddTopic(string name)
        {
            var topic = new Topic() { Name = name };
            Repository.CreateTopic(topic);
            return Redirect("/Home/Topic/" + topic.ID);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            getUserInfo();
            if (Roles.GetRolesForUser(User.Identity.Name).Contains("Admin") || Repository.CurrentUser == Repository.Message(id).Author)
            {
                var mes = Repository.Message(id);
                var model = new EditMessageModel()
                {
                    ID = id,
                    Text = mes.Text
                };
                ViewBag.TopicID = mes.Topic.ID;
                return View(model);
            }
            else
            {
                ViewBag.Message = "Операция невозможна";
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult Edit(EditMessageModel model)
        {
            if (ModelState.IsValid)
            {
                var message = new Message()
                {
                    Text = model.Text,
                    Topic = Repository.Message(model.ID).Topic
                };
                Repository.UpdateMessage(message);
                return Redirect("/Home/Topic/" + message.Topic.ID);
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {

            ViewBag.id = id;
            ViewBag.url = "/Home/Topic/" + Repository.Message(id).Topic.ID;
            return View();
        }
        [HttpPost]
        public ActionResult Delete(int id, string Url)
        {
            Repository.RemoveMessage(id);
            return Redirect(Url);
        }

        void getUserInfo()
        {
            var user = Repository.CurrentUser;
            if (User.Identity.IsAuthenticated && user == null)
                FormsAuthentication.SignOut();
            ViewBag.User = user;
        }
    }
}