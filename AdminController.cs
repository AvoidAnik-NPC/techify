using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Techify.Models;
using Techify.Repository;

namespace Techify.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
       public GenericUnitOfWork _unitOfWork=new GenericUnitOfWork();

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }

        public ActionResult PrivacyPolicy()
        {
            return View();
        }

        public ActionResult TermsConditions()
        {
            return View();
        }
        public ActionResult Help()
        {
            return View();
        }
        public ActionResult Admin_Dashboard()
        {
            return View();
        }
        public ActionResult Admin_Dashboard1()
        {
            return View();
        }

        public ActionResult Categories(string search)
        {
            // Get all records as IQueryable
            var allcategories = _unitOfWork.GetRepositoryInstance<Category>().GetAllRecordsIQueryable();

            // Filter by search term if provided
            if (!string.IsNullOrEmpty(search))
            {
                allcategories = allcategories.Where(c => c.CategoryName.Contains(search));
                ViewBag.SearchTerm = search;
            }

            // Convert to list before returning to the view
            var categoryList = allcategories.ToList();

            return View(categoryList);
        }



        public ActionResult AddCategory()
        {
            return UpdateCategory((int?)null);
        }

        public ActionResult UpdateCategory(int? CategoryID)
        {
            CategoryDetails cd;
            if (CategoryID != null)
            {
                cd = JsonConvert.DeserializeObject<CategoryDetails>(JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<Category>().GetFirstorDefault((int)CategoryID)));
            }
            else
            {
                cd = new CategoryDetails();
            }
            return View("UpdateCategory", cd);
        }

        //following part is changed from video

        [HttpPost]
        public ActionResult SaveCategory(CategoryDetails categoryDetails)
        {
            if (ModelState.IsValid)
            {
                var categoryRepo = _unitOfWork.GetRepositoryInstance<Category>();

                if (categoryDetails.CategoryID == 0)
                {
                    // Adding new category
                    var newCategory = new Category
                    {
                        CategoryName = categoryDetails.CategoryName,
                        Description = categoryDetails.Description
                    };
                    categoryRepo.Add(newCategory);
                }
                else
                {
                    // Updating existing category
                    var existingCategory = categoryRepo.GetFirstorDefault((int)categoryDetails.CategoryID);
                    if (existingCategory != null)
                    {
                        existingCategory.CategoryName = categoryDetails.CategoryName;
                        existingCategory.Description = categoryDetails.Description;
                        categoryRepo.Update(existingCategory);
                    }
                }

                _unitOfWork.SaveChanges();

                return RedirectToAction("Categories");
            }

            return View("UpdateCategory", categoryDetails);
        }
        public ActionResult EditCategory(int? id)
        {
            CategoryDetails cd;

            if (id.HasValue)
            {
                var category = _unitOfWork.GetRepositoryInstance<Category>().GetFirstorDefault(id.Value);

                if (category != null)
                {
                    cd = new CategoryDetails
                    {
                        CategoryID = category.CategoryID,
                        CategoryName = category.CategoryName,
                        Description = category.Description
                    };
                }
                else
                {
                    // Handle case where category with given id is not found (optional)
                    return RedirectToAction("Categories");
                }
            }
            else
            {
                cd = new CategoryDetails();
            }

            return View("EditCategory", cd);
        }
        [HttpPost]
        public ActionResult EditCategory(CategoryDetails categoryDetails)
        {
            if (ModelState.IsValid)
            {
                var categoryRepo = _unitOfWork.GetRepositoryInstance<Category>();

                if (categoryDetails.CategoryID == 0)
                {
                    // Adding new category
                    var newCategory = new Category
                    {
                        CategoryName = categoryDetails.CategoryName,
                        Description = categoryDetails.Description
                    };
                    categoryRepo.Add(newCategory);
                }
                else
                {
                    // Updating existing category
                    var existingCategory = categoryRepo.GetFirstorDefault(categoryDetails.CategoryID);
                    if (existingCategory != null)
                    {
                        existingCategory.CategoryName = categoryDetails.CategoryName;
                        existingCategory.Description = categoryDetails.Description;
                        categoryRepo.Update(existingCategory);
                    }
                    else
                    {
                        // Handle case where category with given id is not found (optional)
                        return RedirectToAction("Categories");
                    }
                }

                _unitOfWork.SaveChanges();

                return RedirectToAction("Categories");
            }

            return View("EditCategory", categoryDetails);
        }
        public ActionResult DeleteCategory(int id)
        {
            var CategoryRepo = _unitOfWork.GetRepositoryInstance<Category>();
            var category = CategoryRepo.GetFirstorDefault(id);

            if (category == null)
            {
                return Json(new { success = false, message = "Category not found." }, JsonRequestBehavior.AllowGet);
            }

            // Perform the deletion directly
            CategoryRepo.Remove(category);
            _unitOfWork.SaveChanges();

            return Json(new { success = true, message = "Category deleted successfully." }, JsonRequestBehavior.AllowGet);
        }







        //**************************Brand**************************************

        public ActionResult Brands(string search)
        {
            // Get all records as IQueryable
            var allBrands = _unitOfWork.GetRepositoryInstance<Brand>().GetAllRecordsIQueryable();

            // Filter by search term if provided
            if (!string.IsNullOrEmpty(search))
            {
                allBrands = allBrands.Where(b => b.BrandName.Contains(search));
                ViewBag.SearchTerm = search;
            }

            // Convert to list before returning to the view
            var brandList = allBrands.ToList();

            return View(brandList);
        }

        public ActionResult AddBrand()
        {
            return UpdateBrand((int?)null);
        }

        public ActionResult UpdateBrand(int? BrandID)
        {
            BrandDetails bd;
            if (BrandID != null)
            {
                bd = JsonConvert.DeserializeObject<BrandDetails>(JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<Brand>().GetFirstorDefault((int)BrandID)));  //enity model theke view model e convert korar jonno
            }
            else
            {
                bd = new BrandDetails();
            }
            return View("UpdateBrand", bd);
        }

        //following part is changed from video

        [HttpPost]
        public ActionResult SaveBrand(BrandDetails BrandDetails)
        {
            if (ModelState.IsValid)
            {
                var BrandRepo = _unitOfWork.GetRepositoryInstance<Brand>();

                if (BrandDetails.BrandID == 0)
                {
                    // Adding new brand
                    var newBrand = new Brand
                    {
                        BrandName = BrandDetails.BrandName,
                        Description = BrandDetails.Description
                    };
                    BrandRepo.Add(newBrand);
                }
                else
                {
                    // Updating existing brand
                    var existingBrand = BrandRepo.GetFirstorDefault((int)BrandDetails.BrandID);
                    if (existingBrand != null)
                    {
                        existingBrand.BrandName = BrandDetails.BrandName;
                        existingBrand.Description = BrandDetails.Description;
                        BrandRepo.Update(existingBrand);
                    }
                }

                _unitOfWork.SaveChanges();

                return RedirectToAction("Brands");
            }

            return View("UpdateBrand", BrandDetails);
        }

        public ActionResult EditBrand(int? id)
        {
            BrandDetails bd;

            if (id.HasValue)
            {
                var brand = _unitOfWork.GetRepositoryInstance<Brand>().GetFirstorDefault(id.Value);

                if (brand != null)
                {
                    bd = new BrandDetails
                    {
                        BrandID = brand.BrandID,
                        BrandName = brand.BrandName,
                        Description = brand.Description
                    };
                }
                else
                {
                    // Handle case where brand with given id is not found (optional)
                    return RedirectToAction("Brands");
                }
            }
            else
            {
                bd = new BrandDetails();
            }

            return View("EditBrand", bd);
        }

        [HttpPost]
        public ActionResult EditBrand(BrandDetails brandDetails)
        {
            if (ModelState.IsValid)
            {
                var brandRepo = _unitOfWork.GetRepositoryInstance<Brand>();

                if (brandDetails.BrandID == 0)
                {
                    // Adding new brand
                    var newBrand = new Brand
                    {
                        BrandName = brandDetails.BrandName,
                        Description = brandDetails.Description
                    };
                    brandRepo.Add(newBrand);
                }
                else
                {
                    // Updating existing brand
                    var existingBrand = brandRepo.GetFirstorDefault(brandDetails.BrandID);
                    if (existingBrand != null)
                    {
                        existingBrand.BrandName = brandDetails.BrandName;
                        existingBrand.Description = brandDetails.Description;
                        brandRepo.Update(existingBrand);
                    }
                    else
                    {
                        // Handle case where brand with given id is not found (optional)
                        return RedirectToAction("Brands");
                    }
                }

                _unitOfWork.SaveChanges();

                return RedirectToAction("Brands");
            }

            return View("EditBrand", brandDetails);
        }


        public ActionResult DeleteBrand(int id)
        {
            var BrandRepo = _unitOfWork.GetRepositoryInstance<Brand>();
            var brand = BrandRepo.GetFirstorDefault(id);

            if (brand == null)
            {
                return Json(new { success = false, message = "Brand not found." }, JsonRequestBehavior.AllowGet);
            }

            // Perform the deletion directly
            BrandRepo.Remove(brand);
            _unitOfWork.SaveChanges();

            return Json(new { success = true, message = "Brand deleted successfully." }, JsonRequestBehavior.AllowGet);
        }


        //***********************Seller********************************************

        public ActionResult Sellers(string search)
        {
            // Get all records as IQueryable
            var allSellers = _unitOfWork.GetRepositoryInstance<Seller>().GetAllRecordsIQueryable();

            // Filter by search term if provided
            if (!string.IsNullOrEmpty(search))
            {
                allSellers = allSellers.Where(s => s.Name.Contains(search) ||
                                                     s.Email.Contains(search) ||
                                                     s.Address.Contains(search) ||
                                                     s.BranchName.Contains(search));
                ViewBag.SearchTerm = search;
            }

            // Convert to list before returning to the view
            var sellerList = allSellers.ToList();

            return View(sellerList);
        }

        public ActionResult SellerRequests(string search = "", bool? isActive = null)
        {
            var sellerRequests = _unitOfWork.GetRepositoryInstance<SellerRequest>().GetAllRecords();

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                sellerRequests = sellerRequests.Where(r => r.Name.ToLower().Contains(search) ||
                                                           r.Email.ToLower().Contains(search) ||
                                                           r.BranchName.ToLower().Contains(search) ||
                                                           r.Address.ToLower().Contains(search));
            }

            if (isActive.HasValue)
            {
                sellerRequests = sellerRequests.Where(r => r.IsActive == isActive.Value);
            }

            return View(sellerRequests);
        }



        [HttpPost]
        public ActionResult ActivateSeller(int requestId)
        {
            var sellerRequestRepo = _unitOfWork.GetRepositoryInstance<SellerRequest>();
            var sellerRequest = sellerRequestRepo.GetFirstorDefault(requestId);

            if (sellerRequest != null && !sellerRequest.IsActive)
            {
                var newSeller = new Seller
                {
                    Name = sellerRequest.Name,
                    Email = sellerRequest.Email,
                    Password = sellerRequest.Password,
                    Address = sellerRequest.Address,
                    PhoneNumber = sellerRequest.PhoneNumber,
                    BranchName = sellerRequest.BranchName
                };

                var sellerRepo = _unitOfWork.GetRepositoryInstance<Seller>();
                sellerRepo.Add(newSeller);
                _unitOfWork.SaveChanges();

                sellerRequest.IsActive = true;
                sellerRequestRepo.Update(sellerRequest);
                _unitOfWork.SaveChanges();

                return Json(new { success = true, message = "Seller activated successfully." });
            }

            return Json(new { success = false, message = "Failed to activate seller." });
        }

      


        public ActionResult AddSeller()
        {
            return UpdateSeller((int?)null);
        }

        public ActionResult UpdateSeller(int? SellerID)
        {
            SellerDetails sd;
            if (SellerID != null)
            {
                sd = JsonConvert.DeserializeObject<SellerDetails>(JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<Seller>().GetFirstorDefault((int)SellerID)));
            }
            else
            {
                sd = new SellerDetails();
            }
            return View("UpdateSeller", sd);
        }

        //following part is changed from video

        [HttpPost]
        public ActionResult SaveSeller(SellerDetails SellerDetails)
        {
            if (ModelState.IsValid)
            {
                var SellerRepo = _unitOfWork.GetRepositoryInstance<Seller>();

                if (SellerDetails.SellerID == 0)
                {
                    // Adding new seller
                    var newSeller = new Seller
                    {
                        Name = SellerDetails.Name,
                        Email = SellerDetails.Email,
                        Password = SellerDetails.Password,
                        Address = SellerDetails.Address,
                        PhoneNumber = SellerDetails.PhoneNumber,
                        BranchName = SellerDetails.BranchName
                    };
                    SellerRepo.Add(newSeller);
                }
                else
                {
                    // Updating existing Seller
                    var existingSeller = SellerRepo.GetFirstorDefault((int)SellerDetails.SellerID);
                    if (existingSeller != null)
                    {
                        existingSeller.Name = SellerDetails.Name;
                        existingSeller.Email = SellerDetails.Email;
                        existingSeller.Password = SellerDetails.Password;
                        existingSeller.Address = SellerDetails.Address;
                        existingSeller.PhoneNumber = SellerDetails.PhoneNumber;
                        existingSeller.BranchName = SellerDetails.BranchName;

                        SellerRepo.Update(existingSeller);
                    }
                }

                _unitOfWork.SaveChanges();

                return RedirectToAction("Sellers");
            }

            return View("UpdateSeller", SellerDetails);
        }
        public ActionResult EditSeller(int? id)
        {
            SellerDetails sd;

            if (id.HasValue)
            {
                var seller = _unitOfWork.GetRepositoryInstance<Seller>().GetFirstorDefault(id.Value);

                if (seller != null)
                {
                    sd = new SellerDetails
                    {
                        SellerID = seller.SellerID,
                        Name = seller.Name,
                        Email = seller.Email,
                        Password = seller.Password,
                        Address = seller.Address,
                        PhoneNumber = seller.PhoneNumber,
                        BranchName = seller.BranchName
                    };
                }
                else
                {
                    // Handle case where seller with given id is not found (optional)
                    return RedirectToAction("Sellers");
                }
            }
            else
            {
                sd = new SellerDetails();
            }

            return View("EditSeller", sd);
        }
        [HttpPost]
        public ActionResult EditSeller(SellerDetails sellerDetails)
        {
            if (ModelState.IsValid)
            {
                var sellerRepo = _unitOfWork.GetRepositoryInstance<Seller>();

                if (sellerDetails.SellerID == 0)
                {
                    // Adding new seller
                    var newSeller = new Seller
                    {
                        Name = sellerDetails.Name,
                        Email = sellerDetails.Email,
                        Password = sellerDetails.Password,
                        Address = sellerDetails.Address,
                        PhoneNumber = sellerDetails.PhoneNumber,
                        BranchName = sellerDetails.BranchName
                    };
                    sellerRepo.Add(newSeller);
                }
                else
                {
                    // Updating existing seller
                    var existingSeller = sellerRepo.GetFirstorDefault(sellerDetails.SellerID);
                    if (existingSeller != null)
                    {
                        existingSeller.Name = sellerDetails.Name;
                        existingSeller.Email = sellerDetails.Email;
                        existingSeller.Password = sellerDetails.Password;
                        existingSeller.Address = sellerDetails.Address;
                        existingSeller.PhoneNumber = sellerDetails.PhoneNumber;
                        existingSeller.BranchName = sellerDetails.BranchName;
                        sellerRepo.Update(existingSeller);
                    }
                    else
                    {
                        // Handle case where seller with given id is not found (optional)
                        return RedirectToAction("Sellers");
                    }
                }

                _unitOfWork.SaveChanges();

                return RedirectToAction("Sellers");
            }

            return View("EditSeller", sellerDetails);
        }
        // GET: Admin/DeleteSeller/{id}
        public ActionResult DeleteSeller(int id)
        {
            var sellerRepo = _unitOfWork.GetRepositoryInstance<Seller>();
            var seller = sellerRepo.GetFirstorDefault(id);

            if (seller == null)
            {
                return Json(new { success = false, message = "Seller not found." }, JsonRequestBehavior.AllowGet);
            }

            // Perform the deletion directly
            sellerRepo.Remove(seller);
            _unitOfWork.SaveChanges();

            return Json(new { success = true, message = "Seller deleted successfully." }, JsonRequestBehavior.AllowGet);
        }













    }
}