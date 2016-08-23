using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository _productRepository;

        public AdminController(IProductRepository productReposiotry)
        {
            _productRepository = productReposiotry;
        }

        public ViewResult Index()
        {
            return View(_productRepository.Products);
        }

        public ViewResult Edit(int productId)
        {
            Product product = _productRepository.Products.FirstOrDefault(p => p.ProductID == productId);

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.SaveProduct(product);
                TempData["Message"] = string.Format("{0} has been saved.", product.Name);

                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        public ActionResult Delete(int productId)
        {
            Product product = _productRepository.DeleteProduct(productId);

            if (product != null)
            {
                TempData["Message"] = string.Format("{0} was deleted", product.Name);
            }

            return RedirectToAction("Index");
        }
    }
}