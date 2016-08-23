using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System.Collections.Generic;

namespace SportsStore.Domain.Concrete {

    public class EFProductRepository : IProductRepository {
        private EFDbContext _context = new EFDbContext();

        public IEnumerable<Product> Products {
            get { return _context.Products; }
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                Product productContext = _context.Products.Find(product.ProductID);

                if (productContext != null)
                {
                    productContext.Name = product.Name;
                    productContext.Description = product.Description;
                    productContext.Price = product.Price;
                    productContext.Category = product.Category;
                }
            }

            _context.SaveChanges();
        }

        public Product DeleteProduct(int productId)
        {
            Product product = _context.Products.Find(productId);

            if(product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }

            return product;
        }
    }
}