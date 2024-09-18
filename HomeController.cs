using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Techify.Models;
using Techify.Models.Home;
using Techify.Repository;

namespace Techify.Controllers
{
    public class HomeController : Controller
    {
        //Anik modified in Index for search
        TechifyEntities ctx = new TechifyEntities();
        public ActionResult Index(int? page)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel(null, 4, page));
        }



        public ActionResult SearchResult(string search, int? page)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View("SearchResult", model.CreateModel(search, 4, page));
        }

        public ActionResult Shop(int? page)
        {
            ShopViewModel model = new ShopViewModel();
            return View(model.CreateModel(ctx, 12, page)); // 12 products per page to show 3 rows with 4 products each
        }
        // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // //
        /// Laptop Filter DONE
        public ActionResult LaptopFilter(int? page)
        {
            int pageSize = 4; // Define your page size here
            HomeIndexViewModel model = Laptopfiltering(pageSize, page);
            return View(model);
        }

        public HomeIndexViewModel Laptopfiltering(int pageSize, int? page)
        {
            IPagedList<Product> data = ctx.Database.SqlQuery<Product>("Laptopfiltering").ToList().ToPagedList(page ?? 1, pageSize);
            return new HomeIndexViewModel
            {
                ListofProducts = data
            };
        }
        
        // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
        // Camera Filter  DONE
        public ActionResult CameraFilter(int? page)
        {
            int pageSize = 4; // Define your page size here
            HomeIndexViewModel model = Camerafiltering(pageSize, page);
            return View(model);
        }

        public HomeIndexViewModel Camerafiltering(int pageSize, int? page)
        {
            IPagedList<Product> data = ctx.Database.SqlQuery<Product>("Camerafiltering").ToList().ToPagedList(page ?? 1, pageSize);
            return new HomeIndexViewModel
            {
                ListofProducts = data
            };
        }

        // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
        // Accessories Filter  DONE
        public ActionResult AccessoriesFilter(int? page)
        {
            int pageSize = 4; // Define your page size here
            HomeIndexViewModel model = Accessoriesfiltering(pageSize, page);
            return View(model);
        }

        public HomeIndexViewModel Accessoriesfiltering(int pageSize, int? page)
        {
            IPagedList<Product> data = ctx.Database.SqlQuery<Product>("Accessoriesfiltering").ToList().ToPagedList(page ?? 1, pageSize);
            return new HomeIndexViewModel
            {
                ListofProducts = data
            };
        }

        // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
        // Desktop Filter  DONE
        public ActionResult DesktopFilter(int? page)
        {
            int pageSize = 4; // Define your page size here
            HomeIndexViewModel model = Desktopfiltering(pageSize, page);
            return View(model);
        }

        public HomeIndexViewModel Desktopfiltering(int pageSize, int? page)
        {
            IPagedList<Product> data = ctx.Database.SqlQuery<Product>("Desktopfiltering").ToList().ToPagedList(page ?? 1, pageSize);
            return new HomeIndexViewModel
            {
                ListofProducts = data
            };
        }

        // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
        // Tablet Filter  DONE
        public ActionResult TabletFilter(int? page)
        {
            int pageSize = 4; // Define your page size here
            HomeIndexViewModel model = Tabletfiltering(pageSize, page);
            return View(model);
        }

        public HomeIndexViewModel Tabletfiltering(int pageSize, int? page)
        {
            IPagedList<Product> data = ctx.Database.SqlQuery<Product>("Tabletfiltering").ToList().ToPagedList(page ?? 1, pageSize);
            return new HomeIndexViewModel
            {
                ListofProducts = data
            };
        }

        // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
        // IEM Filter  DONE
        public ActionResult IEMFilter(int? page)
        {
            int pageSize = 4; // Define your page size here
            HomeIndexViewModel model = IEMfiltering(pageSize, page);
            return View(model);
        }

        public HomeIndexViewModel IEMfiltering(int pageSize, int? page)
        {
            IPagedList<Product> data = ctx.Database.SqlQuery<Product>("IEMfiltering").ToList().ToPagedList(page ?? 1, pageSize);
            return new HomeIndexViewModel
            {
                ListofProducts = data
            };
        }

        // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
        // Keyboard Filter  DONE
        public ActionResult KeyboardFilter(int? page)
        {
            int pageSize = 4; // Define your page size here
            HomeIndexViewModel model = Keyboardfiltering(pageSize, page);
            return View(model);
        }

        public HomeIndexViewModel Keyboardfiltering(int pageSize, int? page)
        {
            IPagedList<Product> data = ctx.Database.SqlQuery<Product>("Keyboardfiltering").ToList().ToPagedList(page ?? 1, pageSize);
            return new HomeIndexViewModel
            {
                ListofProducts = data
            };
        }

        // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 
        // Mouse Filter  DONE
        public ActionResult MouseFilter(int? page)
        {
            int pageSize = 4; // Define your page size here
            HomeIndexViewModel model = Mousefiltering(pageSize, page);
            return View(model);
        }

        public HomeIndexViewModel Mousefiltering(int pageSize, int? page)
        {
            IPagedList<Product> data = ctx.Database.SqlQuery<Product>("Mousefiltering").ToList().ToPagedList(page ?? 1, pageSize);
            return new HomeIndexViewModel
            {
                ListofProducts = data
            };
        }




        public ActionResult Checkout()
        {
            return View();  
        }

        public ActionResult CheckoutDetails()
        {
            return View();
        }


        //Anik Added new method for cart
        //WORKING FINE
        public ActionResult AddToCart(int productId)
        {
            if (Session["cart"] == null)
            {
                Session["cart"] = new List<Item>();
            }
            List<Item> cart = (List<Item>)Session["cart"];
            var product = ctx.Products.Find(productId);

            var existingItem = cart.FirstOrDefault(x => x.Product.ProductID == productId);

            // Check if the product exists and there is enough stock to add
            if (existingItem != null && existingItem.Quantity + 1 > product.StockQuantity)
            {
                TempData["Message"] = "Can't add, Stock Exceeded";
                return RedirectToAction("Index"); // or return a view with an error message
            }

            if (existingItem != null)
            {
                existingItem.Quantity += 1;
            }
            else
            {
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
            }
            Session["cart"] = cart;
            TempData["SuccessMessage"] = "Product added to cart.";
            return RedirectToAction("Index");
        }


        // FILTERWISE CART ADD
        //LaptopFilterandaddcart
        [HttpPost]
    public ActionResult LaptopFilterAddToCart(int productId)
    {
        if (Session["cart"] == null)
        {
            Session["cart"] = new List<Item>();
        }
        List<Item> cart = (List<Item>)Session["cart"];
        var product = ctx.Products.Find(productId);

        var existingItem = cart.FirstOrDefault(x => x.Product.ProductID == productId);

        if (existingItem != null && existingItem.Quantity + 1 > product.StockQuantity)
        {
            TempData["Message"] = "Can't add, Stock Exceeded";
            return RedirectToAction("LaptopFilter");
        }

        if (existingItem != null)
        {
            existingItem.Quantity += 1;
        }
        else
        {
            cart.Add(new Item()
            {
                Product = product,
                Quantity = 1
            });
        }
        Session["cart"] = cart;
            TempData["SuccessMessage"] = "Product added to cart. <a href='" + Url.Action("Index", "Home") + "'>Go to Home</a> to view cart.";
            return RedirectToAction("LaptopFilter");
    }

        //CameraFilterandaddcart
        public ActionResult CameraFilterAddToCart(int productId)
        {
            if (Session["cart"] == null)
            {
                Session["cart"] = new List<Item>();
            }
            List<Item> cart = (List<Item>)Session["cart"];
            var product = ctx.Products.Find(productId);

            var existingItem = cart.FirstOrDefault(x => x.Product.ProductID == productId);

            // Check if the product exists and there is enough stock to add
            if (existingItem != null && existingItem.Quantity + 1 > product.StockQuantity)
            {
                TempData["Message"] = "Can't add, Stock Exceeded";
                return RedirectToAction("Index"); // or return a view with an error message
            }

            if (existingItem != null)
            {
                existingItem.Quantity += 1;
            }
            else
            {
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
            }
            Session["cart"] = cart;
            TempData["SuccessMessage"] = "Product added to cart. <a href='" + Url.Action("Index", "Home") + "'>Go to Home</a> to view cart.";
            return RedirectToAction("CameraFilter");
        }

        //AccessoriesFilterandaddcart
        public ActionResult AccessoriesFilterAddToCart(int productId)
        {
            if (Session["cart"] == null)
            {
                Session["cart"] = new List<Item>();
            }
            List<Item> cart = (List<Item>)Session["cart"];
            var product = ctx.Products.Find(productId);

            var existingItem = cart.FirstOrDefault(x => x.Product.ProductID == productId);

            // Check if the product exists and there is enough stock to add
            if (existingItem != null && existingItem.Quantity + 1 > product.StockQuantity)
            {
                TempData["Message"] = "Can't add, Stock Exceeded";
                return RedirectToAction("Index"); // or return a view with an error message
            }

            if (existingItem != null)
            {
                existingItem.Quantity += 1;
            }
            else
            {
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
            }
            Session["cart"] = cart;
            TempData["SuccessMessage"] = "Product added to cart. <a href='" + Url.Action("Index", "Home") + "'>Go to Home</a> to view cart.";
            return RedirectToAction("AccessoriesFilter");
        }

        //DesktopFilterandaddcart
        public ActionResult DesktopFilterAddToCart(int productId)
        {
            if (Session["cart"] == null)
            {
                Session["cart"] = new List<Item>();
            }
            List<Item> cart = (List<Item>)Session["cart"];
            var product = ctx.Products.Find(productId);

            var existingItem = cart.FirstOrDefault(x => x.Product.ProductID == productId);

            // Check if the product exists and there is enough stock to add
            if (existingItem != null && existingItem.Quantity + 1 > product.StockQuantity)
            {
                TempData["Message"] = "Can't add, Stock Exceeded";
                return RedirectToAction("Index"); // or return a view with an error message
            }

            if (existingItem != null)
            {
                existingItem.Quantity += 1;
            }
            else
            {
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
            }
            Session["cart"] = cart;
            TempData["SuccessMessage"] = "Product added to cart. <a href='" + Url.Action("Index", "Home") + "'>Go to Home</a> to view cart.";
            return RedirectToAction("DesktopFilter");
        }


        //IEMFilterandaddcart
        public ActionResult IEMFilterAddToCart(int productId)
        {
            if (Session["cart"] == null)
            {
                Session["cart"] = new List<Item>();
            }
            List<Item> cart = (List<Item>)Session["cart"];
            var product = ctx.Products.Find(productId);

            var existingItem = cart.FirstOrDefault(x => x.Product.ProductID == productId);

            // Check if the product exists and there is enough stock to add
            if (existingItem != null && existingItem.Quantity + 1 > product.StockQuantity)
            {
                TempData["Message"] = "Can't add, Stock Exceeded";
                return RedirectToAction("Index"); // or return a view with an error message
            }

            if (existingItem != null)
            {
                existingItem.Quantity += 1;
            }
            else
            {
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
            }
            Session["cart"] = cart;
            TempData["SuccessMessage"] = "Product added to cart. <a href='" + Url.Action("Index", "Home") + "'>Go to Home</a> to view cart.";
            return RedirectToAction("IEMFilter");
        }

        //IEMFilterandaddcart
        public ActionResult TabletFilterAddToCart(int productId)
        {
            if (Session["cart"] == null)
            {
                Session["cart"] = new List<Item>();
            }
            List<Item> cart = (List<Item>)Session["cart"];
            var product = ctx.Products.Find(productId);

            var existingItem = cart.FirstOrDefault(x => x.Product.ProductID == productId);

            // Check if the product exists and there is enough stock to add
            if (existingItem != null && existingItem.Quantity + 1 > product.StockQuantity)
            {
                TempData["Message"] = "Can't add, Stock Exceeded";
                return RedirectToAction("Index"); // or return a view with an error message
            }

            if (existingItem != null)
            {
                existingItem.Quantity += 1;
            }
            else
            {
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
            }
            Session["cart"] = cart;
            TempData["SuccessMessage"] = "Product added to cart. <a href='" + Url.Action("Index", "Home") + "'>Go to Home</a> to view cart.";
            return RedirectToAction("TabletFilter");
        }

        //KeyboardFilterandaddcart
        public ActionResult KeyboardFilterAddToCart(int productId)
        {
            if (Session["cart"] == null)
            {
                Session["cart"] = new List<Item>();
            }
            List<Item> cart = (List<Item>)Session["cart"];
            var product = ctx.Products.Find(productId);

            var existingItem = cart.FirstOrDefault(x => x.Product.ProductID == productId);

            // Check if the product exists and there is enough stock to add
            if (existingItem != null && existingItem.Quantity + 1 > product.StockQuantity)
            {
                TempData["Message"] = "Can't add, Stock Exceeded";
                return RedirectToAction("Index"); // or return a view with an error message
            }

            if (existingItem != null)
            {
                existingItem.Quantity += 1;
            }
            else
            {
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
            }
            Session["cart"] = cart;
            TempData["SuccessMessage"] = "Product added to cart. <a href='" + Url.Action("Index", "Home") + "'>Go to Home</a> to view cart.";
            return RedirectToAction("KeyboardFilter");
        }



        //MouseFilterandaddcart
        public ActionResult MouseFilterAddToCart(int productId)
        {
            if (Session["cart"] == null)
            {
                Session["cart"] = new List<Item>();
            }
            List<Item> cart = (List<Item>)Session["cart"];
            var product = ctx.Products.Find(productId);

            var existingItem = cart.FirstOrDefault(x => x.Product.ProductID == productId);

            // Check if the product exists and there is enough stock to add
            if (existingItem != null && existingItem.Quantity + 1 > product.StockQuantity)
            {
                TempData["Message"] = "Can't add, Stock Exceeded";
                return RedirectToAction("Index"); // or return a view with an error message
            }

            if (existingItem != null)
            {
                existingItem.Quantity += 1;
            }
            else
            {
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
            }
            Session["cart"] = cart;
            TempData["SuccessMessage"] = "Product added to cart. <a href='" + Url.Action("Index", "Home") + "'>Go to Home</a> to view cart.";
            return RedirectToAction("MouseFilter");
        }

        //Remove item from cart
        public ActionResult RemoveFromCart(int productId)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            //var product = ctx.Products.Find(productId);
            foreach (var item in cart)
            {
                if (item.Product.ProductID == productId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["cart"] = cart;
            return Redirect("Index");
        }

        ///CHECKOUT AND CHECKOUTDETAILS ER PART 
       //Increase Quantity of Checkout (WORKING) 
        public ActionResult IncreaseQty(int productId)
        {
            if (Session["cart"] != null)
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = ctx.Products.Find(productId);

                if (product != null)
                {
                    foreach (var item in cart)
                    {
                        if (item.Product.ProductID == productId)
                        {
                            int prevQty = item.Quantity;
                            if (prevQty > 0)
                            {
                                if (prevQty + 1 <= product.StockQuantity)
                                {
                                    cart.Remove(item);
                                    cart.Add(new Item()
                                    {
                                        Product = product,
                                        Quantity = prevQty + 1
                                    });
                                }
                                else
                                {
                                    TempData["ErrorMessage"] = "Can't add, Stock Exceeded.";
                                }
                            }
                            break;
                        }
                    }
                    Session["cart"] = cart;
                }
            }
            return RedirectToAction("Checkout");
        }

        //CheckoutDetails theke increase
        public ActionResult IncreaseQty2(int productId)
        {
            if (Session["cart"] != null)
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = ctx.Products.Find(productId);

                if (product != null)
                {
                    foreach (var item in cart)
                    {
                        if (item.Product.ProductID == productId)
                        {
                            int prevQty = item.Quantity;
                            if (prevQty > 0)
                            {
                                if (prevQty + 1 <= product.StockQuantity)
                                {
                                    cart.Remove(item);
                                    cart.Add(new Item()
                                    {
                                        Product = product,
                                        Quantity = prevQty + 1
                                    });
                                }
                                else
                                {
                                    TempData["ErrorMessage"] = "Can't add, Stock Exceeded.";
                                }
                            }
                            break;
                        }
                    }
                    Session["cart"] = cart;
                }
            }
            return RedirectToAction("CheckoutDetails");
        }


        //Checkout theke decrease (DONE)
        public ActionResult DecreaseQty(int productId)
        {
            if (Session["cart"] != null)
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = ctx.Products.Find(productId);

                if (product != null)
                {
                    var item = cart.FirstOrDefault(i => i.Product.ProductID == productId);
                    if (item != null)
                    {
                        item.Quantity--;

                        if (item.Quantity <= 0)
                        {
                            cart.Remove(item);
                        }
                    }
                }

                if (cart.Count == 0)
                {
                    Session["cart"] = null;
                }
                else
                {
                    Session["cart"] = cart;
                }
            }
            return Redirect("Checkout");
        }


        //CheckoutDetails theke decrease (DONE)
        public ActionResult DecreaseQty2(int productId)
        {
            if (Session["cart"] != null)
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = ctx.Products.Find(productId);

                if (product != null)
                {
                    var item = cart.FirstOrDefault(i => i.Product.ProductID == productId);
                    if (item != null)
                    {
                        item.Quantity--;

                        if (item.Quantity <= 0)
                        {
                            cart.Remove(item);
                        }
                    }
                }

                if (cart.Count == 0)
                {
                    Session["cart"] = null;
                }
                else
                {
                    Session["cart"] = cart;
                }
            }
            return Redirect("CheckoutDetails");
        }

            //Product Details 
            public ActionResult ProductDetails(int id)
        {
            var product = ctx.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //Form Fill Up for order confirm
        public ActionResult FormFillUp()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ConfirmOrder(Order order)
        {
            if (Session["Status"] == null)
            {
                ModelState.AddModelError("", "Customer is not logged in.");
                return View("FormFillUp", order);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    // Calculate the total amount
                    decimal totalAmount = 0;
                    var cart = (List<Item>)Session["cart"];
                    foreach (var item in cart)
                    {
                        totalAmount += item.Quantity * item.Product.Price;
                    }

                    // Populate order fields
                    order.OrderDate = DateTime.Now;
                    order.CustomerID = (int)Session["Status"];
                    order.TotalAmount = totalAmount;
                    order.Status = "Pending";
                    order.TrackingNumber = GenerateTrackingNumber();
                    order.CreatedAt = DateTime.Now;
                    order.UpdatedAt = DateTime.Now;

                    ctx.Orders.Add(order);
                    ctx.SaveChanges();

                    // Populate OrderDetails and adjust stock
                    foreach (var item in cart)
                    {
                        OrderDetail orderDetail = new OrderDetail()
                        {
                            OrderID = order.OrderID,
                            ProductID = item.Product.ProductID,
                            SellerID = 1,
                            Quantity = item.Quantity,
                            UnitPrice = item.Product.Price,
                            Discount = null,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now
                        };
                        ctx.OrderDetails.Add(orderDetail);

                        // Adjust stock quantity
                        var product = ctx.Products.Find(item.Product.ProductID);
                        product.StockQuantity -= item.Quantity;
                    }

                    ctx.SaveChanges();

                    // Optionally set a session status message
                    Session["Status"] = "Order confirmed successfully!";
                    Session["cart"] = null; // Clear the cart

                    // Redirect to a confirmation page
                    return RedirectToAction("OrderConfirmation");
                }

                // If we got this far, something failed, redisplay form
                return View("FormFillUp", order);
            }
        }
        public ActionResult OrderDetails(int orderId)
        {
            var orderDetails = ctx.OrderDetails
                                  .Where(od => od.OrderID == orderId)
                                  .Include(od => od.Product)
                                  .ToList();

            return View(orderDetails);
        }




        private static Random random = new Random();

        private string GenerateTrackingNumber()
        {

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            const int length = 10; // Desired length of the tracking number

            char[] trackingNumberArray = new char[length];

            for (int i = 0; i < length; i++)
            {
                trackingNumberArray[i] = chars[random.Next(chars.Length)];
            }

            string trackingNumber = new string(trackingNumberArray);
            return $"TRACK{trackingNumber}";
        }

        public ActionResult OrderConfirmation()
        {
            return View();
        }

        public ActionResult MyOrders()
        {
            int customerId = (int)Session["Status"];

            // Get all orders placed by the customer
            var orders = ctx.Orders
                            .Where(o => o.CustomerID == customerId)
                            .OrderByDescending(o => o.OrderDate)
                            .ToList();

            // Check if the customer has placed any orders
            if (orders.Count == 0)
            {
                ViewBag.Message = "You haven't placed any orders.";
                return View("NoOrders");
            }

            return View(orders);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        ///Rating
        [HttpPost]
        public ActionResult AddReview(int productId, int rating, string comment)
        {
            // Check if customer is logged in
            if (Session["Status"] == null)
            {
                ModelState.AddModelError("", "Customer is not logged in.");
                // Redirect or return appropriate view here, depending on your application flow
                return RedirectToAction("ProductDetails", new { id = productId });
            }
            else
            {
                int customerId = (int)Session["Status"];

                // Retrieve customer name (assuming you have a Customer table with names)
                var customer = ctx.Customers.FirstOrDefault(c => c.CustomerID == customerId);
                string customerName = customer != null ? customer.Name : "Anonymous"; // Fallback to "Anonymous" if customer name not found

                // Create the review object
                var review = new Review
                {
                    CustomerID = customerId,
                    ProductID = productId,
                    Rating = rating,
                    Comment = comment,
                    ReviewDate = DateTime.Now
                };

                ctx.Reviews.Add(review);
                ctx.SaveChanges();

                // Redirect back to the product details page after adding review
                return RedirectToAction("ProductDetails", new { id = productId });
            }
        }

        private string GetCustomerName(int customerId)
        {
            var customer = ctx.Customers.FirstOrDefault(c => c.CustomerID == customerId);
            return customer != null ? customer.Name : "Anonymous";
        }



        private readonly GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        // GET: Display the form for submitting a seller request
        public ActionResult SubmitRequest()
        {
            // Check if the customer is logged in
            if (Session["Status"] == null)
            {
                // Redirect to login page or display a message
                return RedirectToAction("Customer_Login", "Account");
            }

            // Proceed to display the form if logged in
            return View(new SellerRequest());
        }

        // POST: Handle the form submission for a seller request
        [HttpPost]
        public ActionResult SubmitRequest(SellerRequest sellerRequest)
        {
            // Check if the customer is logged in
            if (Session["Status"] == null)
            {
                // Add an error message and redirect to login page
                ModelState.AddModelError("", "Customer is not logged in. Please log in to submit a seller request.");
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                var sellerRequestRepo = _unitOfWork.GetRepositoryInstance<SellerRequest>();
                sellerRequest.CreatedAt = DateTime.Now;
                sellerRequest.IsActive = false; // Ensure the request is inactive by default
                sellerRequestRepo.Add(sellerRequest);
                _unitOfWork.SaveChanges();

                return RedirectToAction("RequestSuccess");
            }

            // Return to the form with validation errors if not valid
            return View(sellerRequest);
        }

        // GET: Display success message after submission
        public ActionResult RequestSuccess()
        {
            return View();
        }


    }
}