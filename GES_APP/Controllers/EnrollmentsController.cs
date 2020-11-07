using GES_APP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using GES_APP.ViewModels;

namespace GES_APP.Controllers
{
    public class EnrollmentsController : Controller
    {
        private ApplicationDbContext _context;

        public EnrollmentsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Enrollment
        public ActionResult Index()
        {
            if (User.IsInRole("TrainingStaff"))
            {
                var enrollments = _context.Enrollments.Include(t => t.Course).Include(t => t.Student).ToList();
                return View(enrollments);
            }
            if (User.IsInRole("Student"))
            {
                var studentID = User.Identity.GetUserId();
                var Res = _context.Enrollments.Where(e => e.StudentID == studentID).Include(t => t.Course).ToList();
                return View(Res);
            }
            return View("Login");
        }
        public ActionResult Create()
        {
            //get trainer
            var role = (from r in _context.Roles where r.Name.Contains("Student") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            //get topic

            var courses = _context.Courses.ToList();

            var EnrollmentVM = new EnrollmentViewModel()
            {
                Courses = courses,
                Students = users,
                Enrollment = new Enrollment()
            };

            return View(EnrollmentVM);
        }

        [HttpPost]
        public ActionResult Create(EnrollmentViewModel model)
        {
            //get trainer
            var role = (from r in _context.Roles where r.Name.Contains("Student") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            //get topic

            var courses = _context.Courses.ToList();


            if (ModelState.IsValid)
            {
                _context.Enrollments.Add(model.Enrollment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            var EnrollmentVM = new EnrollmentViewModel()
            {
                Courses = courses,
                Students = users,
                Enrollment = new Enrollment()
            };

            return View(EnrollmentVM);
        }
        [HttpGet]
        [Authorize(Roles = "TrainingStaff")]
        public ActionResult Edit(int id)
        {
            var EInDb = _context.Enrollments.SingleOrDefault(p => p.Id == id);
            if (EInDb == null)
            {
                return HttpNotFound();
            }
            var viewModel = new EnrollmentViewModel
            {
                Enrollment = EInDb,
                Courses = _context.Courses.ToList(),

            };

            return View(viewModel);
        }
        [HttpPost]
        [Authorize(Roles = "TrainingStaff")]
        public ActionResult Edit(Enrollment enrollment)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            var EInDb = _context.Enrollments.SingleOrDefault(p => p.Id == enrollment.Id);
            if (EInDb == null)
            {
                return HttpNotFound();
            }
            EInDb.CourseId = enrollment.CourseId;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "TrainingStaff")]
        public ActionResult Delete(int id)
        {
            var enrollmentInDb = _context.Enrollments.SingleOrDefault(p => p.Id == id);

            if (enrollmentInDb == null)
            {
                return HttpNotFound();
            }
            _context.Enrollments.Remove(enrollmentInDb);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }
    }
}