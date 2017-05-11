﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using WebUI.Models;


namespace WebUI.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        private ProductRepository repository;
        public int pageSize = 4;
        public ProductsController(ProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repository.Products
                    .OrderBy(game => game.ProductId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = repository.Products.Count()
                }
            };
            return View(model);
            
        }
    }
}