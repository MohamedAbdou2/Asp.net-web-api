using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class ProductRepo : IProductRepo
    {
        private readonly ITIEntity _context;

        public ProductRepo(ITIEntity context)
        {
            _context = context;
        }
        public List<Product> GetAll()
        {
           return _context.Products.ToList();
        }

        public Product GetById(int id)
        {
           
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }
        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
           Product product = GetById(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

       

        public void Update(int id, Product product)
        {
            Product OldProduct = GetById(id);
            if (OldProduct != null) { 
            OldProduct.Name = product.Name;
            OldProduct.Description = product.Description;
            OldProduct.Price = product.Price;
            _context.SaveChanges();
            }
        }
    }
}
