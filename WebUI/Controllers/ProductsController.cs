using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;


namespace WebUI.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        private ProductRepository repository;
        public ProductsController(ProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult List()
        {
            return View(repository.Products);
        }
    }
}