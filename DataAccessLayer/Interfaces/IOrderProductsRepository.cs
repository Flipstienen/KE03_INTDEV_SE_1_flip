using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IOrderProductsRepository
    {
        public IEnumerable<Product> GetAllMostSoldProducts();
    }
}
