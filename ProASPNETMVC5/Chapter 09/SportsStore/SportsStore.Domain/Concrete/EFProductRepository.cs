using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System.Collections.Generic;

namespace SportsStore.Domain.Concrete {

    public class EFProductRepository : IProductRepository {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Product> Products {
            get { return context.Products; }
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product productContext = context.Products.Find(product.ProductID);

                if (productContext != null)
                {
                    productContext.Name = product.Name;
                    productContext.Description = product.Description;
                    productContext.Price = product.Price;
                    productContext.Category = product.Category;
                }

                context.SaveChanges();
            }
        }
    }
}