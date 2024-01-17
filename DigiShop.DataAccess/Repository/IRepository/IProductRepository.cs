using DigiShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiShop.DataAccess.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product obj);
    }


}
