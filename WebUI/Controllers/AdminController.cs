using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        ProductRepository repository;

        public AdminController(ProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Products);
        }
    }
}