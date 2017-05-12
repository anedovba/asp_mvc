using System;
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
        public int pageSize = 8;
        public ProductsController(ProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string category, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repository.Products
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(product => product.ProductId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
        repository.Products.Count() :
        repository.Products.Where(product => product.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
            
        }
    }
}