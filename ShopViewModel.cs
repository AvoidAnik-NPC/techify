using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techify.Models.Home
{
    public class ShopViewModel
    {
        public IPagedList<Product> ListOfProducts { get; set; }

        public ShopViewModel CreateModel(TechifyEntities context, int pageSize, int? page)
        {
            var data = context.Products.OrderBy(p => p.Name).ToList().ToPagedList(page ?? 1, pageSize);

            return new ShopViewModel
            {
                ListOfProducts = data
            };
        }
    }
}