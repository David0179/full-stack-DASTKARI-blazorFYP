using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductServices : IProductServices
    {
        private readonly ProductDAL _productDAL;

        public ProductServices()
        {
            _productDAL = new ProductDAL();
        }

        public void SaveData(ProductsModel obj)
        {
            _productDAL.SaveData(obj);
        }

        public List<ProductsModel> GetData()
        {
            return _productDAL.GetData();
        }

        public void DeleteData(int id)
        {
            _productDAL.DeleteData(id);
        }

        public void UpdateProduct(ProductsModel obj)
        {
            _productDAL.UpdateProduct(obj);
        }

        public List<ProductsModel> GetProductsBySearch(string search)
        {
            return _productDAL.GetProductsBySearch(search);
        }
    }
}
