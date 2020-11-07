﻿using GES_APP.Models;
using GES_APP.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GES_APP.Controllers
{
    public class ManageUsersController : Controller
    {
        private ApplicationDbContext _context;
        public ManageUsersController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: ManageUsers
        public ActionResult UsersWithRoles()
        {
            var usersWithRoles = (from user in _context.Users
                                  select new
                                  {
                                      UserId = user.Id,
                                      Name = user.Name,
                                      Username = user.UserName,
                                      Emailaddress = user.Email,
                                      Password = user.PasswordHash,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in _context.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new Users_In_Role()

                                  {
                                      UserId = p.UserId,
                                      Name = p.Name,
                                      Username = p.Username,
                                      Email = p.Emailaddress,
                                      Role = string.Join(",", p.RoleNames)
                                  });


            return View(usersWithRoles);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

                return RedirectToAction("UsersWithRoles");
            }
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            var userInDb = _context.Users.SingleOrDefault(p => p.Id == id);

            if (userInDb == null)
            {
                return HttpNotFound();
            }
            _context.Users.Remove(userInDb);
            _context.SaveChanges();

            return RedirectToAction("UsersWithRoles");

        }
        [Authorize(Roles = "Admin")]
        public ActionResult ResetPass(ApplicationUser user)
        {
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            userId = user.Id;
            if (userId != null)
            {
                UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
                userManager.RemovePassword(userId);
                String newPassword = "123456";
                userManager.AddPassword(userId, newPassword);
            }
            _context.SaveChanges();
            return RedirectToAction("UsersWithRoles", "ManageUsers");
        }
    }
}
