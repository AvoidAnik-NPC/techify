using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Techify.Models;
using Techify.Repository;

namespace Techify.Controllers
{
    public class SellerController : Controller
    {
        // GET: Seller
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();

        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<Category>().GetAllRecords();
            foreach(var item in cat)
            {
                list.Add(new SelectListItem { Value = item.CategoryID.ToString(), Text =item.CategoryName });
            }
            return list;
        }
        public ActionResult Seller_Dashboard()
        {
            return View();
        }
        public ActionResult Seller_Dashboard_Layout()
        {
            return View();
        }
        public ActionResult Product(string productName, int? categoryId, int? brandId, int? stockQuantity)
        {
            var products = _unitOfWork.GetRepositoryInstance<Product>().GetAllRecordsIQueryable();

            if (!string.IsNullOrEmpty(productName))
            {
                products = products.Where(p => p.Name.Contains(productName));
            }

            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryID == categoryId);
            }

            if (brandId.HasValue)
            {
                products = products.Where(p => p.BrandID == brandId);
            }

            if (stockQuantity.HasValue)
            {
                products = products.Where(p => p.StockQuantity == stockQuantity);
            }

            return View(products);
        }



        // GET: Seller/ProductAdd
        public ActionResult ProductAdd()
        {
            var productDetails = new ProductDetails
            {
                Categories = new SelectList(_unitOfWork.GetRepositoryInstance<Category>().GetAllRecords(), "CategoryID", "CategoryName"),
                Brands = new SelectList(_unitOfWork.GetRepositoryInstance<Brand>().GetAllRecords(), "BrandID", "BrandName"),
                Sellers = new SelectList(_unitOfWork.GetRepositoryInstance<Seller>().GetAllRecords(), "SellerID", "Name")
            };

            return View(productDetails);
        }

        [HttpPost]
        public ActionResult ProductAdd(ProductDetails productDetails, HttpPostedFileBase productImage)
        {
            if (ModelState.IsValid)
            {
                // Save the image to the server
                string imageUrl = null;
                if (productImage != null && productImage.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(productImage.FileName);
                    string path = Path.Combine(Server.MapPath("~/ProductImages/"), fileName);
                    productImage.SaveAs(path);
                    imageUrl = "/ProductImages/" + fileName;
                }

                // Map ProductDetails to Product entity and save to database
                var product = new Product
                {
                    Name = productDetails.Name,
                    Description = productDetails.Description,
                    Price = productDetails.Price,
                    StockQuantity = productDetails.StockQuantity,
                    CategoryID = productDetails.CategoryID.Value,
                    BrandID = productDetails.BrandID.Value,
                    SellerID = productDetails.SellerID.Value,
                    ImageURL = imageUrl
                };

                _unitOfWork.GetRepositoryInstance<Product>().Add(product);
                _unitOfWork.SaveChanges();

                // Redirect to a success page or seller dashboard
                return RedirectToAction("Product");
            }

            // If model state is not valid, repopulate the dropdown lists
            productDetails.Categories = new SelectList(_unitOfWork.GetRepositoryInstance<Category>().GetAllRecords(), "CategoryID", "CategoryName");
            productDetails.Brands = new SelectList(_unitOfWork.GetRepositoryInstance<Brand>().GetAllRecords(), "BrandID", "BrandName");
            productDetails.Sellers = new SelectList(_unitOfWork.GetRepositoryInstance<Seller>().GetAllRecords(), "SellerID", "Name");

            return View(productDetails);
        }

        // GET: Seller/EditProduct/{id}
        public ActionResult EditProduct(int id)
        {
            var product = _unitOfWork.GetRepositoryInstance<Product>().GetFirstorDefault(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            var productDetails = new ProductDetails
            {
                ProductID = product.ProductID,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryID = product.CategoryID,
                BrandID = product.BrandID,
                SellerID = product.SellerID,
                ImageURL = product.ImageURL
            };

            productDetails.Categories = new SelectList(_unitOfWork.GetRepositoryInstance<Category>().GetAllRecords(), "CategoryID", "CategoryName", product.CategoryID);
            productDetails.Brands = new SelectList(_unitOfWork.GetRepositoryInstance<Brand>().GetAllRecords(), "BrandID", "BrandName", product.BrandID);
            productDetails.Sellers = new SelectList(_unitOfWork.GetRepositoryInstance<Seller>().GetAllRecords(), "SellerID", "Name", product.SellerID);

            return View(productDetails);
        }

        // POST: Seller/EditProduct/{id}
        [HttpPost]
        public ActionResult EditProduct(ProductDetails productDetails, HttpPostedFileBase productImage)
        {
            if (ModelState.IsValid)
            {
                var product = _unitOfWork.GetRepositoryInstance<Product>().GetFirstorDefault(productDetails.ProductID);
                if (product == null)
                {
                    return HttpNotFound();
                }

                // Save the image to the server
                if (productImage != null && productImage.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(productImage.FileName);
                    string path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    productImage.SaveAs(path);
                    productDetails.ImageURL = "/Images/" + fileName;
                }

                // Map ProductDetails to Product entity
                product.Name = productDetails.Name;
                product.Description = productDetails.Description;
                product.Price = productDetails.Price;
                product.StockQuantity = productDetails.StockQuantity;
                product.CategoryID = productDetails.CategoryID.Value;
                product.BrandID = productDetails.BrandID.Value;
                product.SellerID = productDetails.SellerID.Value;
                product.ImageURL = productDetails.ImageURL;

                // Update the product in the database using Unit of Work
                _unitOfWork.GetRepositoryInstance<Product>().Update(product);
                _unitOfWork.SaveChanges();

                // Redirect to a success page or seller dashboard
                return RedirectToAction("Product");
            }

            // If model state is not valid, return the form with errors
            productDetails.Categories = new SelectList(_unitOfWork.GetRepositoryInstance<Category>().GetAllRecords(), "CategoryID", "CategoryName", productDetails.CategoryID);
            productDetails.Brands = new SelectList(_unitOfWork.GetRepositoryInstance<Brand>().GetAllRecords(), "BrandID", "BrandName", productDetails.BrandID);
            productDetails.Sellers = new SelectList(_unitOfWork.GetRepositoryInstance<Seller>().GetAllRecords(), "SellerID", "Name", productDetails.SellerID);
            return View(productDetails);
        }

        // POST: Seller/DeleteProduct/{id}
        [HttpPost]
        public ActionResult DeleteProduct(int id)
        {
            var product = _unitOfWork.GetRepositoryInstance<Product>().GetFirstorDefault(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            // Delete the product from the database using Unit of Work
            _unitOfWork.GetRepositoryInstance<Product>().Remove(product);
            _unitOfWork.SaveChanges();

            // Redirect to a success page or seller dashboard
            return RedirectToAction("Product");
        }

        // Action to display list of orders with search functionality
        public ActionResult Orders(int? orderId, DateTime? orderDate, string status)
        {
            int sellerId = 1; // Default SellerID

            var orderDetailRepository = _unitOfWork.GetRepositoryInstance<OrderDetail>();
            var ordersQuery = orderDetailRepository.GetListParameter(od => od.SellerID == sellerId)
                                                   .Select(od => od.Order)
                                                   .Distinct();

            if (orderId.HasValue)
            {
                ordersQuery = ordersQuery.Where(o => o.OrderID == orderId.Value);
            }

            if (orderDate.HasValue)
            {
                ordersQuery = ordersQuery.Where(o => o.OrderDate.Date == orderDate.Value.Date);
            }

            if (!string.IsNullOrEmpty(status))
            {
                ordersQuery = ordersQuery.Where(o => o.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
            }

            var orders = ordersQuery.ToList();
            return View(orders);
        }

        public ActionResult OrderDetails1(int orderId)
        {
            var orderDetailRepository = _unitOfWork.GetRepositoryInstance<OrderDetail>();

            // Fetch order details
            var orderDetails = orderDetailRepository.GetListParameter(od => od.OrderID == orderId)
                                                     .ToList(); // No need for projection if returning full OrderDetail model

            return View(orderDetails);
        }


        // Action to update the status of an order
        [HttpPost]
        public ActionResult UpdateOrderStatus(int orderId, string status)
        {
            var orderRepository = _unitOfWork.GetRepositoryInstance<Order>();
            var order = orderRepository.GetFirstorDefaultByParameter(o => o.OrderID == orderId);

            if (order != null)
            {
                order.Status = status;
                order.UpdatedAt = DateTime.Now;
                orderRepository.Update(order);
                _unitOfWork.SaveChanges();
            }

            return RedirectToAction("Orders");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}