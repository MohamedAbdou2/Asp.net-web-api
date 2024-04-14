using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IProductRepo
    {
        List<Product> GetAll();
        Product GetById(int id);

        void Add(Product product);

        void Update(int id, Product product);

        void Delete(int id);
    }
}
