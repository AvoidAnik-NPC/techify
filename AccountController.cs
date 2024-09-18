using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Techify.Models;

namespace Techify.Controllers
{
    public class AccountController : Controller
    {
        TechifyEntities entity = new TechifyEntities();
        // GET: Account
        public ActionResult Customer_Login()
        {
            return View();
        }
        public ActionResult Customer_Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Customer_Signup(Customer customerinfo)
        {
            customerinfo.Password = PasswordHelper.HashPassword(customerinfo.Password); // Encrypt the password
            entity.Customers.Add(customerinfo);
            entity.SaveChanges();

            return RedirectToAction("Customer_Login");
        }

        [HttpPost]
        public ActionResult Customer_Login(Customer_Login credentials)
        {
            string hashedPassword = PasswordHelper.HashPassword(credentials.Password); // Encrypt the password for comparison
            bool CustomerExists = entity.Customers.Any(x => x.Email == credentials.Email && x.Password == hashedPassword); // Compare encrypted password
            Customer c = entity.Customers.FirstOrDefault(x => x.Email == credentials.Email && x.Password == hashedPassword); // Retrieve customer with encrypted password
            Session["Status"] = null;


            if (CustomerExists)
            {
                FormsAuthentication.SetAuthCookie(c.Name, false); // Save the customer's name

                if (credentials.Email == "admin@gmail.com" && credentials.Password == "1234")
                {
                    return RedirectToAction("Admin_Dashboard1", "Admin");
                }
               
                else if (credentials.Email == "seller@gmail.com" && credentials.Password == "1010")
                {
                    var seller = entity.Customers.FirstOrDefault(x => x.Email == credentials.Email && x.Password == hashedPassword);
                    Session["SellerStatus"] = seller.CustomerID;
                    return RedirectToAction("Seller_Dashboard", "Seller");
                }

                else
                {
                    var customer = entity.Customers.FirstOrDefault(x => x.Email == credentials.Email && x.Password == hashedPassword);

                    Session["Status"] = customer.CustomerID;
                   
                    return RedirectToAction("Index", "Home");
                }
                
            }


            ModelState.AddModelError("", "Username or Password is wrong");
            return View();
        }

        public ActionResult Customer_Logout()
        {

            FormsAuthentication.SignOut();
            Session["Status"] = null;
            return RedirectToAction("Customer_Login");
        }


        //**********************Admin********************************

        //public ActionResult Admin_Login()
        //{
        //    return View();
        //}
        //public ActionResult Admin_Signup()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Admin_Signup(Admin admininfo)
        //{
        //    admininfo.Password = PasswordHelper.HashPassword(admininfo.Password); // Encrypt the password
        //    entity.Admins.Add(admininfo);
        //    entity.SaveChanges();

        //    return RedirectToAction("Admin_Login");
        //}
        //[HttpPost]
        //public ActionResult Admin_Login(Admin_Login admin)
        //{
        //    string hashedPassword = PasswordHelper.HashPassword(admin.Password); // Encrypt the password for comparison
        //    bool AdminExists = entity.Admins.Any(x => x.Email == admin.Email && x.Password == hashedPassword); // Compare encrypted password
        //    Admin  a = entity.Admins.FirstOrDefault(x => x.Email == admin.Email && x.Password == hashedPassword); // Retrieve customer with encrypted password

        //    if (AdminExists)
        //    {
        //        FormsAuthentication.SetAuthCookie(a.Name, false); // Save the admin's name
        //        return RedirectToAction("Index", "Home");
        //    }

        //    ModelState.AddModelError("", "Username or Password is wrong");
        //    return View();
        //}

        //public ActionResult Admin_Logout()
        //{
        //    FormsAuthentication.SignOut();
        //    return RedirectToAction("Admin_Login");
        //}



        ////**********************Seller********************************

        /*
         public ActionResult Seller_Login()
         {  return View(); }

         [HttpPost]
        public ActionResult Seller_Login(Seller_Login credential)
        {
            string hashedPassword = PasswordHelper.HashPassword(credential.Password);

            var seller = entity.Sellers.FirstOrDefault(x => x.Email == credential.Email && x.ActiveString == "True");

            if (seller != null)
            {
                // Check if the password is "1234" (assuming this is a default or initial password)
                var sellercheck = entity.Sellers.FirstOrDefault(x => x.Email == credential.Email && x.Password == "1234");
                if (sellercheck != null)
                {
                    return RedirectToAction("Seller_Dashboard", "Seller");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is wrong");
                    return View();
                }
            }
            else
            {
                ViewBag.Message = "Your account is inactive. Please contact support.";
                return View();
            }
        }
        */


        //public ActionResult Seller_Signup()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Seller_Signup(Seller sellerinfo)
        //{
        //    sellerinfo.Password = PasswordHelper.HashPassword(sellerinfo.Password); // Encrypt the password
        //    entity.Sellers.Add(sellerinfo);
        //    entity.SaveChanges();

        //    return RedirectToAction("Seller_Login");
        //}
        //[HttpPost]
        //public ActionResult Seller_Login(Seller_Login seller)
        //{
        //    string hashedPassword = PasswordHelper.HashPassword(seller.Password); // Encrypt the password for comparison
        //    bool SellerExists = entity.Sellers.Any(x => x.Email == seller.Email && x.Password == hashedPassword); // Compare encrypted password
        //    Seller s = entity.Sellers.FirstOrDefault(x => x.Email == seller.Email && x.Password == hashedPassword); // Retrieve customer with encrypted password

        //    if (SellerExists)
        //    {
        //        FormsAuthentication.SetAuthCookie(s.Name, false); // Save the admin's name
        //        return RedirectToAction("Index", "Home");
        //    }

        //    ModelState.AddModelError("", "Username or Password is wrong");
        //    return View();
        //}

        //public ActionResult Seller_Logout()
        //{
        //    FormsAuthentication.SignOut();
        //    return RedirectToAction("Seller_Login");
        //}



    }
}