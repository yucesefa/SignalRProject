﻿using Microsoft.EntityFrameworkCore;
using SignalR.DataAccessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DataAccessLayer.Repositories;
using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccessLayer.EntityFramework
{
    public class EfProductDal : GenericRepository<Product>, IProductDal
    {
        public EfProductDal(SignalRContext context) : base(context)
        {
        }

        public List<Product> GetProductsWithCategory()
        {
            var context = new SignalRContext();
            var values = context.Products.Include(x => x.Category).ToList();
            return values;
        }

        public int ProductCount()
        {
            var context = new SignalRContext();
            return context.Products.Count();
        }

        public int ProductCountByCategoryDrink()
        {
            var context = new SignalRContext();
            return context.Products.Where(x => x.CategoryId == (context.Categories.Where(y => y.CategoryName == "İçecek").Select(z => z.CategoryId).FirstOrDefault())).Count();
        }

        public int ProductCountByCategoryNameHamburger()
        {
            var context = new SignalRContext();
            return context.Products.Where(x => x.CategoryId == (context.Categories.Where(y => y.CategoryName == "Hamburger").Select(z => z.CategoryId).FirstOrDefault())).Count();
        }

        public string ProductNameByMaxPrice()
        {
            var context = new SignalRContext();
            return context.Products.Where(x => x.Price == (context.Products.Max(y => y.Price))).Select(z => z.ProductName).FirstOrDefault();
        }

        public string ProductNameByMinPrice()
        {
            var context = new SignalRContext();
            return context.Products.Where(x => x.Price == (context.Products.Min(y => y.Price))).Select(z => z.ProductName).FirstOrDefault();
        }

        public decimal ProductPriceAvg()
        {
            var context = new SignalRContext();
            return context.Products.Average(x => x.Price);
        }

        public decimal ProductAvgPriceByHamburger()
        {
            var context = new SignalRContext();
            return context.Products.Where(x => x.CategoryId == (context.Categories.Where(y => y.CategoryName == "Hamburger").Select(z => z.CategoryId).FirstOrDefault())).Average(w => w.Price);

        }

        public decimal ProductPriceBySteakBurger()
        {
            var context = new SignalRContext();
            return context.Products.Where(x => x.ProductName == "Steak Burger").Select(y => y.Price).FirstOrDefault();
        }

        public decimal TotalPriceByDrinkCategory()
        {
            var context = new SignalRContext();
            int id = context.Categories.Where(x => x.CategoryName == "İçecek").Select(y => y.CategoryId).FirstOrDefault();
            return context.Products.Where(x => x.CategoryId == id).Sum(y => y.Price);
        }

        public decimal TotalPriceBySaladCategory()
        {
            var context = new SignalRContext();
            int id = context.Categories.Where(x => x.CategoryName == "Salata").Select(y => y.CategoryId).FirstOrDefault();
            return context.Products.Where(x => x.CategoryId == id).Sum(y => y.Price);
        }

        public List<Product> GetLast9Product()
        {
            var context = new SignalRContext();
            var values = context.Products.Take(9).ToList();
            return values;
        }
    }
}
