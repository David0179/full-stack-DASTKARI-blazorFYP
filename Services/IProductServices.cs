using Entities;
using System.Collections.Generic;

namespace Services
{
    public interface IProductServices
    {
        void SaveData(ProductsModel obj);
        List<ProductsModel> GetData();
        void DeleteData(int id);
        void UpdateProduct(ProductsModel obj);
        List<ProductsModel> GetProductsBySearch(string search);


    }
}
