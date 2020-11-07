using GES_APP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using GES_APP.ViewModels;

namespace GES_APP.Controllers
{
    public class TeachingTopicsController : Controller
    {
        private ApplicationDbContext _context;

        public TeachingTopicsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: TeachingTopics
        public ActionResult Index()
        {
            if (User.IsInRole("TrainingStaff"))
            {
                var teachingtopics = _context.TeachingTopics.Include(t => t.Topic).Include(t => t.Lecturer).ToList();
                return View(teachingtopics);
            }
            if (User.IsInRole("Lecturer"))
            {
                var lecturerId = User.Identity.GetUserId();
                var Res = _context.TeachingTopics.Where(e => e.LecturerID == lecturerId).Include(t => t.Topic).ToList();
                return View(Res);
            }
            return View("Login");
        }
        public ActionResult Create()
        {
            //get lecturer
            var role = (from r in _context.Roles where r.Name.Contains("Lecturer") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            //get topic
            var topics = _context.Topics.ToList();

            var TeachingTopicVM = new TeachingTopicViewModel()
            {
                Topics = topics,
                Lecturers = users,
                TeachingTopic = new TeachingTopic()
            };

            return View(TeachingTopicVM);
        }

        [HttpPost]
        public ActionResult Create(TeachingTopicViewModel model)
        {
            //get lecturer
            var role = (from r in _context.Roles where r.Name.Contains("Lecturer") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            //get topic
            var topics = _context.Topics.ToList();


            if (ModelState.IsValid)
            {
                _context.TeachingTopics.Add(model.TeachingTopic);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            var TeachingTopicVM = new TeachingTopicViewModel()
            {
                Topics = topics,
                Lecturers = users,
                TeachingTopic = new TeachingTopic()
            };

            return View(TeachingTopicVM);
        }
        [HttpGet]
        [Authorize(Roles = "TrainingStaff")]
        public ActionResult Edit(int id)
        {
            var ttInDb = _context.TeachingTopics.SingleOrDefault(p => p.Id == id);
            if (ttInDb == null)
            {
                return HttpNotFound();
            }
            var viewModel = new TeachingTopicViewModel
            {
                TeachingTopic = ttInDb,
                Topics = _context.Topics.ToList(),

            };

            return View(viewModel);
        }
        [HttpPost]
        [Authorize(Roles = "TrainingStaff")]
        public ActionResult Edit(TeachingTopic teachingTopic)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            var ttInDb = _context.TeachingTopics.SingleOrDefault(p => p.Id == teachingTopic.Id);
            if (ttInDb == null)
            {
                return HttpNotFound();
            }
            ttInDb.TopicId = teachingTopic.TopicId;

            _context.SaveChanges();

            return RedirectToAction("Index", "TeachingTopics");
        }

        [Authorize(Roles = "TrainingStaff")]
        public ActionResult Delete(int id)
        {
            var teachingtopicInDb = _context.TeachingTopics.SingleOrDefault(p => p.Id == id);

            if (teachingtopicInDb == null)
            {
                return HttpNotFound();
            }
            _context.TeachingTopics.Remove(teachingtopicInDb);
            _context.SaveChanges();

            return RedirectToAction("Index", "TeachingTopics");

        }
    }
}