﻿using GES_APP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using GES_APP.ViewModels;
using System.Data.Entity.Migrations;

namespace GES_APP.Controllers
{
    public class ManagerStaffViewModelsController : Controller
    {
        ApplicationDbContext _context;
        public ManagerStaffViewModelsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: ManagerStaffViewModelsController
        public ActionResult Index()
        {
            var role = (from r in _context.Roles where r.Name.Contains("Student") select r).FirstOrDefault();
            var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role.Id)).ToList();

            var userVM = users.Select(user => new ManagerStaffViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                RoleName = "Student",
                UserId = user.Id
            }).ToList();


            var role2 = (from r in _context.Roles where r.Name.Contains("Lecturer") select r).FirstOrDefault();
            var admins = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(role2.Id)).ToList();

            var adminVM = admins.Select(user => new ManagerStaffViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                RoleName = "Lecturer",
                UserId = user.Id
            }).ToList();


            var model = new ManagerStaffViewModel { Student = userVM, Lecturer = adminVM };
            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "TrainingStaff")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var appUser = _context.Users.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser);
        }

        [HttpPost]
        [Authorize(Roles = "TrainingStaff")]
        public ActionResult Edit(ApplicationUser user)
        {
            var userInDb = _context.Users.Find(user.Id);

            if (userInDb == null)
            {
                return View(user);
            }

            if (ModelState.IsValid)
            {
                userInDb.Name = user.Name;
                userInDb.UserName = user.UserName;
                userInDb.Phone = user.Phone;
                userInDb.Email = user.Email;


                _context.Users.AddOrUpdate(userInDb);
                _context.SaveChanges();

                return RedirectToAction("Index", "ManagerStaffViewModels");
            }
            return View(user);

        }

        [Authorize(Roles = "TrainingStaff")]
        public ActionResult Delete(string id)
        {
            var userInDb = _context.Users.SingleOrDefault(p => p.Id == id);

            if (userInDb == null)
            {
                return HttpNotFound();
            }
            _context.Users.Remove(userInDb);
            _context.SaveChanges();

            return RedirectToAction("Index", "ManagerStaffViewModels");

        }
    }
}