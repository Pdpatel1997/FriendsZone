using FriendsZone.Models.Data;
using FriendsZone.Models.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.MappingViews;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FriendsZone.Controllers
{
    public class AccountController : Controller
    {
        // GET: /
        public ActionResult Index()
        {
            // confirm user is not logged in
            string userName = User.Identity.Name;

            if (!string.IsNullOrEmpty(userName))
                return Redirect("~/" + userName);
            return View();
        }

        // POST: Account/CreateAccount
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccount(UserViewModel uvm, HttpPostedFileBase img)
        {
            //initialize database   

            FriendsZoneDbContext db = new FriendsZoneDbContext();

            //chech model state
            if (!ModelState.IsValid)
            {
                return View("Index",uvm);
            }

            //make sure user name is unique

            if (db.Users.Any(s => s.UserName.Equals(uvm.UserName)))
            {
                ModelState.AddModelError("Error", "UserName" +uvm.UserName+ "is already Registered.");
                uvm.UserName = "";
                return View("Index", uvm);
            }

            //create user DTO

            UserDTO userDTO = new UserDTO()
            {
                FirstName = uvm.FirstName,
                LastName = uvm.LastName,
                EmailAddress = uvm.EmailAddress,
                UserName = uvm.UserName,
                Password=uvm.Password
            };

            // add to DTO
            db.Users.Add(userDTO);

            //save
            db.SaveChanges();

            //Get inserted id

            int userID = userDTO.ID;

            //login user
            FormsAuthentication.SetAuthCookie(uvm.UserName, false);

            //set upload directory

            var uploadsDirectory = new DirectoryInfo(string.Format("{0}Uploads",Server.MapPath(@"\")));

            //check if file was uploaded
            if (img != null && img.ContentLength > 0)
            {

                //Get Extension

                string ext = img.ContentType.ToLower();

                //Verify Extension
                if (ext != "image/jpg" && ext != "image/jpge" && ext != "image/pjepg" && ext != "image/gif" && ext != "image/x-png" && ext != "image/png")
                {
                    ModelState.AddModelError("Error", "Image type is not supported");
                    return View("Index", uvm);
                }

                //set image name
                string imageName = userID + ".jpg";

                //set image path

                var path = string.Format("{0}\\{1}", uploadsDirectory, imageName);

                //save image file
                img.SaveAs(path);
            }

            //Redirect 
            return Redirect("~/"+uvm.UserName);
        }

        //GET: /{userName}
        public string UserName(string userName="")
        {
            return userName;
        }

        //GET: Account/LogOut
        [Authorize]
        public ActionResult LogOut()
        {
            //sign Out

            FormsAuthentication.SignOut();
            //Redirect
            return Redirect("~/");
        }
    }
}