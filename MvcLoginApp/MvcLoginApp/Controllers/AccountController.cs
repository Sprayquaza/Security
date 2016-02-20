using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcLoginApp.Models;
using System.Security.Cryptography;

namespace MvcLoginApp.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            using (OurDbContext db = new OurDbContext())
            {
                return View(db.userAccount.ToList());
            }
            
        }
        public ActionResult Delete(int Id)
        {
            UserAccount userToDelete;
            using(var ctx = new OurDbContext())
            {
                userToDelete = ctx.userAccount.Where(u => u.UserId == Id).FirstOrDefault<UserAccount>();
            }
            using(OurDbContext db = new OurDbContext())
            {
                db.Entry(userToDelete).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return View("Index");
            }
        }
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Register(UserAccount account)
        {
            if (ModelState.IsValid)
            {
                byte[] salt = GenerateSalt();
                string hash = HashPassword(account.Password, salt);
                using(OurDbContext db = new OurDbContext())
                {
                    account.Password = hash;
                    account.ConfirmPassword = hash;
                    account.Salt = salt;
                    db.userAccount.Add(account);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = account.Username + " succesfully registered.";
            }
            return View();
        }
        //login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserAccount user)
        {
            using (OurDbContext db = new OurDbContext())
            {
                var usr = db.userAccount.Where(u => u.Username == user.Username && u.Password == user.Password).FirstOrDefault();
                if (user != null)
                {
                    Session["UserId"] = usr.UserId.ToString();
                    Session["Username"] = usr.Username.ToString();
                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", "Username or password is wrong.");
                }
            }
            return View();
        }
        public ActionResult LoggedIn()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public string HashPassword(string password, byte[] salt)
        {
            // Create hash
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            //Store both salt and password bytes fo later use;
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            return savedPasswordHash;
        }
        public byte [] GenerateSalt()
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[16];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return buff;
        }
    }
}