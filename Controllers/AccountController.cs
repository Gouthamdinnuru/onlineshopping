
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System.Data.Entity.Validation;
using CaseStudy.DAL;

namespace CaseStudy.Models
{
    public class AccountController : Controller
    {
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User_tbl model)
        {
            if (ModelState.IsValid)
            {
                using (var context = new DBContext())
                {
                    User_tbl user = context.User_Tbls
                    .Where(u => u.UserName == model.UserName && u.Password == model.Password)
                    .FirstOrDefault();

                    if (user != null)
                    {
                        Session["UserName"] = user.UserName;
                        Session["UserID"] = user.UserID;
                        //return RedirectToAction("Register_Aadhar", "Aadhar_Validation");
                        //return RedirectToAction("Login_Aadhar", "Aadhar_Validation");
                        return RedirectToAction("Index", "Home");
                        //return RedirectToAction("Index", "Emp_tbl");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid User Name or Password");
                        return View(model);
                    }
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User_tbl user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var context = new DBContext())
                    {
                        User_tbl user1 = context.User_Tbls
                        .Where(u => u.UserName == user.UserName && u.Password == user.Password)
                        .FirstOrDefault();

                        if (user1 != null)
                        {
                            Session["UserName"] = user.UserName;
                            //return RedirectToAction("Register_Aadhar", "Aadhar_Validation");
                            ModelState.AddModelError("", "User Already Exists");
                            return View(user);
                            //return RedirectToAction("Register", "Account");
                            //return RedirectToAction("Index", "Emp_tbl");
                        }

                        else
                        {

                            using (var db = new DBContext())
                            {
                                //var crypto = new SimpleCrypto.PBKDF2();
                                //var encrypPass = crypto.Compute(user.Password);
                                var newUser = db.User_Tbls.Create();
                                newUser.UserID = user.UserID;
                                newUser.UserName = user.UserName;
                                //newUser.PasswordSalt = crypto.Salt;
                                newUser.Password = user.Password;
                                newUser.ConfirmPassword = user.ConfirmPassword;
                                newUser.EmailAddress = user.EmailAddress;
                                newUser.PhoneNumber = user.PhoneNumber;
                                newUser.PhoneNumber = user.PhoneNumber;
                                newUser.Gender = user.Gender;
                                newUser.Address = user.Address;
                                newUser.RoleId = user.RoleId;
                                db.User_Tbls.Add(newUser);
                                db.SaveChanges();
                                return RedirectToAction("Index", "Home");
                                //return RedirectToAction("Index", "Emp_tbl");
                            }
                        }
                    }
                }

            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                        ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session["UserName"] = string.Empty;
            Session["UserID"] = string.Empty;
            return RedirectToAction("Index", "Home");
        }
        private bool IsValid(string username, string password)
        {
            //var crypto = new SimpleCrypto.PBKDF2();
            bool IsValid = false;

            using (var db = new DBContext())
            {
                var user = db.User_Tbls.FirstOrDefault(u => u.UserName == username);
                if (user != null)
                {
                    if (user.Password == password)
                    {
                        IsValid = true;
                    }
                }
            }
            return IsValid;
        }
    }
}